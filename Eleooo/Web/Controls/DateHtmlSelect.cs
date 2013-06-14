using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public class DateHtmlSelect
    {
        const string SELECT_BEGIN_TPL = "<select id='{0}'>";
        const string SELECT_END_TPL = "</select>";
        const string SELECT_OPTION = "<option value='{0}' {1} >{0}</option>";
        const string DEFAULT_FORMAT = "yyyy-mm-dd";
        const string SELECT_YEAR_ID = "ddlYear";
        const string SELECT_MONTH_ID = "ddlMonth";
        const string SELECT_DAY_ID = "ddlDay";
        const string SELECT_SELECTED = "selected='selected'";
        const string SELECT_EVENT_SCRIPT =
        @"var ddlDateSelectChange = function () {
            var year = $('#ddlYear').val();
            var month = $('#ddlMonth').val();
            var day = $('#ddlDay').val();
            var result = '[FORMAT]'.replace('yyyy', year).replace('mm', month).replace('dd', day);
            $('#[RENDERTO]').val(result);
        }
        $(document).ready(function () {
            $('#ddlYear').change(ddlDateSelectChange);
            $('#ddlMonth').change(ddlDateSelectChange);
            $('#ddlDay').change(ddlDateSelectChange);
        });";

        public static string GetDateSelectCtrl(DateTime dtBegin, DateTime dtEnd,DateTime dtSelected, string renderTo)
        {
            RegScript(renderTo, DEFAULT_FORMAT);
            return string.Concat(BuildSelectYear(dtBegin, dtEnd, dtSelected.Year).ToString( ),
                                 BuildSelectMonth(dtSelected.Month).ToString( ),
                                 BuilSelectDay(dtSelected.Day).ToString( ));
        }
        public static string GetDateSelectCtrl(DateTime dtBegin, DateTime dtEnd, int selectY, int selectM, int selectD, string renderTo)
        {
            RegScript(renderTo, DEFAULT_FORMAT);
            return string.Concat(BuildSelectYear(dtBegin, dtEnd, selectY).ToString( ),
                                 BuildSelectMonth(selectM).ToString( ),
                                 BuilSelectDay(selectD).ToString( ));
        }

        public static string GetDateSelectCtrl(DateTime dtBegin, DateTime dtEnd, string renderTo)
        {
            return GetDateSelectCtrl(dtBegin, dtEnd, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, renderTo);
        }

        private static StringBuilder BuildSelectYear(DateTime dtBegin, DateTime dtEnd, int selectedY)
        {
            StringBuilder sb = new StringBuilder( );
            sb.AppendFormat(SELECT_BEGIN_TPL,SELECT_YEAR_ID);
            int iSpan = dtEnd.Year - dtBegin.Year;
            DateTime dBegin = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day);
            for (int i = 0; i <= iSpan; i++)
            {
                sb.AppendFormat(SELECT_OPTION, dBegin.Year.ToString( ), dBegin.Year == selectedY ? SELECT_SELECTED : string.Empty);
                dBegin = dBegin.AddYears(-1);
            }
            sb.Append(SELECT_END_TPL);
            sb.Append("年");
            return sb;
        }
        private static StringBuilder BuildSelectMonth(int selectedM)
        {
            StringBuilder sb = new StringBuilder( );
            sb.AppendFormat(SELECT_BEGIN_TPL, SELECT_MONTH_ID);
            for (int i = 1; i <= 12; i++)
            {
                sb.AppendFormat(SELECT_OPTION, i, i == selectedM ? SELECT_SELECTED : string.Empty);
            }
            sb.Append(SELECT_END_TPL);
            sb.Append("月");
            return sb;
        }
        private static StringBuilder BuilSelectDay(int selectedD)
        {
            StringBuilder sb = new StringBuilder( );
            sb.AppendFormat(SELECT_BEGIN_TPL, SELECT_DAY_ID);
            for (int i = 1; i <= 31; i++)
            {
                sb.AppendFormat(SELECT_OPTION, i, i == selectedD ? SELECT_SELECTED : string.Empty);
            }
            sb.Append(SELECT_END_TPL);
            sb.Append("日");
            return sb;
        }
        private static void RegScript(string renderTo, string format)
        {
            Utilities.RegisterScriptBlock("DateHtmlSelect",SELECT_EVENT_SCRIPT.Replace("[FORMAT]", format).Replace("[RENDERTO]", renderTo));
        }
    }
}