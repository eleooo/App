using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eleooo.Web.Controls
{
    public partial class UcFooterPaging1 : UserControlBase, IPagingControl
    {
        const string LinkPrevCommandName = "LinkPrev_UcFooterPaging";
        const string LinkNextCommandName = "LinkNext_UcFooterPaging";
        const string PageIndexCommandName = "PageIndex_UcFooterPaging";

        protected string GetLinkPrevCommandName( )
        {
            if (GetControlPrefix != null)
                return GetControlPrefix( ) + "_" + LinkPrevCommandName;
            else
                return LinkPrevCommandName;
        }
        protected string GetLinkNextCommandName( )
        {
            if (GetControlPrefix != null)
                return GetControlPrefix( ) + "_" + LinkNextCommandName;
            else
                return LinkNextCommandName;
        }
        protected string GetPageIndexCommandName( )
        {
            if (GetControlPrefix != null)
                return GetControlPrefix( ) + "_" + PageIndexCommandName;
            else
                return PageIndexCommandName;
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