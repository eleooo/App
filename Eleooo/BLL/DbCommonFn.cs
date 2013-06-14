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

        public static string FnCompare(string constraint, SubSonic.TableSchema.TableColumn columnA, string valueB, object eqVal)
        {
            return string.Concat(constraint, '(', __FnCompare, '(', columnA.QualifiedName, ',', '\'', valueB, '\'', ')', '=', eqVal, ')');
        }
    }
}
