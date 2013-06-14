using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Web.Controls;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysAdsClickSettings : ActionPage
    {
        EditableGrid<SysAdsClickSetting> clickGrid;
        StringBuilder sb;
        protected void Page_Load(object sender, EventArgs e)
        {
            var clickQuery = DB.Select( ).From<SysAdsClickSetting>( );
            clickGrid = new EditableGrid<SysAdsClickSetting>(SysAdsClickSetting.table.Name, clickQuery);
            clickGrid.AddShowColumn(SysAdsClickSetting.OrderSumLimitColumn)
                     .AddShowColumn(SysAdsClickSetting.ClickCountLimitColumn);
            clickGrid.MaxAllowRow = 5;
            clickGrid.OnBeforSave += new EditableGrid<SysAdsClickSetting>.OnEditableGridSaveHandler(clickGrid_OnBeforSave);
            sb = new StringBuilder( );
            gridContainer.SetRenderMethodDelegate((w, c) =>
                {
                    w.Write(sb);
                });
            txtMessage.InnerHtml = string.Empty;
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            clickGrid.RenderGrid(sb);
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            try
            {
                if (clickGrid.Save( ))
                    txtMessage.InnerHtml = "保存成功";
                else
                    txtMessage.InnerHtml = "保存失败";
            }
            catch (Exception ex)
            {
                txtMessage.InnerHtml = ex.Message;
                Logging.Log("SysAdsClickSettings->On_ActionEdit", ex);
            }
            On_ActionQuery(sender, e);
        }
        void clickGrid_OnBeforSave(SysAdsClickSetting item, out string message)
        {
            message = string.Empty;
            if (!item.ClickCountLimit.HasValue || item.ClickCountLimit < 0)
                message = "每天点击限制次数必须大于零";
            if (!item.OrderSumLimit.HasValue)
                message = "请输入消费层次";
        }
    }
}