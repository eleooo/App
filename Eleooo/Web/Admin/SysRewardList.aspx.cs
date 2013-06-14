using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysRewardList : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
            txtDesc.InnerHtml = string.Empty;
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            var query = DB.Select( ).From<SysReward>( )
                          .Where(SysReward.RewardDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                          .And(SysReward.RewardEndDateColumn).IsLessThanOrEqualTo(dtEnd)
                          .OrderDesc(SysReward.Columns.RewardID);
            gridView.DataSource = query;
            gridView.AddShowColumn(SysReward.RewardDateColumn)
                    .AddShowColumn(SysReward.RewardEndDateColumn)
                    .AddShowColumn(SysReward.RewardRateColumn)
                    .AddShowColumn(SysReward.RewardFlagColumn)
                    .AddShowColumn(SysReward.RewardMemoColumn)
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "RewardMemo":
                    result = string.Format("<div style=\"text-align:left\">{0}</div>", Formatter.SubStr(rowData[column], 25));
                    break;
                case "RewardFlag":
                    string flag = Utilities.ToString(rowData[column]);
                    if (RewardBLL.RewardFlag.ContainsKey(flag))
                        result = RewardBLL.RewardFlag[flag];
                    else
                        result = flag;
                    break;
                case "RewardDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "RewardEndDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "RewardRate":
                    result = Utilities.ToDecimal(rowData[column]).ToString("####.###") + "%";
                    break;
                case "Action":
                    object id = rowData[SysReward.RewardIDColumn.ColumnName];
                    result = string.Concat(string.Format(ACTION_DEL_TEMPLATE, id, "[删除]"), "&nbsp;", string.Format(ACTION_DLG_TEMPLATE, id, "[编辑]"));
                    break;
                default:
                    result = Utilities.ToString(rowData[column]);
                    break;
            }
            return result;
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            int id = Utilities.ToInt(EVENTARGUMENT);
            if (id > 0)
            {
                SysReward.Delete(id);
                txtDesc.InnerHtml = "删除成功";
            }
            else
                txtDesc.InnerHtml = "参数错误";
            On_ActionQuery(sender, e);
        }
    }
}