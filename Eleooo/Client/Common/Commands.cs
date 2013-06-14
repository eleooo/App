using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro.ColorTables;

namespace Eleooo.Client
{
    public class Commands
    {
        private static Command _cmdThemes;
        public static Command CmdThemes
        {
            get
            {
                if (_cmdThemes == null)
                {
                    _cmdThemes = new Command( );
                    _cmdThemes.Executed += new EventHandler(CmdThemes_Executed);
                }
                return _cmdThemes;
            }
        }


        private static Command _cmdShowPanel;
        public static Command CmdShowPanel
        {
            get
            {
                if (_cmdShowPanel == null)
                {
                    _cmdShowPanel = new Command( );
                    _cmdShowPanel.Executed += new EventHandler(CmdShowPanel_Executed);
                }
                return _cmdShowPanel;
            }
        }


        private static Command _cmdClosePanel;
        public static Command CmdClosePanel
        {
            get
            {
                if (_cmdClosePanel == null)
                {
                    _cmdClosePanel = new Command( );
                    _cmdClosePanel.Executed += new EventHandler(CmdClosePanel_Executed);
                }
                return _cmdClosePanel;
            }
        }

        private static Command _cmdCloseApp;
        public static Command CmdCloseApp
        {
            get
            {
                if (_cmdCloseApp == null)
                {
                    _cmdCloseApp = new Command( );
                    _cmdCloseApp.Executed += new EventHandler(CmdCloseApp_Executed);
                }
                return _cmdCloseApp;
            }
        }

        private static Command _cmdRefreshInfo;
        public static Command CmdRefreshInfo
        {
            get
            {
                if (_cmdRefreshInfo == null)
                {
                    _cmdRefreshInfo = new Command( );
                    _cmdRefreshInfo.Executed += new EventHandler(CmdRefreshInfo_Executed);
                }
                return _cmdRefreshInfo;
            }
        }

        private static Command _cmdEleooo;
        public static Command CmdEleooo
        {
            get
            {
                if (_cmdEleooo == null)
                {
                    _cmdEleooo = new Command( );
                    _cmdEleooo.Executed += new EventHandler(CmdEleooo_Executed);
                }
                return _cmdEleooo;
            }
        }

        static void CmdEleooo_Executed(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ServiceProvider.Host);
        }

        static void CmdRefreshInfo_Executed(object sender, EventArgs e)
        {
            PageManager.Home.SetFoucs( );
        }

        static void CmdCloseApp_Executed(object sender, EventArgs e)
        {
            MainMetroForm.Instance.Close( );
        }

        static void CmdClosePanel_Executed(object sender, EventArgs e)
        {
            if (MainMetroForm.Instance.ActivateChild != null)
            {
                CmdRefreshInfo_Executed(sender, e);
                MainMetroForm.Instance.CloseModalPanel(MainMetroForm.Instance.ActivateChild, MainMetroForm.FROM_SLIDESIDE);
                MainMetroForm.Instance.ActivateChild = null;
            }
        }

        static void CmdShowPanel_Executed(object sender, EventArgs e)
        {
            UserControlBase child = GetSenderParamter(sender);
            if (child == null || MainMetroForm.Instance.ActivateChild == child)
                return;
            MainMetroForm.Instance.ShowModalPanel(child, MainMetroForm.FROM_SLIDESIDE);
            MainMetroForm.Instance.ActivateChild = child;
            child.SetFoucs( );
        }
        static void CmdThemes_Executed(object sender, EventArgs e)
        {
            ButtonItem source = (ButtonItem)sender;
            MetroColorGeneratorParameters theme = (MetroColorGeneratorParameters)source.CommandParameter;
            StyleManager.MetroColorGeneratorParameters = theme;
        }
        private static UserControlBase GetSenderParamter(object sender)
        {
            return TypeHelper.GetPropertyValue(sender, "CommandParameter") as UserControlBase;
        }
    }
}
