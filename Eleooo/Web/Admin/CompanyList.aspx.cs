using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Transactions;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class CompanyList : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string COMPANY_EDITOR_LINK = "/Admin/CompanyEdit.aspx?ID={0}";
        const string ACTION_COLUMN = "CompanyList_Action";
        private string _textDel;
        private string _textDlg;
        private string _textStatusActive;
        private string _textStatusInactive;
        protected string TextDel
        {
            get
            {
                if (string.IsNullOrEmpty(_textDel))
                    _textDel = ResBLL.GetRes("CompanyList_Action_Del", "删除", "商家列表删除操作名称");
                return _textDel;
            }
        }
        protected string TextDlg
        {
            get
            {
                if (string.IsNullOrEmpty(_textDlg))
                    _textDlg = ResBLL.GetRes("CompanyList_Action_Edit", "编辑", "商家列表编辑操作名称");
                return _textDlg;
            }
        }
        protected string TextStatusActive
        {
            get
            {
                if (string.IsNullOrEmpty(_textStatusActive))
                    _textStatusActive = ResBLL.GetRes("CompanyList_Action_StatusActive", "正常", "商家列表正常状态操作名称");
                return _textStatusActive;
            }
        }
        protected string TextStatusInactive
        {
            get
            {
                if (string.IsNullOrEmpty(_textStatusInactive))
                    _textStatusInactive = ResBLL.GetRes("CompanyList_Action_StatusInactive", "审核", "商家列表待审核状态操作名称");
                return _textStatusInactive;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd" );
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
            lblMessage.InnerHtml = string.Empty;
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            gridView.DataSource = DB.Select( ).From<SysCompany>( )
                                    .Where(SysCompany.CompanyDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                    .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                    .CloseEx( )
                                    .OrderDesc(SysCompany.Columns.CompanyDate);
            gridView.AddShowColumn(SysCompany.CompanyCodeColumn)
                    .AddShowColumn(SysCompany.CompanyNameColumn)
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(SysCompany.CompanySaleSumColumn)
                    .AddShowColumn(SysCompany.CompanyBalanceCashColumn)
                    .AddShowColumn(SysCompany.CompanyBalanceColumn)
                    .AddShowColumn(SysCompany.CompanyDateColumn)
                    .AddCustomColumn(ACTION_COLUMN, ResBLL.GetRes(ACTION_COLUMN, "操作", "商家列表操作列名称"));
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(EVENTARGUMENT);
            //SysCompany company = SysCompany.FetchByID(id);
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            try
            {
                int id = Utilities.ToInt(EVENTARGUMENT);
                SysCompany company = SysCompany.FetchByID(id);
                if (company != null)
                {
                    int status = Utilities.ToInt(company.CompanyStatus);
                    company.CompanyStatus = status == 0 || status == 2 ? 1 : 2;
                    SysMember user = UserBLL.CompanyToMember(company);
                    if (!user.IsNew)
                        user.MemberStatus = company.CompanyStatus;
                    using (TransactionScope ts = new TransactionScope( ))
                    {
                        using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                        {
                            user.Save( );
                            company.Save( );
                            ts.Complete( );
                        }
                    }
                    lblMessage.InnerHtml = "保存成功!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerHtml = string.Concat("保存失败:", ex.Message);
                Logging.Log("CompanyList->On_ActionEdit", ex, true);
            }

            On_ActionQuery(sender, e);
        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "CompanyDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "CompanyName":
                    result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, rowData[SysCompany.Columns.Id]), rowData[column]);
                    break;
                case ACTION_COLUMN:
                    result = string.Concat("[", GetStatusHtml(rowData), "][",
                        //GetMemberDelHtml(rowData),"|", 暂去掉删除功能
                                           GetEditDlgHtml(rowData), "]");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        string GetStatusHtml(System.Data.DataRow rowData)
        {
            int status = Utilities.ToInt(rowData[SysCompany.Columns.CompanyStatus]);
            if (status == 1)
                return string.Format(ACTION_EDIT_TEMPLATE, rowData[SysMember.Columns.Id],TextStatusActive );
            else
                return string.Format(ACTION_EDIT_TEMPLATE, rowData[SysMember.Columns.Id], TextStatusInactive);
        }
        string GetDelHtml(System.Data.DataRow rowData)
        {
            return string.Format(ACTION_DEL_TEMPLATE, rowData[SysCompany.Columns.Id], TextDel);
        }
        string GetEditDlgHtml(System.Data.DataRow rowData)
        {
            return string.Format(ACTION_DLG_TEMPLATE, rowData[SysCompany.Columns.Id], TextDlg);
        }
    }
}