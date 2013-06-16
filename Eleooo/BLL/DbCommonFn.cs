using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eleooo.DAL;
using SubSonic;

namespace Eleooo.BLL
{
    public static class DbCommonFn
    {
        private static readonly string __FnCompare = "[dbo].[Compare]";
        private static readonly string __FnGetUserOrderInfo = "[dbo].[GetUserOrderInfo]";

        public static string FnCompare(string constraint, SubSonic.TableSchema.TableColumn columnA, string valueB, object eqVal)
        {
            return string.Concat(constraint, '(', __FnCompare, '(', columnA.QualifiedName, ',', '\'', valueB, '\'', ')', '=', eqVal, ')');
        }
        public static string RenderFnGetUserOrderInfoAsColumn(SubSonic.TableSchema.TableColumn userCol,
                                                              SubSonic.TableSchema.TableColumn companyCol,
                                                              SubSonic.TableSchema.TableColumn dateCol,
                                                              string dataFormat, string column)
        {
            return string.Concat(__FnGetUserOrderInfo, '(', userCol.QualifiedName, ',', companyCol.QualifiedName, ',', dateCol.QualifiedName, ',', '\'', dataFormat, '\'', ") AS ", column);
        }
    }
}
