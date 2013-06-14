using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using SubSonic;
using System.IO;
using System.Text;
using System.Data;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{

    public class EditableGrid<T> where T : ActiveRecord<T>, new( )
    {
        public const string ACTION_COL_NAME = "editableGrid_Action";
        const string GRID_SCRIPT_PATH = "/Scripts/editablegrid-2.0.1/editablegrid-2.0.1.js";
        const string GRID_EXT_SCRIPT_PATH = "/Scripts/editablegrid-2.0.1/editablegrid-ext.js";
        const string GRID_CSS_PATH = "/Scripts/editablegrid-2.0.1/editablegrid-2.0.1.css";
        const string GRID_ADD_ROW_BUTTON = "<input type=\"button\" style=\"width:70px;\" onclick=\"EditableGrid.gridList.{0}.addRow();\" value=\"新增\"/>";
        private bool IsActColAdded { get; set; }
        private DataTable _data;
        private DataTable Data
        {
            get
            {
                if (_data == null && DataSource != null)
                {
                    _data = DataSource.ExecuteDataTable( );
                }
                return _data;
            }
        }
        private string _gridName;
        public string GridName
        {
            get
            {
                return _gridName;
            }
        }
        private Dictionary<string, GridColumn> _columns;
        public Dictionary<string, GridColumn> Columns
        {
            get
            {
                if (_columns == null)
                    _columns = new Dictionary<string, GridColumn>( );
                return _columns;
            }
        }
        private TableSchema.Table _schema;
        public TableSchema.Table Schema
        {
            get
            {
                if (_schema == null)
                    _schema = new T( ).GetSchema( );
                return _schema;
            }
        }
        private TableSchema.TableColumn _pkCol;
        public TableSchema.TableColumn PKCol
        {
            get
            {
                if (_pkCol == null)
                    _pkCol = Schema.PrimaryKey;
                return _pkCol;
            }
        }
        private ChangedData _chgData;
        public ChangedData ChgData
        {
            get
            {
                if (_chgData == null)
                    _chgData = GetChgData( );
                return _chgData;
            }
        }
        public string Caption { get; set; }
        public UcFormView FormView { get; set; }
        private StringBuilder ValidateMessage { get; set; }
        public int MaxAllowRow { get; set; }

        public event OnEditableGridSaveHandler OnBeforSave;
        public event OnEditableGridSaveHandler OnAfterSave;

        public EditableGrid(string gridName)
            : this(gridName, null)
        {
        }
        public EditableGrid(string gridName, SqlQuery dataSource)
        {
            DataSource = dataSource;
            _gridName = gridName;
            ValidateMessage = new StringBuilder( );
            IsNeedLoadScript = true;
            MaxAllowRow = -1;
            AddKeyColumn( );
        }
        public SqlQuery DataSource { get; set; }
        public bool IsNeedLoadScript { get; set; }

        public EditableGrid<T> AddShowColumn(TableSchema.TableColumn col, string lblText = "", bool editable = true, bool renderable = true, string dataType = "")
        {
            if (col == null)
                goto lbl_end;
            AddColumnCore(col, col.ColumnName, string.IsNullOrEmpty(lblText) ? ResBLL.GetColumnRes(col) : lblText, editable, renderable, dataType);
        lbl_end:
            return this;
        }
        public EditableGrid<T> AddCustomColumn(string colName, string defValue = "", string lblText = "", bool editable = true, bool renderable = true, string dataType = "")
        {
            AddColumnCore(null, colName, lblText, editable, renderable, dataType, defValue);
            return this;
        }
        public EditableGrid<T> AddActionColumn( )
        {
            if (IsActColAdded)
                return this;
            if (Columns.ContainsKey(ACTION_COL_NAME))
                throw new ArgumentException(ACTION_COL_NAME + " has exsit!");
            //{"name":"action","label":"","datatype":"html","bar":true,"editable":false,"values":null}
            GridColumn col = new GridColumn
            {
                TabColumn = null,
                name = ACTION_COL_NAME,
                editable = false,
                renderable = true,
                datatype = "html",
                label = "操作",
                bar = true
            };
            this.Columns.Add(ACTION_COL_NAME, col);
            IsActColAdded = true;
            return this;
        }
        private void AddKeyColumn( )
        {
            string id = PKCol.ColumnName.ToLower( );
            if (this.Columns.ContainsKey(id))
                return;
            GridColumn col = new GridColumn
            {
                TabColumn = null,
                name = id,
                editable = false,
                renderable = false,
                datatype = "integer",
                label = "编号",
                bar = true
            };
            this.Columns.Add(id, col);
        }
        private void AddColumnCore(TableSchema.TableColumn col, string colName, string lblText, bool editable, bool renderable, string dataType, string defValue = "")
        {
            if (string.IsNullOrEmpty(colName))
                throw new ArgumentNullException("column name is not nullable");
            if (Columns.ContainsKey(colName))
                throw new ArgumentException(colName + " has exsit!");
            GridColumn gridCol = new GridColumn
            {
                TabColumn = col,
                name = colName,
                label = lblText,
                editable = editable,
                renderable = renderable,
                datatype = string.IsNullOrEmpty(dataType) ? GetColumnDataType(col) : dataType,
                bar = true,
                columnIndex = -1,
                precision = -1,
                cellValidators = "[]",
                DefaultValue = defValue
            };
            Columns.Add(colName, gridCol);
        }
        private string GetColumnDataType(TableSchema.TableColumn col)
        {
            if (col == null || col.IsString)
                return "string";
            else if (col.IsDateTime)
                return "date";
            else if (col.DataType == System.Data.DbType.Boolean)
                return "boolean";
            else if (Formatter.IsNumberic(col.DataType))
                return "double";
            else if (Formatter.IsInteger(col.DataType))
                return "integer";
            else
                return "string";
        }
        public void RenderGrid(StringBuilder sb, string cssClass = "", string tableID = "")
        {
            string renderToClient = string.Format("grid_{0}", GridName);
            sb.AppendFormat("<input id='{0}' name='{0}' type='hidden' />", GridName);
            sb.AppendFormat("<div id='{0}' >", renderToClient);
            sb.Append("</div>");
            if (ValidateMessage.Length > 0)
            {
                sb.AppendLine("<div style='color:red;'>");
                sb.Append(ValidateMessage);
                sb.Append("</div>");
            }
            AddActionColumn( );
            RenderInitGrid(sb, renderToClient, cssClass, tableID);
        }
        private void RenderInitGrid(StringBuilder sb, string renderToClient, string cssClass = "editablegrid", string tableID = "")
        {
            ActionPage page = AppContext.ActPage;
            bool isRenderToHead = page != null && page.MasterPage != null;
            StringBuilder buff = isRenderToHead ? page.MasterPage.AddLoadedScript(Environment.NewLine) : sb;
            if (isRenderToHead)
            {
                if (IsNeedLoadScript)
                {
                    page.MasterPage.AddCssPath(GRID_CSS_PATH);
                    page.MasterPage.AddScriptPath(GRID_SCRIPT_PATH);
                    page.MasterPage.AddScriptPath(GRID_EXT_SCRIPT_PATH);
                }
                page.MasterPage.ValidateScriptBuffer.AppendLine( );
                page.MasterPage.ValidateScriptBuffer.AppendFormat("$('#{0}').val(EditableGrid.gridList.{0}.getChangedJSON());", GridName);
            }
            else
            {
                buff.AppendLine("<script language=\"javascript\" type=\"text/javascript\">");
                buff.AppendLine("$(document).ready(function(){");
            }
            buff.AppendFormat("var {0} = new EditableGrid('{0}',", GridName);
            buff.Append('{');
            buff.AppendFormat("currentClassName:'{0}',currentTableid:'{1}'", cssClass, tableID);
            if (IsActColAdded)
                buff.AppendFormat(",actionColName:'{0}'", ACTION_COL_NAME);

            if (_chgData != null && _chgData.deleted != null && _chgData.deleted.Count > 0)
            {
                buff.AppendFormat(",deletedData:{0}", JsonConvert.SerializeObject(_chgData.deleted));
            }
            if (_chgData != null && _chgData.newAdded != null)
            {
                buff.AppendFormat(",newAddedRowCount:{0}", _chgData.newAdded.Rows.Count);
            }
            if (MaxAllowRow > 0)
            {
                buff.AppendFormat(",maxAllowRow:{0}", MaxAllowRow);
            }
            buff.Append(",caption:'");
            buff.AppendFormat(GRID_ADD_ROW_BUTTON, GridName);
            buff.Append(Caption);
            buff.Append("'});");
            buff.AppendLine( );
            buff.AppendFormat("{0}.processJSON(", GridName);
            //buff.Append('{');
            BuildEditableGridJSON(buff);
            buff.Append(");");
            buff.AppendLine( );
            buff.AppendFormat("{0}.renderGrid(\"{1}\");", GridName, renderToClient);
            if (!isRenderToHead)
            {
                buff.AppendLine("});");
                buff.AppendLine("</script>");
            }
        }
        private void BuildEditableGridJSON(StringBuilder sb)
        {
            using (StringWriter sw = new StringWriter(sb))
            {
                using (JsonTextWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartObject( );
                    BuildMetaDataJSON(writer);
                    BuildDataJSON(writer);
                    writer.WriteEndObject( );
                }
            }
        }
        private void BuildMetaDataJSON(JsonTextWriter writer)
        {
            writer.WritePropertyName("metadata");
            writer.WriteRawValue(JsonConvert.SerializeObject(Columns.Values));
        }
        private void BuildDataJSON(JsonTextWriter writer)
        {
            string pk = PKCol.ColumnName;
            if (!Data.Columns.Contains(pk))
                throw new ArgumentException(pk + " column does't exist");
            DataColumn pkDC = Data.Columns[pk];
            writer.WritePropertyName("data");
            writer.WriteStartArray( );
            foreach (DataRow row in Data.Rows)
            {
                if (RowCellDataIsDeleted(row, pkDC))
                    continue;
                writer.WriteStartObject( );
                writer.WritePropertyName("id");
                writer.WriteValue(row[pkDC]);
                DataRow dr = GetModifiedRowCellData(row, pkDC);
                if (!dr.Equals(dr))
                {
                    writer.WritePropertyName("isModified");
                    writer.WriteValue(true);
                }
                writer.WritePropertyName("values");
                writer.WriteStartObject( );
                foreach (KeyValuePair<string, GridColumn> pair in Columns)
                {
                    writer.WritePropertyName(pair.Key);
                    writer.WriteValue(Data.Columns.Contains(pair.Key) ? dr[pair.Key] : pair.Value.DefaultValue);
                }
                writer.WriteEndObject( );
                writer.WriteEndObject( );
            }
            if (_chgData != null && _chgData.newAdded != null && _chgData.newAdded.Rows.Count > 0)
            {
                foreach (DataRow row in _chgData.newAdded.Rows)
                {
                    writer.WriteStartObject( );
                    writer.WritePropertyName("id");
                    writer.WriteValue(row[pkDC.ColumnName]);
                    writer.WritePropertyName("isModified");
                    writer.WriteValue(true);
                    writer.WritePropertyName("isNew");
                    writer.WriteValue(true);
                    writer.WritePropertyName("values");
                    writer.WriteStartObject( );
                    foreach (KeyValuePair<string, GridColumn> pair in Columns)
                    {
                        writer.WritePropertyName(pair.Key);
                        writer.WriteValue(_chgData.newAdded.Columns.Contains(pair.Key) ? row[pair.Key] : pair.Value.DefaultValue);
                    }
                    writer.WriteEndObject( );
                    writer.WriteEndObject( );
                }
            }
            writer.WriteEndArray( );
        }
        private bool RowCellDataIsDeleted(DataRow row, DataColumn pkDC)
        {
            if (_chgData == null || _chgData.deleted == null || _chgData.deleted.Count == 0)
                goto lbl_false;
            int id = Utilities.ToInt(row[pkDC]);
            return _chgData.deleted.Contains(id);
        lbl_false:
            return false;
        }
        private DataRow GetModifiedRowCellData(DataRow row, DataColumn pkDC)
        {
            if (_chgData == null || _chgData.modified == null || _chgData.modified.Rows.Count == 0)
                goto lbl_curRow;
            object keyVal = row[pkDC];
            foreach (DataRow dr in _chgData.modified.Rows)
            {
                if (dr[pkDC.ColumnName] == keyVal)
                    return dr;
            }
        lbl_curRow:
            return row;
        }
        private DataRow GetDataRow(DataRow modifiedRow)
        {
            int keyVal = Utilities.ToInt( modifiedRow[PKCol.ColumnName]);
            foreach (DataRow row in Data.Rows)
            {
                if (Utilities.ToInt(row[PKCol.ColumnName]) == keyVal)
                    return row;
            }
            return null;
        }
        public ChangedData GetChgData( )
        {
            string json = HttpContext.Current.Request.Params[GridName];
            if (string.IsNullOrEmpty(json))
                return null;
            return JsonConvert.DeserializeObject(json, typeof(ChangedData)) as ChangedData;
        }
        public bool Save( )
        {
            try
            {
                if (ChgData.deleted != null)
                    foreach (var del in ChgData.deleted)
                    {
                        if (del.id > 0)
                            ActiveRecord<T>.Delete(del.id);
                    }
                string message = string.Empty;
                if (ChgData.modified != null)
                    foreach (DataRow row in ChgData.modified.Rows)
                    {
                        T t = new T( );
                        DataRow origRow = GetDataRow(row);
                        if (origRow == null)
                        {
                            ValidateMessage.AppendLine("数据异常");
                            return false;
                        }
                        t.ValidateWhenSaving = false;
                        t.Load(origRow);
                        t.MarkClean( );
                        t.Load(row);
                        t.MarkOld( );
                        if (OnBeforSave != null)
                            OnBeforSave(t, out message);
                        if (!string.IsNullOrEmpty(message))
                        {
                            ValidateMessage.AppendLine(message);
                            return false;
                        }
                        t.Save( );
                        foreach (DataColumn dc in ChgData.modified.Columns)
                        {
                            if (origRow.Table.Columns.Contains(dc.ColumnName))
                                origRow[dc.ColumnName] = row[dc];
                        }
                        if (OnAfterSave != null)
                            OnAfterSave(t, out message);
                    }
                if (ChgData.newAdded != null)
                    foreach (DataRow row in ChgData.newAdded.Rows)
                    {
                        T t = new T( );
                        t.ValidateWhenSaving = false;
                        t.Load(row);
                        t.MarkNew( );
                        if (OnBeforSave != null)
                            OnBeforSave(t, out message);
                        if (!string.IsNullOrEmpty(message))
                        {
                            ValidateMessage.AppendLine(message);
                            return false;
                        }
                        t.Save( );
                        if (OnAfterSave != null)
                            OnAfterSave(t, out message);
                    }
                _chgData = null;
                return true;
            }
            catch (Exception ex)
            {
                Logging.Log("EditableGrid->Save", ex, true);
                ValidateMessage.AppendLine(ex.Message);
                return false;
            }
        }
        public class ChangedData
        {
            public class Deleted
            {
                public int id { get; set; }
            }
            public class DeletedCollection : List<Deleted>
            {
                public bool Contains(int id)
                {
                    return this.Find(d => d.id == id) != null;
                }
            }
            public DeletedCollection deleted { get; set; }
            public DataTable newAdded { get; set; }
            public DataTable modified { get; set; }
        }

        public delegate void OnEditableGridSaveHandler(T item, out string message);
        [JsonObject(MemberSerialization.OptIn)]
        public class GridColumn
        {
            public string DefaultValue { get; set; }
            public TableSchema.TableColumn TabColumn { get; set; }
            public bool IsCustomCol
            {
                get
                {
                    return TabColumn == null;
                }
            }
            public string ValidateMessage { get; set; }
            [JsonProperty]
            public string name { get; set; }
            [JsonProperty]
            public string label { get; set; }
            [JsonProperty]
            public bool editable { get; set; }
            [JsonProperty]
            public bool renderable { get; set; }
            [JsonProperty]
            public string datatype { get; set; }
            [JsonProperty]
            public string unit { get; set; }
            [JsonProperty]
            public int precision { get; set; }
            [JsonProperty]
            public string nansymbol { get; set; }
            [JsonProperty]
            public string decimal_point { get; set; }
            [JsonProperty]
            public string thousands_separator { get; set; }
            [JsonProperty]
            public bool unit_before_number { get; set; }
            [JsonProperty]
            public bool bar { get; set; }
            [JsonProperty, JsonConverter(typeof(JavaScriptObjectConverter))]
            public string headerRenderer { get; set; }
            [JsonProperty, JsonConverter(typeof(JavaScriptObjectConverter))]
            public string headerEditor { get; set; }
            [JsonProperty, JsonConverter(typeof(JavaScriptObjectConverter))]
            public string cellRenderer { get; set; }
            [JsonProperty, JsonConverter(typeof(JavaScriptObjectConverter))]
            public string cellEditor { get; set; }
            [JsonProperty, JsonConverter(typeof(JavaScriptObjectConverter))]
            public string cellValidators { get; set; }
            [JsonProperty, JsonConverter(typeof(JavaScriptObjectConverter))]
            public string enumProvider { get; set; }
            [JsonProperty, JsonConverter(typeof(JavaScriptObjectConverter))]
            public string optionValues { get; set; }
            [JsonProperty]
            public int columnIndex { get; set; }
        }
    }
}