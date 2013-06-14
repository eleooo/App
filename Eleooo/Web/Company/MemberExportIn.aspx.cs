using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using SubSonic.Sugar;
using Eleooo.DAL;
using System.Data;
using System.Transactions;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class MemberExportIn : ActionPage
    {
        #region const
        const string UPLOAD_CMD = "UploadFile";
        const string CHECK_CMD = "CheckMember";
        const string IMPORT_CMD = "ImportMember";
        const string CHECK_COL = "Check";
        const string ORIG_PHONENUMBER = "ORIG_PHONENUMBER";
        const string WANING_HTML = "<font color='greed'>{0}</font>";
        const string ERROR_HTML = "<font color='red'>{0}</font>";
        #endregion

        #region ColumnMap
        private readonly Dictionary<TableSchema.TableColumn, string> ColumnMap = new Dictionary<TableSchema.TableColumn, string>( );
        public MemberExportIn( )
        {
            ColumnMap.Add(SysMember.MemberFullnameColumn, "会员姓名");
            ColumnMap.Add(SysMember.MemberPhoneNumberColumn, "手机号码");
            ColumnMap.Add(SysMember.MemberPwdColumn, "密码");
            ColumnMap.Add(SysMember.MemberEmailColumn, "邮箱");
            ColumnMap.Add(SysMember.MemberSexColumn, "性别");
            ColumnMap.Add(SysMember.MemberBirthdayColumn, "生日");
            ColumnMap.Add(SysMember.MemberAddress1Column, "住址");
            ColumnMap.Add(SysCompanyMemberGrade.GradeNameColumn, "会员级别");
            ColumnMap.Add(SysMemberCash.CashRateColumn, "折扣比例");
            ColumnMap.Add(SysMemberCash.CashRateSaleColumn, "积分比例");
            ColumnMap.Add(SysMember.MemberBalanceCashColumn, "储值余额");
            ColumnMap.Add(SysMember.MemberBalanceColumn, "积分余额");
        }
        #endregion

        private List<SysCompanyMemberGrade> _grades;
        public List<SysCompanyMemberGrade> grades
        {
            get
            {
                if (_grades == null)
                {
                    _grades = DB.Select( ).From<SysCompanyMemberGrade>( )
                                .Where(SysCompanyMemberGrade.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                                .ExecuteTypedList<SysCompanyMemberGrade>( );
                }
                return _grades;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            if (!CheckOnce( ))
                return;
            lblErrorInfo.Text = string.Empty;
            lblErrorInfo2.Text = string.Empty;
            if (IsPostBack)
            {
                if (EVENTTARGET == btnExportIn.UniqueID)
                    Upload( );
                else if (EVENTTARGET == btnCheck.UniqueID)
                    Check( );
                else if (EVENTTARGET == btnPost.UniqueID)
                    Import( );
            }

            DataTable dt = CacheTable(null);
            if (dt != null && tbPost.Visible)
            {
                gridView.Visible = true;
                gridView.DataSource = dt;
                gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
                gridView.OnDataBindHeader += new Web.Controls.DataBindHeaderHandle(gridView_OnDataBindHeader);
                gridView.DataBind( );
            }
        }

        void gridView_OnDataBindHeader(string column, ref string caption, ref bool isRenderedCell)
        {
            if (Utilities.Compare(column, CHECK_COL) || Utilities.Compare(column, ORIG_PHONENUMBER))
            {
                isRenderedCell = true;
                caption = string.Empty;
            }
        }

        string gridView_OnDataBindColumn(string column, DataRow rowData, ref bool isRenderedCell)
        {
            if (Utilities.Compare(column, CHECK_COL) || Utilities.Compare(column,ORIG_PHONENUMBER))
            {
                isRenderedCell = true;
                return string.Empty;
            }
            if (Utilities.IsNull(rowData[column]))
                return string.Empty;
            else
                return Convert.ToString(rowData[column]);
        }

        #region upload
        void Upload( )
        {
            try
            {
                string message, absPath;
                Eleooo.Common.FileUpload.SaveUploadFile(txtCustomer.PostedFile, FileType.Doc, SaveType.Custome, out absPath, out message, true);
                if (!string.IsNullOrEmpty(message))
                    throw new Exception(message);
                DataTable dt = ExcelHelper.ExportExcelInDT(absPath, out message);
                if (!string.IsNullOrEmpty(message))
                    throw new Exception(message);
                if (dt == null)
                    throw new Exception("读入文件错误!");
                if (dt.Rows.Count > 0)
                    this.lblErrorInfo.Text = string.Format("成功读取到{0}个会员", dt.Rows.Count);
                else
                    throw new Exception(string.Format("你上传的文件不完整，{0}未提供数据", txtCustomer.PostedFile.FileName));
                dt.Columns.Add(CHECK_COL, typeof(string));
                dt.Columns.Add(ORIG_PHONENUMBER, typeof(string));
                CacheTable(dt);
                tbPost.Visible = true;
                btnPost.Enabled = false;
                btnCheck.Enabled = true;
                btnCheck.Visible = true;
                btnExportIn.Enabled = false;
            }
            catch (Exception ex)
            {
                Logging.Log("MemberExportIn->Upload", ex);
                lblErrorInfo.Text = ex.Message;
            }
        }
        #endregion

        #region Check
        void Check( )
        {
            DataTable dt = CacheTable(null);
            if (dt == null)
            {
                lblErrorInfo2.Text = "导入文件数据不存在!";
                return;
            }
            DataColumn cCol = dt.Columns[CHECK_COL];
            DataColumn origPhoneNum = dt.Columns[ORIG_PHONENUMBER];
            DataColumn phoneNum = dt.Columns[ColumnMap[SysMember.MemberPhoneNumberColumn]];
            DataColumn col;
            bool bValidate = false;
            List<string> phones = new List<string>( );
            List<DataRow> removedRow = new List<DataRow>( );
            foreach (DataRow row in dt.Rows)
            {
                row[cCol] = "1";
                row[origPhoneNum] = row[phoneNum];
                if (Utilities.IsNull(row[phoneNum]))
                {
                    removedRow.Add(row);
                    continue;
                }
                foreach (KeyValuePair<TableSchema.TableColumn, string> pair in ColumnMap)
                {
                    #region check column
                    if (!dt.Columns.Contains(pair.Value))
                        continue;
                    col = dt.Columns[pair.Value];
                    string val = Utilities.ToString(row[col]);
                    if (!pair.Key.IsNullable && Utilities.IsNull(val))
                    {
                        row[col] = Format(val, "此单元不能为空");
                        row[cCol] = "-1";
                        continue;
                    }
                    if (pair.Key.IsNumeric && !SubSonic.Sugar.Validation.IsNumeric(val))
                    {
                        row[col] = Format(val, "此单元必须是数字");
                        row[cCol] = "-1";
                        continue;
                    }
                    if (pair.Key.IsDateTime && !string.IsNullOrEmpty(val))
                    {
                        val = val.Replace(".", "-");
                        if (!SubSonic.Sugar.Dates.IsDate(val))
                        {
                            row[col] = Format(val, "此单元必须是日期格式");
                            row[cCol] = "-1";
                            continue;
                        }
                        row[col] = val;
                    }
                    if (pair.Key.IsString && val.Length > pair.Key.MaxLength)
                    {
                        row[col] = Format(val, "此单元长度不能大于" + pair.Key.MaxLength.ToString( ));
                        row[cCol] = "-1";
                        continue;
                    }
                    if (pair.Key.ColumnName == SysMember.MemberPwdColumn.ColumnName && val.Length < 6)
                    {
                        row[col] = Format(val, "会员密码长度要大于6数");
                        row[cCol] = "-1";
                        continue;
                    }
                    if (pair.Key.ColumnName == SysMember.MemberPhoneNumberColumn.ColumnName)
                    {
                        if (val.Length < 11)
                        {
                            row[col] = Format(val, "会员手机长度不正确!");
                            row[cCol] = "-1";
                            continue;
                        }
                        int nCheck = CompanyBLL.CheckIsOwnerUserOrExist(val, CurrentUser.CompanyId.Value);
                        if (nCheck == -2)
                        {
                            row[col] = Format(val, "此账号属于商家账号!");
                            row[cCol] = "-1";
                            continue;
                        }
                        if (nCheck == 0)
                        {
                            row[col] = Format(val, "此账号已经是本店会员!");
                            row[cCol] = "-1";
                            continue;
                        }
                        if (nCheck > 0)
                        {
                            row[col] = Format(val, "此账号已经注册!");
                            continue;
                        }
                        if (phones.Contains(val))
                        {
                            row[col] = Format(val, "此账号在列表里重复出现!");
                            row[cCol] = "-1";
                            continue;
                        }
                        phones.Add(val);
                    }
                    #endregion
                }
                if (!bValidate)
                    bValidate = row[cCol].ToString( ) == "1";
            }
            foreach (DataRow row in removedRow)
            {
                dt.Rows.Remove(row);
            }
            tbPost.Visible = true;
            if (bValidate)
            {
                btnPost.Visible = true;
                btnPost.Enabled = true;
                btnCheck.Enabled = false;
                btnExportIn.Enabled = false;
            }
            else
            {
                btnExportIn.Enabled = true;
                btnCheck.Enabled = false;
            }
            CacheTable(dt);
        }
        string Format(string val, string message)
        {
            return string.Concat(string.Format(WANING_HTML, val), string.Format(ERROR_HTML, message));
        }
        #endregion

        #region Import
        void Import( )
        {
            DataTable dt = CacheTable(null);
            if (dt == null)
            {
                lblErrorInfo2.Text = "导入数据不存在!";
                return;
            }
            DataColumn cCol = dt.Columns[CHECK_COL];
            string strCheck;
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    strCheck = Utilities.ToString(row[cCol]);
                    if (strCheck == string.Empty || strCheck == "-1")
                        continue;
                    SysMember user = SaveMemberData(row);
                    SaveMemberCash(row, user);
                    SaveMemberCompany(row, user);

                }
                AppContext.Context.Company.MarkClean( );
                AppContext.Context.Company.MarkOld( );
                AppContext.Context.Company.IsUseFinger = false;
                AppContext.Context.Company.Save( );
                OrderBLL.UpdateBalance( );
                ts.Complete( );
                lblErrorInfo2.Text = "导入成功!";
                btnPost.Enabled = false;
            }
            catch (Exception ex)
            {
                Logging.Log("MemberExportIn->Import", ex,true);
                lblErrorInfo2.Text = ex.Message + Environment.NewLine + ex.StackTrace;
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
                tbPost.Visible = true;
                btnPost.Enabled = false;
                btnCheck.Enabled = false;
            }
        }
        SysMember SaveMemberData(DataRow row)
        {
            string phoneNum = Utilities.ToString(row[ORIG_PHONENUMBER]);
            SysMember user = UserBLL.GetUserByPhoneNum(phoneNum);
            if (user == null)
            {
                user = new SysMember( );
                user.MarkNew( );
                foreach (TableSchema.TableColumn col in SysMember.Schema.Columns)
                {
                    if (col.IsReadOnly || col.AutoIncrement || col.IsReservedColumn)
                        continue;
                    if (col == SysMember.MemberCompanyIDColumn)
                        user.SetColumnValue(col.ColumnName, CurrentUser.CompanyId);
                    else if (col == SysMember.MemberPhoneNumberColumn)
                        user.SetColumnValue(col.ColumnName, phoneNum);
                    else if (col == SysMember.MemberDateColumn)
                        user.SetColumnValue(col.ColumnName, DateTime.Now);
                    else if (col == SysMember.MemberGradeColumn)
                        user.SetColumnValue(col.ColumnName, GetMemberGrade(Utilities.ToString(row[ColumnMap[SysCompanyMemberGrade.GradeNameColumn]])));
                    else if (col == SysMember.MemberRoleIdColumn)
                        user.SetColumnValue(col.ColumnName, UserBLL.GetDefaultUseRole((int)SubSystem.Member));
                    else if (col == SysMember.MemberSexColumn)
                        user.SetColumnValue(col.ColumnName, GetMemberSex(Utilities.ToString(row[ColumnMap[col]])));
                    else if (ColumnMap.ContainsKey(col))
                        user.SetColumnValue(col.ColumnName, row[ColumnMap[col]]);
                    else if (col == SysMember.AdminRoleIdColumn)
                        user.AdminRoleId = 0;
                    else if (col == SysMember.CompanyIdColumn)
                        user.CompanyId = 0;
                    else if (col == SysMember.CompanyRoleIdColumn)
                        user.CompanyRoleId = 0;
                    else if (col == SysMember.MemberStatusColumn)
                        user.MemberStatus = 1;
                    else if (col == SysMember.MemberCityColumn)
                        user.MemberCity = AppContext.Context.Company.CompanyCity;
                    else if (col == SysMember.MemberAreaColumn)
                        user.MemberArea = AppContext.Context.Company.CompanyArea;
                    else if (col == SysMember.MemberLocationColumn)
                        user.MemberLocation = AppContext.Context.Company.CompanyLocation;
                    else if (col == SysMember.AreaDepth1Column)
                        user.AreaDepth1 = AppContext.Context.Company.AreaDepth;
                    else if (col == SysMember.AreaDepth2Column)
                        user.AreaDepth2 = null;
                    else if (col == SysMember.AreaDepth3Column)
                        user.AreaDepth3 = null;
                    else if (col == SysMember.AreaModifyDateColumn)
                        user.AreaModifyDate = null;
                    else if (col == SysMember.MemberMsnPhoneColumn)
                        user.MemberMsnPhone = user.MemberPhoneNumber;
                    else
                        user.SetColumnValue(col.ColumnName, col.DefaultValue);
                }
                user.Save( );
            }
            else
            {
                user.MarkOld( );
            }
            return user;
        }
        bool SaveMemberCompany(DataRow row, SysMember user)
        {
            if (user.IsNew || CompanyBLL.CheckIsOwnerUser(user.Id, CurrentUser.CompanyId.Value))
                return false;
            new SysMemberCompany
            {
                MemberCompanyCompanyID = CurrentUser.CompanyId.Value,
                MemberCompanyMemberID = user.Id,
                MemberCompanyDate = DateTime.Now,
                MemberGrade = GetMemberGrade(Utilities.ToString(row[ColumnMap[SysCompanyMemberGrade.GradeNameColumn]]))
            }.Save( );
            return true;
        }
        void SaveMemberCash(DataRow row, SysMember user)
        {
            decimal dBalanceCash = row.Table.Columns.Contains(ColumnMap[SysMember.MemberBalanceCashColumn]) ? Utilities.ToDecimal(row[ColumnMap[SysMember.MemberBalanceCashColumn]]) : 0M,
                dBalancePoint = row.Table.Columns.Contains(ColumnMap[SysMember.MemberBalanceColumn]) ? Utilities.ToInt(row[ColumnMap[SysMember.MemberBalanceColumn]]) : 0M,
                     dMemberRate = (row.Table.Columns.Contains(ColumnMap[SysMemberCash.CashRateColumn]) ? Utilities.ToDecimal(row[ColumnMap[SysMemberCash.CashRateColumn]]) : 0M) / 100M;
            decimal dCashRateSale = 0M;
            if (row.Table.Columns.Contains(ColumnMap[SysMemberCash.CashRateSaleColumn]))
                dCashRateSale = Utilities.ToDecimal(row[ColumnMap[SysMemberCash.CashRateSaleColumn]]) / 100M;
            if (dBalanceCash > 0)
            {
                SysMemberCash cash = new SysMemberCash
                {
                    CashCompanyID = CurrentUser.CompanyId,
                    CashMemberID = user.Id,
                    CashDate = DateTime.Now,
                    CashMemo = GetMemberGrade(Utilities.ToString(row[ColumnMap[SysCompanyMemberGrade.GradeNameColumn]])).ToString( ),
                    CashOrderID = -1,
                    CashSum = dBalanceCash,
                    CashRate = dMemberRate,
                    CashPoint = 0,
                    CashRateSale = dCashRateSale
                };
                cash.Save( );
                new PaymentCash
                {
                    PaymentCashCode = string.Empty,
                    PaymentCashCompanyID = CurrentUser.CompanyId,
                    PaymentCashMemberID = user.Id,
                    PaymentCashDate = DateTime.Now,
                    PaymentMemo = string.Format("在【{0}】已经成功导入{1:0.00}元储值", AppContext.Context.Company.CompanyName, dBalanceCash.ToString("###0.00")),
                    PaymentOrderID = cash.CashID,
                    PaymentStatus = 1,
                    PaymentCashSum = dBalanceCash,
                    PaymentType = (int)PaymentCashType.Import
                }.Save( );
            }
            if (dBalancePoint > 0)
            {
                new Payment
                {
                    PaymentCode = string.Empty,
                    PaymentCompanyID = CurrentUser.CompanyId,
                    PaymentMemberID = user.Id,
                    PaymentDate = DateTime.Now,
                    PaymentMemo = string.Format("在【{0}】已经成功导入{1:0.00}个积分", AppContext.Context.Company.CompanyName, dBalancePoint.ToString("###0.00")),
                    PaymentOrderID = 0,
                    PaymentStatus = 1,
                    PaymentSum = dBalancePoint,
                    PaymentType = (int)PaymentType.Import,
                    PaymentEmail = string.Empty
                }.Save( );
            }
        }
        #endregion

        #region Check Once
        bool CheckOnce( )
        {
            if (AppContext.Context.CompanyType == CompanyType.MealCompany)
            {
                lblErrorInfo.Text = "阁下的商家类型无权使用此功能";
                return false;
            }
            if(AppContext.Context.Company.IsUseFinger.HasValue &&
                !AppContext.Context.Company.IsUseFinger.Value)
            {
                this.lblErrorInfo.Text = "导入会员只能操作一次！你已经完成导入，无法再次操作！";
                this.lblErrorInfo.Visible = true;
                this.btnExportIn.Visible = false;
                return false;
            }
            return true;
        }
        #endregion

        #region CacheTable
        DataTable CacheTable(DataTable dt)
        {
            if (dt == null)
            {
                return Utilities.GetCache<DataTable>(CurrentUser.CompanyId.ToString( ));
            }
            else
            {
                Utilities.AddCache(CurrentUser.CompanyId.ToString( ), dt, 1);
                return dt;
            }
        }
        #endregion

        int GetMemberGrade(string gradeName)
        {
            SysCompanyMemberGrade grade = grades.Find((SysCompanyMemberGrade match) =>
            {
                return Utilities.Compare(match.GradeName, gradeName);
            });
            if (grade == null)
            {
                grade = new SysCompanyMemberGrade( );
                grade.GradeName = gradeName;
                grade.GradeOrder = grades.Count + 1;
                grade.CompanyID = CurrentUser.CompanyId;
                grade.Save( );
                grades.Add(grade);
            }
            return grade.Id;
        }
        bool GetMemberSex(string sexName)
        {
            return Utilities.Compare("男", sexName);
        }
    }
}