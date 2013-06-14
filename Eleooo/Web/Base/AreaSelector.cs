using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.Web.Controls;
using Eleooo.Common;
using Eleooo.DAL;

namespace Eleooo.Web
{
    public class AreaSelector
    {
        private static readonly Dictionary<string, object> _selector1 = new Dictionary<string, object>( );
        private static readonly Dictionary<string, object> _selector2 = new Dictionary<string, object>( );
        private static readonly Dictionary<string, object> _selector3 = new Dictionary<string, object>( );
        static AreaSelector( )
        {
            _selector1.Add("SelectorCount", (uint)3);
            _selector1.Add("Id_Prefix", "slMulitSelector1_");
            _selector1.Add("SelectorWidth", "100,100,100");

            _selector2.Add("SelectorCount", (uint)3);
            _selector2.Add("Id_Prefix", "slMulitSelector2_");
            _selector2.Add("SelectorWidth", "100,100,100");
            _selector2.Add("IsLoadScript", false);

            _selector3.Add("SelectorCount", (uint)3);
            _selector3.Add("Id_Prefix", "slMulitSelector3_");
            _selector3.Add("SelectorWidth", "100,100,100");
            _selector3.Add("IsLoadScript", false);
            _selector3.Add("IsAllowMulti", true);
            _selector3.Add("IsNeedTextext", true);
        }
        public static MulitSelector Selector1
        {
            get
            {
                return Utilities.GetInstance<MulitSelector>("slMulitSelector1_", _selector1);
            }
        }

        public static MulitSelector Selector2
        {
            get
            {
                return Utilities.GetInstance<MulitSelector>("slMulitSelector2_", _selector2);
            }
        }

        public static MulitSelector Selector3
        {
            get
            {
                return Utilities.GetInstance<MulitSelector>("slMulitSelector3_", _selector3);
            }
        }

        public static string GetSelectedLocation1(int selector = 1)
        {
            return GetSelectedText(0, selector);
        }
        public static string GetSelectedLocation2(int selector = 1)
        {
            return GetSelectedText(1, selector);
        }
        public static string GetSelectedLocation3(int selector = 1)
        {
            return GetSelectedText(2, selector);
        }
        public static string GetSelectedText(int index, int selector = 1)
        {
            int v;
            if (int.TryParse(Selector1.GetSelectedValue(index), out v))
            {
                return DB.Select(SysArea.Columns.AreaName).From<SysArea>( )
                         .Where(SysArea.IdColumn).IsEqualTo(v)
                         .ExecuteScalar<string>( );
            }
            else
                return string.Empty;
        }
        public static string SelectedArea1
        {
            get
            {
                int nVal = 0;
                string sTemp = Selector1.GetSelectedValue(2);
                int.TryParse(sTemp, out nVal);
                if (nVal <= 0)
                {
                    sTemp = Selector1.GetSelectedValue(1);
                    int.TryParse(sTemp, out nVal);
                }
                if (nVal <= 0)
                {
                    sTemp = Selector1.GetSelectedValue(0);
                    int.TryParse(sTemp, out nVal);
                }
                SysArea area = SysArea.FetchByID(nVal);
                if (area == null)
                    return string.Empty;
                else
                    return area.Depth;
            }
        }
        public static string SelectedArea2
        {
            get
            {
                int nVal = 0;
                string sTemp = Selector2.GetSelectedValue(2);
                int.TryParse(sTemp, out nVal);
                if (nVal <= 0)
                {
                    sTemp = Selector2.GetSelectedValue(1);
                    int.TryParse(sTemp, out nVal);
                }
                if (nVal <= 0)
                {
                    sTemp = Selector2.GetSelectedValue(0);
                    int.TryParse(sTemp, out nVal);
                }
                SysArea area = SysArea.FetchByID(nVal);
                if (area == null)
                    return string.Empty;
                else
                    return area.Depth;
            }
        }
        public static string SelectedArea3
        {
            get
            {
                return Selector3.GetMulitValue( );
            }
        }
    }
}