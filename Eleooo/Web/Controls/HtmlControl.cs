using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public class HtmlControl
    {
        public const string SETFILTER_TPL = "javascript:setFilterParam('{0}','{1}');";
        const string SELECT_BEGIN_TPL = "<select id='{0}' name='{0}' {1}>";
        const string SELECT_END_TPL = "</select>";
        const string SELECT_OPTION = "<option value='{0}' {1} >{2}</option>";
        const string SELECT_SELECTED = "selected='selected'";

        const string RADIO_TEMPLATE = @"<input class='radio' type='radio' name='{0}' value='{1}' {2} />{3}";
        const string RADIO_CHECKED = "checked='checked'";

        const string CHECKBOX_TEMPLATE = "<input class='radio' type=\"checkbox\" name=\"{0}_{2}\" value=\"{1}\" {3} /><label for=\"{0}_{2}\" style=\"margin-right:5px;\">{1}</label>";

        const string PHOTO_TEMPLATE =
        @" <input id='{0}' name='{0}' value='{1}' type='text'  class='txtUpload' />
           <input type='button' value='上传图片' id='btnUpload' txt='{0}' img='{0}_img' class='button btnUpload' />
           <br />
           <img id='{0}_img' src='{1}' width='100px' height='100px;' class='imgUpload' {2} />";

        public static readonly Dictionary<string, string> BoolChceckSource;
        static HtmlControl( )
        {
            BoolChceckSource = new Dictionary<string, string>( );
            BoolChceckSource.Add("1", "是");
            BoolChceckSource.Add("0", "否");
        }

        public static StringBuilder GetBoolRadioHtml(string postName, string value)
        {
            return GetRadioHtml(BoolChceckSource, postName, value);
        }

        public static string GetCheckBoxPostValue(string dataSource, string postName)
        {
            if (dataSource == null)
                dataSource = "";
            char[] spliter = new char[] { ',' };
            string[] arr = dataSource.Split(spliter, StringSplitOptions.RemoveEmptyEntries);
            List<string> vLst = new List<string>( );
            for (int i = 0; i < arr.Length; i++)
            {
                string v = AppContext.Page.Request.Params[postName + "_" + i.ToString( )];
                if (!string.IsNullOrEmpty(v))
                    vLst.Add(v);
            }
            return string.Join(",", vLst.ToArray( ));
        }
        public static StringBuilder GetCheckBoxHtml(string dataSource, string postName, string value)
        {
            StringBuilder sb = new StringBuilder( );
            if (string.IsNullOrEmpty(dataSource))
                return sb;
            if (value == null)
                value = "";
            char[] spliter = new char[] { ',' };
            string[] arr = dataSource.Split(spliter, StringSplitOptions.RemoveEmptyEntries);
            List<string> vLst = new List<string>(value.Split(spliter, StringSplitOptions.RemoveEmptyEntries));
            for (int i = 0; i < arr.Length; i++)
            {
                string item = arr[i];
                if (vLst.Contains(item))
                    sb.AppendFormat(CHECKBOX_TEMPLATE, postName, item, i, RADIO_CHECKED);
                else
                    sb.AppendFormat(CHECKBOX_TEMPLATE, postName, item, i, "");
            }
            return sb;
        }
        public static StringBuilder GetSelectHtml(Dictionary<string, string> dataSource, string postName, object value, string onChange = null)
        {
            StringBuilder sb = new StringBuilder( );
            sb.AppendFormat(SELECT_BEGIN_TPL, postName, onChange);
            bool isSelected = false;
            foreach (KeyValuePair<string, string> pair in dataSource)
            {
                if (pair.Key == Convert.ToString(value) && !isSelected)
                {
                    sb.AppendFormat(SELECT_OPTION, pair.Key, SELECT_SELECTED, pair.Value);
                    isSelected = true;
                }
                else
                {
                    sb.AppendFormat(SELECT_OPTION, pair.Key, string.Empty, pair.Value);
                }
            }
            sb.Append(SELECT_END_TPL);
            return sb;
        }
        public static StringBuilder GetRadioHtml(Dictionary<string, string> dataSource, string postName, object value)
        {
            StringBuilder sb = new StringBuilder( );
            bool isChecked = false;
            string strChecked;
            foreach (KeyValuePair<string, string> pair in dataSource)
            {
                if (pair.Key == Utilities.ToString(value) && !isChecked)
                {
                    strChecked = RADIO_CHECKED;
                    isChecked = true;
                }
                else
                    strChecked = string.Empty;
                sb.AppendFormat(RADIO_TEMPLATE, postName, pair.Key, strChecked, pair.Value);
            }
            return sb;
        }
        public static string GetPhotoHtml(string postName, string value)
        {
            return string.Format(PHOTO_TEMPLATE,
                                    postName,
                                    value,
                                    string.IsNullOrEmpty(value) ? "style='display: none;'" : string.Empty
                                    );
        }
        public static string GetTextAreaHtml(string postName, string value)
        {
            return string.Format(UcFormView.FORM_VIEW_TEXTAREA_TEMPLATE, postName, value);
        }
        public static string GetDatePicker(string postName, string value)
        {
            return string.Format(UcFormView.FORM_VIEW_DATE_TEMPLATE, postName, value);
        }
        public static string GetPhoneHtml(string postName, string value)
        {
            return string.Format(UcFormView.FORM_VIEW_PHONE_TEMPLATE, postName, value);
        }
        public static string GetPwdHtml(string postName, string value)
        {
            return string.Format(UcFormView.FORM_VIEW_PWD_TEMPLATE, postName, value);
        }
        public static string GetXheditorHtml(string postName, string value)
        {
            AppContext.ActPage.MasterPage.ValidateScriptBuffer.AppendLine( );
            AppContext.ActPage.MasterPage.ValidateScriptBuffer.AppendFormat("$('#{0}').val($('#{0}').val());", postName);
            AppContext.ActPage.MasterPage.ValidateScriptBuffer.AppendLine( );
            return string.Format(Eleooo.Web.Controls.UcFormView.FORM_VIEW_XHEDITOR_TEMPLATE, postName, AppContext.Page.Server.HtmlDecode(value));
        }
    }
}