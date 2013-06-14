using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcMemberInfo : System.Web.UI.UserControl
    {
        const string LI_TPL = "<li style=\"overflow:hidden\">{0}：<span >{1}</span></li>";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.EnableViewState = false;
        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (AppContext.Context.CurrentSubSys == SubSystem.Member)
            {
                RenderMember(writer);
            }
            else
            {
                RenderAdmin(writer);
            }
            base.Render(writer);
        }
        private void RenderMember(HtmlTextWriter writer)
        {
            DAL.SysMember user = DAL.SysMember.FetchByID(AppContext.Context.User.Id);
            writer.Write("<div class=\"mynavinfo\"><h3>");
            writer.Write(ResBLL.GetRes("MemberInfoBox_title", "我的帐户", "会员信息框标题"));
            writer.Write("</h3><div class=\"myuserinfomain\"><ul>");
            writer.Write("<li class=\"myuser\">{0}{1} </li>", ResBLL.GetRes("MemberInfoBox_phone", "我的帐号：", "会员信息框账号信息"), user.MemberPhoneNumber);
            writer.Write("<li class=\"myprice\">{0}{1}</li>", ResBLL.GetRes("MemberInfoBox_cash", "我的储值：", "会员信息框储值信息"), user.MemberBalanceCash);
            writer.Write("<li class=\"mypoint\">{0}{1}</li>", ResBLL.GetRes("MemberInfoBox_point", "我的积分：", "会员信息框积分信息"), user.MemberBalance);
            writer.Write("</ul></div><div class=\"buttom\"></div></div>");
        }
        private void RenderAdmin(HtmlTextWriter writer)
        {
            System.Data.DataTable dt;
            if (AppContext.Context.Company != null)
                dt = UserBLL.GetCompanyInfoDataTable(DAL.SysCompany.FetchByID(AppContext.Context.Company.Id));
            else
                dt = UserBLL.GetUserInfoDataTable(AppContext.Context.User);
            writer.Write("<div class=\"lb-title\"><span>");
            writer.Write(ResBLL.GetRes("MemberInfoBox_title", "我的帐户", "会员信息框标题"));
            writer.Write("</span></div><div class=\"lb01-main\"><ul>");
            foreach (System.Data.DataRow row in dt.Rows)
            {
                writer.Write(LI_TPL, row[0], row[1]);
            }
            writer.Write("</ul></div>");

        }
        private System.Data.DataTable GetMemberInfo(DAL.SysMember user)
        {
            System.Data.DataTable dtUserInfo = SubSonic.Utilities.EntityFormat.GetUserInfoTable( );
            //dtUserInfo.Rows.Add(ResBLL.GetRes("MemberInfoBox_phone", "我的帐号：", "会员信息框账号信息"), user.MemberPhoneNumber);
            dtUserInfo.Rows.Add(ResBLL.GetRes("MemberInfoBox_name", "我的姓名：", "会员信息框姓名信息"), user.MemberFullname);

            dtUserInfo.Rows.Add(ResBLL.GetRes("MemberInfoBox_cash", "我的储值：", "会员信息框储值信息"), user.MemberBalanceCash);
            dtUserInfo.Rows.Add(ResBLL.GetRes("MemberInfoBox_point", "我的积分：", "会员信息框积分信息"), user.MemberBalance);
            return dtUserInfo;
        }
    }
}