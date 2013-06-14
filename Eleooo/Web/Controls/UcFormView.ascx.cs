using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using System.Text;
using System.Data;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public delegate void OnFormViewDataBindRow(string columnName, UcFormView.FormViewRow viewRow);
    public delegate void OnFormViewValidate(string columnName, UcFormView.FormViewRow viewRow);
    public delegate void OnFormViewSaveHandle(object item);
    public partial class UcFormView : UserControlBase
    {
        #region Template Text
        const string CSS1 = "tbl_body";
        const string CSS2 = "tjTb";
        const string FORM_VIEW_HEADER_TEMPLATE =
        @"<table class='{0}' border='0' cellspacing='1' cellpadding='5' width='99%'>
            <tbody>";
        const string FORM_VIEW_SAVE_TEMPLATE =
        @"<input type='button' style='line-height: 18px; width: 100px; height: 25px; font-size: 14px;
                       font-weight: bold' OnClick='__doPostBack({0});' id='btnSubmit' value='提交(S)' />";
        const string FORM_VIEW_CANCEL_TEMPLATE =
        @"<input type='button' style='line-height: 18px; width: 100px; height: 25px; font-size: 14px;
                       font-weight: bold' onclick='__onBtnCloseClick();' id='btnClose' value='取消(C)' />";
        const string FORM_VIEW_FOOTER_TEMPLATE =
        @"    <tr class='tbl_form_row' height='26' bgcolor='#f0f0f5'>
                 <td colspan='2' align='middle' bgcolor='#f0f0f5'>
                    {0}
                    &nbsp;&nbsp;
                    {1}
                  </td>
              </tr>  
            </tbody>
          </table>";
        const string FORM_VIEW_ROW_TEMPLATE =
        @"  <tr class='tbl_form_row'>
              <td width='15%' align='right'>
                {0}
              </td>
              <td style='line-height: 18px;'>
                {1}
                {2}
              </td>
             </tr>";
        public const string FORM_VIEW_XHEDITOR_TEMPLATE = "<textarea name='{0}' rows='2' cols='20' id='{0}' class=\"xheditor {{skin:'o2007blue',clickCancelDialog:false,forcePtag:false}}\" style='height:360px;width:100%;'>{1}</textarea>";
        public const string FORM_VIEW_TEXTAREA_TEMPLATE =
        "<textarea name=\"{0}\" id=\"{0}\" style=\"width: 400px; height: 60px;\" rows=\"2\" cols=\"20\">{1}</textarea>";
        public const string FORM_VIEW_TEXT_DISABLED_TEMPATE =
        @"<input id='{0}' name='{0}' value='{1}' type='text' disabled />";
        public const string FORM_VIEW_PHONE_TEMPLATE =
        @"<input maxLength='11' id='{0}' name='{0}' value='{1}' type='text' />";
        public const string FORM_VIEW_TEXT_TEMPLATE =
        @"<input id='{0}' name='{0}' value='{1}' type='text' />";
        public const string FORM_VIEW_TEXT_WIDTH_TEMPLATE =
        @"<input id='{0}' name='{0}' value='{1}' type='text' style='width:{2}px;' />";
        public const string FORM_VIEW_HIDDEN_TEMPLATE =
        @"<input id='{0}' name='{0}' value='{1}' type='hidden' />";
        public const string FORM_VIEW_PWD_TEMPLATE =
        @"<input maxLength='6'  id='{0}' name='{0}' value='{1}' type='password' />";
        public const string FORM_VIEW_DATE_TEMPLATE =
        @"<input class='txtDate' onclick='WdatePicker()' id='{0}' name='{0}' value='{1}' type='text' />";
        const string FORM_VIEW_CLIENT_VALIDATE_SCRIPT =
        @"if(!$('#{0}').val()){{
            $('#{1}').html('{2}');
            $('#{0}').focus();
            return false;
        }}";
        const string FORM_VIEW_CLIENT_VALIDAE_PRESCRIPT =
        @"$('#{0}').html('');
         ";
        const string FORM_VIEW_VALIDATE_BOX =
        @"<span class='validate_info' id='{0}'>{1}</span>";
        const string FORM_VIEW_VALIDATE_MESSAGE =
        @"{0}不能为空!";
        #endregion
        private static readonly Type __gridType = typeof(EditableGrid<>);
        private bool isAllowSave = true;
        private bool isClear = false;
        public bool IsAllowSave
        {
            get { return isAllowSave; }
            set { isAllowSave = value; }
        }
        private bool IsDataBinded;
        private Dictionary<string, FormViewRow> _columns;
        protected Dictionary<string, FormViewRow> Columns
        {
            get
            {
                if (_columns == null)
                    _columns = new Dictionary<string, FormViewRow>( );
                return _columns;
            }
        }
        private DataTable _defVal;
        protected DataTable DefVal
        {
            get
            {
                if (_defVal == null)
                {
                    _defVal = DataSource.ExecuteDataTable( );
                }
                return _defVal;
            }
        }
        private StringBuilder _sbHtmlBuff;
        protected StringBuilder SbHtmlBuff
        {
            get
            {
                if (_sbHtmlBuff == null)
                    _sbHtmlBuff = new StringBuilder( );
                return _sbHtmlBuff;
            }
        }
        private StringBuilder _sbBeforeSubmitScript;
        protected StringBuilder SbBeforeSubmitScript
        {
            get
            {
                if (_sbBeforeSubmitScript == null)
                    _sbBeforeSubmitScript = new StringBuilder( );
                return _sbBeforeSubmitScript;
            }
        }

        private string _headerTemplate;
        public string HeaderTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(_headerTemplate))
                    return FORM_VIEW_HEADER_TEMPLATE;
                else
                    return _headerTemplate;
            }
            set { _headerTemplate = value; }
        }
        private string _footerTemplate;
        public string FooterTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(_footerTemplate))
                    return FORM_VIEW_FOOTER_TEMPLATE;
                else
                    return _footerTemplate;
            }
            set { _footerTemplate = value; }
        }
        private string _rowTemplate;
        public string RowTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(_rowTemplate))
                    return FORM_VIEW_ROW_TEMPLATE;
                else
                    return _rowTemplate;
            }
            set { _rowTemplate = value; }
        }
        private string _saveTemplate;
        public string SaveTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(_saveTemplate))
                    return FORM_VIEW_SAVE_TEMPLATE;
                else
                    return _saveTemplate;
            }
            set { _saveTemplate = value; }
        }
        private string _cancelTemplate;
        public string CancelTemplate
        {
            get
            {
                if (String.IsNullOrEmpty(_cancelTemplate))
                    return FORM_VIEW_CANCEL_TEMPLATE;
                else
                    return _cancelTemplate;
            }
            set { _cancelTemplate = value; }
        }

        private string _cssClass;
        public string CssClass
        {
            get
            {
                if (string.IsNullOrEmpty(_cssClass))
                {
                    SubSystem subSys = AppContext.Context.CurrentSubSys;
                    if (subSys == SubSystem.ALL || subSys == SubSystem.Member)
                        return CSS2;
                    else
                        return CSS1;
                }
                else
                    return _cssClass;
            }
            set { _cssClass = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind( );
        }
        protected override void OnInit(EventArgs e)
        {
            this.EnableViewState = false;
            base.OnInit(e);
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            DataBind( );
            writer.Write(SbHtmlBuff);
            base.RenderControl(writer);
        }
        public override void DataBind( )
        {
            if (!IsDataBinded && DataSource != null)
            {
                SbHtmlBuff.AppendFormat(HeaderTemplate, CssClass);
                BuildDataCellHtml(SbHtmlBuff);
                SbHtmlBuff.AppendFormat(FooterTemplate,
                    (IsAllowSave ? string.Format(SaveTemplate, DefVal.Rows.Count == 0 ? "\"Add\"" : "\"Edit\"") : string.Empty), CancelTemplate);
                BuildValidateScript( );
                IsDataBinded = true;
            }
        }
        protected virtual void BuildDataCellHtml(StringBuilder sbDataCell)
        {
            if (DataSource == null)
                return;
            foreach (KeyValuePair<string, FormViewRow> pair in Columns)
            {
                if (pair.Value.IsSkip)
                {
                    GetDefVaule(pair.Value);
                    RenderDefFieldHtml(pair.Value);
                }
            }
            foreach (KeyValuePair<string, FormViewRow> pair in Columns)
            {
                GetDefVaule(pair.Value);
                RenderDefFieldHtml(pair.Value);
                if (OnDataBindRow != null)
                    OnDataBindRow(pair.Key, pair.Value);
                if (pair.Value.IsSkip)
                    continue;
                if (pair.Value.IsHideField)
                    sbDataCell.Insert(0, pair.Value.RenderHtml);
                else if (pair.Value.IsGridColumn)
                {
                    sbDataCell.AppendFormat(@"<tr class='tbl_form_row'>
                                            <td width='15%' align='right'>
                                                {0}
                                            </td>
                                            <td style='line-height: 18px;'>", pair.Value.LblText);
                    pair.Value.Column.CallPrivateMethod<object>("RenderGrid", new Type[] { sbDataCell.GetType( ), typeof(string), typeof(string) }, sbDataCell, "", "");
                    sbDataCell.Append("</td></tr>");
                }
                else
                    sbDataCell.AppendLine(string.Format(RowTemplate, pair.Value.LblText, pair.Value.RenderHtml, pair.Value.ValidateBox));
            }
        }
        private void BuildValidateScript( )
        {
            StringBuilder sbScript = BasePage.MasterPage.ValidateScriptBuffer;
            foreach (FormViewRow viewRow in Columns.Values)
            {
                if (viewRow.IsClientValidate && !viewRow.IsDisabled)
                {
                    sbScript.Insert(0, string.Format(FORM_VIEW_CLIENT_VALIDAE_PRESCRIPT, viewRow.ValidateBoxID));
                    sbScript.AppendLine(string.Format(FORM_VIEW_CLIENT_VALIDATE_SCRIPT,
                        viewRow.ParamName, viewRow.ValidateBoxID, string.Format(FORM_VIEW_VALIDATE_MESSAGE, viewRow.LblText)));
                }
            }
            if (_sbBeforeSubmitScript != null)
                sbScript.AppendLine(_sbBeforeSubmitScript.ToString( ));
            sbScript.AppendLine("$('#btnSubmit').attr('disabled','true');");
        }
        private bool Validate(FormViewRow viewRow)
        {
            //default validate
            if (string.IsNullOrEmpty(viewRow.Value) && viewRow.TableColumn != null && !viewRow.TableColumn.IsNullable)
                viewRow.ValidateMessage = string.Format(FORM_VIEW_VALIDATE_MESSAGE, viewRow.LblText);
            if (OnValidate != null)
                OnValidate(viewRow.Name, viewRow);
            if (viewRow.IsDisabled)
                viewRow.ValidateMessage = string.Empty;
            return string.IsNullOrEmpty(viewRow.ValidateMessage);
        }
        public UcFormView AddShowColumn(TableSchema.TableColumn column, string defValue = "", bool isDisabled = false, string lblText = "", string checkBoxDataSource = "", int width = 0)
        {
            if (column == null)
                throw new ArgumentException("column not exist!");
            if (Columns.ContainsKey(column.ColumnName))
                throw new ArgumentException(string.Format("column: {0} has exist!", column.ColumnName));
            this.Columns.Add(column.ColumnName, new FormViewRow
            {
                Column = column,
                LblText = string.IsNullOrEmpty(lblText) ? ResBLL.GetColumnRes(column) : lblText,
                DefValue = defValue,
                IsDisabled = isDisabled,
                CheckBoxDataSource = checkBoxDataSource,
                Width = width
            });
            return this;
        }
        public UcFormView AddHideColumn(TableSchema.TableColumn column, string defValue = "")
        {
            if (column == null)
                throw new ArgumentException("column not exist!");
            if (Columns.ContainsKey(column.ColumnName))
                throw new ArgumentException(string.Format("column: {0} has exist!", column.ColumnName));
            this.Columns.Add(column.ColumnName, new FormViewRow
            {
                Column = column,
                LblText = ResBLL.GetColumnRes(column),
                IsHideField = true,
                DefValue = defValue
            });
            return this;
        }
        public UcFormView AddShowColumn(params TableSchema.TableColumn[] columns)
        {
            if (columns != null && columns.Length > 0)
                foreach (TableSchema.TableColumn column in columns)
                    AddShowColumn(column);
            return this;
        }
        public UcFormView AddShowColumn(string column, string defValue = "", bool isDisabled = false, string lblText = "", string checkBoxDataSource = "")
        {
            if (this.Columns.ContainsKey(column))
                throw new ArgumentException(string.Format("column: {0} has exist!", column));
            return AddShowColumn(GetColumn(column), defValue, isDisabled, lblText, checkBoxDataSource);
        }
        public UcFormView AddShowColumn(params string[] columns)
        {
            if (columns != null && columns.Length > 0)
                foreach (string column in columns)
                    AddShowColumn(column);
            return this;
        }
        public UcFormView AddCustomColumn(string column, string lblText, string defValue, bool isDisabled = false, string checkBoxDataSource = "")
        {
            if (Columns.ContainsKey(column))
                throw new ArgumentException(string.Format("column: {0} has exist!", column));
            Columns.Add(column, new FormViewRow { Column = column, LblText = lblText, DbValue = defValue, IsDisabled = isDisabled, CheckBoxDataSource = checkBoxDataSource });
            return this;
        }
        public UcFormView AddHideColumn(string column, string defValue)
        {
            if (Columns.ContainsKey(column))
                throw new ArgumentException(string.Format("column: {0} has exist!", column));
            Columns.Add(column, new FormViewRow { Column = column, DbValue = defValue, IsHideField = true });
            return this;
        }
        public EditableGrid<T> AddGridColumn<T>(string column, SqlQuery dataSource, string lblText = "")
            where T : ActiveRecord<T>, new( )
        {
            if (Columns.ContainsKey(column))
                throw new ArgumentException(string.Format("column: {0} has exist!", column));
            EditableGrid<T> grid = new EditableGrid<T>(column, dataSource);
            Columns.Add(column, new FormViewRow { Column = grid, LblText = lblText });
            return grid;
        }
        public EditableGrid<T> AddGridColumn<T>(TableSchema.TableColumn column, SqlQuery dataSource)
            where T : ActiveRecord<T>, new( )
        {
            if (Columns.ContainsKey(column.ColumnName))
                throw new ArgumentException(string.Format("column: {0} has exist!", column.ColumnName));
            EditableGrid<T> grid = new EditableGrid<T>(column.ColumnName, dataSource);
            Columns.Add(column.ColumnName, new FormViewRow { Column = grid, LblText = ResBLL.GetColumnRes(column) });
            return grid;
        }
        public UcFormView AddGridColumn<T>(EditableGrid<T> grid, string lblText = "")
            where T : ActiveRecord<T>, new( )
        {
            if (Columns.ContainsKey(grid.GridName))
                throw new ArgumentException(string.Format("column: {0} has exist!", grid.GridName));
            Columns.Add(grid.GridName, new FormViewRow { Column = grid, LblText = lblText });
            return this;
        }
        public void ClearValue( )
        {
            foreach (FormViewRow viewRow in Columns.Values)
            {
                viewRow.DbValue = null;
                viewRow.ParamValue = null;
                viewRow.ValidateMessage = null;
            }
            isClear = true;
        }
        public UcFormView AddBeforSubmitScript(string txtScript)
        {
            if (!string.IsNullOrEmpty(txtScript))
                SbBeforeSubmitScript.AppendLine(txtScript);
            return this;
        }
        public UcFormView AddBeforSubmitScript(string txtScript, params object[] args)
        {
            if (args != null && args.Length > 0)
                return AddBeforSubmitScript(string.Format(txtScript, args));
            else
                return AddBeforSubmitScript(txtScript);
        }
        private TableSchema.TableColumn GetColumn(string column)
        {
            string tableName = string.Empty;
            string colName = string.Empty;
            column = column.Replace("[", string.Empty).Replace("]", string.Empty).Replace("dbo.", string.Empty).Trim( );
            if (column.Contains(" "))
                return null;
            if (column.Contains("."))
            {
                string[] arr = column.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                tableName = arr[0];
                colName = arr[arr.Length - 1];
            }
            else
            {
                colName = column;
            }
            TableSchema.TableColumn result = null;
            if (!string.IsNullOrEmpty(tableName))
            {
                TableSchema.Table table = DataService.GetSchema(tableName);
                result = table.GetColumn(colName);
            }
            else
            {
                foreach (TableSchema.Table table in DataSource.FromTables)
                {
                    foreach (TableSchema.TableColumn col in table.Columns)
                    {
                        if (Utilities.Compare(col.ColumnName, colName))
                        {
                            result = col;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        public FormViewRow GetViewRow(string colName)
        {
            if (this.Columns.ContainsKey(colName))
            {
                GetDefVaule(Columns[colName]);
                return Columns[colName];
            }
            else
                return null;

        }
        public FormViewRow GetViewRow(TableSchema.TableColumn col)
        {
            return GetViewRow(col.ColumnName);
        }
        private void AutoBuildShowColumn( )
        {
            char[] splitChar = new char[] { ' ' };
            if (Columns.Count == 0 && DataSource != null)
            {
                if (DataSource.SelectColumnList == null || DataSource.SelectColumnList.Length == 0)
                {
                    foreach (TableSchema.Table table in DataSource.FromTables)
                    {
                        AddShowColumn(table.Columns.ToArray( ));
                    }
                }
                else
                {
                    foreach (string column in DataSource.SelectColumnList)
                    {
                        TableSchema.TableColumn col = GetColumn(column);
                        if (col != null)
                            AddShowColumn(col);
                        else
                        {
                            string[] colArr = column.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
                            string strCol = colArr[colArr.Length - 1].Trim( );
                            AddCustomColumn(strCol, strCol, string.Empty);
                        }
                    }
                }
            }

        }

        private void GetDefVaule(FormViewRow viewRow)
        {
            if (DefVal != null && DefVal.Columns.Contains(viewRow.Name) && DefVal.Rows.Count > 0 && !isClear)
            {
                if (DefVal.Columns[viewRow.Name].DataType == typeof(DateTime))
                    viewRow.DbValue = Utilities.ToDate(DefVal.Rows[0][viewRow.Name]);
                else
                    viewRow.DbValue = HttpUtility.HtmlDecode(Utilities.ToString(DefVal.Rows[0][viewRow.Name]));
            }
            if (BasePage.Params.ContainsKey(viewRow.ParamName) && !isClear)
                viewRow.ParamValue = HttpUtility.HtmlEncode(BasePage.Params[viewRow.ParamName]);
            if (!string.IsNullOrEmpty(viewRow.CheckBoxDataSource) && !isClear)
                viewRow.ParamValue = HtmlControl.GetCheckBoxPostValue(viewRow.CheckBoxDataSource, viewRow.ParamName);
        }
        private void RenderDefFieldHtml(FormViewRow viewRow)
        {
            if (!string.IsNullOrEmpty(viewRow.RenderHtml) || viewRow.IsGridColumn)
                return;
            if (!IsAllowSave || viewRow.IsDisabled)
                viewRow.RenderHtml = string.Format(FORM_VIEW_TEXT_DISABLED_TEMPATE, viewRow.ParamName, viewRow.Value);
            else if (viewRow.IsCustomColumn && !viewRow.IsHideField && string.IsNullOrEmpty(viewRow.CheckBoxDataSource))
                viewRow.RenderHtml = string.Format(viewRow.Width > 0 ? FORM_VIEW_TEXT_WIDTH_TEMPLATE : FORM_VIEW_TEXT_TEMPLATE, viewRow.ParamName, viewRow.Value, viewRow.Width);
            else if (viewRow.IsHideField)
                viewRow.RenderHtml = string.Format(FORM_VIEW_HIDDEN_TEMPLATE, viewRow.ParamName, viewRow.Value);
            else if (viewRow.TableColumn.DataType == DbType.DateTime ||
                viewRow.TableColumn.DataType == DbType.Date ||
                viewRow.TableColumn.DataType == DbType.DateTime2)
                viewRow.RenderHtml = string.Format(FORM_VIEW_DATE_TEMPLATE, viewRow.ParamName, viewRow.Value);
            else if (!string.IsNullOrEmpty(viewRow.CheckBoxDataSource))
                viewRow.RenderHtml = HtmlControl.GetCheckBoxHtml(viewRow.CheckBoxDataSource, viewRow.ParamName, viewRow.Value).ToString( );
            else
                viewRow.RenderHtml = string.Format(viewRow.Width > 0 ? FORM_VIEW_TEXT_WIDTH_TEMPLATE : FORM_VIEW_TEXT_TEMPLATE, viewRow.ParamName, viewRow.Value, viewRow.Width);
        }
        public SqlQuery DataSource { get; set; }
        public object SavedItem { get; set; }
        public T GetValue<T>(string column)
        {
            if (DefVal.Rows.Count == 0)
                return default(T);
            else
                return DefVal.Rows[0].Field<T>(column);
        }
        public void SetValue(string column, object val)
        {
            if (DefVal.Columns.Contains(column) && DefVal.Rows.Count > 0)
                DefVal.Rows[0][column] = val;
        }
        public int Save<T>(object id)
            where T : ActiveRecord<T>, new( )
        {
            T t = ReadOnlyRecord<T>.FetchByID(id);
            if (t == null)
            {
                t = new T( );
                t.MarkNew( );
            }
            List<object> gridColumns = new List<object>( );
            TableSchema.Table table = t.GetSchema( );
            foreach (KeyValuePair<string, FormViewRow> pair in Columns)
            {
                bool isNeedCol = table.Columns.Contains(pair.Value.TableColumn);
                GetDefVaule(pair.Value);
                if (!Validate(pair.Value) && isNeedCol)
                    return -1;
                else
                    pair.Value.ValidateMessage = string.Empty;
                if (pair.Value.IsGridColumn)
                    gridColumns.Add(pair.Value.Column);
                else if (isNeedCol && !pair.Value.IsDisabled)
                {
                    t.SetColumnValue(pair.Key, GetValue(pair.Value));
                }
            }
            if (OnBeforeSaved != null)
                OnBeforeSaved(t);
            t.Save( );
            SavedItem = t;
            foreach (object grid in gridColumns)
            {
                bool isSuccess = grid.CallPrivateMethod<bool>("Save");
                if (!isSuccess)
                    return -1;
            }
            if (OnAfterSaved != null)
                OnAfterSaved(t);
            return 0;
        }
        private object GetValue(FormViewRow viewRow)
        {
            string v = viewRow.Value;
            if (viewRow.TableColumn != null)
            {
                switch (viewRow.TableColumn.DataType)
                {
                    case DbType.Boolean:
                        if (Utilities.IsNull(v))
                            return null;
                        if (SubSonic.Sugar.Validation.IsNumeric(v))
                        {
                            int n;
                            if (!int.TryParse(v, out n) || n <= 0)
                                return false;
                            else
                                return true;
                        }
                        bool b;
                        Boolean.TryParse(v, out b);
                        return b;
                    case DbType.Date:
                        if (!string.IsNullOrEmpty(v))
                            return Convert.ToDateTime(HttpUtility.HtmlDecode(v));
                        else
                            return null;
                    case DbType.DateTime:
                        if (!string.IsNullOrEmpty(v))
                            return Convert.ToDateTime(v);
                        else
                            return null;
                    default:
                        if (viewRow.TableColumn.IsNumeric &&
                            viewRow.TableColumn.IsNullable &&
                            string.IsNullOrEmpty(v))
                            return null;
                        else
                            return HttpUtility.HtmlEncode(v);
                }
            }
            else
                return HttpUtility.HtmlEncode(v);
        }
        public event OnFormViewDataBindRow OnDataBindRow;
        public event OnFormViewValidate OnValidate;
        public event OnFormViewSaveHandle OnBeforeSaved;
        public event OnFormViewSaveHandle OnAfterSaved;

        public class FormViewRow
        {
            private Type _colType;
            private object _column;
            public object Column
            {
                get { return _column; }
                set
                {
                    _column = value;
                    if (value != null)
                        _colType = value.GetType( );
                    IsClientValidate = !IsCustomColumn && TableColumn != null && !TableColumn.IsNullable;
                }
            }
            public string LblText { get; set; }
            public string DbValue { get; set; }
            public string DefValue { get; set; }
            public string ParamValue { get; set; }
            public string RenderHtml { get; set; }
            public int Width { get; set; }
            public string CheckBoxDataSource { get; set; }
            public bool IsHideField { get; set; }
            public bool IsCustomColumn
            {
                get
                {
                    if (_colType == typeof(TableSchema.TableColumn) || IsGridColumn)
                        return false;
                    else
                        return true;
                }
            }
            public bool IsGridColumn
            {
                get
                {
                    return _colType != null && _colType.IsGenericType &&
                        _colType.GetGenericTypeDefinition( ) == UcFormView.__gridType;
                }
            }
            public bool IsSkip { get; set; }
            public TableSchema.TableColumn TableColumn
            {
                get
                {
                    return Column as TableSchema.TableColumn;
                }
            }
            public string Value
            {
                get
                {

                    if (AppContext.Page.IsPostBack)
                        return ParamValue;
                    else
                    {
                        if (string.IsNullOrEmpty(ParamValue) && string.IsNullOrEmpty(DbValue))
                            return DefValue;
                        else if (string.IsNullOrEmpty(ParamValue))
                            return DbValue;
                        else
                            return ParamValue;
                    }
                }
                set
                {
                    ParamValue = value;
                }
            }
            public string ParamName
            {
                get
                {
                    if (IsCustomColumn)
                        return Utilities.ToString(Column);
                    else if (IsGridColumn)
                        return Column.GetPrivateProperty<string>("GridName");
                    else
                        return string.Concat(TableColumn.Table.TableName, "_", TableColumn.ColumnName);
                }
            }
            public string Name
            {
                get
                {
                    if (IsCustomColumn)
                        return Utilities.ToString(Column);
                    else if (IsGridColumn)
                        return Column.GetPrivateProperty<string>("GridName");
                    else
                        return TableColumn.ColumnName;
                }
            }
            public bool IsDisabled { get; set; }
            public string ValidateMessage { get; set; }
            public bool IsClientValidate { get; set; }
            internal string ValidateBoxID
            {
                get
                {
                    return string.Concat(ParamName, "_box");
                }
            }
            internal string ValidateBox
            {
                get
                {
                    return string.Format(FORM_VIEW_VALIDATE_BOX, ValidateBoxID, ValidateMessage);
                }
            }
        }
    }
}