using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Eleooo.DAL;

namespace Eleooo.Client
{
    public class CashBLL
    {
        private DataTable _userInfo;
        public DataTable UserInfo
        {
            get
            {
                if (_userInfo == null)
                {
                    _userInfo = new DataTable( );
                    _userInfo.Columns.Add("key");
                    _userInfo.Columns.Add("Value");
                }
                return _userInfo;
            }
        }
        private CashEntity _cashData;
        public CashEntity CashData
        {
            get
            {
                if (_cashData == null)
                    _cashData = new CashEntity( );
                return _cashData;
            }
        }
        public SysMember CashUser
        {
            get { return CashData.CashUser; }
            set { CashData.CashUser = value; }
        }
        public void InintCash()
        {
            UserInfo.Rows.Clear( );
            CashData.InitCashData( );
        }
        public bool GetCashUserByPhone(string phoneNum, out string message)
        {
            message = string.Empty;
            try
            {
                SysMember user;
                DataTable dtUserInfo;
                bool bRet = ServiceProvider.Service.GetMemberForCash(0, phoneNum, out user, out dtUserInfo, out message);
                if (_userInfo != null)
                    _userInfo = dtUserInfo;
                CashUser = user;
                return bRet;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        public SysMemberCash GetUserLatestCash(out string message)
        {
            return GetUserLatestCash(CashUser.Id, AppContext.Company.Id, out message);
        }
        public SysMemberCash GetUserLatestCash(int userID, int companyID,out string message)
        {
            var query = DB.Select( ).Top("1").From<SysMemberCash>( )
                                               .Where(SysMemberCash.CashMemberIDColumn).IsEqualTo(userID)
                                               .And(SysMemberCash.CashCompanyIDColumn).IsEqualTo(companyID)
                                               .And(SysMemberCash.CashSumColumn).IsGreaterThanOrEqualTo(0)
                                               .OrderDesc(SysMemberCash.Columns.CashID);
            try
            {
                message = string.Empty;
                return ServiceProvider.Service.ExecuteSingle<SysMemberCash>(query);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
        }
        public int SaveCash(SysMember user, SysMemberCash cash, out string message)
        {
            message = string.Empty;
            try
            {
                if (cash.CashSum <= 0)
                {
                    message = "充值金额不能小于零";
                    return -1;
                }
                if (cash.CashPoint == null)
                    cash.CashPoint = 0;
                if (cash.CashRate == null)
                    cash.CashRate = 0;
                return ServiceProvider.Service.SaveCash(cash, user, out message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return -1;
            }
        }
    }
}
