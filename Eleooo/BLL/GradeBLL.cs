using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;

namespace Eleooo.Web
{
    public class GradeBLL
    {
        public static bool CheckExist(int id)
        {
            return SysCompanyMemberGrade.FetchByID(id) != null;
        }
        public static SysCompanyMemberGrade GetGradeByName(SysCompany company, string gradeName)
        {
            var query = DB.Select( ).From<SysCompanyMemberGrade>( )
                          .Where(SysCompanyMemberGrade.CompanyIDColumn).IsEqualTo(company.Id)
                          .And(SysCompanyMemberGrade.GradeNameColumn).IsEqualTo(gradeName.Trim( ));
            return query.ExecuteSingle<SysCompanyMemberGrade>( );
        }
        public static List<SysCompanyMemberGrade> GetGradeList(SysCompany company)
        {
            var query = DB.Select( ).From<SysCompanyMemberGrade>( )
                          .Where(SysCompanyMemberGrade.CompanyIDColumn).IsEqualTo(company.Id)
                          .OrderAsc(SysCompanyMemberGrade.Columns.GradeOrder);
            return query.ExecuteTypedList<SysCompanyMemberGrade>( );
        }
        public static string GetUserCurrentGrade(int companyID, int userID)
        {
            string cashMemo = DB.Select(SysMemberCash.Columns.CashMemo).Top("1").From<SysMemberCash>( )
                                .Where(SysMemberCash.CashCompanyIDColumn).IsEqualTo(companyID)
                                .And(SysMemberCash.CashMemberIDColumn).IsEqualTo(userID)
                                .And(SysMemberCash.CashSumColumn).IsGreaterThan(0)
                                .OrderDesc(SysMemberCash.Columns.CashID)
                                .ExecuteScalar<string>( );
            if (string.IsNullOrEmpty(cashMemo))
                cashMemo = DB.Select("MemberGrade").Top("1").From<VSysMember>( )
                             .Where(VSysMember.IdColumn).IsEqualTo(userID)
                             .And(VSysMember.MemberCompanyIDColumn).IsEqualTo(companyID)
                             .ExecuteScalar<string>( );
            if (string.IsNullOrEmpty(cashMemo))
                return string.Empty;
            var query = DB.Select(SysCompanyMemberGrade.Columns.GradeName).Top("1").From<SysCompanyMemberGrade>( );
            int gradeID;
            if (int.TryParse(cashMemo, out gradeID) && gradeID > 0)
                query = query.Where(SysCompanyMemberGrade.IdColumn).IsEqualTo(gradeID);
            else
                return cashMemo;
            cashMemo = query.ExecuteScalar<string>( );
            return string.IsNullOrEmpty(cashMemo) ? string.Empty : cashMemo;
        }
    }
}