using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Data;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcOrderMealSelectCompany : UserControlBase
    {
        private int? _mansionId;
        protected int MansionId
        {
            get
            {
                if (!_mansionId.HasValue)
                    _mansionId = Utilities.ToInt(Request["MansionId"]);
                return _mansionId.Value;
            }
        }
        private SysAreaMansion _mansion;
        protected SysAreaMansion Mansion
        {
            get
            {
                if (_mansion == null)
                    _mansion = SysAreaMansion.FetchByID(MansionId);
                return _mansion;
            }
        }
        char _cbJF, _cbMyCompany, _cbCompany;

        void InitChceckBox( )
        {
            _cbCompany = _cbJF = _cbMyCompany = '0';
            if (!string.IsNullOrEmpty(Request.Form["cbjf"]) && Request.Form["cbjf"][0] >= '1' && Request.Form["cbjf"][0] <= '2')
                _cbJF = Request.Form["cbjf"][0];
            if (!string.IsNullOrEmpty(Request.Form["cbMyCompany"]) && Request.Form["cbMyCompany"][0] >= '1' && Request.Form["cbMyCompany"][0] <= '2')
                _cbMyCompany = Request.Form["cbMyCompany"][0];
            if (!string.IsNullOrEmpty(Request.Form["cbCompany"]) && Request.Form["cbCompany"][0] >= '1' && Request.Form["cbCompany"][0] <= '2')
                _cbCompany = Request.Form["cbCompany"][0];
        }
        protected void RenderCheckBox(int cb, char val)
        {
            if (cb == 0 && val == _cbJF)
                goto lbl_checked;
            else if (cb == 1 && val == _cbMyCompany)
                goto lbl_checked;
            else if (cb == 2 && val == _cbCompany)
                goto lbl_checked;
            return;
        lbl_checked:
            Response.Output.Write("checked=\"checked\"");
        }
        protected string BindCompanyInfo( )
        {
            bool isSetTop = !Utilities.IsNull(Eval("SetTopDate"));
            bool isPoint = Utilities.ToBool(Eval("IsPoint"));
            bool isOnSale = Utilities.ToBool(Eval("IsOnSale"));
            string result = isSetTop ? "<span class=\"rec\"></span>" : string.Empty;
            result += Formatter.Join(isOnSale ? string.Empty : null, "<span class=\"cu", isPoint ? string.Empty : " big", "\">促</span>");
            result += Formatter.Join(isPoint ? string.Empty : null, "<span class=\"ji", isOnSale ? string.Empty : " big2", "\">积</span>");
            return result;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitChceckBox( );
            if (Mansion == null)
            {
                Utilities.Redirect("/Public/OrderMealPage.aspx");
                return;
            }
            var isLogin = AppContext.Context.CurrentSubSys != SubSystem.ALL;
            //if (!IsPostBack)
            {
                rpFavCompany.EmptyDataIsShowHeaderAndFooterTemplate = _cbMyCompany != '0';
                rpJf.EmptyDataIsShowHeaderAndFooterTemplate = _cbJF != '0';
                rpRmCompany.EmptyDataIsShowHeaderAndFooterTemplate = _cbCompany != '0';
                BindData(isLogin);
            }
        }
        private void BindData(bool isLogin)
        {
            List<int> favCmps = null;
            if (isLogin)
            {
                favCmps = UserBLL.GetUserFavCompany(AppContext.Context.User.Id);
                if (favCmps.Count > 0)
                {
                    rpFavCompany.DataSource = GetUserFavCompany(favCmps, Mansion, _cbMyCompany);
                    rpFavCompany.DataBind( );
                }
            }

            rpJf.DataSource = GetMealCompanyItems(isLogin, Mansion, _cbJF);
            rpJf.DataBind( );

            rpRmCompany.DataSource = GetMealCompany(Mansion, favCmps, _cbCompany);
            rpRmCompany.DataBind( );
        }

        private static DataTable GetMealCompany(SysAreaMansion mansion, List<int> favCmps, char cbCompany)
        {
            var query = DB.Select(Utilities.GetTableColumns(SysCompany.Schema)).Top("200")
                          .From<SysCompany>( )
                          .InnerJoin(SysCompanyMansion.CompanyIDColumn, SysCompany.IdColumn)
                          .Where(SysCompany.CompanyTypeColumn).IsEqualTo((int)CompanyType.MealCompany)
                          .And(SysCompany.CompanyStatusColumn).IsEqualTo(1)
                          .And(SysCompanyMansion.MansionIDColumn).IsEqualTo(mansion.Id)
                          .OrderDesc(SysCompany.Columns.SetTopDate, SysCompany.Columns.IsOnSale, SysCompany.Columns.IsPoint);
            if (favCmps != null && favCmps.Count > 0)
                query.And(SysCompany.IdColumn).NotIn(favCmps);
            if (cbCompany == '1') //营业中
                query.ConstraintExpression(CompanyBLL.FuncCheckIsWorkingTime(1));
            else if (cbCompany == '2')
                query.AndEx(SysCompany.ServiceSumColumn).IsEqualTo(0)
                     .Or(SysCompany.ServiceSumColumn).IsNull( )
                     .CloseEx( );
            return query.ExecuteDataTable( );
        }
        private static DataTable GetMealCompanyItems(bool isLogin, SysAreaMansion mansion, char cbJf)
        {
            var userLastOrderSum = UserBLL.GetUserAvgOrderSum(AppContext.Context.User.Id);
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyItem.Schema), CompanyBLL.FuncIsWorkingTime( ))
                //.Top("3")
                          .From<SysCompanyItem>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyItem.CompanyIDColumn)
                          .InnerJoin(SysCompanyMansion.CompanyIDColumn, SysCompany.IdColumn)
                          .Where(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyItem.ItemEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                          .And(SysCompanyItem.IsPassColumn).IsEqualTo(true)
                          .And(SysCompany.CompanyStatusColumn).IsEqualTo(1)
                          .And(SysCompany.CompanyTypeColumn).IsEqualTo((int)CompanyType.MealCompany)
                          .And(SysCompanyMansion.MansionIDColumn).IsEqualTo(mansion.Id)
                          .And(SysCompanyItem.ItemStatusColumn).IsEqualTo(1)
                //.ConstraintExpression(string.Format(" AND({0} - {1} > 0) ", Utilities.GetTableColumn(SysCompanyItem.ItemAmountColumn), Utilities.GetTableColumn(SysCompanyItem.ItemClickedColumn)))
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyItem.AreaDepthColumn, mansion.AreaDepth, true), 1)
                          .OrderDesc(Utilities.GetTableColumn(SysCompanyItem.ItemIDColumn));
            //if (isLogin)
            //    query.ConstraintExpression(CompanyItemBLL.RenderCheckItemFuncInVals(AppContext.Context.User.Id, userLastOrderSum, "1", "-20","-19"));
            if (cbJf == '1') //营业中
                query.ConstraintExpression(CompanyBLL.FuncCheckIsWorkingTime(1));
            else if (cbJf == '2')
                query.AndEx(SysCompany.ServiceSumColumn).IsEqualTo(0)
                     .Or(SysCompany.ServiceSumColumn).IsNull( )
                     .CloseEx( );

            //Logging.Log("test->GetMealCompanyItems", query.ToString( ), string.Empty);
            return query.ExecuteDataTable( );
            //using (var dr = query.ExecuteReader( ))
            //{
            //    while (dr.Read( ))
            //    {
            //        SysCompanyItem item = new SysCompanyItem( );
            //        item.Load(dr);
            //        yield return item;
            //    }
            //}
        }
        internal static DataTable GetUserFavCompany(List<int> favCmps, SysAreaMansion mansion, char cbMyCompany)
        {
            var query = DB.Select(Utilities.GetTableColumns(SysCompany.Schema))
                          .From<SysCompany>( )
                          .InnerJoin(SysCompanyMansion.CompanyIDColumn, SysCompany.IdColumn)
                          .Where(SysCompany.CompanyTypeColumn).IsEqualTo((int)CompanyType.MealCompany)
                          .And(SysCompany.CompanyStatusColumn).IsEqualTo(1)
                          .And(SysCompanyMansion.MansionIDColumn).IsEqualTo(mansion.Id)
                          .And(SysCompany.IdColumn).In(favCmps)
                          .OrderDesc(SysCompany.Columns.SetTopDate, SysCompany.Columns.IsOnSale, SysCompany.Columns.IsPoint);
            if (cbMyCompany == '1') //营业中
                query.ConstraintExpression(CompanyBLL.FuncCheckIsWorkingTime(1));
            else if (cbMyCompany == '2') //免费送餐
                query.AndEx(SysCompany.ServiceSumColumn).IsEqualTo(0)
                     .Or(SysCompany.ServiceSumColumn).IsNull( )
                     .CloseEx( );
            //using (var dr = query.ExecuteReader( ))
            //{
            //    while (dr.Read( ))
            //    {
            //        SysCompany cmp = new SysCompany( );
            //        cmp.Load(dr);
            //        yield return cmp;
            //    }
            //}
            return query.ExecuteDataTable( );

        }
        public override void On_ActionDelete( )
        {
            if (AppContext.Context.CurrentSubSys != SubSystem.ALL)
            {
                int companyId = Utilities.ToInt(BasePage.EVENTARGUMENT);
                UserBLL.RemoveUserFavCompany(AppContext.Context.User.Id, companyId);
                BindData(true);
            }
            else
                BindData(false);
        }
    }
}