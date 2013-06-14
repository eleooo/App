using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysAreaMansionEdit : ActionPage
    {
        int MansionId
        {
            get
            {
                return Utilities.ToInt(Request.Params["id"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = DB.Select( ).From<SysAreaMansion>( )
                          .Where(SysAreaMansion.IdColumn).IsEqualTo(MansionId);
            formView.DataSource = query;
            formView.AddShowColumn(SysAreaMansion.AreaDepthColumn)
                    .AddShowColumn(SysAreaMansion.NameColumn);
                    //.AddShowColumn(SysAreaMansion.AddressColumn);
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnValidate += new Web.Controls.OnFormViewValidate(formView_OnValidate);
            formView.OnBeforeSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnBeforeSaved);
            lblMessage.InnerHtml = string.Empty;
        }

        void formView_OnBeforeSaved(object item)
        {
            SysAreaMansion mansion = (SysAreaMansion)item;
            var area = AreaBLL.GetAreaByDepth(mansion.AreaDepth);
            if (MansionId == 0)
                mansion.Address = null;
            if (area != null)
                mansion.AreaID = area.Id;
            else
                throw new Exception("请选择大厦所在区域");
            if(MansionBLL.CheckAreaMansionExist(area.Id,mansion.Name,MansionId))
                throw new Exception("已存在同名的大厦.");
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }

        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            try
            {
                if (formView.Save<SysAreaMansion>(MansionId) == 0)
                    lblMessage.InnerHtml = "保存成功";
                else
                    lblMessage.InnerHtml = "保存失败";
            }
            catch (Exception ex)
            {
                lblMessage.InnerHtml = ex.Message;
                Logging.Log("SysAreaMansionEdit->On_ActionEdit", ex);
            }
            On_ActionQuery(sender, e);
        }

        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            try
            {
                if (formView.Save<SysAreaMansion>(0) == 0)
                    lblMessage.InnerHtml = "保存成功";
                else
                    lblMessage.InnerHtml = "保存失败";
            }
            catch (Exception ex)
            {
                lblMessage.InnerHtml = ex.Message;
                Logging.Log("SysAreaMansionEdit->On_ActionAdd", ex);
            }
            On_ActionQuery(sender, e);
        }

        void formView_OnValidate(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            if (Utilities.Compare(columnName, "AreaDepth"))
            {
                string depth = string.Empty;
                depth = AreaSelector.SelectedArea1;
                if (string.IsNullOrEmpty(depth))
                {
                    viewRow.ValidateMessage = "请选择大厦所在区域!";
                    return;
                }
                viewRow.ValidateMessage = string.Empty;
                viewRow.ParamValue = depth;
            }
            else if (Utilities.Compare(columnName, "Name"))
            {
                if (string.IsNullOrEmpty(viewRow.ParamValue))
                    viewRow.ValidateMessage = "请输入大厦名称.";
                else
                    viewRow.ValidateMessage = string.Empty;
                return;
            }
        }

        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "AreaDepth":
                    using (AreaSelector.Selector1)
                    {
                        AreaSelector.Selector1.DefaultValue = viewRow.Value;
                        AreaSelector.Selector1.RenderTo = viewRow.ParamName;
                        viewRow.RenderHtml = AreaSelector.Selector1.RenderResult( ).ToString( );
                        AreaSelector.Selector1.IsShowLabel = false;
                    }
                    break;
            }
        }


    }
}