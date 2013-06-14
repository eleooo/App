using System;
using System.Collections.Generic;
using System.Text;

namespace Eleooo.Common
{
    public class CompanyRate
    {
        public decimal Rate { get; set; }
        public override string ToString( )
        {
            return Rate.ToString("####0.####") + "%";
        }
    }
    public class CompanyRateCollection : List<CompanyRate>
    {
        public bool Add(decimal rate)
        {
            if (Contains(rate))
                return false;
            else
            {
                base.Add(new CompanyRate { Rate = rate });
                return true;
            }
        }

        public bool Contains(decimal rate)
        {
            return Find(rate) != null;
        }
        public CompanyRate Find(decimal rate)
        {
            CompanyRate item = this.Find((CompanyRate match) =>
            {
                return match.Rate == rate;
            });
            return item;
        }
        public int FindIndex(decimal rate)
        {
            return FindIndex((CompanyRate match) =>
            {
                return match.Rate == rate;
            });
        }
        public int IndexOf(decimal rate)
        {
            return FindIndex(rate);
        }
        public System.Web.UI.WebControls.ListItem[] ToListItem( )
        {
            List<System.Web.UI.WebControls.ListItem> itemColls = new List<System.Web.UI.WebControls.ListItem>( );
            foreach (var rate in this)
            {
                itemColls.Add(new System.Web.UI.WebControls.ListItem(rate.ToString( ), Convert.ToString(rate.Rate)));
            }
            return itemColls.ToArray( );
        }
    }
}
