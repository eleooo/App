using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Linq;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class MyArea : ActionPage
    {

        protected string GetAreaDepthSelectedValue(string depth)
        {
            var ids = AreaBLL.ConvertDepthToIds(depth);
            if (ids == null || ids.Length == 0)
                return ",";
            if (ids.Length == 1)
                return ids[0];
            else if (ids.Length == 2)
                return string.Join(",", ids);
            else
                return string.Concat(ids[ids.Length - 2], ",", ids[ids.Length - 1]);
        }
        protected string GetMultiDepthsNameIds(string depths)
        {
            var areas = AreaBLL.GetAreasByDepths(depths);
            return Utilities.ObjToJSON(areas.Select((area) => new { id = area.Id, name = area.AreaName }).ToArray( ));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //boxName.Visible = string.IsNullOrEmpty(CurrentUser.MemberFullname);
            //boxSex.Visible = !CurrentUser.MemberSex.HasValue;
            //boxEmail.Visible = boxName.Visible && boxSex.Visible && string.IsNullOrEmpty(CurrentUser.MemberEmail);
            txtMessage.InnerHtml = "";
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {

        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            CurrentUser.MarkClean( );
            CurrentUser.MarkOld( );
            bool isNeedRedirect = false;
            //if (boxName.Visible)
            //{
            //    if (string.IsNullOrEmpty(txtMemberName.Text.Trim( )))
            //    {
            //        txtMessage.InnerHtml = "请填写你的姓名";
            //        goto lbl_end;
            //    }
            //    CurrentUser.MemberFullname = txtMemberName.Text.Trim( );
            //    boxName.Visible = false;
            //    isNeedRedirect = true;
            //}
            //if (boxEmail.Visible && !string.IsNullOrEmpty(txtMemberEmail.Text.Trim( )))
            //{
            //    if (!SubSonic.Sugar.Validation.IsEmail(txtMemberEmail.Text.Trim( )))
            //    {
            //        txtMessage.InnerHtml = "Email格式填写不正确！";
            //        goto lbl_end;
            //    }
            //    else
            //        CurrentUser.MemberEmail = txtMemberEmail.Text.Trim( );
            //    boxEmail.Visible = boxName.Visible;
            //}
            //if (boxSex.Visible)
            //{
            //    bool sex;
            //    if (rbSex.SelectedIndex < 0 || !bool.TryParse(rbSex.Text, out sex))
            //    {
            //        txtMessage.InnerHtml = "请选择你的性别！";
            //        goto lbl_end;
            //    }
            //    else
            //        CurrentUser.MemberSex = sex;
            //    boxSex.Visible = false;
            //    isNeedRedirect = true;
            //}
            string depth1 = Request.Form["liveAreaS2"];
            string depth2 = Request.Form["workAreaS2"];
            string depth3 = Request.Form["fbArea"];

            SysArea area = SysArea.FetchByID(Utilities.ToInt(depth1));
            depth1 = area != null ? area.Depth : null;
            if (string.IsNullOrEmpty(depth1) && string.IsNullOrEmpty(depth2) && string.IsNullOrEmpty(depth3))
            {
                txtMessage.InnerHtml = "请选择你的圈子";
                goto lbl_end;
            }
            area = SysArea.FetchByID(Utilities.ToInt(depth2));
            depth2 = area != null ? area.Depth : null;
            if (!string.IsNullOrEmpty(depth3))
            {
                var ids = depth3.Split(',');
                var query = DB.Select( ).From<SysArea>( ).Where(SysArea.IdColumn).In(ids);
                depth3 = string.Join(",", query.ExecuteTypedList<SysArea>( ).Select((item) => item.Depth).ToArray( ));
            }
            bool isChg1 = !Utilities.Compare(CurrentUser.AreaDepth1, depth1);
            bool isChg2 = !Utilities.Compare(CurrentUser.AreaDepth2, depth2);
            bool isChg3 = !Utilities.Compare(CurrentUser.AreaDepth3, depth3);
            if (isChg1 && !CheckModifiedDate( ) || isChg2 && !CheckArea2ModifiedDate( ) || isChg3 && !CheckArea3Modifed( ))
            {
                txtMessage.InnerHtml = "对圈子的设定3个月内只能修改一次哦";
                goto lbl_end;
            }

            if (isChg1)
            {
                int city = Utilities.ToInt(AreaSelector.Selector1.GetSelectedValue(0));
                if (CurrentUser.MemberCity != city)
                    CurrentUser.MemberCity = city;
                CurrentUser.AreaDepth1 = depth1;

            }
            if (isChg2)
            {
                CurrentUser.AreaDepth2 = depth2;
                AppContext.Context.UserConfig.Area2ModifyDate = DateTime.Now;
            }
            if (isChg3)
                CurrentUser.AreaDepth3 = depth3;
            if (isChg1 || isChg3)
            {
                AppContext.Context.UserConfig.AreaModifyDate = DateTime.Now;
                AppContext.Context.UserConfig.AreaModifyCount++;
            }
            if (isChg1 || isChg2 || isChg3)
            {
                CurrentUser.Save( );
                AppContext.Context.UserConfig.Save( );
            }

            txtMessage.InnerHtml = "修改成功!";
            if (isNeedRedirect)
            {
                Utilities.ShowMessageRedirect("设定成功", "/Member/MyCompany.aspx");
                return;
            }
        lbl_end:
            On_ActionQuery(sender, e);
        }
        private bool CheckModifiedDate( )
        {
            if (string.IsNullOrEmpty(CurrentUser.AreaDepth1))
                return true;
            if (!AppContext.Context.UserConfig.AreaModifyDate.HasValue ||
                !AppContext.Context.UserConfig.AreaModifyCount.HasValue)
                return true;
            TimeSpan span = DateTime.Now - AppContext.Context.UserConfig.AreaModifyDate.Value;
            return span.Days >= 90 && AppContext.Context.UserConfig.AreaModifyCount.Value < 2;
        }
        private bool CheckArea2ModifiedDate( )
        {
            if (string.IsNullOrEmpty(CurrentUser.AreaDepth2))
                return true;
            if (!AppContext.Context.UserConfig.Area2ModifyDate.HasValue)
                return true;
            TimeSpan span = DateTime.Now - AppContext.Context.UserConfig.Area2ModifyDate.Value;
            return span.Days >= 90;
        }
        private bool CheckArea3Modifed( )
        {
            var depth3 = CurrentUser.AreaDepth3;
            return string.IsNullOrEmpty(depth3) || depth3.Count(c => c == ',') < 2;
        }
    }
}