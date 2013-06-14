using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using SubSonic;
using System.Data;

namespace Eleooo.Web.Controls
{
    [Designer("System.Web.UI.Design.WebControls.DataListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Editor("System.Web.UI.Design.WebControls.DataListComponentEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(ComponentEditor)), ControlValueProperty("SelectedValue")]
    [ToolboxData("<{0}:DataListExt runat=server></{0}:DataListExt>")]
    public class DataListExt : Repeater
    {
        #region fields
        private const string ItemCountViewStateKey = "_!ItemCount";
        private const string PageIndexKeyPrefix = "_PageIndex";
        private static readonly Type _rpType = typeof(Repeater);
        private ArrayList _itemsArray;
        private bool _pagingEventFired;
        private bool _ensuredPagingControl;
        private int? _pageIndex;
        private int? _totalRecord;
        private int _pageSize;
        private IPagingControl _headerPagingControl;
        private IPagingControl _footerPagingControl;
        private Dictionary<string, string> _headerDict;
        private StringBuilder _sbLog;
        private ITemplate _emptyDataTemplate;
        #endregion

        #region methods
        public DataListExt( )
        {
            EnableViewState = false;
            _pagingEventFired = false;
            _pageSize = 10;
            AllowPaging = true;
            EmptyDataIsShowHeaderAndFooterTemplate = true;
        }
        private void EnsurePagingControl( )
        {
            if (AllowPaging && !_ensuredPagingControl)
            {
                if (ShowHeadPaging)
                {
                    if (string.IsNullOrEmpty(HeaderPagingTemplate))
                        HeaderPagingTemplate = "~/Controls/UcHeaderPaging.ascx";
                    _headerPagingControl = Page.LoadControl(HeaderPagingTemplate) as IPagingControl;
                    if (_headerPagingControl != null)
                    {
                        _headerPagingControl.CallPrivateMethod<object>("OnInit", EventArgs.Empty);
                        _headerPagingControl.GetControlPrefix = GetControlPrefix;
                        _headerPagingControl.GetCurrentPage = GetCurrentPage;
                        _headerPagingControl.GetPageCount = GetPageCount;
                        _headerPagingControl.GetPageSize = GetPageSize;
                        _headerPagingControl.GetTotalRecord = GetTotalRecord;
                        _headerPagingControl.OnPageIndexChange = OnPageIndexChange;
                        _headerPagingControl.OnPageSizeChange = OnPageSizeChange;
                    }
                }
                if (ShowFootPaging)
                {
                    if (string.IsNullOrEmpty(FooterPagingTemplate))
                        FooterPagingTemplate = "~/Controls/UcFooterPaging.ascx";
                    _footerPagingControl = Page.LoadControl(FooterPagingTemplate) as IPagingControl;
                    if (_footerPagingControl != null)
                    {
                        _footerPagingControl.CallPrivateMethod<object>("OnInit", EventArgs.Empty);
                        _footerPagingControl.GetControlPrefix = GetControlPrefix;
                        _footerPagingControl.GetCurrentPage = GetCurrentPage;
                        _footerPagingControl.GetPageCount = GetPageCount;
                        _footerPagingControl.GetPageSize = GetPageSize;
                        _footerPagingControl.GetTotalRecord = GetTotalRecord;
                        _footerPagingControl.OnPageIndexChange = OnPageIndexChange;
                        _footerPagingControl.OnPageSizeChange = OnPageSizeChange;
                    }
                }
                _ensuredPagingControl = true;
            }
        }
        protected override void CreateChildControls( )
        {
            this.Controls.Clear( );
            if (this.ViewState["_!ItemCount"] != null)
            {
                this.CreateControlHierarchy(false);
            }
            base.ClearChildViewState( );
        }
        protected override void CreateControlHierarchy(bool useDataSource)
        {
            IEnumerable data = null;
            int dataItemCount = -1;
            this.itemsArray.Clear( );
            if (!useDataSource)
            {
                dataItemCount = (int)this.ViewState[ItemCountViewStateKey];
                if (dataItemCount != -1)
                {
                    data = new DummyDataSource(dataItemCount);
                    this.itemsArray.Capacity = dataItemCount;
                }
            }
            else
            {
                data = this.GetData( );
                ICollection is2 = data as ICollection;
                if (is2 != null)
                {
                    this.itemsArray.Capacity = is2.Count;
                }
            }
            if (data != null)
            {
                int itemIndex = 0;
                bool flag = this.SeparatorTemplate != null;
                dataItemCount = 0;
                if (this.HeaderTemplate != null)
                {
                    this.CreateItem(-1, ListItemType.Header, useDataSource, null);
                }
                foreach (object obj2 in data)
                {
                    if (flag && (dataItemCount > 0))
                    {
                        this.CreateItem(itemIndex - 1, ListItemType.Separator, useDataSource, null);
                    }
                    ListItemType itemType = (((itemIndex + 1) % AlternatingItemIndex) == 0) ? ListItemType.AlternatingItem : ListItemType.Item;
                    RepeaterItem item = this.CreateItem(itemIndex, itemType, useDataSource, obj2);
                    this.itemsArray.Add(item);
                    dataItemCount++;
                    itemIndex++;
                }
                if (this.FooterTemplate != null)
                {
                    this.CreateItem(-1, ListItemType.Footer, useDataSource, null);
                }
            }
            if (useDataSource)
            {
                this.ViewState["_!ItemCount"] = (data != null) ? dataItemCount : -1;
            }

        }
        protected override void OnDataBinding(EventArgs e)
        {
            FirePagingEvent( );
            if (QuerySource != null)
            {
                QuerySource = QuerySource.Paged(PageIndex, PageSize);
                if (IsDebug)
                {
                    _sbLog = new StringBuilder( );
                    _sbLog.AppendLine(QuerySource.ToString( ));
                }
                if (CustomizeFetch != null)
                    base.DataSource = CustomizeFetch(QuerySource);
                else if (FetchType == QueryFetchType.DataReader)
                    base.DataSource = QuerySource.GetDataReaderEnumerator( );
                else
                    base.DataSource = QuerySource.ExecuteDataTable( );
            }
            base.OnDataBinding(e);
        }
        protected override void OnItemCreated(RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.DataItem = _headerDict;
            }
            base.OnItemCreated(e);
        }
        private RepeaterItem CreateItem(int itemIndex, ListItemType itemType, bool dataBind, object dataItem)
        {
            RepeaterItem item = this.CreateItem(itemIndex, itemType);
            RepeaterItemEventArgs e = new RepeaterItemEventArgs(item);
            this.InitializeItem(item);
            if (dataBind)
            {
                item.DataItem = dataItem;
            }
            this.OnItemCreated(e);
            this.Controls.Add(item);
            if (dataBind)
            {
                item.DataBind( );
                this.OnItemDataBound(e);
                item.DataItem = null;
            }
            return item;
        }
        private void AddHeader(TableSchema.TableColumn column)
        {
            if (column != null)
                AddHeader(column.ColumnName, ResBLL.GetColumnRes(column));
        }
        private void AddHeader(string column, string lblText)
        {
            if (HeaderTemplate == null)
                return;
            if (_headerDict == null)
            {
                _headerDict = new Dictionary<string, string>( );
            }
            _headerDict[column] = lblText;
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            if (itemsArray.Count > 0 || EmptyDataIsShowHeaderAndFooterTemplate)
            {
                if (_headerPagingControl != null && (this.PageIndex > 1 || (PageIndex == 1 && itemsArray.Count >= this.PageSize)))
                    _headerPagingControl.RenderControl(writer);
                base.RenderControl(writer);
                if (_footerPagingControl != null && (this.PageIndex > 1 || (PageIndex == 1 && itemsArray.Count >= this.PageSize)))
                    _footerPagingControl.RenderControl(writer);
                if (AllowPaging)
                {
                    writer.Write("<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\" />", PageIndexControlId, PageIndex);
                }
            }
            else
            {
                this.Controls.Clear( );
                if (_emptyDataTemplate != null)
                {
                    _emptyDataTemplate.InstantiateIn(this);
                    base.RenderControl(writer);
                }
            }
            if (_sbLog != null)
            {
                writer.WriteBeginTag("div");
                writer.Write(_sbLog);
                writer.WriteBeginTag("div");
            }
        }

        public DataListExt AddShowColumn(TableSchema.TableColumn column)
        {
            AddHeader(column);
            return this;
        }
        public DataListExt AddShowColumn(params TableSchema.TableColumn[] columns)
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
        public DataListExt AddShowColumn(string column, string lblText)
        {
            AddHeader(column, lblText);
            return this;
        }

        private void FirePagingEvent( )
        {
            if (!_pagingEventFired)
            {
                EnsurePagingControl( );
                if (_headerPagingControl != null && _headerPagingControl.FirePageEventHandler != null)
                    _headerPagingControl.FirePageEventHandler(this, EventArgs.Empty);
                if (_footerPagingControl != null && _footerPagingControl.FirePageEventHandler != null)
                    _footerPagingControl.FirePageEventHandler(this, EventArgs.Empty);
            }
            _pagingEventFired = true;
        }
        string GetControlPrefix( )
        {
            return this.ID;
        }
        int GetCurrentPage( )
        {
            return PageIndex;
        }
        void OnPageIndexChange(int pageIndex)
        {
            PageIndex = pageIndex;
        }
        void OnPageSizeChange(int pageSize)
        {
            _pageSize = pageSize;
        }
        int GetPageSize( )
        {
            return _pageSize;
        }
        int GetPageCount( )
        {
            int result;
            int pageCount = Math.DivRem(TotalRecord, PageSize, out result);
            if (result > 0)
                pageCount++;
            return pageCount;
        }
        int GetTotalRecord( )
        {
            return TotalRecord;
        }
        #endregion

        #region properteis
        private bool IsDebug
        {
            get
            {
                return Page.Request != null && Page.Request.Params["Debug"] == "1";
            }
        }
        public string PageIndexControlId
        {
            get
            {
                return this.ID + PageIndexKeyPrefix;
            }
        }
        private ArrayList itemsArray
        {
            get
            {
                if (_itemsArray == null)
                {
                    _itemsArray = new ArrayList( );
                    _rpType.SetTypePrivateField(this, "itemsArray", _itemsArray);
                }
                return _itemsArray;
            }
        }
        public int AlternatingItemIndex
        {
            get
            {
                object obj2 = this.ViewState["AlternatingItemIndex"];
                if (obj2 != null)
                {
                    return (int)obj2;
                }
                return 2;
            }
            set
            {
                if (value > 2)
                {
                    this.ViewState["AlternatingItemIndex"] = value;
                    this.OnDataPropertyChanged( );
                }
            }

        }
        public int PageIndex
        {
            get
            {
                if (!_pageIndex.HasValue)
                    _pageIndex = Convert.ToInt32(HttpContext.Current.Request.Params[PageIndexControlId]);
                if (_pageIndex.Value <= 0)
                    _pageIndex = 1;
                return _pageIndex.Value;
            }
            set
            {
                if (value > 0 && (!_pageIndex.HasValue || (_pageIndex.HasValue && _pageIndex.Value != value)))
                {
                    _pageIndex = value;
                }
            }
        }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (_pageSize > 0)
                    _pageSize = value;
            }
        }
        public int TotalRecord
        {
            get
            {
                if (!_totalRecord.HasValue)
                {
                    if (QuerySource == null)
                        _totalRecord = itemsArray.Count;
                    else
                        _totalRecord = QuerySource.Paged(0, 0).GetRecordCount( );
                }
                return _totalRecord.Value;
            }
        }
        public SqlQuery QuerySource { get; set; }
        public Func<SqlQuery, object> CustomizeFetch { get; set; }
        public int ItemCount
        {
            get
            {
                return itemsArray.Count;
            }
        }

        [DefaultValue(0)]
        public QueryFetchType FetchType { get; set; }

        [DefaultValue(true)]
        public bool AllowPaging
        {
            get;
            set;
        }

        [DefaultValue(true)]
        public bool ShowHeadPaging { get; set; }
        [DefaultValue(true)]
        public bool ShowFootPaging { get; set; }

        public bool EmptyDataIsShowHeaderAndFooterTemplate { get; set; }

        [Category("Appearance")]
        [DefaultValue("")]
        [UrlProperty]
        public string HeaderPagingTemplate { get; set; }

        [Category("Appearance")]
        [DefaultValue("")]
        [UrlProperty]
        public string FooterPagingTemplate { get; set; }

        [DefaultValue("")]
        [TemplateContainer(typeof(RepeaterItem))]
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual ITemplate EmptyDataTemplate
        {
            get
            {
                return this._emptyDataTemplate;
            }
            set
            {
                this._emptyDataTemplate = value;
            }
        }

        #endregion
    }
}
