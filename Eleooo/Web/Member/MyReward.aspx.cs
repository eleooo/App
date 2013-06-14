using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class MyReward : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd" );
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            var query = DB.Select(Utilities.GetTableColumns(SysMemberReward.Schema),
                                  SysCompany.Columns.CompanyName,
                                  SysMember.Columns.MemberPhoneNumber)
                          .From<SysMemberReward>( )
                          .InnerJoin(SysMember.IdColumn, SysMemberReward.OrderMemberIDColumn)
                          .InnerJoin(SysCompany.IdColumn, SysMemberReward.OrderCompanyIDColumn)
                          .Where(SysMemberReward.RewardDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(SysMemberReward.RewardMemberIDColumn).IsEqualTo(CurrentUser.Id)
                          .OrderDesc(SysMemberReward.Columns.RewardID);
            view.QuerySource = query;
            view.DataBind( );
            //gridView.DataSource = query;
            //gridView.AddShowColumn(SysMemberReward.RewardDateColumn)
            //        .AddCustomColumn(SysMember.MemberPhoneNumberColumn.ColumnName, "好友账号")
            //        .AddCustomColumn(SysCompany.Columns.CompanyName, "消费地点")
            //        .AddShowColumn(SysMemberReward.OrderSumOkColumn)
            //        .AddShowColumn(SysMemberReward.RewardPointColumn);
            //gridView.OnDataBindRow += new Web.Controls.DataBindRowHandler(gridView_OnDataBindRow);
            //gridView.DataBind( );
        }

    }
}