using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;

namespace Eleooo.Web.Member
{
    public partial class RewardFriend : ActionPage
    {
        private SysReward _reward;
        public SysReward Reward
        {
            get
            {
                if (_reward == null)
                    _reward = RewardBLL.Reward;
                return _reward;
            }
        }
        public string RewardRateStr
        {
            get
            {
                return Reward != null ? Reward.RewardRate.Value.ToString("#####.###") + "%" : "";
            }
        }
        public string RewardMemo
        {
            get
            {
                return Reward != null ? HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(Reward.RewardMemo)) : "";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}