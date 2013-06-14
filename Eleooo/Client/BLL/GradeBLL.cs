using System;
using System.Collections.Generic;
using Eleooo.DAL;

namespace Eleooo.Client
{
    public class GradeBLL
    {
        private static List<SysCompanyMemberGrade> _gradeList;
        public static List<SysCompanyMemberGrade> GradeList
        {
            get
            {
                if (_gradeList == null)
                {
                    _gradeList = GetGradeList(AppContext.Company);
                    if (_gradeList.Count == 0)
                    {
                        _gradeList.Add(new SysCompanyMemberGrade { Id = 0, GradeName = "一般" });
                    }
                }
                return _gradeList;
            }
        }
        public static SysCompanyMemberGrade GetGradeByName(SysCompany company, string gradeName)
        {
            var query = DB.Select( ).From<SysCompanyMemberGrade>( )
                          .Where(SysCompanyMemberGrade.CompanyIDColumn).IsEqualTo(company.Id)
                          .And(SysCompanyMemberGrade.GradeNameColumn).IsEqualTo(gradeName.Trim( ));
            return ServiceProvider.Service.ExecuteSingle<SysCompanyMemberGrade>(query);
        }
        public static SysCompanyMemberGrade GetGradeByID(int id)
        {
            var query = DB.Select( ).From<SysCompanyMemberGrade>( )
                          .Where(SysCompanyMemberGrade.IdColumn).IsEqualTo(id);
            return ServiceProvider.Service.ExecuteSingle<SysCompanyMemberGrade>( query );
        }
        public static List<SysCompanyMemberGrade> GetGradeList(SysCompany company)
        {
            var query = DB.Select( ).From<SysCompanyMemberGrade>( )
                          .Where(SysCompanyMemberGrade.CompanyIDColumn).IsEqualTo(company.Id)
                          .OrderAsc(SysCompanyMemberGrade.Columns.GradeOrder);
            return ServiceProvider.Service.ExecuteAsCollection<SysCompanyMemberGrade>(query);
        }
        public static string DefaultGradeIDStr
        {
            get
            {
                if (GradeList.Count == 0)
                    return string.Empty;
                else
                    return GradeList[0].Id.ToString( );
            }
        }
        public static int DefaultGradeID
        {
            get
            {
                if (GradeList.Count == 0)
                    return 0;
                else
                    return GradeList[0].Id;
            }
        }
        public static string[] GetGradeNames( )
        {
            List<string> nameLst = new List<string>( );
            foreach (SysCompanyMemberGrade grade in GradeList)
                nameLst.Add(grade.GradeName);
            return nameLst.ToArray( );
        }
        public static string GetNameByID(string id)
        {
            string name = string.Empty;
            foreach (SysCompanyMemberGrade grade in GradeList)
            {
                if (grade.Id.ToString( ) == id)
                {
                    name = grade.GradeName;
                    break;
                }
            }
            return name;
        }
        public static int GetIDByName(string name)
        {
            int id = 0;
            foreach (SysCompanyMemberGrade grade in GradeList)
            {
                if (string.Compare(name, grade.GradeName, 0) == 0)
                {
                    id = grade.Id;
                    break;
                }
            }
            return id;
        }
        public static int DefGrade
        {
            get
            {
                return GradeList[0].Id;
            }
        }
    }
}