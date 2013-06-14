using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Security.Permissions;
using System.Data;
using System.Text;

namespace Eleooo.Web.Controls
{
    [ToolboxItem(false), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class ListViewExTemplate : Control, INamingContainer
    {
        private LiteralControl control = null;
        private ITemplate _template;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Html
        {
            get
            {
                if (control == null)
                    return string.Empty;
                else
                    return control.Text;
            }
            set
            {
                this.Controls.Clear( );
                this.Controls.Add(new LiteralControl(value));
            }
        }
        public override string ToString( )
        {
            return Html;
        }
        public ITemplate Template
        {
            get
            {
                return _template;
            }
            set
            {
                _template = value;
                if (_template != null)
                {
                    _template.InstantiateIn(this);
                    if (this.Controls.Count > 0)
                        control = this.Controls[0] as LiteralControl;
                }
            }
        }
    }
    public delegate void BuildListViewHeaderHandler(string column, ref string caption);
    public delegate void BuildListViewRowHandler(int index,DataRow rowData,bool isAlterRow,string rowTemplate,StringBuilder buffer);
    public delegate void BuildListViewItemHander(int index, DataRow rowData, string column, string itemTemplate, StringBuilder buffer);
    public delegate void BuildListViewFooterHander(string footerTemplate,StringBuilder buffer);
}