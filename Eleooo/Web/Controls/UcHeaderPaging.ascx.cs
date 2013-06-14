using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eleooo.Web.Controls
{
    public partial class UcHeaderPaging : UserControlBase, IPagingControl
    {
        protected override void OnInit(EventArgs e)
        {
            this.LinkFirst_UcHeaderPaging.Text = ResBLL.GetRes("paper_first", "第一页", "第一页");
            this.LinkPrev_UcHeaderPaging.Text = ResBLL.GetRes("paper_prev", "上一页", "上一页");
            this.LinkNext_UcHeaderPaging.Text = ResBLL.GetRes("paper_next", "下一页", "下一页");
            this.LinkLast_UcHeaderPaging.Text = ResBLL.GetRes("paper_last", "最后一页", "最后一页");
            lblCount.SetRenderMethodDelegate(new RenderMethod(RenderPageInfoMethod));
            base.OnInit(e);
        }
        private string GetCommandID(Control control)
        {
            string prefix = GetControlPrefix != null ? GetControlPrefix( ) : this.ID;
            return prefix + "_" + control.ID;
        }
        private void FixCommandId( )
        {

            this.LinkFirst_UcHeaderPaging.ID = GetCommandID(this.LinkFirst_UcHeaderPaging);
            this.LinkPrev_UcHeaderPaging.ID = GetCommandID(this.LinkPrev_UcHeaderPaging);
            this.LinkNext_UcHeaderPaging.ID = GetCommandID(this.LinkNext_UcHeaderPaging);
            this.LinkLast_UcHeaderPaging.ID = GetCommandID(this.LinkLast_UcHeaderPaging);
            this.cboPage_UcHeaderPaging.ID = GetCommandID(this.cboPage_UcHeaderPaging);
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            this.LinkFirst_UcHeaderPaging.Enabled = EnableLinkFirst;
            this.LinkNext_UcHeaderPaging.Enabled = EnableLinkNext;
            this.LinkPrev_UcHeaderPaging.Enabled = EnableLinkPrev;
            this.LinkLast_UcHeaderPaging.Enabled = EnableLinkLast;
            FixCommandId( );
            base.RenderControl(writer);
        }

        private void RenderPageInfoMethod(HtmlTextWriter output, Control container)
        {
            output.Write(ResBLL.GetRes("paper_format", "共 {0} 行,共 {1} 页, 第 {2} 页.", "分页显示格式"), TotalRecord, PageCount, PageIndex);
        }

        private void LinkFirst_Click(object sender, EventArgs e)
        {
            this.PageIndex = 1;
        }

        private void LinkLast_Click(object sender, EventArgs e)
        {
            this.PageIndex = PageCount;
        }

        private void LinkNext_Click(object sender, EventArgs e)
        {
            this.PageIndex++;
        }

        private void LinkPrev_Click(object sender, EventArgs e)
        {
            this.PageIndex--;
        }

        private void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageIndex = Convert.ToInt32(Request.Params[System.Web.UI.Page.postEventArgumentID]);
        }
        private void ChangePageSize(string vPageSize)
        {
            bool bFound = false;
            foreach (ListItem item in cboPage_UcHeaderPaging.Items)
            {
                item.Selected = false;
                if (item.Value == vPageSize)
                {
                    bFound = true;
                    item.Selected = true;
                    break;
                }
            }
            if (!bFound)
            {
                cboPage_UcHeaderPaging.Items.Add(new ListItem { Text = vPageSize, Value = vPageSize, Selected = true });
                var items = cboPage_UcHeaderPaging.Items.OfType<ListItem>( ).OrderBy(item => item.Value).ToArray( );
                cboPage_UcHeaderPaging.Items.Clear( );
                cboPage_UcHeaderPaging.Items.AddRange(items);
            }
            this.cboPage_UcHeaderPaging.Text = vPageSize;
        }
        void OnFirePageEventHandler(object sender, EventArgs e)
        {
            string argment = HttpContext.Current.Request.Params[System.Web.UI.Page.postEventSourceID];
            if (IsPostBack && !string.IsNullOrEmpty(argment))
            {
                if (argment.EndsWith(GetCommandID(LinkFirst_UcHeaderPaging)))
                    LinkFirst_Click(sender, e);
                else if (argment.EndsWith(GetCommandID(LinkPrev_UcHeaderPaging)))
                    LinkPrev_Click(sender, e);
                else if (argment.EndsWith(GetCommandID(LinkNext_UcHeaderPaging)))
                    LinkNext_Click(sender, e);
                else if (argment.EndsWith(GetCommandID(LinkLast_UcHeaderPaging)))
                    LinkLast_Click(sender, e);
            }
            ChangePageSize(PageSize.ToString( ));
            string vPageSize = HttpContext.Current.Request.Params[GetCommandID(cboPage_UcHeaderPaging)];
            if (!string.IsNullOrEmpty(vPageSize) && OnPageSizeChange != null && PageSize.ToString( ) != vPageSize)
            {
                PageIndex = 1;
                OnPageSizeChange(Convert.ToInt32(vPageSize));
                ChangePageSize(vPageSize);
            }
        }

        private bool EnableLinkFirst
        {
            get
            {
                return PageIndex > 1 && TotalRecord > 0;
            }
        }
        private bool EnableLinkNext
        {
            get
            {
                return PageIndex < PageCount && TotalRecord > 0;
            }
        }
        private bool EnableLinkPrev
        {
            get
            {
                return EnableLinkFirst;
            }
        }
        private bool EnableLinkLast
        {
            get
            {
                return EnableLinkNext;
            }
        }


        public int PageCount
        {
            get
            {
                if (GetPageCount != null)
                    return GetPageCount( );
                else
                    return 0;
            }
        }
        public int PageIndex
        {
            get
            {
                if (GetCurrentPage != null)
                    return GetCurrentPage( );
                else
                    return 1;
            }
            set
            {
                if (OnPageIndexChange != null)
                    OnPageIndexChange(value);
            }
        }
        public int PageSize
        {
            get
            {
                if (GetPageSize != null)
                    return GetPageSize( );
                else
                    return 10;
            }
        }
        public int TotalRecord
        {
            get
            {
                if (GetTotalRecord != null)
                    return GetTotalRecord( );
                else
                    return 0;
            }
        }

        #region IPagingControl 成员

        public Func<string> GetControlPrefix { get; set; }

        public Func<int> GetCurrentPage { get; set; }

        public Action<int> OnPageIndexChange { get; set; }

        public Action<int> OnPageSizeChange { get; set; }

        public Func<int> GetTotalRecord { get; set; }

        public Func<int> GetPageSize { get; set; }

        public Func<int> GetPageCount { get; set; }

        public EventHandler FirePageEventHandler
        {
            get { return OnFirePageEventHandler; }
        }

        #endregion
    }
}