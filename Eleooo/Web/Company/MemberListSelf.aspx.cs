using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class MemberListSelf : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string MEMBER_EDITOR_LINK = "/Company/MemberEdit.aspx?ID={0}";
        const string ACTION_COLUMN = "MemberList_Action";
        const string StatusInactive = "<font color='red'>停用</font>";
        const string StatusActive = "正常";
        const string MEMBER_DESCRIBE = "共计消费了{0}元，还有{1}元储值，{2}个积分";

        decimal dMemberSum = 0, dMemberCashSum = 0, dMemberPointSum = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            string vColumns = Utilities.GetTableColumns(VSysMember.Schema);
            gridView.DataSource = DB.Select(vColumns)
                                    .From<VSysMember>( )
                                    .Where(VSysMember.MemberDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .AndEx(VSysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                                    .Or(VSysMember.MemberFullnameColumn).Like(filterMemberTel)
                                    .CloseEx( )
                                    .And(VSysMember.MemberCompanyIDColumn).IsEqualTo(this.CurrentUser.CompanyId)
                                    .And(VSysMember.CompanyIdColumn).IsNotEqualTo(this.CurrentUser.CompanyId)
                                    .OrderDesc(VSysMember.Columns.MemberDate);
            gridView.AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(SysMember.MemberSumColumn)
                    .AddShowColumn(SysMember.MemberBalanceCashColumn)
                    .AddShowColumn(SysMember.MemberBalanceColumn)
                    .AddShowColumn(SysMember.MemberDateColumn)
                    .AddShowColumn(SysMember.MemberStatusColumn)
                    .AddShowColumn(SysCompanyMemberGrade.GradeNameColumn)
                    .AddCustomColumn(ACTION_COLUMN, ResBLL.GetRes(ACTION_COLUMN, "操作", "会员列表操作列名称"));
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
            txtDesc.InnerHtml = string.Format(MEMBER_DESCRIBE, dMemberSum, dMemberCashSum, dMemberPointSum);
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "MemberBalanceCash":
                    result = UserBLL.GetUserBalanceCash(Utilities.ToInt(rowData[SysMember.Columns.Id]), CurrentUser.CompanyId.Value).ToString( );
                    break;
                case "MemberDate":
                    result = Utilities.ToDate(rowData[column]);
                    CalcMemberBalance(rowData);
                    break;
                case "MemberFullname":
                    result = string.Format(LINK_TEMPLATE, string.Format(MEMBER_EDITOR_LINK, rowData[SysMember.Columns.Id]), rowData[column]);
                    break;
                case "MemberStatus":
                    result = GetStatusHtml(rowData);
                    break;
                case ACTION_COLUMN:
                    result = string.Concat("[", GetEditDlgHtml(rowData, "编辑", 0), "]");
                    break;
                case "GradeName":
                    string grade = GradeBLL.GetUserCurrentGrade(Utilities.ToInt(rowData[SysMember.Columns.MemberCompanyID]), Utilities.ToInt(rowData[SysMember.Columns.Id]));
                    if (string.IsNullOrEmpty(grade) || grade == "0")
                        result = "普通会员";
                    else
                        result = grade;
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        string GetStatusHtml(System.Data.DataRow rowData)
        {
            if (Convert.ToInt32(rowData[SysMember.Columns.MemberStatus]) == 1)
                return StatusActive;
            else
                return StatusInactive;
        }
        string GetEditDlgHtml(System.Data.DataRow rowData, string TextDlg, int index)
        {
            return string.Format(ACTION_DLG_INDEX_TEMPLATE, rowData[SysMember.Columns.Id], TextDlg, index);
        }
        void CalcMemberBalance(System.Data.DataRow rowData)
        {
            decimal dSum = 0;
            decimal dCash = 0;
            decimal dPoint = 0;
            if (!Utilities.IsNull(rowData["MemberSum"]))
                dSum = Convert.ToDecimal(rowData["MemberSum"]);
            if (!Utilities.IsNull(rowData["MemberBalanceCash"]))
                dCash = Convert.ToDecimal(rowData["MemberBalanceCash"]);
            if (!Utilities.IsNull(rowData["MemberBalance"]))
                dPoint = Convert.ToDecimal(rowData["MemberBalance"]);
            dMemberSum += dSum;
            dMemberCashSum += dCash;
            dMemberPointSum += dPoint;
        }
    }
}