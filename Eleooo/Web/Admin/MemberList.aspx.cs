using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class MemberList : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string MEMBER_EDITOR_LINK = "/Admin/MemberEdit.aspx?ID={0}";
        const string COMPANY_EDITOR_LINK = "/Admin/CompanyEdit.aspx?ID={0}";
        const string ACTION_COLUMN = "MemberList_Action";

        private string _textDel;
        private string _textDlg;
        private string _textStatusActive;
        private string _textStatusInactive;
        protected string TextDel
        {
            get
            {
                if (string.IsNullOrEmpty(_textDel))
                    _textDel = ResBLL.GetRes("MemberList_Action_Del", "删除", "会员列表删除操作名称");
                return _textDel;
            }
        }
        protected string TextDlg
        {
            get
            {
                if (string.IsNullOrEmpty(_textDlg))
                    _textDlg = ResBLL.GetRes("MemberList_Action_Edit", "编辑", "会员列表编辑操作名称");
                return _textDlg;
            }
        }
        protected string TextStatusActive
        {
            get
            {
                if (string.IsNullOrEmpty(_textStatusActive))
                    _textStatusActive = ResBLL.GetRes("MemberList_Action_StatusActive", "正常", "会员列表正常状态操作名称");
                return _textStatusActive;
            }
        }
        protected string TextStatusInactive
        {
            get
            {
                if (string.IsNullOrEmpty(_textStatusInactive))
                    _textStatusInactive = ResBLL.GetRes("MemberList_Action_StatusInactive", "待审核", "会员列表待审核状态操作名称");
                return _textStatusInactive;
            }
        }
        protected string TextSetAdmin
        {
            get
            {
                return "设为管理员";
            }
        }
        protected string TextUnSetAdmin
        {
            get
            {
                return "取消管理员";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
            lblMessage.InnerHtml = string.Empty;
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            gridView.DataSource = DB.Select(Utilities.GetTableColumns(SysMember.Schema), SysCompany.Columns.CompanyName)
                                    .From<SysMember>( )
                                    .InnerJoin(SysCompany.IdColumn, SysMember.MemberCompanyIDColumn)
                                    .Where(SysMember.MemberDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .And(SysMember.CompanyIdColumn).IsEqualTo(0)
                                    .AndEx(SysMember.MemberPhoneNumberColumn.ColumnName).Like(filterMemberTel)
                                    .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                                    .CloseEx( )
                                    .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                    .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                    .CloseEx( )
                                    .OrderDesc(SysMember.Columns.MemberDate);
            gridView.AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(SysMember.MemberSumColumn)
                    .AddShowColumn(SysMember.MemberBalanceCashColumn)
                    .AddShowColumn(SysMember.MemberBalanceColumn)
                    .AddShowColumn(SysMember.MemberDateColumn)
                    .AddShowColumn(SysCompany.CompanyNameColumn)
                    .AddCustomColumn(ACTION_COLUMN, ResBLL.GetRes(ACTION_COLUMN, "操作", "会员列表操作列名称"));
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(EVENTARGUMENT);
            //SysMember user = SysMember.FetchByID(id);
            //UserHelper.DeleteUser(user);
            //On_ActionQuery(sender, e);
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            int id;
            int.TryParse(EVENTARGUMENT, out id);
            SysMember user = SysMember.FetchByID(Math.Abs(id));
            if (user != null)
            {
                if (id > 0)
                {
                    int status = Utilities.ToInt(user.MemberStatus);
                    user.MemberStatus = status == 0 || status == 2 ? 1 : 2;
                }
                else
                    user.AdminRoleId = user.AdminRoleId > 0 ? 0 : UserBLL.GetDefaultUseRole((int)SubSystem.Admin);
                user.Save( );
                if (id > 0)
                    lblMessage.InnerHtml = "审核成功!";
                else
                    lblMessage.InnerHtml = "管理员权限设置成功!";
            }
            On_ActionQuery(sender, e);
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "MemberDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "MemberFullname":
                    result = string.Format(LINK_TEMPLATE, string.Format(MEMBER_EDITOR_LINK, rowData[SysMember.Columns.Id]), rowData[column]);
                    break;
                case "CompanyName":
                    result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, rowData[SysMember.Columns.MemberCompanyID]), rowData[column]);
                    break;
                case ACTION_COLUMN:
                    result = string.Concat("[", GetStatusHtml(rowData), "][",
                                           GetEditDlgHtml(rowData), "][",
                                           GetAdminHtml(rowData), "]");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        string GetStatusHtml(System.Data.DataRow rowData)
        {

            int status = Utilities.ToInt(rowData[SysMember.Columns.MemberStatus]);
            if (status == 1)
                return string.Format(ACTION_EDIT_TEMPLATE, rowData[SysMember.Columns.Id], TextStatusActive);
            else
                return string.Format(ACTION_EDIT_TEMPLATE, rowData[SysMember.Columns.Id], TextStatusInactive);
        }
        string GetAdminHtml(System.Data.DataRow rowData)
        {
            int role = Utilities.ToInt(rowData[SysMember.Columns.AdminRoleId]);
            if (role > 0)
                return string.Format(ACTION_EDIT_TEMPLATE, string.Format("-{0}", rowData[SysMember.Columns.Id]), TextUnSetAdmin);
            else
                return string.Format(ACTION_EDIT_TEMPLATE, string.Format("-{0}", rowData[SysMember.Columns.Id]), TextSetAdmin);
        }
        string GetDelHtml(System.Data.DataRow rowData)
        {
            return string.Format(ACTION_DEL_TEMPLATE, rowData[SysMember.Columns.Id], TextDel);
        }
        string GetEditDlgHtml(System.Data.DataRow rowData)
        {
            return string.Format(ACTION_DLG_TEMPLATE, rowData[SysMember.Columns.Id], TextDlg);
        }
    }
}