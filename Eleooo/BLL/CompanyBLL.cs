using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class CompanyBLL
    {
        #region _HotCompanyQuery
        private static readonly string __CompanyOrderRankingQuery = @"
select
  *
from 
  (
    select
      ROW_NUMBER()
      over(order by count(*) desc)
      as RowNum,
      t2.ID,
      count(*)
      as Amount
    from
      orders
      as t1,
      sys_company as t2
    where
      t1.OrderSellerID= t2.ID
    and
      t1.OrderStatus <> 5
    and
      t2.CompanyLocation= @CompanyArea
    group by
      t2.ID
  ) t WHERE t.ID=@ID;";
        #endregion
        private static readonly string __CompanyOrderElapsedRankingQuery = "SELECT RowNum FROM ( SELECT ROW_NUMBER() OVER(order by OrderElapsed ASC ) as RowNum,  ID, OrderElapsed FROM SYS_COMPANY WHERE DATALENGTH( OrderElapsed) > 0) t WHERE t.ID = @ID";
        private const string __FuncCheckIsWorkingTime = "([dbo].[CheckIsWorkingTime]({0},{1})={2})";
        private const string __FuncIsWorkingTime = "[dbo].[CheckIsWorkingTime]({0},{1}) as IsWorkingTime";
        public const string IS_OWNER = "Is_Owner";
        public static decimal MaxPointLimit
        {
            get
            {
                return Utilities.ToDecimal(ResBLL.GetRes("MaxPointLimit", "499", "最大累计赠送积分限制!"));
            }
        }

        public static object GetCompanyInfo(SysCompany company)
        {
            int ranking1, ranking2;
            int amount;
            GetCompanyOrderRankingInfo(company, out ranking1, out amount);
            ranking2 = GetCompanyOrderElapsedRankingInfo(company);
            var result = new
            {
                OrderElapsed = company.OrderElapsed,
                Ranking2 = ranking2,
                CompanyWorkTime = company.CompanyWorkTime,
                OnSetSum = company.OnSetSum,
                IsSuspend = company.IsSuspend,
                Ranking1 = ranking1,
                Amount = amount,
                Area = company.CompanyLocation
            };
            return result;
        }

        public static void GetCompanyOrderRankingInfo(SysCompany company, out int ranking, out int amount)
        {
            ranking = amount = 0;
            QueryCommand cmd = new QueryCommand(__CompanyOrderRankingQuery);
            cmd.AddParameter("@ID", company.Id, System.Data.DbType.Int32);
            cmd.AddParameter("@CompanyArea", company.CompanyLocation, System.Data.DbType.String);
            using (var dr = DataService.GetReader(cmd))
            {
                if (dr.Read( ))
                {
                    ranking = Utilities.ToInt(dr["RowNum"]);
                    amount = Utilities.ToInt(dr["Amount"]);
                }
            }
        }
        public static int GetCompanyOrderElapsedRankingInfo(SysCompany company)
        {
            QueryCommand cmd = new QueryCommand(__CompanyOrderElapsedRankingQuery);
            cmd.AddParameter("@ID", company.Id, System.Data.DbType.Int32);
            return Utilities.ToInt(DataService.ExecuteScalar(cmd));
        }

        public static string FuncIsWorkingTime( )
        {
            return string.Format(__FuncIsWorkingTime, SysCompany.Columns.CompanyWorkTime, SysCompany.Columns.CompanyType);
        }
        public static bool CheckIsWorkingTime(string workTime, int companyType)
        {
            string vSql = string.Format("Select " + __FuncIsWorkingTime, "'" + workTime + "'", companyType);
            var r = DataService.ExecuteScalar(new QueryCommand(vSql));
            return Convert.ToInt32(r) == 1;
        }
        public static string FuncCheckIsWorkingTime(int eqVal)
        {
            return FuncCheckIsWorkingTime(SysCompany.Columns.CompanyWorkTime, SysCompany.Columns.CompanyType, eqVal);
        }
        public static string FuncCheckIsWorkingTime(string columnName, string companyTypeCol, int eqVal)
        {
            return string.Format("And " + __FuncCheckIsWorkingTime, columnName, companyTypeCol, eqVal);
        }
        public static string GetCompanyName(int companyID)
        {
            return DB.Select(SysCompany.CompanyNameColumn.ColumnName).From(SysCompany.Schema.TableName)
                     .Where(SysCompany.IdColumn).IsEqualTo(companyID).ExecuteScalar<string>( );
        }
        public static CompanyType GetCompanyTypeById(int companyId)
        {
            var type = DB.Select(SysCompany.CompanyTypeColumn.ColumnName).From(SysCompany.Schema.TableName)
                       .Where(SysCompany.IdColumn).IsEqualTo(companyId).ExecuteScalar<int>( );
            return (CompanyType)type;
        }
        public static CompanyType GetCompanyType(int? typeId)
        {
            if (!typeId.HasValue)
                return CompanyType.UnionCompany;
            else
                return (CompanyType)typeId.Value;
        }
        public static string GetCompanyTypeAsCol(SysCompany company)
        {
            return string.Format("{0} as {1}", company.CompanyType, SysCompany.Columns.CompanyType);
        }
        public static void FlushCompanyAds( )
        {
            if (!FlushCompanyFunc(AppContextBase.Context.Company, CompanyType.SpecialCompany))
                FlushCompanyFunc(AppContextBase.Context.Company, CompanyType.MealCompany);
        }
        public static void FlushCompanyItem( )
        {
            if (!FlushCompanyFunc(AppContextBase.Context.Company, CompanyType.AdCompany))
                FlushCompanyFunc(AppContextBase.Context.Company, CompanyType.MealCompany);
        }
        public static void FlushCompanyUnion( )
        {
            if (!FlushCompanyFunc(AppContextBase.Context.Company, CompanyType.AdCompany) &&
                FlushCompanyFunc(AppContextBase.Context.Company, CompanyType.MealCompany))
                FlushCompanyFunc(AppContextBase.Context.Company, CompanyType.SpecialCompany);
        }
        public static bool FlushCompanyFunc(SysCompany company, CompanyType notAllowType)
        {
            CompanyType companyType = Formatter.ToEnum<CompanyType>(company.CompanyType);
            if (notAllowType == companyType)
            {
                HttpContext.Current.Response.Write("商家类型不允许使用此功能");
                AppContextBase.Page.Visible = false;
                //AppContextBase.Page.Response.Flush( );
                //AppContextBase.Page.Response.End( );
                return true;
            }
            return false;
        }
        public static string RenderUserPhone(string userID, string companyID, string phone, string userCompanyID, bool isAsCol = true)
        {
            if (isAsCol)
                return string.Format("dbo.RenderUserPhone({0},{1},{2},{3}) as MemberPhoneNumber",
                                      userID, companyID, phone, userCompanyID);
            else
                return string.Format("dbo.RenderUserPhone({0},{1},{2},{3})",
                      userID, companyID, phone, userCompanyID);
        }

        public static string RenderUserPhone(SubSonic.TableSchema.TableColumn userID,
                                             SubSonic.TableSchema.TableColumn companyID,
                                             SubSonic.TableSchema.TableColumn phone,
                                             SubSonic.TableSchema.TableColumn memberCompanyID, bool isAsCol = true)
        {
            return RenderUserPhone(Utilities.GetTableColumn(userID), Utilities.GetTableColumn(companyID), Utilities.GetTableColumn(phone), Utilities.GetTableColumn(memberCompanyID), isAsCol);
        }

        public static string RenderUserPhone(string phone)
        {
            return string.Format("dbo.EnUserPhone({0}) as MemberPhoneNumber", phone);
        }
        public static string RenderUserPhone(SubSonic.TableSchema.TableColumn phone)
        {
            return RenderUserPhone(Utilities.GetTableColumn(phone));
        }

        public static string RenderIsOwner(string userID, string companyID, MemberFilter filter)
        {
            if (filter == MemberFilter.Outer)
                return string.Format(" And (dbo.IsOwnerMember({0},{1}) = 0) ", userID, companyID);
            else if (filter == MemberFilter.Owner)
                return string.Format(" And (dbo.IsOwnerMember({0},{1}) = 1) ", userID, companyID);
            else
                return " And (1=1) ";
        }
        public static string RenderIsOwner(SubSonic.TableSchema.TableColumn userID, SubSonic.TableSchema.TableColumn companyID, MemberFilter filter)
        {
            return RenderIsOwner(Utilities.GetTableColumn(userID), Utilities.GetTableColumn(companyID), filter);
        }
        public static string RenderIsOwner(string userID, string companyID)
        {
            return string.Format("dbo.IsOwnerMember({0},{1}) as {2}", userID, companyID, IS_OWNER);
        }
        public static string RenderIsOwner(SubSonic.TableSchema.TableColumn userID, SubSonic.TableSchema.TableColumn companyID)
        {
            return RenderIsOwner(Utilities.GetTableColumn(userID), Utilities.GetTableColumn(companyID));
        }
        public static string RenderIsOwner(string value)
        {
            return string.Format("{0} as {1}", value, IS_OWNER);
        }

        public static string ReplacePhoneToName(string source, string phoneNum, string name)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            source = source.Replace("-", "").Replace("[]", "");
            if (!phoneNum.EndsWith("**"))
                return source.Replace(phoneNum, name);
            phoneNum = phoneNum.Substring(0, phoneNum.Length - 2);
            int len = source.IndexOf(phoneNum) + phoneNum.Length;
            return source.Remove(len, 2).Replace(phoneNum, name);
        }

        //-2 is company account, -1 not exist, 0 is my user, >0 not my user and return user id
        public static int CheckIsOwnerUserOrExist(string phoneNum, int companyID)
        {
            SysMember user = DB.Select( ).From<SysMember>( )
                               .Where(SysMember.MemberPhoneNumberColumn).IsEqualTo(phoneNum)
                               .ExecuteSingle<SysMember>( );

            if (user == null)
                return -1;
            else if (user.CompanyId.HasValue && user.CompanyId.Value > 0)
                return -2;
            int n = DB.Select("Count(*)").From<VSysMember>( )
                      .Where(VSysMember.MemberCompanyIDColumn).IsEqualTo(companyID)
                      .And(VSysMember.MemberPhoneNumberColumn).IsEqualTo(phoneNum)
                      .ExecuteScalar<int>( );
            if (n > 0)
                return 0;
            else
                return user.Id;
        }
        public static int CheckIsOwnerUserOrExist(int userId, int companyID)
        {
            SysMember user = SysMember.FetchByID(userId);
            if (user != null && user.CompanyId.Value > 0)
                return -2;
            else if (user == null)
                return -1;
            else
                return 0;
            //int n = DB.Select("Count(*)").From<VSysMember>( )
            //          .Where(VSysMember.MemberCompanyIDColumn).IsEqualTo(companyID)
            //          .And(VSysMember.IdColumn).IsEqualTo(userId)
            //          .ExecuteScalar<int>( );
            //if (n > 0)
            //    return 0;
            //else
            //    return user.Id;
        }
        public static bool CheckIsOwnerUser(int userID, int companyID)
        {
            int n = DB.Select("Count(*)").From<VSysMember>( )
                      .Where(VSysMember.MemberCompanyIDColumn).IsEqualTo(companyID)
                      .And(VSysMember.IdColumn).IsEqualTo(userID)
                      .ExecuteScalar<int>( );
            return n > 0;
        }
        public static void UpdateUserPhone(string newPhone, string oldPhone)
        {
            var updator = SP_.UpdatePhoneNum(oldPhone, newPhone);
            updator.Execute( );
        }
        public static string EnUserPhoe(string phoneNum)
        {
            return System.Text.RegularExpressions.Regex.Replace(phoneNum, @"(\d{3})\d+(\d{2}$)", "$1******$2");
            //return string.Format("{0}**", phoneNum.Substring(0, phoneNum.Length - 2));
        }
        public static SysCompany GetCompanyByTel(string tel)
        {
            if (string.IsNullOrEmpty(tel))
                return null;
            return DB.Select( ).From<SysCompany>( )
                     .Where(SysCompany.CompanyTelColumn).IsEqualTo(tel).ExecuteSingle<SysCompany>( );
        }

        public static int GetCompanyIdByTel(string tel, CompanyType? companyType = null)
        {
            var query = DB.Select(SysCompany.IdColumn.QualifiedName).From<SysCompany>( )
                          .Where(SysCompany.CompanyTelColumn).IsEqualTo(tel);
            if (companyType.HasValue)
                query.And(SysCompany.CompanyTypeColumn).IsEqualTo((int)companyType.Value);
            return query.ExecuteScalar<int>( );
        }


        public static bool IsMaxPointLevel(int companyID, decimal point)
        {
            string vSql = string.Format("SELECT SUM(PAYMENTSUM) FROM PAYMENT WHERE PAYMENTCOMPANYID = {0} AND  paymentStatus=1", companyID);
            QueryCommand cmd = new QueryCommand(vSql);
            decimal? p = Utilities.ToDecimal(DataService.ExecuteScalar(cmd));
            if ((MaxPointLimit > 0 && p.HasValue &&
                (Math.Abs(p.Value) + point) > (MaxPointLimit + 50)) ||
                Math.Abs(p.Value) > (MaxPointLimit + 0.1M))
            {
                return true;
            }
            return false;
        }
        //积分比例
        public static decimal GetCompanyDefaultRate(SysCompany company)
        {
            var rates = GetCompanyRates(company);
            if (rates.Count > 0)
            {
                return rates[0].Rate / 100;
            }
            return 0;
        }

        public static CompanyRateCollection GetCompanyRates(SysCompany company)
        {
            CompanyRateCollection _companyRates = new CompanyRateCollection( );
            string sCompanyRate = string.IsNullOrEmpty(company.CompanyRate) ? string.Empty : company.CompanyRate.Replace(" ", string.Empty);
            decimal d = 0;
            foreach (string rate in sCompanyRate.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (decimal.TryParse(rate, out d) && d >= 0)
                {
                    _companyRates.Add(d);
                }
            }
            return _companyRates;
        }
    }

}