using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;

namespace Eleooo.Client
{
    public partial class UcAdsSettings : UserControl
    {
        public UcAdsSettings( )
        {
            InitializeComponent( );
        }
        public DataTable DataSource
        {
            get
            {
                return grid.DataSource as DataTable;
            }
            set
            {
                if (grid.Columns.Count == 0 &&
                    value != null)
                {
                    AutoBuildColumn(value);
                }
                grid.DataSource = value;
            }
        }
        private uint _maxRowLimit;
        public uint MaxRowLimit { get { return _maxRowLimit; } set { _maxRowLimit = value; this.grid.Tag = value; } }
        private void AutoBuildColumn(DataTable data)
        {
            for (int i = data.Columns.Count - 1; i >= 0; i--)
            {
                DataColumn col = data.Columns[i];
                if (!grid.Columns.Contains(col.ColumnName))
                {
                    DataGridViewColumn gridCol = GetEditorType(col.DataType);
                    gridCol.Name = col.ColumnName;
                    gridCol.HeaderText = string.IsNullOrEmpty(col.Caption) ? col.ColumnName : col.Caption;
                    gridCol.DataPropertyName = col.ColumnName;
                    grid.Columns.Insert(0, gridCol);
                }
            }
            BuildActionColumn( );
        }
        private void BuildActionColumn( )
        {
            ActionButtonColumn actCol = new ActionButtonColumn( );
            actCol.Name = "ActionButtonColumn";
            actCol.HeaderText = "操作";
            actCol.Width = 50;
            actCol.DefaultCellStyle.NullValue = "删除";
            grid.Columns.Add(actCol);
        }
        DataGridViewColumn GetEditorType(Type type)
        {
            if (type == typeof(int))
                return new DataGridViewIntegerInputColumn( ) { ShowUpDown = true };
            else if (type == typeof(double) || type == typeof(decimal))
                return new DataGridViewDoubleInputColumn( ) { ShowUpDown = true };
            else if (type == typeof(DateTime))
                return new DataGridViewDateTimeInputColumn( );
            else
                return new DataGridViewTextBoxColumn( );
        }

        private void grid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (MaxRowLimit > 0)
                grid.AllowUserToAddRows = grid.RowCount <= MaxRowLimit;
        }
        private void grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBoxEx.Show(e.Exception.Message);
        }

        class ActionButtonColumn : DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn
        {
            public ActionButtonColumn( )
            {
                Click += new EventHandler<EventArgs>(ActionButtonColumn_Click);
                ColorTable = eButtonColor.OrangeWithBackground;
            }

            void ActionButtonColumn_Click(object sender, EventArgs e)
            {
                DataTable dt = this.DataGridView.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0 &&
                    dt.Rows.Count > this.DataGridView.CurrentRow.Index)
                {
                    dt.Rows.RemoveAt(this.DataGridView.CurrentRow.Index);
                }
                else
                {
                    this.DataGridView.CancelEdit( );
                }
                uint maxRowLimit = Utilities.ChangeType<uint>(this.DataGridView.Tag);
                if (maxRowLimit > 0)
                    this.DataGridView.AllowUserToAddRows = this.DataGridView.RowCount <= maxRowLimit;
            }
        }

    }
}
