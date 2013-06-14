using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DevComponents.DotNetBar;

namespace Eleooo.Client
{
    public class BindingButtonItem : ButtonItem
    {
        private IList _dataSource;
        private Command _cmd;
        private ButtonItem _selectedItem;
        public ButtonItem SelectedItem
        {
            get
            {
                if (_selectedItem == null &&
                    SubItems.Count > 0)
                {
                    _selectedItem = SubItems[0] as ButtonItem;
                    CheckedItem(_selectedItem);
                }
                return _selectedItem;
            }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    CheckedItem(_selectedItem);
                }
            }
        }
        public int SelectedIndex
        {
            get
            {
                return SubItems.IndexOf(_selectedItem);
            }
            set
            {
                if (value >= 0 && value < SubItems.Count)
                {
                    _selectedItem = _selectedItem = SubItems[value] as ButtonItem;
                    CheckedItem(_selectedItem);
                }
            }
        }

        public void BuildSubItem(IList dataSource, string textProperty)
        {
            BuildSubItem(dataSource, textProperty, null,0);
        }
        public void BuildSubItem(IList dataSource, string textProperty, Command cmd)
        {
            BuildSubItem(dataSource, textProperty, cmd, 0);
        }
        public void BuildSubItem(IList dataSource, string textProperty, Command cmd,int selectedIndex)
        {
            if (_dataSource == dataSource)
                return;
            this.SubItems.Clear( );
            _cmd = cmd == null ? new Command( ) : cmd;
            ButtonItem defItem = null;
            for (int i = 0; i < dataSource.Count; i++)
            {
                object item = dataSource[i];
                if (item == null)
                    continue;
                string text = TypeHelper.GetStringValue(item, textProperty);
                if (string.IsNullOrEmpty(text))
                    text = item.ToString( );
                object cmdParam = TypeHelper.GetPropertyValue(item, "CommandParameter");
                if (cmdParam == null)
                    cmdParam = item;
                Command itemCmd = TypeHelper.GetPropertyValue<Command>(item, "Command");
                ButtonItem subItem = new ButtonItem(GetSubItemName(i), text)
                {
                    CommandParameter = cmdParam,
                    Command = _cmd
                };
                if (itemCmd != null)
                {
                    subItem.Command = itemCmd;
                    itemCmd.Executed += new EventHandler(_cmd_Executed);
                }
                this.SubItems.Add(subItem);
                if (i == selectedIndex)
                    defItem = subItem;
            }
            _cmd.Executed += new EventHandler(_cmd_Executed);
            _dataSource = dataSource;
            this.AutoExpandOnClick = SubItems.Count > 0;
            if (defItem != null)
                _cmd.Execute(defItem);
        }

        void _cmd_Executed(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            int index = _dataSource.IndexOf(sender);
            if (index > -1)
                SelectedIndex = index;
            else
            {
                IList subItems = SubItems as IList;
                index = subItems.IndexOf(sender);
                SelectedIndex = index;
            }
        }
        private void CheckedItem(ButtonItem subItem)
        {
            foreach (ButtonItem item in this.SubItems)
            {
                item.Checked = String.CompareOrdinal(item.Name, subItem.Name) == 0;
            }
            _selectedItem = subItem;
        }
        protected override void OnClick( )
        {
            //base.OnClick( );
        }
        private string GetSubItemName(int i)
        {
            return string.Format("{0}_{1}", Name, i);
        }
        //private 
    }
}
