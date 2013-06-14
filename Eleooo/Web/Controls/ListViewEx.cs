using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Security.Permissions;
using System.Data;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    [ToolboxData("<{0}:ListViewEx runat=server></{0}:ListViewEx>")]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class ListViewEx : WebControl
    {
        #region field
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
        private bool IsDataBinded = false;
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
        private bool IsDebug
        {
            get
            {
                return Page.Request != null && Page.Request.Params["Debug"] == "1";
            }
        }
        private bool _isShowPageAtHead = true;
        private IDictionary<ListViewExTemplateType, ListViewExTemplate> _templaes;
        private IDictionary<ListViewExTemplateType, ListViewExTemplate> Templates
        {
            get
            {
                if (_templaes == null)
                {
                    _templaes = new Dictionary<ListViewExTemplateType, ListViewExTemplate>( );
                    for (int i = 0; i < Enum.GetNames(typeof(ListViewExTemplateType)).Length; i++)
                    {
                        _templaes.Add((ListViewExTemplateType)i, GetTemplateInstance( ));
                    }
                }
                return _templaes;
            }
        }

        private Dictionary<string, object> _columns;
        private Dictionary<string, object> Columns
        {
            get
            {
                if (_columns == null)
                    _columns = new Dictionary<string, object>( );
                return _columns;
            }
        }
        private StringBuilder _result;
        private StringBuilder Result
        {
            get
            {
                if (_result == null)
                    _result = new StringBuilder( );
                return _result;
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
        #endregion

        #region properties
        public event BuildListViewFooterHander OnBuildFooter;
        public event BuildListViewHeaderHandler OnBuildHeader;
        public event BuildListViewRowHandler OnBuildRow;
        public event BuildListViewItemHander OnBuildItem;
        public bool IsShowPageAtHead { get { return _isShowPageAtHead; } set { _isShowPageAtHead = value; } }
        public bool IsShowPageAtFoot { get; set; }
        public int ColumnCount
        {
            get { return this.Columns.Count; }
        }
        private bool _showHeader = true;
        [DefaultValue(true)]
        public bool IsShowHeader { get { return _showHeader; } set { _showHeader = value; } }
        public bool AllowPaper { get; set; }
        private int _rowColSpan = 1;
        public int RowColSpan { get { return _rowColSpan; } set { if (value > 0) _rowColSpan = value; } }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int AlternatingRow { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public object DataSource { get; set; }

        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewBeginTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewBeginTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewBeginTemplate].Template = value;
            }
        }
        [Browsable(false), DefaultValue((string)null), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewEndTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewEndTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewEndTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewRowTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewRowTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewRowTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewAlternatingRowTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewAlternatingRowTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewAlternatingRowTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewFooterTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewFooterTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewFooterTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewHeaderTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewHeaderTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewHeaderTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewItemTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewItemTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewItemTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewHeaderBeginTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewHeaderBeginTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewHeaderBeginTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewHeaderEndTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewHeaderEndTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewHeaderEndTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewRowBeginTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewRowBeginTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewRowBeginTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewRowEndTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewRowEndTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewRowEndTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewAlternatingRowBeginTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewAlternatingRowBeginTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewAlternatingRowBeginTemplate].Template = value;
            }
        }
        [Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(ListViewExTemplate))]
        public ITemplate ViewAlternatingRowEndTemplate
        {
            get
            {
                return Templates[ListViewExTemplateType.ViewAlternatingRowEndTemplate].Template;
            }
            set
            {
                Templates[ListViewExTemplateType.ViewAlternatingRowEndTemplate].Template = value;
            }
        }
        #endregion

        #region Paper support
        public event EventHandler OnPageIndexChange;
        public event EventHandler OnPageSizeChange;
        private UcGridPaper _ucPaper;
        protected UcGridPaper UcPaper
        {
            get
            {
                if (AllowPaper && _ucPaper == null)
                    InitPaper( );
                return _ucPaper;
            }
        }
        protected void InitPaper( )
        {
            if (AllowPaper && _ucPaper==null)
            {
                _ucPaper = (UcGridPaper)(AppContext.Page.LoadControl("~/Controls/UcGridPaper.ascx"));
                if (_pageSize > 0)
                    UcPaper.PageSize = _pageSize;
                UcPaper.CallPrivateMethod<object>("OnInit", EventArgs.Empty);
                UcPaper.OnPageIndexChange += new EventHandler(_ucPaper_OnPageIndexChange);
                UcPaper.OnPageSizeChange += new EventHandler(_ucPaper_OnPageSizeChange);
                UcPaper.OnGetTotalRecord += new GetTotalRecordHandler(_ucPaper_OnGetTotalRecord);
                UcPaper.IsVisiblePageSize = IsShowPageAtFoot || IsShowPageAtHead;
                UcPaper.FireEvent(null, EventArgs.Empty);
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

        #region private method
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
        private ListViewExTemplate GetTemplateInstance( )
        {
            if (this.Page != null)
                return (ListViewExTemplate)(Page.LoadControl(typeof(ListViewExTemplate), null));
            else
                return new ListViewExTemplate( );
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
        private void BuildBeginView(StringBuilder sb)
        {
            sb.Append(Templates[ListViewExTemplateType.ViewBeginTemplate]);
        }
        private void BuildEndView(StringBuilder sb)
        {
            sb.Append(Templates[ListViewExTemplateType.ViewEndTemplate]);
        }
        private void BuildHeader(StringBuilder sb)
        {
            if (!IsShowHeader)
                return;
            string caption;
            string headTemplate = Templates[ListViewExTemplateType.ViewHeaderTemplate].Html;
            sb.Append(Templates[ListViewExTemplateType.ViewHeaderBeginTemplate]);
            for (int i = 1; i <= RowColSpan; i++)
            {
                foreach (KeyValuePair<string, object> item in Columns)
                {
                    TableSchema.TableColumn column = null;
                    if ((column = item.Value as TableSchema.TableColumn) != null)
                        caption = ResBLL.GetColumnRes(column);
                    else
                        caption = Utilities.ToHTML(item.Value);
                    if (OnBuildHeader != null)
                        OnBuildHeader(item.Key, ref caption);
                    sb.AppendLine(string.IsNullOrEmpty(headTemplate) ? caption : string.Format(headTemplate, caption));
                }
                if (i == Data.Rows.Count)
                    break;
            }
            sb.Append(Templates[ListViewExTemplateType.ViewHeaderEndTemplate]);
        }
        private void BuildDataRow(StringBuilder sb)
        {
            if (DataSource == null)
                return;
            DataTable dataTable = Data;
            if (dataTable == null)
                return;
            int rowCount = dataTable.Rows.Count;
            int totalRow = GetTotalRowCount(rowCount);
            string rowTemplate = Templates[ListViewExTemplateType.ViewRowTemplate].Html;
            string alterRowTemplate = Templates[ListViewExTemplateType.ViewAlternatingRowTemplate].Html;
            string itemTemplate = Templates[ListViewExTemplateType.ViewItemTemplate].Html;
            string rowBeginTemplate = Templates[ListViewExTemplateType.ViewRowBeginTemplate].Html;
            string rowEndTemplate = Templates[ListViewExTemplateType.ViewAlternatingRowEndTemplate].Html;
            string alterRowBeginTemplate = Templates[ListViewExTemplateType.ViewAlternatingRowBeginTemplate].Html;
            string alterRowEndTemplate = Templates[ListViewExTemplateType.ViewRowEndTemplate].Html;
            int actRow = 0;
            bool isAlterRow;
            for (int i = 0; i < totalRow; i++)
            {
                isAlterRow = IsAlternatingRow(i);
                sb.Append(isAlterRow ? alterRowBeginTemplate : rowBeginTemplate);
                for (int j = 0; j < RowColSpan; j++)
                {
                    DataRow row = actRow < rowCount ? Data.Rows[actRow] : null;
                    if (row != null)
                    {
                        if (OnBuildRow != null)
                            OnBuildRow(actRow, row,isAlterRow, isAlterRow ? alterRowTemplate : rowTemplate, sb);
                        else
                        {
                            foreach (KeyValuePair<string, object> item in Columns)
                            {
                                TableSchema.TableColumn column = item.Value as TableSchema.TableColumn;
                                string columnName = column == null ? item.Key : column.ColumnName;
                                if (OnBuildItem != null)
                                    OnBuildItem(actRow, row, columnName, itemTemplate, sb);
                                else
                                {
                                    string value = dataTable.Columns.Contains(column.ColumnName) ? Utilities.ToHTML(row[columnName]) : string.Empty;
                                    if (!string.IsNullOrEmpty(itemTemplate))
                                        sb.AppendFormat(itemTemplate, value);
                                    else
                                        sb.Append(value);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<string, object> item in Columns)
                        {
                            if (!string.IsNullOrEmpty(itemTemplate))
                                sb.AppendFormat(itemTemplate, string.Empty);
                            else
                                sb.Append(string.Empty);
                        }
                    }
                    actRow++;
                }
                sb.Append(isAlterRow ? rowEndTemplate : alterRowEndTemplate);
            }
        }
        private void BuildFooter(StringBuilder sb)
        {
            string footerTemplate = Templates[ListViewExTemplateType.ViewFooterTemplate].Html;
            if (OnBuildFooter != null)
                OnBuildFooter(footerTemplate, sb);
            else
                sb.Append(footerTemplate);
        }
        private bool IsAlternatingRow(int index)
        {
            if (AlternatingRow <= 1)
                return false;
            int n;
            Math.DivRem(index + 1, AlternatingRow, out n);
            return n == 0;
        }
        private int GetTotalRowCount(int rowCount)
        {
            int n;
            int r = Math.DivRem(rowCount, RowColSpan, out n);
            if (n > 0)
                return r + 1;
            else
                return r;
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            subSys = AppContext.Context.CurrentSubSys;
            this.EnableViewState = false;
            InitPaper( );
            base.OnInit(e);
        }
        protected override void RenderContents(HtmlTextWriter writer)
        {
            DataBind( );
            if (IsDebug)
                writer.Write("<div>{0}</div>", Log);
            if (AllowPaper && this.IsShowPageAtHead)
            {
                UcPaper.IsRenderBottom = false;
                UcPaper.RenderControl(writer);
            }
            writer.Write(Result);
            if (AllowPaper && IsShowPageAtFoot)
            {
                UcPaper.IsRenderBottom = true;
                UcPaper.IsRenderHead = false;
                UcPaper.RenderControl(writer);
            }            
            base.RenderContents(writer);
        }
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            
        }
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            
        }
        public override void DataBind( )
        {
            if (!IsDataBinded)
            {
                if (AllowPaper)
                    this.UcPaper.FireEvent(null, null);
                AutoBuildShowColumn( );
                BuildBeginView(Result);
                BuildHeader(Result);
                BuildDataRow(Result);
                BuildFooter(Result);
                BuildEndView(Result);
                IsDataBinded = true;
            }
        }
        #region public method
        public ListViewEx AddShowColumn(TableSchema.TableColumn column)
        {
            if (column == null)
                throw new ArgumentException("column not exist!");
            if (Columns.ContainsKey(column.ColumnName))
                throw new ArgumentException(string.Format("column: {0} has exist!", column.ColumnName));
            this.Columns.Add(column.ColumnName, column);
            return this;
        }
        public ListViewEx AddShowColumn(params TableSchema.TableColumn[] columns)
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
        public ListViewEx AddShowColumn(string column)
        {
            if (this.Columns.ContainsKey(column))
                throw new ArgumentException(string.Format("column: {0} has exist!", column));
            return AddShowColumn(GetColumn(column));
        }
        public ListViewEx AddShowColumn(params string[] columns)
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
        public ListViewEx AddCustomColumn(string column, string lblText)
        {
            if (Columns.ContainsKey(column))
                throw new ArgumentException(string.Format("column: {0} has exist!", column));
            Columns.Add(column, lblText);
            return this;
        }
        #endregion

        enum ListViewExTemplateType
        {
            ViewBeginTemplate,
            ViewEndTemplate,
            ViewHeaderBeginTemplate,
            ViewHeaderTemplate,
            ViewHeaderEndTemplate,
            ViewRowBeginTemplate,
            ViewRowTemplate,
            ViewRowEndTemplate,
            ViewAlternatingRowBeginTemplate,
            ViewAlternatingRowTemplate,
            ViewAlternatingRowEndTemplate,
            ViewFooterTemplate,
            ViewItemTemplate
        }
    }
}
