using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Eleooo.DAL;

namespace Eleooo.Web.Controls
{
    public class GradeHtmlSelect
    {
        const string SELECT_BEGIN_TPL = "<select id='{0}' name='{0}'>";
        const string SELECT_END_TPL = "</select>";
        const string SELECT_OPTION = "<option value='{0}' {1} >{2}</option>";
        const string SELECT_SELECTED = "selected='selected'";
        public static string GetHtmlSelect(int companyID, string ctrlName, object selectedValue)
        {
            StringBuilder sb = new StringBuilder( );
            sb.AppendFormat(SELECT_BEGIN_TPL, ctrlName);
            int id = Convert.ToInt32(selectedValue);
            SysCompanyMemberGradeCollection grades = DB.Select( ).From<SysCompanyMemberGrade>( )
                                                        .Where(SysCompanyMemberGrade.CompanyIDColumn).IsEqualTo(companyID)
                                                        .OrderAsc(SysCompanyMemberGrade.Columns.GradeOrder)
                                                        .ExecuteAsCollection<SysCompanyMemberGradeCollection>( );
            if (grades.Count == 0)
            {
                grades.Add(new SysCompanyMemberGrade { Id = 0, GradeName = "一般" });
            }
            foreach (SysCompanyMemberGrade grade in grades)
            {
                sb.AppendFormat(SELECT_OPTION, grade.Id, grade.Id == id ? SELECT_SELECTED : string.Empty, grade.GradeName);
            }
            sb.Append(SELECT_END_TPL);
            return sb.ToString( );
        }
    }
}