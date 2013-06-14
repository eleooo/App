using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Eleooo.DAL;
using SubSonic;
using System.Transactions;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysAreaEdit : ActionPage
    {
        const string LEVEL_NBSP = "　";
        const string PREFIX = "┝";
        const string UPDATER = "update Sys_Area set Depth=replace(Depth,'{0}','{1}') where Depth like '{0}%';";
        char[] separator = new char[] { '/' };
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMessage.InnerHtml = string.Empty;
        }
        private string GetAreamSelectText(SysArea area)
        {
            string result = string.Empty;
            for (int i = 0; i < area.Depth.Split(separator, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                result = string.Concat(result, LEVEL_NBSP);
            return string.Concat(result, PREFIX, area.AreaName);
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAreaName.Text))
            {
                txtMessage.InnerHtml = "区域名称不能为空!";
                goto lbl_end;
            }
            int pid;
            if (!int.TryParse(AreaPID, out pid) || pid < 0)
                pid = 0;
            SysArea area = new SysArea
            {
                AreaName = txtAreaName.Text.Trim( ),
                Depth = string.Empty,
                PId = pid,
                AreaCode = txtCode.Text
            };
            if (string.IsNullOrEmpty(area.AreaCode))
            {
                txtCode.Text = GetAreaCode(area);
                area.AreaCode = txtCode.Text;
            }
            area.Save( );
            area.Depth = GetAreaDepth(area.Id, pid);
            area.Save( );
            this.txtMessage.InnerHtml = "新增成功!";
        lbl_end:
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            var query = DB.Select( ).From<SysArea>( )
              .OrderAsc(SysArea.Columns.Depth, SysArea.Columns.Id); ;
            List<SysArea> areas = query.ExecuteTypedList<SysArea>( );
            int nPID;
            if (!int.TryParse(AreaPID, out nPID) || nPID < 0)
                nPID = 0;
            ddlParent.Items.Clear( );
            ddlParent.Items.Add(new ListItem("┝顶级区域", "0"));
            string selectedID = nPID.ToString( );
            foreach (SysArea area in areas)
            {
                ListItem item = new ListItem(GetAreamSelectText(area), area.Id.ToString( ));
                if (item.Value == AreaID)
                {
                    txtAreaName.Text = area.AreaName;
                    txtCode.Text = area.AreaCode;
                    selectedID = area.PId.ToString( );
                }

                ddlParent.Items.Add(item);
            }
            ddlParent.SelectedValue = selectedID;
        }
        private string GetAreaCode(SysArea area)
        {
            string result = string.Empty;
            if (area == null || !string.IsNullOrEmpty(area.AreaCode))
                goto label_return;
            result = PinYin.GetChineseSpell(area.AreaName);
        label_return:
            return result;

        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAreaName.Text))
            {
                txtMessage.InnerHtml = "区域名称不能为空!";
                goto lbl_end;
            }
            SysArea area = SysArea.FetchByID(AreaID);
            if (area == null)
            {
                txtMessage.InnerHtml = "当前区域不存在";
                goto lbl_end;
            }
            int pid = Utilities.ToInt(AreaPID);
            if (pid == area.Id)
            {
                txtMessage.InnerHtml = "当前区域的父层不能等于本层!";
                goto lbl_end;
            }
            SysArea pArea = null;
            if (pid != 0)
            {
                pArea = SysArea.FetchByID(pid);
                if (pArea == null)
                {
                    txtMessage.InnerHtml = "所选择的父层不存在!";
                    goto lbl_end;
                }
                if (pArea.Depth.StartsWith(area.Depth))
                {
                    txtMessage.InnerHtml = "父层不能移动到子层!";
                    goto lbl_end;
                }
            }
            area.AreaName = txtAreaName.Text.Trim( );
            area.AreaCode = txtCode.Text;
            if (string.IsNullOrEmpty(area.AreaCode))
            {
                txtCode.Text = GetAreaCode(area);
                area.AreaCode = txtCode.Text;
            }
            using (TransactionScope ts = new TransactionScope( ))
            {
                using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                {
                    if (area.PId != pid)
                    {
                        string newDepth = GetAreaDepth(area.Id, pid, pArea);
                        QueryCommand cmd = new QueryCommand(string.Format(UPDATER, area.Depth, newDepth), DB.Provider.Name);
                        area.PId = pid;
                        DataService.ExecuteQuery(cmd);
                    }
                    area.Save( );
                    ts.Complete( );
                }
            }
            this.txtMessage.InnerHtml = "修改成功!";
        lbl_end:
            On_ActionQuery(sender, e);
        }
        private string GetAreaDepth(int id, int pID, SysArea pArea = null)
        {
            string sDepth = string.Format("/{0}/", id);
            if (pID > 0)
            {
                pArea = pArea == null ? SysArea.FetchByID(pID) : pArea;
                if (pArea != null)
                    sDepth = string.Format("{0}{1}/", pArea.Depth, id);
            }
            return sDepth;
        }
        public string AreaID
        {
            get
            {
                return Request.Params["id"];
            }
        }

        public string AreaPID
        {
            get
            {
                return !IsPostBack ? Request.Params["PID"] : Request.Params[ddlParent.UniqueID];
            }
        }

        public string CurAction
        {
            get
            {
                return string.IsNullOrEmpty(AreaID) ? "Add" : "Edit";
            }
        }
    }
}