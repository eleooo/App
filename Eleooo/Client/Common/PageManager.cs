using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Eleooo.Client
{
    public class PageManager
    {
        static PageManager( )
        {
            MainMetroForm.Instance.Resize += (sender, e) =>
                {
                    ResetBounds( );
                };
        }
        private static UcHome _home;
        public static UcHome Home
        {
            get
            {
                if (_home == null)
                {
                    _home = new UcHome( );
                    //_home.Load += (sender,e)=>{
                    //    _home.Bounds = GetWorkSpace( );
                    //};
                }
                return _home;
            }
        }

        private static UcOrder _memberOrder;
        public static UcOrder MemberOrder
        {
            get
            {
                if (_memberOrder == null)
                {
                    _memberOrder = new UcOrder( );
                    //_memberOrder.Load += (sender,e)=>{
                    //    _memberOrder.Bounds = GetWorkSpace( );
                    //};
                }
                return _memberOrder;
            }
        }

        private static UcMemberCash _memberCash;
        public static UcMemberCash MemberCash
        {
            get
            {
                if (_memberCash == null)
                {
                    _memberCash = new UcMemberCash( );
                    //_memberCash.Load += (sender, e) =>
                    //{
                    //    _memberCash.Bounds = GetWorkSpace( );
                    //};
                }
                return _memberCash;
            }
        }

        private static UcMemberReg _memberReg;
        public static UcMemberReg MemberReg
        {
            get
            {
                if (_memberReg == null)
                {
                    _memberReg = new UcMemberReg( );
                    //_memberReg.Load += (sender, e) =>
                    //{
                    //    _memberReg.Bounds = GetWorkSpace( );
                    //};
                }
                return _memberReg;
            }
        }

        private static UcSystem _systemCfg;
        public static UcSystem SystemCfg
        {
            get
            {
                if (_systemCfg == null)
                    _systemCfg = new UcSystem( );
                return _systemCfg;
            }
        }


        private static UcCompanyItem _companyItem;
        public static UcCompanyItem CompanyItem
        {
            get
            {
                if (_companyItem == null)
                    _companyItem = new UcCompanyItem( );
                return _companyItem;
            }
        }

        private static UcCompanyAds _companyAds;
        public static UcCompanyAds CompanyAds
        {
            get
            {
                if (_companyAds == null)
                    _companyAds = new UcCompanyAds( );
                return _companyAds;
            }
        }

        public static Rectangle GetWorkSpace( )
        {
            return MainMetroForm.Instance.GetWorkSpace( );
        }
        public static Rectangle GetHomeWorkSpace( )
        {
            return MainMetroForm.Instance.GetHomeWorkSpace( );
        }

        public static void ResetBounds( )
        {
            Rectangle bounds = GetWorkSpace( );
            if (Home.Visible)
                Home.Bounds = GetHomeWorkSpace();
            else
                Home.OpenBounds = GetHomeWorkSpace();
            if (_memberOrder != null)
                _memberOrder.Bounds = bounds;
            if (_memberCash != null)
                _memberCash.Bounds = bounds;
            if (_memberReg != null)
                _memberReg.Bounds = bounds;
            if (_systemCfg != null)
                _systemCfg.Bounds = bounds;
            //if (_pwdInfo != null)
            //    _pwdInfo.Bounds = bounds;
        }
    }
}
