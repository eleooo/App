using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysRewardEdit : ActionPage
    {
        int RewardID
        {
            get
            {
                return Utilities.ToInt(Request.Params["id"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = DB.Select( ).From<SysReward>( )
                          .Where(SysReward.RewardIDColumn).IsEqualTo(RewardID);
            formView.DataSource = query;
            formView.AddShowColumn(SysReward.RewardDateColumn, DateTime.Today.ToString("yyyy-MM-dd"))
                    .AddShowColumn(SysReward.RewardEndDateColumn, DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd"))
                    .AddShowColumn(SysReward.RewardRateColumn)
                    .AddShowColumn(SysReward.RewardFlagColumn, "1")
                    .AddShowColumn(SysReward.RewardMemoColumn);
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnValidate += new Web.Controls.OnFormViewValidate(formView_OnValidate);
            lblMessage.InnerHtml = string.Empty;
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (formView.Save<SysReward>(0) == 0)
            {
                lblMessage.InnerHtml = "保存成功";
                formView.ClearValue( );
            }
            else
                lblMessage.InnerHtml = "保存失败";
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (formView.Save<SysReward>(RewardID) == 0)
                lblMessage.InnerHtml = "保存成功";
            else
                lblMessage.InnerHtml = "保存失败";
            On_ActionQuery(sender, e);
        }
        void formView_OnValidate(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            if (columnName == SysReward.Columns.RewardDate)
            {
                DateTime dtBegin, dtEnd;
                if (!DateTime.TryParse(viewRow.Value, out dtBegin) || dtBegin < DateTime.Today)
                {
                    viewRow.ValidateMessage = "请输入有效的开始日期";
                    return;
                }
                Controls.UcFormView.FormViewRow rowEndDate = formView.GetViewRow(SysReward.RewardEndDateColumn);
                if (!DateTime.TryParse(rowEndDate.Value, out dtEnd) || dtEnd < dtBegin)
                    rowEndDate.ValidateMessage = "请输入有效的结束日期";
            }
            else if (columnName == SysReward.Columns.RewardRate)
            {
                decimal d = Utilities.ToDecimal(viewRow.Value);
                if (d <= 0)
                    viewRow.ValidateMessage = "奖励比例不能小于零";
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }
        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            if (columnName == SysReward.Columns.RewardMemo)
                viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetTextAreaHtml(viewRow.ParamName, viewRow.Value);
            else if (columnName == SysReward.Columns.RewardFlag)
                viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetRadioHtml(RewardBLL.RewardFlag, viewRow.ParamName, viewRow.Value).ToString( );
        }
    }
}