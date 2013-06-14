using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using System.ComponentModel;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public delegate string DataBindColumnHandle(string column, DataRow rowData, ref bool isRenderedCell);
    public delegate void DataBindHeaderHandle(string column, ref string caption, ref bool isRenderedCell);
    public delegate string DataBindFooterHandler( );
    public delegate string DataBindRowHandler(int rowIndex, DataRow rowData, ref bool isRenderedRow);
    public partial class UcGridView : UserControlBase
    {
        #region template const define
        const string CSS1 = "tbl_bg";
        const string CSS2 = "homeMainList clearall";
        const string ROWCOLSPAN = "</tr><tr class='tbl_row' height='25' align='middle'>";
        const string DATA_CELL_TEMPLATE = "<td>{0}</td>";
        const string DATA_ROW_TEMPLATE = @"<tr class='tbl_row' height='25' align='middle'>
                                                       {0}
                                                     </tr>";
        const string DATA_ROW_BEGIN_TEMPLATE = "<tr class='tbl_row' height='25' align='middle' >";
        const string DATA_ROW_END_TEMPLATE = "</tr>";
        const string EDITOR_TEXT = "Grid_Editor_Text";
        const string DELETER_HEADER_TEXT = "Grid_Deleter_Text";

        const string THEAND = @"<thead><tr class='tbl_head' height='25' align='middle' >
                          {0}
                         </tr></thead>";
        const string TBODY = "<tbody>{0}</tbody>";
        const string TFOOT = "<tfoot>{0}</tfoot>";
        const string GRIDVIEW_TEMPLATE = @"<table class='{0}' border='0' cellspacing='1' cellpadding='2' width='100%' align='center'>";
        const string GRIDVIEW_TEMPLATE_MEMBER = @"<table class='{0}' border='0' cellspacing='0' cellpadding='0'>";
        #endregion

        #region Paper support
        public event EventHandler OnPageIndexChange;
        public event EventHandler OnPageSizeChange;
        private bool isVisiblePageSize = true;
        public bool IsVisiblePageSize
        {
            get
            {
                return isVisiblePageSize;
            }
            set
            {
                if (UcPaper != null)
                    UcPaper.IsVisiblePageSize = value;
                isVisiblePageSize = value;
            }
        }
        protected UcGridPaper UcPaper
        {
            get;
            set;
        }
        protected void InitPaper( )
        {
            if (AllowPaper)
            {
                UcPaper = (UcGridPaper)(this.LoadControl("~/Controls/UcGridPaper.ascx"));
                if (_pageSize > 0)
                    UcPaper.PageSize = _pageSize;
                UcPaper.CallPrivateMethod<object>("OnInit", EventArgs.Empty);
                UcPaper.OnPageIndexChange += new EventHandler(_ucPaper_OnPageIndexChange);
                UcPaper.OnPageSizeChange += new EventHandler(_ucPaper_OnPageSizeChange);
                UcPaper.OnGetTotalRecord += new GetTotalRecordHandler(_ucPaper_OnGetTotalRecord);
                UcPaper.IsVisiblePageSize = isVisiblePageSize;
            }
        }
        int _ucPaper_OnGetTotalRecord( )
        {
            if (IsSqlQuerySource)
                return SqlQuerySource.Paged(0, 0).GetRecordCount( );
            else
                return Data.Rows.Count;
        }
        void _ucPaper_OnPageSizeChange(object sender, EventArgs e)
        {
            if (OnPageSizeChange != null)
            {
                OnPageSizeChange(sender, e);
            }
        }
        void _ucPaper_OnPageIndexChange(object sender, EventArgs e)
        {
            if (OnPageIndexChange != null)
                OnPageIndexChange(sender, e);
        }
        private int _pageindex;
        private int _pageSize;
        private int _totalRecord;
        public int PageSize
        {
            get
            {
                if (AllowPaper && UcPaper != null)
                    return UcPaper.PageSize;
                else
                    return _pageSize;
            }
            set
            {
                if (AllowPaper && UcPaper != null)
                    UcPaper.PageSize = value;
                else
                    _pageSize = value;
            }
        }
        public int PageCount
        {
            get
            {
                if (AllowPaper && UcPaper != null)
                    return UcPaper.PageCount;
                else
                {
                    int result;
                    int pageCount = Math.DivRem(TotalRecord, PageSize, out result);
                    if (result > 0)
                        pageCount++;
                    return pageCount;
                }

            }
        }
        public int PageIndex
        {
            get
            {
                if (AllowPaper && UcPaper != null)
                {
                    this.UcPaper.FireEvent(null, null);
                    return UcPaper.PageIndex;
                }
                else
                    return _pageindex;

            }
            set
            {
                if (AllowPaper && UcPaper != null)
                    UcPaper.PageIndex = value;
                else
                    _pageindex = value;
            }
        }
        public int TotalRecord
        {
            get
            {
                if (AllowPaper && UcPaper != null)
                    return UcPaper.TotalRecord;
                else if (_totalRecord == 0)
                    _totalRecord = _ucPaper_OnGetTotalRecord( );
                return _totalRecord;
            }
        }
        public int RowCount
        {
            get
            {
                return Data.Rows.Count;
            }
        }
        public string PageSizeID
        {
            get
            {
                return UcPaper.PageSizeID;
            }
        }
        public string PageIndexID
        {
            get
            {
                return UcPaper.PageIndexID;
            }
        }
        #endregion

        #region properties define
        private Dictionary<string, ColumnFormat> _colFmat;
        private Dictionary<string, ColumnFormat> ColFmat
        {
            get
            {
                if (_colFmat == null)
                    _colFmat = new Dictionary<string, ColumnFormat>( );
                return _colFmat;
            }
        }

        private Dictionary<string, object> _columns;
        protected Dictionary<string, object> Columns
        {
            get
            {
                if (_columns == null)
                    _columns = new Dictionary<string, object>( );
                return _columns;
            }
        }

        private int _rowColSpan = 1;
        public int RowColSpan { get { return _rowColSpan; } set { if (value > 0) _rowColSpan = value; } }
        public bool AllowPaper { get; set; }
        private bool _showHeader = true;
        [DefaultValue(true)]
        public bool ShowHeader { get { return _showHeader; } set { _showHeader = value; } }
        private object _dataSource;
        public object DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
                if (UcPaper != null)
                    UcPaper.FireEvent(null, EventArgs.Empty);
            }
        }
        private bool IsSqlQuerySource
        {
            get
            {
                return DataSource is SqlQuery;
            }
        }
        private SqlQuery SqlQuerySource
        {
            get
            {
                return DataSource as SqlQuery;
            }
        }
        private Query QuerySource
        {
            get
            {
                return DataSource as Query;
            }
        }
        private DataTable TableSource
        {
            get
            {
                return DataSource as DataTable;
            }
        }
        private string SqlSource
        {
            get
            {
                return DataSource as string;
            }
        }
        SubSystem subSys;
        private DataTable _data;
        protected DataTable Data
        {
            get
            {
                if (_data == null)
                    _data = GetDataTable( );
                return _data;
            }
        }
        private StringBuilder _tbody;
        private StringBuilder Tbody
        {
            get
            {
                if (_tbody == null)
                    _tbody = new StringBuilder( );
                return _tbody;
            }
        }
        private StringBuilder _theader;
        private StringBuilder Theader
        {
            get
            {
                if (_theader == null)
                    _theader = new StringBuilder( );
                return _theader;
            }
        }
        private StringBuilder _log;
        private StringBuilder Log
        {
            get
            {
                if (_log == null)
                    _log = new StringBuilder( );
                return _log;
            }
        }
        private bool IsDebug
        {
            get
            {
                return Request != null && Request.Params["Debug"] == "1";
            }
        }
        public event DataBindColumnHandle OnDataBindColumn;
        public event DataBindHeaderHandle OnDataBindHeader;
        public event DataBindFooterHandler OnDataBindFooter;
        public event DataBindRowHandler OnDataBindRow;
        private string _cssClass;
        public string CssClass
        {
            get
            {
                if (string.IsNullOrEmpty(_cssClass))
                {
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
        private bool _isShowPageAtHead = true;
        public bool IsShowPageAtHead { get { return _isShowPageAtHead; } set { _isShowPageAtHead = value; } }
        public bool IsShowPageAtFoot { get; set; }
        public int ColumnCount
        {
            get { return this.Columns.Count; }
        }
        private bool IsDataBinded;
        private bool _isAlignCenter = true;
        public bool IsAlignCenter { get { return _isAlignCenter; } set { _isAlignCenter = value; } }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            subSys = AppContext.Context.CurrentSubSys;
            this.EnableViewState = false;
            base.OnInit(e);
            InitPaper( );
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind( );
        }
        public override void DataBind( )
        {
            if (!IsDataBinded)
            {
                if (AllowPaper)
                    this.UcPaper.FireEvent(null, null);
                AutoBuildShowColumn( );
                BuildDataCellHtml(Tbody);
                BuildHeaderHtml(Theader);
                IsDataBinded = true;
            }
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            DataBind( );
            if (IsDebug)
                writer.Write("<div>{0}</div>", Log);
            if (AllowPaper && this.IsShowPageAtHead)
            {
                UcPaper.IsRenderBottom = false;
                UcPaper.RenderControl(writer);
            }
            string footer = string.Empty;
            if (OnDataBindFooter != null)
                footer = OnDataBindFooter( );
            if (subSys == SubSystem.ALL || subSys == SubSystem.Member)
                writer.Write(GRIDVIEW_TEMPLATE_MEMBER, CssClass);
            else
                writer.Write(IsAlignCenter ? GRIDVIEW_TEMPLATE : GRIDVIEW_TEMPLATE.Replace("align='center'", ""), CssClass);
            if (ShowHeader)
            {
                writer.Write("<thead>");
                writer.Write(Theader);
                writer.Write("</thead>");
            }
            writer.Write("<tbody>");
            writer.Write(Tbody);
            writer.Write("</tbody>");
            if (!string.IsNullOrEmpty(footer))
            {
                writer.Write("<tfoot>");
                writer.Write(footer);
                writer.Write("</foot>");
            }
            writer.Write("</table>");
            if (AllowPaper && IsShowPageAtFoot)
            {
                UcPaper.IsRenderBottom = true;
                UcPaper.IsRenderHead = false;
                UcPaper.RenderControl(writer);
            }
            base.RenderControl(writer);
        }
        public string GetRenderResult( )
        {
            StringBuilder sb = new StringBuilder( );
            using (System.IO.StringWriter sw = new System.IO.StringWriter(sb))
            {
                using (HtmlTextWriter writer = new HtmlTextWriter(sw))
                {
                    this.RenderControl(writer);
                }
            }
            return sb.ToString( );
        }

        private TableSchema.TableColumn GetColumn(string column)
        {
            SqlQuery sqlQuerySource = SqlQuerySource;
            if (sqlQuerySource == null)
                return null;
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
                foreach (TableSchema.Table table in sqlQuerySource.FromTables)
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

        protected virtual void BuildHeaderHtml(StringBuilder sbHeader)
        {
            if (DataSource == null || !ShowHeader)
                goto lbl_return;
            string caption;
            bool isRenderedCell;
            for (int i = 1; i <= RowColSpan; i++)
            {
                foreach (KeyValuePair<string, object> item in Columns)
                {
                    isRenderedCell = false;
                    TableSchema.TableColumn column = null;
                    if ((column = item.Value as TableSchema.TableColumn) != null)
                        caption = ResBLL.GetColumnRes(column);
                    else
                        caption = Utilities.ToHTML(item.Value);
                    if (OnDataBindHeader != null)
                        OnDataBindHeader(item.Key, ref caption, ref isRenderedCell);
                    sbHeader.AppendLine(isRenderedCell ? caption : string.Format(DATA_CELL_TEMPLATE, caption));
                }
                if (i == Data.Rows.Count)
                    break;
            }
        lbl_return:
            return;
        }
        protected virtual void BuildDataCellHtml(StringBuilder sbDataCell)
        {
            if (DataSource == null)
                goto lbl_return;
            DataTable dataTable = Data;
            if (dataTable == null)
                goto lbl_return;
            int rem;
            int rowCount = dataTable.Rows.Count;
            StringBuilder rowData = new StringBuilder( );
            if (RowColSpan > 1)
            {
                sbDataCell.Append(IsAlignCenter ? DATA_ROW_BEGIN_TEMPLATE : DATA_ROW_BEGIN_TEMPLATE.Replace("align='middle'", ""));
            }
            string rowTemplate;
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = dataTable.Rows[i];
                bool isRenderedCell = false;
                bool isRenderedRow = false;
                string cellHtml = string.Empty;
                rowTemplate = IsAlignCenter ? DATA_ROW_TEMPLATE : DATA_ROW_TEMPLATE.Replace("align='middle'", "");
                if (OnDataBindRow != null)
                    rowTemplate = OnDataBindRow(i, row, ref isRenderedRow);
                if (isRenderedRow)
                    goto lblSkipRenderCell;
                foreach (KeyValuePair<string, object> item in Columns)
                {
                    TableSchema.TableColumn column = null;
                    if ((column = item.Value as TableSchema.TableColumn) != null && dataTable.Columns.Contains(column.ColumnName))
                    {
                        cellHtml = ProcessColumnFmat(column.ColumnName, row, ref isRenderedCell);
                        rowData.Append(isRenderedCell ? cellHtml : string.Format(DATA_CELL_TEMPLATE, cellHtml));
                    }
                    else
                    {
                        cellHtml = ProcessColumnFmat(item.Key, row, ref isRenderedCell);
                        rowData.Append(isRenderedCell ? cellHtml : string.Format(DATA_CELL_TEMPLATE, cellHtml));
                    }
                }
            lblSkipRenderCell:
                if (RowColSpan > 1)
                {
                    Math.DivRem(i + 1, RowColSpan, out rem);
                    if (rem == 0 && i + 1 != rowCount)
                        rowData.Append(IsAlignCenter ? ROWCOLSPAN : ROWCOLSPAN.Replace("align='middle'", ""));
                    if (i + 1 == rowCount && rem > 0)
                    {
                        for (int k = 0; k < (RowColSpan - rem) * Columns.Count; k++)
                        {

                            rowData.AppendFormat(DATA_CELL_TEMPLATE, "&nbsp;");
                        }
                    }
                    sbDataCell.Append(rowData);
                }
                else
                {
                    sbDataCell.AppendFormat(rowTemplate, rowData);
                }
                rowData.Remove(0, rowData.Length);
            }
            if (RowColSpan > 1)
                sbDataCell.Append(DATA_ROW_END_TEMPLATE);
        lbl_return:
            return;
        }
        private DataTable GetDataTable( )
        {
            return GetDataTable(PageIndex, PageSize);
        }
        private DataTable GetDataTable(int pageIndex, int pageSize)
        {
            SqlQuery sqlQuerySource = SqlQuerySource;
            if (sqlQuerySource != null)
            {
                sqlQuerySource = sqlQuerySource.Paged(pageIndex, pageSize);
                //log support
                if (IsDebug)
                    Log.AppendLine(sqlQuerySource.ToString( ));
                return sqlQuerySource.ExecuteDataTable( );
            }
            Query querySource = QuerySource;
            if (querySource != null)
            {
                return querySource.ExecuteDataTable( );
            }
            DataTable dt = TableSource;
            if (dt != null)
                return dt;
            string sqlSource = SqlSource;
            if (!string.IsNullOrEmpty(sqlSource))
            {
                QueryCommand cmd = new QueryCommand(sqlSource, DataService.Provider.Name);
                return DataService.GetDataTable(cmd);
            }
            return null;
        }
        protected string ProcessColumnFmat(string column, DataRow rowData, ref bool isRenderedCell)
        {
            isRenderedCell = false;
            if (OnDataBindColumn != null)
                return OnDataBindColumn(column, rowData, ref isRenderedCell);
            if (!ColFmat.ContainsKey(column))
            {
                if (rowData.Table.Columns.Contains(column))
                    return Utilities.ToHTML(rowData[column]);
                else
                    return string.Empty;
            }
            ColumnFormat fmat = ColFmat[column];
            string[] paramCol = fmat.ValuesStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (paramCol != null && paramCol.Length > 0)
            {
                List<string> lstParamVal = new List<string>( );
                foreach (string p in paramCol)
                {
                    lstParamVal.Add(Utilities.ToHTML(rowData[p]));
                }
                return string.Format(fmat.FormatStr, lstParamVal.ToArray( ));
            }
            else
                return fmat.FormatStr;

        }

        public UcGridView AddShowColumn(TableSchema.TableColumn column)
        {
            if (column == null)
                throw new ArgumentException("column not exist!");
            if (Columns.ContainsKey(column.ColumnName))
                throw new ArgumentException(string.Format("column: {0} has exist!", column.ColumnName));
            this.Columns.Add(column.ColumnName, column);
            return this;
        }
        public UcGridView AddShowColumn(params TableSchema.TableColumn[] columns)
        {
            if (columns != null && columns.Length > 0)
            {
                foreach (TableSchema.TableColumn column in columns)
                {
                    AddShowColumn(column);
                }
            }
            return this;
        }
        public UcGridView AddShowColumn(string column)
        {
            if (this.Columns.ContainsKey(column))
                throw new ArgumentException(string.Format("column: {0} has exist!", column));
            return AddShowColumn(GetColumn(column));
        }
        public UcGridView AddShowColumn(params string[] columns)
        {
            if (columns != null || columns.Length > 0)
            {
                foreach (string column in columns)
                {
                    AddShowColumn(column);
                }
            }
            return this;
        }
        public UcGridView AddCustomColumn(string column, string lblText)
        {
            if (Columns.ContainsKey(column))
                throw new ArgumentException(string.Format("column: {0} has exist!", column));
            Columns.Add(column, lblText);
            return this;
        }
        public UcGridView AddColumnFmat(string column, string fmatStr, string paramsStr)
        {
            if (!this.ColFmat.ContainsKey(column))
                this.ColFmat.Add(column, new ColumnFormat { FormatStr = fmatStr, ValuesStr = string.IsNullOrEmpty(paramsStr) ? string.Empty : paramsStr });
            return this;
        }
        public UcGridView ClearColumnFmat(string column)
        {
            if (this.ColFmat.ContainsKey(column))
                this.ColFmat.Remove(column);
            return this;
        }
        private void AutoBuildShowColumn( )
        {
            SqlQuery querySource = this.SqlQuerySource;
            char[] splitChar = new char[] { ' ' };
            if (Columns.Count == 0 && querySource != null)
            {
                if (querySource.SelectColumnList == null || querySource.SelectColumnList.Length == 0)
                {
                    foreach (TableSchema.Table table in querySource.FromTables)
                    {
                        AddShowColumn(table.Columns.ToArray( ));
                    }
                }
                else
                {
                    foreach (string column in querySource.SelectColumnList)
                    {
                        TableSchema.TableColumn col = GetColumn(column);
                        if (col != null)
                            AddShowColumn(col);
                        else
                        {
                            string[] colArr = column.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
                            string strCol = colArr[colArr.Length - 1].Trim( );
                            AddCustomColumn(strCol, strCol);
                        }
                    }
                }
            }
            AutoBuildShowColumnFromDataTable( );
        }
        private void AutoBuildShowColumnFromDataTable( )
        {
            if (Columns.Count == 0 && Data != null)
            {
                foreach (DataColumn col in Data.Columns)
                {
                    AddCustomColumn(col.ColumnName, col.ColumnName);
                }
            }
        }
        private class ColumnFormat
        {
            public string FormatStr { get; set; }
            public string ValuesStr { get; set; }
        }
    }
}