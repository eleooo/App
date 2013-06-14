using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eleooo.Web.Controls
{
    public partial class UcFaceBookCompany : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int good, bad, normal;
            FaceBookBLL.GetOrderMealRateCount(CompanyID, out good, out normal, out bad);
            FbGoodCount = good;
            FbBadCount = bad;
            FbNormalCount = normal;
        }
        public int CompanyID { get; set; }
        public int FbGoodCount { get; set; }
        public int FbNormalCount { get; set; }
        public int FbBadCount { get; set; }
    }
}