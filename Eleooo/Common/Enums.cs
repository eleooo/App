using System;
using System.Collections.Generic;
using System.Web;

namespace Eleooo.Common
{
    public enum SubSystem
    {
        ALL = 0,
        Member = 1,
        Company = 2,
        Admin = 3
    }
    public enum CompanyRStatus
    {
        InProgress,
        Completed
    }
    public enum OrderModel
    {
        Auto,
        Manual
    }
    public enum MemberFilter
    {
        All = 0, //所有 
        Owner = 1, //本店
        Outer = 2 //外来
    }

    public enum MemberCompanyItemStatus
    {
        //1 已抢购 2已消费 3 已过期 4已取消
        None = 0, //没有抢购
        InProgress = 1,
        Completed = 2,
        OutDate = 3,
        Cancel = 4
    }
    public enum OrderType
    {
        Common = 1, //普通消费
        CompanyItem = 2,  //优惠消费
        OrderMeal = 3 //订快餐
    }
    public enum OrderStatus
    {
        Commonn = 1,//普通消费
        NotStart = 2,//待处理
        Modified = 3, //已经修改
        InProgress = 4,//处理中
        Canceled = 5,//已取消
        Completed = 6//订餐成功
    }
    public enum RoleAssignment
    {
        Reject = 0,
        Allow = 1
    }

    public enum FaceBookType
    {
        Company,
        CompanyItem,
        CompanyAds,
        OrderMeal,
        Eleooo
    }

    public enum Actions
    {
        Query = 0,
        Delete = 1,
        Edit = 2,
        Add = 3,
        Login = 4,
        Logout = 5,
        Custom = 6
    }

    public enum MenuRegion
    {
        Main = 0,
        Header = 1,
        Left = 2,
        Foot = 3
    }

    public enum DataSourceType
    {
        SqlQuery = 0,
        StoredProcedure = 1,
        InlineQuery = 2
    }

    public enum PaymentType
    {
        //所有
        All = 0,
        //消费赠送
        ConsumeGive = 1,
        //抵扣
        Mortgage = 2,
        //充值赠送
        PrepaidGive = 3,
        //转移
        Move = 4,
        //结算
        SetMethod = 5,
        //特约商家,优惠项目结算
        CompanyItem = 6,
        //看广告赠送
        AdvsGive = 7,
        //导入积分不纳入结算范围
        Import = 8,
        //推荐会员赠送
        Reward = 9
    }
    public enum PaymentCashType
    {
        //所有
        All = 0,
        //充值
        Prepaid = 1,
        //抵扣
        Mortgage = 2,
        //导入储值,不纳入结算
        Import = 3
    }

    public enum EleoooRoleDefine
    {
        Admin = 1,
        Member = 2,
        Company = 3,
        OrderMeal = 4
    }

    //'1' 联盟商家
    //'2' 特约商家
    //'3' 广告商家
    //'4' 热点商家";
    public enum CompanyType
    {
        //联盟商家
        UnionCompany = 1,
        //特约商家
        SpecialCompany = 2,
        //广告商家
        AdCompany = 3,
        //快餐商家
        MealCompany = 4
    }
    public enum SaveType
    {
        Backup = 0,
        Company = 1,
        Custome = 2,
        Support = 3,
        Doc = 4,
        CompanyItem = 5,
        CompanyAds = 6,
        Voice = 7
    }

    [Flags]
    public enum FileType
    {
        Image = 1, //.bmp.jpg.jpeg.png.gif
        Zip = 2, //.zip.rar.7z
        Doc = 4, //.doc.docx.xls.xlsx.ppt.pptx.txt.rtf
        Media = 8, //.mp3.mov.avi.rm.rmvb.wmv
        Bak = 16, //.bak
        All = 32  //All
    }

    public enum LoginSystem
    {
        Web,
        Client,
        Mobile
    }

    public enum ViewCompanyAdsViews
    {
        Default, AnswerQuestion, AnswerRight, AnswerWrong, Message
    }
}