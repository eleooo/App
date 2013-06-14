using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public delegate int GetTotalRecordHandler( );
    public partial class UcGridPaper : UserControlBase
    {
        const string PAGE_PREV_NEXT_LINK = "<a href=\"javascript:__doPostBack('{0}','')\">{1}</a>";
        const string PAGE_LINK = "<a href=\"javascript:__doPostBack('ddlPage',{0});\">{0}</a>";
        const string PAGE_LINK_CUR = "<a class=\"current\" href=\"javascript:__doPostBack('ddlPage',{0});\">{0}</a>";

        public event EventHandler OnPageIndexChange;
        public event EventHandler OnPageSizeChange;
        public event GetTotalRecordHandler OnGetTotalRecord;
        private bool isRenderPageIndex = false;
        public bool IsRenderHead
        {
            get { return paperHeader.Visible; }
            set { paperHeader.Visible = value; }
        }
        public bool IsRenderBottom
        {
            get { return paperBottom.Visible; }
            set { paperBottom.Visible = value; }
        }


        private bool _eventFired = false;

        public void FireEvent(object sender, EventArgs e)
        {
            string argment = HttpContext.Current.Request.Params[System.Web.UI.Page.postEventSourceID];
            if (IsPostBack && !_eventFired && !string.IsNullOrEmpty(argment))
            {
                if (argment.EndsWith("LinkFirst"))
                    LinkFirst_Click(sender, e);
                else if (argment.EndsWith("LinkPrev"))
                    LinkPrev_Click(sender, e);
                else if (argment.EndsWith("LinkNext"))
                    LinkNext_Click(sender, e);
                else if (argment.EndsWith("LinkLast"))
                {
                    LinkLast_Click(sender, e);
                }
                else if (argment.EndsWith("ddlPage"))
                    ddlPage_SelectedIndexChanged(sender, e);
                else if (argment.EndsWith("cboPage"))
                    cboPage_SelectedIndexChanged(sender, e);
                _eventFired = true;
            }
        }
        private string IndexID
        {
            get
            {
                return txtPageIndex == null ? "txtPageIndex" : txtPageIndex.UniqueID;
            }
        }
        private void InitializeComponent( )
        {
            //this.ddlPage.SelectedIndexChanged += new EventHandler(this.ddlPage_SelectedIndexChanged);
            //this.cboPage.SelectedIndexChanged += new EventHandler(this.cboPage_SelectedIndexChanged);
            string val = Request.Params[IndexID];
            if (txtPageIndex != null && !string.IsNullOrEmpty(val) && SubSonic.Sugar.Numbers.IsInteger(val))
                this.txtPageIndex.Value = val;
            val = Request.Params[cboPage.UniqueID];
            if (!string.IsNullOrEmpty(val) && SubSonic.Sugar.Numbers.IsInteger(val))
                this.cboPage.SelectedValue = val;
            this.LinkFirst.Text = ResBLL.GetRes("paper_first", "第一页", "第一页");
            this.LinkPrev.Text = ResBLL.GetRes("paper_prev", "上一页", "上一页");
            this.LinkNext.Text = ResBLL.GetRes("paper_next", "下一页", "下一页");
            this.LinkLast.Text = ResBLL.GetRes("paper_last", "最后一页", "最后一页");
            //FireEvent(this, EventArgs.Empty);
            
        }
        private void cboPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(cboPage.Text);
            this.PageIndex = 1;
            if (OnPageSizeChange != null)
                OnPageSizeChange(sender, e);
        }

        private void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageIndex = Convert.ToInt32(Request.Params[System.Web.UI.Page.postEventArgumentID]);
            if (OnPageIndexChange != null)
                OnPageIndexChange(sender, e);
        }

        private void RenderLink( )
        {
            //this.lblCount.Text = string.Format(ResBLL.GetRes("paper_format", "共 {0} 行,共 {1} 页, 第 {2} 页.", "分页显示格式"), TotalRecord, PageCount, PageIndex);
            this.LinkFirst.Enabled = EnableLinkFirst;
            this.LinkNext.Enabled = EnableLinkNext;
            this.LinkPrev.Enabled = EnableLinkPrev;
            this.LinkLast.Enabled = EnableLinkLast;
        }

        private void LinkFirst_Click(object sender, EventArgs e)
        {
            this.PageIndex = 1;
            if (OnPageIndexChange != null)
                OnPageIndexChange(sender, e);
        }

        private void LinkLast_Click(object sender, EventArgs e)
        {
            this.PageIndex = PageCount;
            if (OnPageIndexChange != null)
                OnPageIndexChange(sender, e);
        }

        private void LinkNext_Click(object sender, EventArgs e)
        {
            this.PageIndex++;
            if (OnPageIndexChange != null)
                OnPageIndexChange(sender, e);
        }

        private void LinkPrev_Click(object sender, EventArgs e)
        {
            this.PageIndex--;
            if (OnPageIndexChange != null)
                OnPageIndexChange(sender, e);
        }

        protected override void OnInit(EventArgs e)
        {
            paperBottom.SetRenderMethodDelegate(new RenderMethod(RenderBottomMethod));
            lblCount.SetRenderMethodDelegate(new RenderMethod(RenderPageInfoMethod));
            this.InitializeComponent( );
            base.OnInit(e);
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            RenderLink( );
            if (!isRenderPageIndex)
                txtPageIndex.Visible = true;
            else
                txtPageIndex.Visible = false;
            base.RenderControl(writer);
            isRenderPageIndex = true;
        }
        private void RenderPageInfoMethod(HtmlTextWriter output, Control container)
        {
            output.Write(ResBLL.GetRes("paper_format", "共 {0} 行,共 {1} 页, 第 {2} 页.", "分页显示格式"), TotalRecord, PageCount, PageIndex);
        }
        private void RenderBottomMethod(HtmlTextWriter output, Control container)
        {
            if (!IsRenderBottom)
                return;
            //render page fist
            output.Write(PAGE_PREV_NEXT_LINK, LinkPrev.UniqueID, LinkPrev.Text);
            //render page index
            for (int i = 1; i <= PageCount; i++)
            {
                if (i == PageIndex)
                    output.Write(PAGE_LINK_CUR, i);
                else
                    output.Write(PAGE_LINK, i);
            }
            //render page prev
            output.Write(PAGE_PREV_NEXT_LINK, LinkNext.UniqueID, LinkNext.Text);
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

        public string PageSizeID
        {
            get
            {
                return this.cboPage.ClientID;
            }
        }
        public string PageIndexID
        {
            get
            {
                return txtPageIndex.ClientID;
            }
        }
        public int PageCount
        {
            get
            {
                int result;
                int pageCount = Math.DivRem(TotalRecord, PageSize, out result);
                if (result > 0)
                    pageCount++;
                return pageCount;
            }
        }
        public int PageIndex
        {
            get
            {
                int index = Convert.ToInt32(txtPageIndex.Value);
                if (index <= 0)
                {
                    index = 1;
                    txtPageIndex.Value = Utilities.ToHTML(index);
                }
                return index;
            }
            set
            {
                txtPageIndex.Value = Utilities.ToHTML(value);
            }
        }
        public int PageSize
        {
            get
            {
                if (string.IsNullOrEmpty(this.cboPage.Text))
                    return 10;
                else
                    return Convert.ToInt32(this.cboPage.Text);
            }
            set
            {
                bool bFound = false;
                string v = value.ToString( );
                foreach (ListItem item in cboPage.Items)
                {
                    if (Convert.ToInt32(item.Value) == value)
                    {
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                    cboPage.Items.Add(v);
                this.cboPage.Text = v;
            }
        }
        private int _totalRecord;
        public int TotalRecord
        {
            get
            {
                if (_totalRecord == 0 && OnGetTotalRecord != null)
                    _totalRecord = OnGetTotalRecord( );
                return _totalRecord;
            }
            set
            {
                _totalRecord = value;
            }
        }
        public bool IsVisiblePageSize
        {
            get
            {
                return cboPage.Visible;
            }
            set
            {
                cboPage.Visible = value;
                lblPageSize.Visible = value;
            }
        }
    }
}