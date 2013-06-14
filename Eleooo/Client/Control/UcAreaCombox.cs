using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Eleooo.DAL;
using DevComponents.DotNetBar;

namespace Eleooo.Client
{
    public partial class UcAreaCombox : UserControl
    {
        public UcAreaCombox( )
        {
            InitializeComponent( );
            btnAdd.Visible = false;
            areaContainer.Visible = false;
            this.Height = cbCity.Height;
        }
        private string _areaDepth;
        private bool cityLocked = false;
        private bool areaLocked = false;
        private bool locationLocked = false;
        private char[] Spliter = new char[] { '/' };
        public bool IsMulitArea
        {
            get
            {
                return btnAdd.Visible;
            }
            set
            {
                btnAdd.Visible = value;
                areaContainer.Visible = value;
            }
        }
        public string AreaDepth
        {
            get { GetAreaDepth( ); return _areaDepth; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !Utilities.Compare(_areaDepth, value))
                {
                    _areaDepth = value;
                    DepthChanged( );
                }
            }
        }
        private void AddTab(SysArea area)
        {
            if (area.Id <= 0)
            {
                MessageBoxEx.Show("请选择商圈");
                return;
            }
            if (AreaIsExist(area))
            {
                MessageBoxEx.Show("此商圈已经存在!");
                return;
            }
            SuperTabItem item = new SuperTabItem( );
            item.Tag = area;
            item.Text = area.AreaName;
            areaContainer.Tabs.Add(item);
        }
        private bool AreaIsExist(SysArea area)
        {
            bool isExist = false;
            SysArea _area;
            foreach (SuperTabItem item in areaContainer.Tabs)
            {
                if (item.Tag != null &&
                    ((_area = item.Tag as SysArea) != null) &&
                    _area.Id == area.Id)
                {
                    isExist = true;
                    break;
                }
            }
            return isExist;
        }
        private string GetMulitAreaDepth( )
        {
            List<string> depths = new List<string>( );
            SysArea _area;
            foreach (SuperTabItem item in areaContainer.Tabs)
            {
                if(item.Tag != null &&((_area = item.Tag as SysArea) != null))
                    depths.Add(_area.Depth);
            }
            return string.Join(",",depths.ToArray());
        }
        public string MulitAreaDepth
        {
            get
            {
                return GetMulitAreaDepth( );
            }
        }
        public void ClearItem( )
        {
            areaContainer.Tabs.Clear( );
            areaContainer.SelectedTab = null;
        }
        void DepthChanged( )
        {
            if (string.IsNullOrEmpty(_areaDepth))
                return;
            string[] ids = _areaDepth.Split(Spliter, StringSplitOptions.RemoveEmptyEntries);
            int cityID = 0, areaID = 0, locationID = 0;
            if (ids.Length >= 0 && int.TryParse(ids[0], out cityID))
            {
                if (cbCity.Items.Count == 0)
                    AddItems(cbCity, 0);
                SysArea area = AreaBLL.GetAreaByID(cityID);
                if (area != null)
                {
                    cityLocked = true;
                    cbCity.SelectedItem = area;
                    cityLocked = false;
                }
            }
            if (ids.Length >= 1 && int.TryParse(ids[1], out areaID))
            {
                SysArea area = AreaBLL.GetAreaByID(areaID);
                if (area != null)
                {
                    areaLocked = true;
                    cbArea.SelectedItem = area;
                    areaLocked = false;
                }
            }
            if (ids.Length >= 2 && int.TryParse(ids[2], out locationID))
            {
                SysArea area = AreaBLL.GetAreaByID(locationID);
                if (area != null)
                {
                    locationLocked = true;
                    cbLocation.SelectedItem = area;
                    locationLocked = false;
                }
            }

        }

        private void cbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            SysArea area = cbCity.SelectedItem as SysArea;
            if (area != null)
            {
                AddItems(cbArea, area.Id);
            }
            //GetAreaDepth( );
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            SysArea area = cbArea.SelectedItem as SysArea;
            if (area != null)
            {
                AddItems(cbLocation, area.Id);
            }
            //GetAreaDepth( );
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!locationLocked)
            //    GetAreaDepth( );
        }
        SysArea GetSelectedArea( )
        {
            SysArea area = null;
            if (cbLocation.SelectedIndex > 0 && !locationLocked)
                area = cbLocation.SelectedItem as SysArea;
            else if (cbArea.SelectedIndex > 0 && !areaLocked)
                area = cbArea.SelectedItem as SysArea;
            else if (cbCity.SelectedIndex > 0 && !cityLocked)
                area = cbCity.SelectedItem as SysArea;
            return area;
        }
        void GetAreaDepth( )
        {
            SysArea area = GetSelectedArea( );
            if (area != null)
                _areaDepth = area.Depth;
            else
                _areaDepth = string.Empty;
        }
        void AddItems(DevComponents.DotNetBar.Controls.ComboBoxEx cb, int id)
        {
            cb.Items.Clear( );
            cb.Items.AddRange(AreaBLL.GetAreaListForCombox(id).ToArray( ));
            cb.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (areaContainer.Tabs.Count >= 5)
            {
                MessageBoxEx.Show("最多只允许选择五个商圈!");
                return;
            }
            SysArea area = GetSelectedArea( );
            //if (area == null || cbLocation.SelectedIndex == 0)
            //{
            //    MessageBoxEx.Show("请选择商圈");
            //    return;
            //}
            AddTab(area);
        }

        private void UcAreaCombox_Load(object sender, EventArgs e)
        {
            if (IsMulitArea)
            {
                areaContainer.Width = this.Width - 20;
            }
        }
    }
}
