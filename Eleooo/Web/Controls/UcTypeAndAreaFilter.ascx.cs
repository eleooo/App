using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public delegate string OnGetTypeNameHandler(string typeName);
    public delegate string OnGetAreaNameHandler(string areaName, string depth);
    public partial class UcTypeAndAreaFilter : UserControlBase
    {
        public UcTypeAndAreaFilter( )
        {
            ResetPageIndexControl = "txtPageIndex";
            IsShowAreaFilter = true;
            IsShowTypeFilter = true;
        }
        public const string TYPE_PARAM = "UcTypeAndAreaFilter_type";
        public const string AREA_PARAM = "UcTypeAndAreaFilter_area";
        const string ITEM_HTML = "<li><a href=\"{0}\">{1}</a></li>";
        const string ITEM_HTML_CUR = "<li><a href=\"{0}\" class=\"current\">{1}</a></li>";
        public event OnGetTypeNameHandler OnGetTypeName;
        public event OnGetAreaNameHandler OnGetAreaName;
        private string _cssClass;
        public string CssClass
        {
            get
            {
                if (string.IsNullOrEmpty(_cssClass))
                {
                    _cssClass = "typeItemNav";
                }
                return _cssClass;
            }
            set { _cssClass = value; }
        }
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<div class=\"{0}\">", CssClass);
            if (IsShowTypeFilter)
                RenderType(writer);
            if (IsShowAreaFilter)
                RenderArea(writer);
            writer.Write("</div>");
            base.Render(writer);
        }
        private string FormatQueryUrl(string arg, string value)
        {
            return string.Format(HtmlControl.SETFILTER_TPL, arg, HttpUtility.UrlEncodeUnicode(value));
        }
        private string GetItemHtml(string key, string name, string value = "")
        {
            string lblText = name;
            if (TYPE_PARAM.Equals(key, StringComparison.InvariantCultureIgnoreCase))
            {
                if (OnGetTypeName != null)
                    lblText = OnGetTypeName(name);
                return string.Format(name.Equals(CurTypeValue, StringComparison.InvariantCultureIgnoreCase) ? ITEM_HTML_CUR : ITEM_HTML, FormatQueryUrl(key, string.IsNullOrEmpty(value) ? name : value), lblText);
            }
            else
            {
                if (OnGetAreaName != null)
                    lblText = OnGetAreaName(name, value);
                return string.Format(value.Equals(CurAreaValue, StringComparison.InvariantCultureIgnoreCase) ? ITEM_HTML_CUR : ITEM_HTML, FormatQueryUrl(key, string.IsNullOrEmpty(value) ? name : value), lblText);
            }
        }
        private void RenderType(HtmlTextWriter writer)
        {
            if (App.TypeFilterDefineList.Count == 0)
                return;
            writer.Write("<div class=\"type_dot\">");
            writer.Write("<span class=\"sort\">类别：</span>");
            writer.Write("<ul class=\"typeUL\">");
            foreach (string type in App.TypeFilterDefineList.Keys)
                writer.Write(GetItemHtml(TYPE_PARAM, type));
            writer.Write("</ul></div>");
        }
        private void RenderArea(HtmlTextWriter writer)
        {
            string typeName = "区域";
            List<SysArea> areas = null;
            if (IsShowLoginInfo && AppContext.Context.CurrentSubSys != SubSystem.ALL)
            {
                var user = AppContext.Context.User;
                var depths = string.Format("{0},{1},{2}", user.AreaDepth2, user.AreaDepth1, user.AreaDepth3).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (depths.Length > 0)
                {
                    SysArea city = AreaBLL.GetCurrentCity( );
                    var query = DB.Select( ).Distinct( ).From<SysArea>( )
                                        //.Where(SysArea.PIdColumn).IsEqualTo(city.Id)
                                        .Where(SysArea.DepthColumn).In(depths);
                    //writer.Write("<div>{0}</div>", query.ToString( ));
                    areas = query.ExecuteTypedList<SysArea>( );
                }
            }
            if (areas == null || areas.Count == 0)
            {
                SysArea city = AreaBLL.GetCurrentCity( );
                areas = DB.Select( ).From<SysArea>( )
                                    .Where(SysArea.PIdColumn).IsEqualTo(city.Id)
                                    .ExecuteTypedList<SysArea>( );
            }
            else
                typeName = "圈子";
            if (areas == null || areas.Count == 0)
                return;
            writer.Write("<div class=\"type_dot\">");
            writer.Write("<span class=\"sort\">{0}：</span>", typeName);
            writer.Write("<ul class=\"typeUL\">");
            foreach (SysArea area in areas)
            {
                writer.Write(GetItemHtml(AREA_PARAM, area.AreaName, area.Depth));
            }
            writer.Write("</ul></div>");
        }
        private string GetFilterValue(string type)
        {
            string val = UcTypeAndAreaFilter_Value.Value;
            if (string.IsNullOrEmpty(val))
                return string.Empty;
            else if (val.StartsWith(type))
                return HttpUtility.UrlDecode(val.Replace(type, ""));
            else
                return string.Empty;
        }
        private string _curTypeValue;
        public string CurTypeValue
        {
            get
            {
                if (string.IsNullOrEmpty(_curTypeValue))
                    _curTypeValue = GetFilterValue(TYPE_PARAM);
                return _curTypeValue;
            }
        }
        private string _curAreaValue;
        public string CurAreaValue
        {
            get
            {
                if (string.IsNullOrEmpty(_curAreaValue))
                    _curAreaValue = GetFilterValue(AREA_PARAM);
                return _curAreaValue;
            }
        }
        public string ResetPageIndexControl
        {
            get;
            set;
        }
        public bool IsShowTypeFilter { get; set; }
        public bool IsShowAreaFilter { get; set; }
        public bool IsShowLoginInfo { get; set; }
    }
}