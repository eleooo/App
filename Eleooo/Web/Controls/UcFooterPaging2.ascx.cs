using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eleooo.Web.Controls
{
    public partial class UcFooterPaging2 : UserControlBase, IPagingControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected const int MaxPagingLimit = 10;
        protected int GetStartIndex( )
        {
            var index = PageIndex - (PageIndex - 1) % MaxPagingLimit;
            if (index + MaxPagingLimit > PageCount && PageCount > MaxPagingLimit)
                return PageCount - MaxPagingLimit;
            else
                return index;
        }
        protected int GetEndIndex( )
        {
            var index = GetStartIndex( ) + MaxPagingLimit;
            if (index > PageCount)
                return PageCount;
            else
                return index;
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
            get { return null; }
        }

        #endregion
    }
}