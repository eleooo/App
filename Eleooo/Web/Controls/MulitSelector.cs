using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Security.Permissions;
using System.ComponentModel;
using Newtonsoft.Json;
using Eleooo.DAL;
using Eleooo.Web;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), DefaultProperty("Text"), ToolboxData("<{0}:MulitSelector runat=\"server\"> </{0}:MulitSelector>")]
    public class MulitSelector : WebControl
    {
        const string SELECTOR_SCRIPT = "/Scripts/jquery-selector.js";
        const string TEXTEXT_SCRIPT = "/Scripts/TextboxList/TextboxList.js";
        const string TEXTEXT_CSS = "/Scripts/TextboxList/TextboxList.css";
        const string AREA_TEXTEXT_SCRIPT = "/Scripts/jquery.TextboxList.area.js";
        const string TEXTEXT_TEXTAREA = "<input type='text' id='{0}'>";
        const string TEXTEXT_BTNADD = "<input type='button' id='{0}' style='width:60px;height:20px;' value='添加'/>";

        const string SELECTOR_HANDLER = "/Public/RestHandler.ashx/Area?pid=";
        const int SELECTOR_COUNT = 2;
        const string FIRSTITEMTEXT = "...请选择...";
        const string FIRSTITEMVALUE = "-1";
        const string DEFAULT_LABEL = "<a href='/Admin/SysAreaEdit.aspx?PID=0' target='_blank'>添加区域</a>&nbsp;&nbsp;&nbsp;&nbsp;";
        const string SELECTOR_ID_PREFIX = "slMulitSelector_";
        const string SELECTOR_TPL = "<select name='{0}' id='{0}' {1} {2}></select>";
        const string SELECTOR_CSS = "class='{0}'";
        const string SELECTOR_WIDTH = "style='width:{0}px'";
        const string INITAREATEXTEXT = @"
    $('#{0}').areaext({{ btnAddId: '{1}',
        selectorId: '{2}',
        postTagsId: '{3}',
        selectedItems:{4},
        areaLimit:{5}
    }});";
        const string SELECTOR_OPTIONS = @"
    var options_{7} = {{
        url:'{0}',    //get json data url
        sid:'{1}',             //selector's id
        dv:'{2}',                    //default value
        ft:'{3}',        //first item text
        fv:'{4}',                    //first item value
        pid:{5},                       //top level pid
        renderTo:'{6}'
    }};
    $.fn.regMulitSelector(options_{7});";
        Char[] separator = new char[] { '/', ',' };
        public MulitSelector( )
            : this("div")
        {
        }
        public MulitSelector(string tagName)
            : base(tagName)
        {
            IsLoadScript = true;
            IsAreaLimit = false;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {

            writer.Write(RenderResult( ));
            base.RenderContents(writer);
        }
        string GetSelectorCss(int index)
        {
            if (string.IsNullOrEmpty(SelectorCssClass))
                return string.Empty;
            string[] arr = SelectorCssClass.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int i = arr.Length == 1 || index >= arr.Length ? 0 : index;
            if (!SubSonic.Sugar.Validation.IsNumeric(arr[i]))
                return string.Empty;
            else
                return string.Format(SELECTOR_CSS, arr[i]);
        }
        string GetSelectorWidth(int index)
        {
            if (string.IsNullOrEmpty(SelectorWidth))
                return string.Empty;
            string[] arr = SelectorWidth.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int i = arr.Length == 1 || index >= arr.Length ? 0 : index;
            if (!SubSonic.Sugar.Validation.IsNumeric(arr[i]))
                return string.Empty;
            else
                return string.Format(SELECTOR_WIDTH, arr[i]);
        }
        string GetSelectorID(int index)
        {
            return string.Concat(string.IsNullOrEmpty(Id_Prefix) ? SELECTOR_ID_PREFIX : Id_Prefix, index);
        }
        public string GetSelectedValue(int index)
        {
            return AppContext.Page.Request.Params[GetSelectorID(index)];
        }
        public string GetMulitValue( )
        {
            return SelectedIDToDepths(AppContext.Page.Request.Params[_textext_PostTags]);
        }
        public string GetSelectedValue(string sSpliter, uint selectCount)
        {
            string[] arr = new string[selectCount];
            for (int i = 0; i < selectCount; i++)
            {
                string sTemp = GetSelectedValue(i);
                arr[i] = string.IsNullOrEmpty(sTemp) ? string.Empty : sTemp;
            }
            return string.Join(sSpliter, arr);
        }
        public StringBuilder RenderResult( )
        {
            StringBuilder sb = new StringBuilder( );
            if (IsLoadScript)
                AppContext.ActPage.MasterPage.AddScriptPath(SELECTOR_SCRIPT);
            string scriptOptions = string.Format(SELECTOR_OPTIONS,
                                    _SelectorHandler,
                                    _SelectorID,
                                    _DefaultValue,
                                    _FirstItemText,
                                    _FirstItemValue,
                                    _FirstPID, _renderTo, Id_Prefix);
            AppContext.ActPage.MasterPage.AddLoadedScript(scriptOptions);
            if (IsAllowMulti)
            {
                string iniTextext = string.Format(INITAREATEXTEXT, _textext_TextArea, _textext_BtnAdd, _SelectorID, _textext_PostTags, this.ConvertMulitVal(_DefaultMultiValue), IsAreaLimit.ToString( ).ToLower( ));
                AppContext.ActPage.MasterPage.AddLoadedScript(iniTextext);
            }
            for (int i = 0; i < _SelectCount; i++)
            {
                if (i > 0 && i < _SelectCount)
                    sb.Append("&nbsp;-&nbsp;");
                sb.AppendFormat(SELECTOR_TPL, GetSelectorID(i), GetSelectorCss(i), GetSelectorWidth(i));
            }
            if (IsAllowMulti)
            {
                //sb.Append("&nbsp;&nbsp;");
                sb.AppendFormat(TEXTEXT_BTNADD, _textext_BtnAdd);
                sb.Append("&nbsp;&nbsp;");
                sb.Append(_LabelText);
                sb.Append("<br />");
                sb.AppendFormat(TEXTEXT_TEXTAREA, _textext_TextArea);
            }
            else
            {
                sb.Append("&nbsp;&nbsp;");
                sb.Append(_LabelText);
            }
            return sb;
        }
        private uint _SelectCount
        {
            get
            {
                return SelectorCount <= 0 ? SELECTOR_COUNT : SelectorCount;
            }
        }
        private string _SelectorHandler
        {
            get
            {
                return string.IsNullOrEmpty(SelectorHandler) ? SELECTOR_HANDLER : SelectorHandler;
            }
        }
        private string _SelectorID
        {
            get
            {
                string text = string.Empty;
                for (int i = 0; i < _SelectCount; i++)
                    text = string.Concat(text, GetSelectorID(i), ",");
                return text.Remove(text.Length - 1);
            }
        }
        private string _FirstPID
        {
            get
            {
                return string.IsNullOrEmpty(FirstPID) ? "0" : FirstPID;
            }
        }
        private string _FirstItemText
        {
            get
            {
                string temp, text = string.Empty;
                string[] arr = string.IsNullOrEmpty(FirstItemText) ? null : FirstItemText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < _SelectCount; i++)
                {
                    temp = arr != null && i < arr.Length ? Convert.ToString(arr[i]) : FIRSTITEMTEXT;
                    text = string.Concat(text, temp, ",");
                }
                return text.Remove(text.Length - 1);
            }
        }
        private string _FirstItemValue
        {
            get
            {
                string temp, text = string.Empty;
                string[] arr = string.IsNullOrEmpty(FirstItemValue) ? null : FirstItemValue.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < _SelectCount; i++)
                {
                    temp = arr != null && i < arr.Length ? Convert.ToString(arr[i]) : FIRSTITEMVALUE;
                    text = string.Concat(text, temp, ",");
                }
                return text.Remove(text.Length - 1);
            }
        }
        private string _DefaultValue
        {
            get
            {
                string s1 = GetLastDefMulitVal( );
                if (!string.IsNullOrEmpty(s1))
                {
                    List<string> s = new List<string>(s1.Split(separator, StringSplitOptions.RemoveEmptyEntries));
                    return string.Join(",", s.ToArray( ));
                }
                string temp;
                string text = string.Empty;
                string[] arr = string.IsNullOrEmpty(DefaultValue) ? null : DefaultValue.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < _SelectCount; i++)
                {
                    temp = arr != null && i < arr.Length ? Convert.ToString(arr[i]) : string.Empty;
                    text = string.Concat(text, temp, ",");
                }
                return text.Remove(text.Length - 1);
            }
        }
        private string _LabelText
        {
            get
            {
                return string.IsNullOrEmpty(LabelText) && IsShowLabel ? DEFAULT_LABEL : LabelText;
            }
        }
        private string _renderTo
        {
            get
            {
                if (string.IsNullOrEmpty(RenderTo))
                    return string.Empty;
                else
                    return RenderTo;
            }
        }

        private string _DefaultMultiValue
        {
            get
            {
                if (string.IsNullOrEmpty(DefaultMultiValue))
                    return string.Empty;
                else
                    return DefaultMultiValue;
            }
        }
        private string _textext_TextArea
        {
            get
            {
                return string.Concat(Id_Prefix, "TextArea");
            }
        }
        private string _textext_BtnAdd
        {
            get
            {
                return string.Concat(Id_Prefix, "btnAdd");
            }
        }
        private string _textext_PostTags
        {
            get
            {
                return string.Concat(Id_Prefix, "postTags");
            }
        }
        private string SelectedIDToDepths(string ids)
        {
            List<SysArea> areas = AreaBLL.GetAreasByIds(ids);
            List<string> depths = new List<string>( );
            foreach (SysArea area in areas)
                depths.Add(area.Depth);
            return string.Join(",", depths.ToArray( ));
        }
        private string SelectedDepthsToIDs(string depths)
        {
            List<SysArea> areas = AreaBLL.GetAreasByDepths(depths);
            List<string> ids = new List<string>( );
            foreach (SysArea area in areas)
                ids.Add(area.Id.ToString( ));
            return string.Join(",", ids.ToArray( ));
        }
        private string ConvertMulitVal(string depth)
        {
            List<SysArea> areas = AreaBLL.GetAreasByDepths(depth);
            List<NameIDResult> result = new List<NameIDResult>( );
            foreach (var area in areas)
            {
                NameIDResult item = new NameIDResult { id = area.Id, name = area.AreaName };
                result.Add(item);
            }
            return JsonConvert.SerializeObject(result);
        }
        private string GetLastDefMulitVal( )
        {
            if (!IsAllowMulti || string.IsNullOrEmpty(_DefaultMultiValue))
                return string.Empty;
            string[] arr = _DefaultMultiValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return arr[arr.Length - 1];
        }
        public uint SelectorCount { get; set; }
        public string SelectorHandler { get; set; }
        public string FirstPID { get; set; }

        public string SelectorWidth { get; set; }
        public string SelectorCssClass { get; set; }
        public string FirstItemText { get; set; }
        public string FirstItemValue { get; set; }
        public string DefaultValue { get; set; }
        public string LabelText { get; set; }
        public string RenderTo { get; set; }
        public string Id_Prefix { get; set; }
        public bool IsShowLabel { get; set; }
        public bool IsLoadScript { get; set; }
        public bool IsAllowMulti { get; set; }
        public bool IsNeedTextext { get; set; }
        public string DefaultMultiValue { get; set; }
        public bool IsAreaLimit { get; set; }
    }
}