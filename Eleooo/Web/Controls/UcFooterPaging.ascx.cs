using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eleooo.Web.Controls
{
    public partial class UcFooterPaging : UserControlBase, IPagingControl
    {
        const string PAGE_PREV_NEXT_LINK = "<a href=\"javascript:__doPostBack('{0}','')\">{1}</a>";
        const string PAGE_LINK = "<a href=\"javascript:__doPostBack('{0}',{1});\">{1}</a>";
        const string PAGE_LINK_CUR = "<a class=\"current\" href=\"javascript:__doPostBack('{0}',{1});\">{1}</a>";
        const string LinkPrevCommandName = "LinkPrev_UcFooterPaging";
        const string LinkNextCommandName = "LinkNext_UcFooterPaging";
        const string PageIndexCommandName = "PageIndex_UcFooterPaging";

        private string GetLinkPrevCommandName( )
        {
            if (GetControlPrefix != null)
                return GetControlPrefix( ) + "_" + LinkPrevCommandName;
            else
                return LinkPrevCommandName;
        }
        private string GetLinkNextCommandName( )
        {
            if (GetControlPrefix != null)
                return GetControlPrefix( ) + "_" + LinkNextCommandName;
            else
                return LinkNextCommandName;
        }
        private string GetPageIndexCommandName( )
        {
            if (GetControlPrefix != null)
                return GetControlPrefix( ) + "_" + PageIndexCommandName;
            else
                return PageIndexCommandName;
        }

        protected override void OnInit(EventArgs e)
        {
            paperBottom.SetRenderMethodDelegate(new RenderMethod(RenderBottomMethod));
            base.OnInit(e);
        }
        private void RenderBottomMethod(HtmlTextWriter output, Control container)
        {
            //render page fist
            output.Write(PAGE_PREV_NEXT_LINK, GetLinkPrevCommandName( ), ResBLL.GetRes("paper_prev", "上一页", "上一页"));
            //render page index
            for (int i = 1; i <= PageCount; i++)
            {
                if (i == PageIndex)
                    output.Write(PAGE_LINK_CUR, GetPageIndexCommandName( ), i);
                else
                    output.Write(PAGE_LINK, GetPageIndexCommandName( ), i);
            }
            //render page prev
            output.Write(PAGE_PREV_NEXT_LINK, GetLinkNextCommandName( ), ResBLL.GetRes("paper_next", "下一页", "下一页"));
        }

        void OnFirePageEventHandler(object sender, EventArgs e)
        {
            string argment = HttpContext.Current.Request.Params[System.Web.UI.Page.postEventSourceID];
            if (IsPostBack && !string.IsNullOrEmpty(argment))
            {
                if (argment.EndsWith(GetLinkPrevCommandName( )))
                    PageIndex--;
                else if (argment.EndsWith(GetLinkNextCommandName( )))
                    PageIndex++;
                else if (argment.EndsWith(GetPageIndexCommandName( )))
                    PageIndex = Convert.ToInt32(Request.Params[System.Web.UI.Page.postEventArgumentID]);
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