using System;
using System.Collections.Generic;
using System.Text;

namespace Eleooo.Web.Controls
{
    interface IPagingControl
    {
        Func<string> GetControlPrefix { set; get; }
        Func<int> GetCurrentPage { set; get; }
        Action<int> OnPageIndexChange { set; get; }
        Action<int> OnPageSizeChange { set; get; }
        Func<int> GetTotalRecord { set; get; }
        Func<int> GetPageSize { set; get; }
        Func<int> GetPageCount { set; get; }
        EventHandler FirePageEventHandler { get; }
        void RenderControl(System.Web.UI.HtmlTextWriter writer);
    }
}
