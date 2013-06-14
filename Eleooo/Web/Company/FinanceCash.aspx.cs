using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using SubSonic;
using System.Transactions;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class FinanceCash : ActionPage
    {
        const string CHECKMEMBER_CMD = "CheckMember";

        private decimal dMemberRate = 0;
        private int iMemberPoint = 0;
        private decimal dRateSale = 0;
        private decimal dMemberCash = 0;
        private SysMember user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtMemberPoint.Enabled = AppContext.Context.CompanyType.Value == CompanyType.UnionCompany;
            rblCompanyRate.Enabled = txtMemberPoint.Enabled;
            txtMessage.InnerHtml = string.Empty;
            txtMemberPhone.Text = hdnMemberPhone.Value;
            List<SysCompanyMemberGrade> lstGrade = GradeBLL.GetGradeList(AppContext.Context.Company);
            var rates = CompanyBLL.GetCompanyRates(AppContext.Context.Company);
            foreach (SysCompanyMemberGrade grade in lstGrade)
                cbMemberGrade.Items.Add(new ListItem(grade.GradeName, grade.Id.ToString( )));
            if (cbMemberGrade.Items.Count == 0)
                cbMemberGrade.Items.Add(new ListItem("一般", "0"));
            cbMemberGrade.SelectedIndex = GradeID;
            rblCompanyRate.Items.AddRange(rates.ToListItem( ));
            decimal _dRate = Utilities.ToDecimal(Request.Params[rblCompanyRate.UniqueID]);
            int index = rates.IndexOf(_dRate);
            if (index >= 0)
                rblCompanyRate.SelectedIndex = index;
            else if (rblCompanyRate.Items.Count > 0)
                rblCompanyRate.SelectedIndex = 0;
            if (IsPostBack)
            {
                if (EVENTTARGET == CHECKMEMBER_CMD)
                    btnMemberValidate_Click(sender, e);
            }
            else
            {
                ResetField( );
            }
        }
        private int GradeID
        {
            get
            {
                int val;
                int.TryParse(Request.Params[cbMemberGrade.UniqueID], out val);
                return val;
            }
        }
        private void btnMemberValidate_Click(object sender, EventArgs e)
        {
            if (!ValidateMember( ))
                return;
            string message;
            bool bRet = UserBLL.GetMemberForCash(user, out message);
            lblMemberInfo.InnerHtml = message;
            if (!bRet)
            {
                txtMemberPhone.Focus( );
                return;
            }
            SysMemberCash cash = UserBLL.GetUserLatestCash(user.Id, CurrentUser.CompanyId.Value);
            if (cash != null)
            {
                if (cash.CashRate.HasValue && cash.CashRate.Value > 0)
                {
                    txtMemberRate.Text = (cash.CashRate.Value * 100M).ToString("#####");
                }
                if (cash.CashMemo != null)
                {
                    if (SubSonic.Sugar.Numbers.IsInteger(cash.CashMemo))
                        cbMemberGrade.SelectedValue = cash.CashMemo;
                    else
                        cbMemberGrade.Text = cash.CashMemo;
                }
                if (cash.CashRateSale.HasValue && cash.CashRateSale.Value > 0)
                {
                    var rates = CompanyBLL.GetCompanyRates(AppContext.Context.Company);
                    int index = rates.IndexOf(cash.CashRateSale.Value);
                    if (index >= 0)
                        rblCompanyRate.SelectedIndex = index;
                }
            }
            txtMemberPhone.Enabled = false;
            btnPost.Enabled = true;
            txtMemberCash.Focus( );
        }
        void ResetField( )
        {
            cbMemberGrade.SelectedIndex = 0;
            txtMemberCash.Text = string.Empty;
            txtMemberPhone.Text = string.Empty;
            hdnCompanyType.Value = Convert.ToString(AppContext.Context.Company.CompanyType);
            txtMemberPoint.Text = string.Empty;
            txtMemberRate.Text = string.Empty;
            lblMemberCashInfo.InnerHtml = string.Empty;
            lblMemberInfo.InnerHtml = string.Empty;
            lblMemberPointInfo.InnerHtml = string.Empty;
            lblMemberRateInfo.InnerHtml = string.Empty;
            txtMemberPhone.Focus( );
        }
        bool ValidateMember( )
        {
            string phoneNum = hdnMemberPhone.Value;
            if (string.IsNullOrEmpty(phoneNum))
            {
                lblMemberInfo.InnerHtml = "会员账号不能为空!";
                txtMemberPhone.Focus( );
                return false;
            }

            user = UserBLL.GetUserByPhoneNum(hdnMemberPhone.Value);
            if (user == null)
            {
                lblMemberInfo.InnerHtml = "你输入的会员账号不存在! ";
                txtMemberPhone.Focus( );
                return false;
            }
            if (user.CompanyId > 0)
            {
                lblMemberInfo.InnerHtml = "你不能用此账号进行充值,此账号属于商家账号!";
                txtMemberPhone.Focus( );
                return false;
            }

            //if (user.MemberGrade != null)
            //{
            //    cbMemberGrade.SelectedIndex = Utilities.ToInt(user.MemberGrade);
            //}
            return true;
        }
        bool ValidateData( )
        {
            if (string.IsNullOrEmpty(txtMemberCash.Text))
            {
                lblMemberCashInfo.InnerHtml = "充值金额不能为空!";
                txtMemberCash.Focus( );
                return false;
            }
            if (!decimal.TryParse(txtMemberCash.Text, out dMemberCash) || dMemberCash <= 0)
            {
                lblMemberCashInfo.InnerHtml = "充值金额必须大于零!";
                txtMemberCash.Focus( );
                return false;
            }
            decimal.TryParse(txtMemberRate.Text, out dMemberRate);
            if (dMemberRate < 0 || dMemberRate > 99)
            {
                lblMemberRateInfo.InnerHtml = "折扣必须是1-99之间的数字!";
                txtMemberRate.Focus( );
                return false;
            }
            dRateSale = Utilities.ToDecimal(Request.Params[rblCompanyRate.UniqueID]);
            if (dRateSale < 0)
            {
                lblRateSaleInfo.InnerHtml = "积分比例不能小于零";
                return false;
            }
            int.TryParse(txtMemberPoint.Text, out iMemberPoint);
            if (iMemberPoint < 0)
            {
                lblMemberPointInfo.InnerHtml = "赠送积分不能小于零!";
                txtMemberPoint.Focus( );
                return false;
            }
            return ValidateMember( );
        }

        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (AppContext.Context.CompanyType == CompanyType.MealCompany)
            {
                txtMessage.InnerHtml = "阁下的商家类型无权使用此功能";
                return;
            }
            if (!ValidateData( ))
            {
                txtMessage.InnerHtml = "保存失败!";
                return;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope( ))
                {
                    using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                    {
                        SaveData( );
                        OrderBLL.UpdateBalance( );
                        ts.Complete( );
                    }
                }
                txtMessage.InnerHtml = "保存成功!";
                ResetField( );
            }
            catch (Exception ex)
            {
                Logging.Log("FinanceCash->On_ActionAdd", ex, true);
                txtMessage.InnerHtml = ex.Message;
            }
        }

        void SaveData( )
        {
            SysMemberCash cash = new SysMemberCash
            {
                CashCompanyID = AppContext.Context.Company.Id,
                CashMemberID = user.Id,
                CashDate = DateTime.Now,
                CashSum = dMemberCash,
                CashRate = dMemberRate / 100M,
                CashPoint = iMemberPoint,
                CashMemo = GradeID.ToString( ),
                CashOrderID = 0,
                CashRateSale = dRateSale
            };
            string message;
            if (!OrderBLL.SaveMemberCash(cash, user, out message))
                throw new Exception(message);
        }
    }
}