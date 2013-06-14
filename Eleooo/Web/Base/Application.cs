using System;
using System.Collections.Generic;
using System.Text;
using SubSonic;
using Eleooo.Common;
using Eleooo.Web.Controls;
using Eleooo.DAL;

namespace Eleooo.Web
{
    public class App
    {
        private static readonly object __orderQueryLocker = new object( );

        public static void SetOrderMealQueryView(UcGridView gridView, string txtCompany, string txtMember, int rbStatus, int model, string beginDate, string endDate, bool isViewAll)
        {
            gridView.DataSource = OrderMealBLL.GetOrderMealQuery(txtCompany, txtMember, rbStatus, model, beginDate, endDate, isViewAll);
            gridView.AddShowColumn(Order.OrderNumColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddCustomColumn("OrderDetail", "餐点明细")
                    .AddShowColumn(SysCompany.CompanyNameColumn)
                    .AddShowColumn(Order.OrderCodeColumn)
                    .AddCustomColumn("Action", "操作")
                    .AddShowColumn(Order.OrderStatusColumn);
            gridView.OnDataBindColumn += new DataBindColumnHandle(OrderMealBLL.gridView_OnDataBindColumn);
            lock (__orderQueryLocker)
            {
                gridView.DataBind( );
            }
        }

        public static void RecycleAdminOrder( )
        {
            if (AppContext.Context.CurrentRole == (int)EleoooRoleDefine.OrderMeal)
            {
                QueryCommand cmd = new QueryCommand("Update Orders Set OrderOper=null where OrderOper=@OrderOper and OrderStatus in(2,3,4);");
                cmd.AddParameter("@OrderOper", AppContext.Context.User.Id);
                DataService.ExecuteQuery(cmd);
            }
        }
        private static Dictionary<string, string> _sex;
        public static Dictionary<string, string> Sex
        {
            get
            {
                if (_sex == null)
                {
                    _sex = new Dictionary<string, string>( );
                    _sex.Add(bool.TrueString, "男");
                    _sex.Add(bool.FalseString, "女");
                }
                return _sex;
            }
        }
        public static string GetSexRadio(string postName)
        {
            return GetSexRadio(postName, string.Empty);
        }
        public static string GetSexRadio(string postName, string value)
        {
            if (string.IsNullOrEmpty(value))
                value = bool.TrueString;
            return Controls.HtmlControl.GetRadioHtml(Sex, postName, value).ToString( );
        }
        internal static void ValidateCompany(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "CompanyRateMaster":
                    if (!string.IsNullOrEmpty(viewRow.Value))
                    {
                        if (!SubSonic.Sugar.Numbers.IsNumber(viewRow.Value))
                        {
                            viewRow.ValidateMessage = "佣金比例必须是数字";
                        }
                    }
                    break;
                case "CompanyCode":
                    string depth = string.Empty;
                    depth = AreaSelector.SelectedArea1;
                    if (string.IsNullOrEmpty(depth))
                    {
                        viewRow.ValidateMessage = "请选择商家所在区域!";
                        break;
                    }
                    viewRow.Value = AreaBLL.GenCompanyCodeByDepth(depth);
                    viewRow.ValidateMessage = string.Empty;
                    break;
                case "CompanyPwd":
                    if (string.IsNullOrEmpty(viewRow.Value))
                    {
                        viewRow.ValidateMessage = "密码不能为空!";
                        goto label_break;
                    }
                    if (viewRow.Value.Length < 6)
                    {
                        viewRow.ValidateMessage = "密码不能小于6位!";
                        goto label_break;
                    }
                    string confirmPwd = AppContext.Page.Request.Params[string.Concat(viewRow.ParamName, "_1")];
                    if (!Utilities.Compare(viewRow.DbValue, viewRow.ParamValue) &&
                        !Utilities.Compare(viewRow.Value, confirmPwd, false) &&
                        !Utilities.Compare(viewRow.Value, Utilities.DESEncrypt(confirmPwd)))
                    {
                        viewRow.ValidateMessage = "两次密码输入不一致!";
                        goto label_break;
                    }
                label_break:
                    break;
                case "CompanyName":
                    if (string.IsNullOrEmpty(viewRow.Value))
                        viewRow.ValidateMessage = "商家名称不能为空!";
                    break;
                case "CompanyTel":
                    string message;
                    if (!UserBLL.CheckUserName(viewRow.Value, out message))
                    {
                        viewRow.ValidateMessage = message;
                        break;
                    }
                    //if (string.IsNullOrEmpty(viewRow.Value))
                    //{
                    //    viewRow.ValidateMessage = "你的登录账号不能为空!";
                    //    break;
                    //}
                    //if (!SubSonic.Sugar.Validation.IsNumeric(viewRow.Value))
                    //{
                    //    viewRow.ValidateMessage = "登录账号不合法,请使用手机号码作为登录账号!";
                    //    break;
                    //}
                    //if (viewRow.Value.Length != 11)
                    //{
                    //    viewRow.ValidateMessage = "你的登录账号不是11位数字,请使用手机号码作为登录账号!";
                    //    break;
                    //}
                    if (string.IsNullOrEmpty(viewRow.DbValue) && UserBLL.CheckUserExist(viewRow.Value))
                    {
                        viewRow.ValidateMessage = "此账号已经存在,请使用其他账号注册!";
                        break;
                    }
                    break;
                case "CompanyPhone":
                    if (string.IsNullOrEmpty(viewRow.Value))
                        viewRow.ValidateMessage = "商家联系电话不能为空,请输入商家的固定电话!";
                    break;
                case "CompanyEmail":
                    if (!string.IsNullOrEmpty(viewRow.Value) && !SubSonic.Sugar.Validation.IsEmail(viewRow.Value))
                    {
                        viewRow.ValidateMessage = "输入的Email格式不正确!";
                    }
                    break;
            }
        }

        public static string TypeFilterDefine
        {
            get
            {
                return ResBLL.GetRes(Eleooo.Web.Controls.UcTypeAndAreaFilter.TYPE_PARAM, "体检,齿科,美容,美发,健身,培训,KTV,瑜伽", "商家项目查询类别");
            }
        }
        private static Dictionary<string, string> _typeFilterDefineList;
        public static Dictionary<string, string> TypeFilterDefineList
        {
            get
            {
                if (_typeFilterDefineList == null)
                {
                    _typeFilterDefineList = new Dictionary<string, string>( );
                    string[] arr = TypeFilterDefine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in arr)
                    {
                        if (!_typeFilterDefineList.ContainsKey(item))
                            _typeFilterDefineList.Add(item, item);
                    }
                }
                return _typeFilterDefineList;
            }
        }
    }
}
