using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Eleooo.Client
{
    /// <summary>
    /// Win32 常用API定义
    /// </summary>
    public static class WinApi
    {
        #region Message Const
        //wMsg参数常量值：
        //WM_KEYDOWN 按下一个键
        public static int WM_KEYDOWN = 0x0100;
        //释放一个键
        public static int WM_KEYUP = 0x0101;
        //按下某键，并已发出WM_KEYDOWN， WM_KEYUP消息
        public static int WM_CHAR = 0x102;
        //当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口
        public static int WM_DEADCHAR = 0x103;
        //当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口
        public static int WM_SYSKEYDOWN = 0x104;
        //当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口
        public static int WM_SYSKEYUP = 0x105;
        //当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后提交此消息给拥有焦点的窗口
        public static int WM_SYSCHAR = 0x106;
        //当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后发送此消息给拥有焦点的窗口
        public static int WM_SYSDEADCHAR = 0x107;
        //在一个对话框程序被显示前发送此消息给它，通常用此消息初始化控件和执行其它任务
        public static int WM_INITDIALOG = 0x110;
        //当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译
        public static int WM_COMMAND = 0x111;
        //当用户选择窗口菜单的一条命令或//当用户选择最大化或最小化时那个窗口会收到此消息
        public static int WM_SYSCOMMAND = 0x112;
        //发生了定时器事件
        public static int WM_TIMER = 0x113;
        //当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件
        public static int WM_HSCROLL = 0x114;
        //当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口也，发送给拥有它的控件
        public static int WM_VSCROLL = 0x115;
        //当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单
        public static int WM_INITMENU = 0x116;
        //当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要改变全部
        public static int WM_INITMENUPOPUP = 0x117;
        //当用户选择一条菜单项时发送此消息给菜单的所有者（一般是窗口）
        public static int WM_MENUSELECT = 0x11F;
        //当菜单已被激活用户按下了某个键（不同于加速键），发送此消息给菜单的所有者
        public static int WM_MENUCHAR = 0x120;
        //当一个模态对话框或菜单进入空载状态时发送此消息给它的所有者，一个模态对话框或菜单进入空载状态就是在处理完一条或几条先前的消息后没有消息它的列队中等待
        public static int WM_ENTERIDLE = 0x121;
        //在windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色
        public static int WM_CTLCOLORMSGBOX = 0x132;
        //当一个编辑型控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色
        public static int WM_CTLCOLOREDIT = 0x133;

        //当一个列表框控件将要被绘制前发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色
        public static int WM_CTLCOLORLISTBOX = 0x134;
        //当一个按钮控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色
        public static int WM_CTLCOLORBTN = 0x135;
        //当一个对话框控件将要被绘制前发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色
        public static int WM_CTLCOLORDLG = 0x136;
        //当一个滚动条控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色
        public static int WM_CTLCOLORSCROLLBAR = 0x137;
        //当一个静态控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以 通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色
        public static int WM_CTLCOLORSTATIC = 0x138;
        //当鼠标轮子转动时发送此消息个当前有焦点的控件
        public static int WM_MOUSEWHEEL = 0x20A;
        //双击鼠标中键
        public static int WM_MBUTTONDBLCLK = 0x209;
        //释放鼠标中键
        public static int WM_MBUTTONUP = 0x208;
        //移动鼠标时发生，同WM_MOUSEFIRST
        public static int WM_MOUSEMOVE = 0x200;
        //按下鼠标左键
        public static int WM_LBUTTONDOWN = 0x201;
        //释放鼠标左键
        public static int WM_LBUTTONUP = 0x202;
        //双击鼠标左键
        public static int WM_LBUTTONDBLCLK = 0x203;
        //按下鼠标右键
        public static int WM_RBUTTONDOWN = 0x204;
        //释放鼠标右键
        public static int WM_RBUTTONUP = 0x205;
        //双击鼠标右键
        public static int WM_RBUTTONDBLCLK = 0x206;
        //按下鼠标中键
        public static int WM_MBUTTONDOWN = 0x207;

        public static int WM_USER = 0x0400;
        public static int MK_LBUTTON = 0x0001;
        public static int MK_RBUTTON = 0x0002;
        public static int MK_SHIFT = 0x0004;
        public static int MK_CONTROL = 0x0008;
        public static int MK_MBUTTON = 0x0010;
        public static int MK_XBUTTON1 = 0x0020;
        public static int MK_XBUTTON2 = 0x0040;
        //创建一个窗口
        public static int WM_CREATE = 0x01;
        //当一个窗口被破坏时发送
        public static int WM_DESTROY = 0x02;
        //移动一个窗口
        public static int WM_MOVE = 0x03;
        //改变一个窗口的大小
        public static int WM_SIZE = 0x05;
        //一个窗口被激活或失去激活状态
        public static int WM_ACTIVATE = 0x06;
        //一个窗口获得焦点
        public static int WM_SETFOCUS = 0x07;
        //一个窗口失去焦点
        public static int WM_KILLFOCUS = 0x08;
        //一个窗口改变成Enable状态
        public static int WM_ENABLE = 0x0A;
        //设置窗口是否能重画
        public static int WM_SETREDRAW = 0x0B;
        //应用程序发送此消息来设置一个窗口的文本
        public static int WM_SETTEXT = 0x0C;
        //应用程序发送此消息来复制对应窗口的文本到缓冲区
        public static int WM_GETTEXT = 0x0D;
        //得到与一个窗口有关的文本的长度（不包含空字符）
        public static int WM_GETTEXTLENGTH = 0x0E;
        //要求一个窗口重画自己
        public static int WM_PAINT = 0x0F;
        //当一个窗口或应用程序要关闭时发送一个信号
        public static int WM_CLOSE = 0x10;
        //当用户选择结束对话框或程序自己调用ExitWindows函数
        public static int WM_QUERYENDSESSION = 0x11;
        //用来结束程序运行
        public static int WM_QUIT = 0x12;
        //当用户窗口恢复以前的大小位置时，把此消息发送给某个图标
        public static int WM_QUERYOPEN = 0x13;
        //当窗口背景必须被擦除时（例在窗口改变大小时）
        public static int WM_ERASEBKGND = 0x14;
        //当系统颜色改变时，发送此消息给所有顶级窗口
        public static int WM_SYSCOLORCHANGE = 0x15;
        //当系统进程发出WM_QUERYENDSESSION消息后，此消息发送给应用程序，通知它对话是否结束
        public static int WM_ENDSESSION = 0x16;
        //当隐藏或显示窗口是发送此消息给这个窗口
        public static int WM_SHOWWINDOW = 0x18;
        //发此消息给应用程序哪个窗口是激活的，哪个是非激活的
        public static int WM_ACTIVATEAPP = 0x1C;
        //当系统的字体资源库变化时发送此消息给所有顶级窗口
        public static int WM_FONTCHANGE = 0x1D;
        //当系统的时间变化时发送此消息给所有顶级窗口
        public static int WM_TIMECHANGE = 0x1E;
        //发送此消息来取消某种正在进行的摸态（操作）
        public static int WM_CANCELMODE = 0x1F;
        //如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，就发消息给某个窗口
        public static int WM_SETCURSOR = 0x20;
        //当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给//当前窗口
        public static int WM_MOUSEACTIVATE = 0x21;
        //发送此消息给MDI子窗口//当用户点击此窗口的标题栏，或//当窗口被激活，移动，改变大小
        public static int WM_CHILDACTIVATE = 0x22;
        //此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的hook程序分离出用户输入消息
        public static int WM_QUEUESYNC = 0x23;
        //此消息发送给窗口当它将要改变大小或位置
        public static int WM_GETMINMAXINFO = 0x24;
        //发送给最小化窗口当它图标将要被重画
        public static int WM_PAINTICON = 0x26;
        //此消息发送给某个最小化窗口，仅//当它在画图标前它的背景必须被重画
        public static int WM_ICONERASEBKGND = 0x27;
        //发送此消息给一个对话框程序去更改焦点位置
        public static int WM_NEXTDLGCTL = 0x28;
        //每当打印管理列队增加或减少一条作业时发出此消息
        public static int WM_SPOOLERSTATUS = 0x2A;
        //当button，combobox，listbox，menu的可视外观改变时发送
        public static int WM_DRAWITEM = 0x2B;
        //当button, combo box, list box, list view control, or menu item 被创建时
        public static int WM_MEASUREITEM = 0x2C;
        //此消息有一个LBS_WANTKEYBOARDINPUT风格的发出给它的所有者来响应WM_KEYDOWN消息
        public static int WM_VKEYTOITEM = 0x2E;
        //此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息
        public static int WM_CHARTOITEM = 0x2F;
        //当绘制文本时程序发送此消息得到控件要用的颜色
        public static int WM_SETFONT = 0x30;
        //应用程序发送此消息得到当前控件绘制文本的字体
        public static int WM_GETFONT = 0x31;
        //应用程序发送此消息让一个窗口与一个热键相关连
        public static int WM_SETHOTKEY = 0x32;
        //应用程序发送此消息来判断热键与某个窗口是否有关联
        public static int WM_GETHOTKEY = 0x33;
        //此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标，应用程序能返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标
        public static int WM_QUERYDRAGICON = 0x37;
        //发送此消息来判定combobox或listbox新增加的项的相对位置
        public static int WM_COMPAREITEM = 0x39;
        //显示内存已经很少了
        public static int WM_COMPACTING = 0x41;
        //发送此消息给那个窗口的大小和位置将要被改变时，来调用setwindowpos函数或其它窗口管理函数
        public static int WM_WINDOWPOSCHANGING = 0x46;
        //发送此消息给那个窗口的大小和位置已经被改变时，来调用setwindowpos函数或其它窗口管理函数
        public static int WM_WINDOWPOSCHANGED = 0x47;
        //当系统将要进入暂停状态时发送此消息
        public static int WM_POWER = 0x48;
        //当一个应用程序传递数据给另一个应用程序时发送此消息
        public static int WM_COPYDATA = 0x4A;
        //当某个用户取消程序日志激活状态，提交此消息给程序
        public static int WM_CANCELJOURNA = 0x4B;
        //当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口
        public static int WM_NOTIFY = 0x4E;
        //当用户选择某种输入语言，或输入语言的热键改变
        public static int WM_INPUTLANGCHANGEREQUEST = 0x50;
        //当平台现场已经被改变后发送此消息给受影响的最顶级窗口
        public static int WM_INPUTLANGCHANGE = 0x51;
        //当程序已经初始化windows帮助例程时发送此消息给应用程序
        public static int WM_TCARD = 0x52;
        //此消息显示用户按下了F1，如果某个菜单是激活的，就发送此消息个此窗口关联的菜单，否则就发送给有焦点的窗口，如果//当前都没有焦点，就把此消息发送给//当前激活的窗口
        public static int WM_HELP = 0x53;
        //当用户已经登入或退出后发送此消息给所有的窗口，//当用户登入或退出时系统更新用户的具体设置信息，在用户更新设置时系统马上发送此消息
        public static int WM_USERCHANGED = 0x54;
        //公用控件，自定义控件和他们的父窗口通过此消息来判断控件是使用ANSI还是UNICODE结构
        public static int WM_NOTIFYFORMAT = 0x55;
        //当用户某个窗口中点击了一下右键就发送此消息给这个窗口
        //public static int WM_CONTEXTMENU = ??;
        //当调用SETWINDOWLONG函数将要改变一个或多个 窗口的风格时发送此消息给那个窗口
        public static int WM_STYLECHANGING = 0x7C;
        //当调用SETWINDOWLONG函数一个或多个 窗口的风格后发送此消息给那个窗口
        public static int WM_STYLECHANGED = 0x7D;
        //当显示器的分辨率改变后发送此消息给所有的窗口
        public static int WM_DISPLAYCHANGE = 0x7E;
        //此消息发送给某个窗口来返回与某个窗口有关连的大图标或小图标的句柄
        public static int WM_GETICON = 0x7F;
        //程序发送此消息让一个新的大图标或小图标与某个窗口关联
        public static int WM_SETICON = 0x80;
        //当某个窗口第一次被创建时，此消息在WM_CREATE消息发送前发送
        public static int WM_NCCREATE = 0x81;
        //此消息通知某个窗口，非客户区正在销毁
        public static int WM_NCDESTROY = 0x82;
        //当某个窗口的客户区域必须被核算时发送此消息
        public static int WM_NCCALCSIZE = 0x83;
        //移动鼠标，按住或释放鼠标时发生
        public static int WM_NCHITTEST = 0x84;
        //程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时
        public static int WM_NCPAINT = 0x85;
        //此消息发送给某个窗口仅当它的非客户区需要被改变来显示是激活还是非激活状态
        public static int WM_NCACTIVATE = 0x86;
        //发送此消息给某个与对话框程序关联的控件，widdows控制方位键和TAB键使输入进入此控件通过应
        public static int WM_GETDLGCODE = 0x87;
        //当光标在一个窗口的非客户区内移动时发送此消息给这个窗口 非客户区为：窗体的标题栏及窗 的边框体
        public static int WM_NCMOUSEMOVE = 0xA0;
        //当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息
        public static int WM_NCLBUTTONDOWN = 0xA1;
        //当用户释放鼠标左键同时光标某个窗口在非客户区十发送此消息
        public static int WM_NCLBUTTONUP = 0xA2;
        //当用户双击鼠标左键同时光标某个窗口在非客户区十发送此消息
        public static int WM_NCLBUTTONDBLCLK = 0xA3;
        //当用户按下鼠标右键同时光标又在窗口的非客户区时发送此消息
        public static int WM_NCRBUTTONDOWN = 0xA4;
        //当用户释放鼠标右键同时光标又在窗口的非客户区时发送此消息
        public static int WM_NCRBUTTONUP = 0xA5;
        //当用户双击鼠标右键同时光标某个窗口在非客户区十发送此消息
        public static int WM_NCRBUTTONDBLCLK = 0xA6;
        //当用户按下鼠标中键同时光标又在窗口的非客户区时发送此消息
        public static int WM_NCMBUTTONDOWN = 0xA7;
        //当用户释放鼠标中键同时光标又在窗口的非客户区时发送此消息
        public static int WM_NCMBUTTONUP = 0xA8;
        //当用户双击鼠标中键同时光标又在窗口的非客户区时发送此消息
        public static int WM_NCMBUTTONDBLCLK = 0xA9;
        #endregion

        #region User32.dll  struct
        public struct Win32
        {
            public const string TOOLBARCLASSNAME = "ToolbarWindow32";

            public const int WS_CHILD = 0x40000000;
            public const int WS_VISIBLE = 0x10000000;
            public const int WS_CLIPCHILDREN = 0x2000000;
            public const int WS_CLIPSIBLINGS = 0x4000000;
            public const int WS_BORDER = 0x800000;

            public const int CCS_NODIVIDER = 0x40;
            public const int CCS_NORESIZE = 0x4;
            public const int CCS_NOPARENTALIGN = 0x8;

            public const int I_IMAGECALLBACK = -1;
            public const int I_IMAGENONE = -2;

            public const int TBSTYLE_TOOLTIPS = 0x100;
            public const int TBSTYLE_FLAT = 0x800;
            public const int TBSTYLE_LIST = 0x1000;
            public const int TBSTYLE_TRANSPARENT = 0x8000;

            public const int TBSTYLE_EX_DRAWDDARROWS = 0x1;
            public const int TBSTYLE_EX_HIDECLIPPEDBUTTONS = 0x10;
            public const int TBSTYLE_EX_DOUBLEBUFFER = 0x80;

            public const int CDRF_DODEFAULT = 0x0;
            public const int CDRF_SKIPDEFAULT = 0x4;
            public const int CDRF_NOTIFYITEMDRAW = 0x20;
            public const int CDDS_PREPAINT = 0x1;
            public const int CDDS_ITEM = 0x10000;
            public const int CDDS_ITEMPREPAINT = CDDS_ITEM | CDDS_PREPAINT;

            public const int CDIS_HOT = 0x40;
            public const int CDIS_SELECTED = 0x1;
            public const int CDIS_DISABLED = 0x4;

            public const int WM_SETREDRAW = 0x000B;
            public const int WM_CANCELMODE = 0x001F;
            public const int WM_NOTIFY = 0x4e;
            public const int WM_KEYDOWN = 0x100;
            public const int WM_KEYUP = 0x101;
            public const int WM_CHAR = 0x0102;
            public const int WM_SYSKEYDOWN = 0x104;
            public const int WM_SYSKEYUP = 0x105;
            public const int WM_COMMAND = 0x111;
            public const int WM_MENUCHAR = 0x120;
            public const int WM_MOUSEMOVE = 0x200;
            public const int WM_LBUTTONDOWN = 0x201;
            public const int WM_MOUSELAST = 0x20a;
            public const int WM_USER = 0x0400;
            public const int WM_REFLECT = WM_USER + 0x1c00;

            public const int NM_CUSTOMDRAW = -12;

            public const int TTN_NEEDTEXTA = ((0 - 520) - 0);
            public const int TTN_NEEDTEXTW = ((0 - 520) - 10);

            public const int TBN_QUERYINSERT = ((0 - 700) - 6);
            public const int TBN_DROPDOWN = ((0 - 700) - 10);
            public const int TBN_HOTITEMCHANGE = ((0 - 700) - 13);

            public const int TBIF_IMAGE = 0x1;
            public const int TBIF_TEXT = 0x2;
            public const int TBIF_STATE = 0x4;
            public const int TBIF_STYLE = 0x8;
            public const int TBIF_COMMAND = 0x20;

            public const int MNC_EXECUTE = 2;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public class INITCOMMONCONTROLSEX
            {
                public int dwSize = 8;
                public int dwICC;
            }

            public const int ICC_BAR_CLASSES = 4;
            public const int ICC_COOL_CLASSES = 0x400;

            [DllImport("comctl32.dll")]
            public static extern bool InitCommonControlsEx(INITCOMMONCONTROLSEX icc);

            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int x;
                public int y;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct NMHDR
            {
                public IntPtr hwndFrom;
                public int idFrom;
                public int code;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct NMTOOLBAR
            {
                public NMHDR hdr;
                public int iItem;
                public TBBUTTON tbButton;
                public int cchText;
                public IntPtr pszText;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct NMCUSTOMDRAW
            {
                public NMHDR hdr;
                public int dwDrawStage;
                public IntPtr hdc;
                public RECT rc;
                public int dwItemSpec;
                public int uItemState;
                public int lItemlParam;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct LPNMTBCUSTOMDRAW
            {
                public NMCUSTOMDRAW nmcd;
                public IntPtr hbrMonoDither;
                public IntPtr hbrLines;
                public IntPtr hpenLines;
                public int clrText;
                public int clrMark;
                public int clrTextHighlight;
                public int clrBtnFace;
                public int clrBtnHighlight;
                public int clrHighlightHotTrack;
                public RECT rcText;
                public int nStringBkMode;
                public int nHLStringBkMode;
            }

            public const int TTF_RTLREADING = 0x0004;

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct TOOLTIPTEXT
            {
                public NMHDR hdr;
                public IntPtr lpszText;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szText;
                public IntPtr hinst;
                public int uFlags;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct TOOLTIPTEXTA
            {
                public NMHDR hdr;
                public IntPtr lpszText;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szText;
                public IntPtr hinst;
                public int uFlags;
            }

            public const int TB_PRESSBUTTON = WM_USER + 3;
            public const int TB_INSERTBUTTON = WM_USER + 21;
            public const int TB_BUTTONCOUNT = WM_USER + 24;
            public const int TB_GETITEMRECT = WM_USER + 29;
            public const int TB_BUTTONSTRUCTSIZE = WM_USER + 30;
            public const int TB_SETBUTTONSIZE = WM_USER + 32;
            public const int TB_SETIMAGELIST = WM_USER + 48;
            public const int TB_GETRECT = WM_USER + 51;
            public const int TB_SETBUTTONINFO = WM_USER + 64;
            public const int TB_HITTEST = WM_USER + 69;
            public const int TB_GETHOTITEM = WM_USER + 71;
            public const int TB_SETHOTITEM = WM_USER + 72;
            public const int TB_SETEXTENDEDSTYLE = WM_USER + 84;

            public const int TBSTATE_CHECKED = 0x01;
            public const int TBSTATE_ENABLED = 0x04;
            public const int TBSTATE_HIDDEN = 0x08;

            public const int BTNS_BUTTON = 0;
            public const int BTNS_SEP = 0x1;
            public const int BTNS_DROPDOWN = 0x8;
            public const int BTNS_AUTOSIZE = 0x10;
            public const int BTNS_WHOLEDROPDOWN = 0x80;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct TBBUTTON
            {
                public int iBitmap;
                public int idCommand;
                public byte fsState;
                public byte fsStyle;
                public byte bReserved0;
                public byte bReserved1;
                public int dwData;
                public int iString;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct TBBUTTONINFO
            {
                public int cbSize;
                public int dwMask;
                public int idCommand;
                public int iImage;
                public byte fsState;
                public byte fsStyle;
                public short cx;
                public IntPtr lParam;
                public IntPtr pszText;
                public int cchText;
            }

            public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

            public const int WH_MSGFILTER = -1;
            public const int MSGF_MENU = 2;
            public const int SPI_GETFLATMENU = 0x1022;


            [StructLayout(LayoutKind.Sequential)]
            public struct MSG
            {
                public IntPtr hwnd;
                public int message;
                public IntPtr wParam;
                public IntPtr lParam;
                public int time;
                public int pt_x;
                public int pt_y;
            }

            public const string REBARCLASSNAME = "ReBarWindow32";

            public const int RBS_VARHEIGHT = 0x200;
            public const int RBS_BANDBORDERS = 0x400;
            public const int RBS_AUTOSIZE = 0x2000;

            public const int RBN_FIRST = -831;
            public const int RBN_HEIGHTCHANGE = RBN_FIRST - 0;
            public const int RBN_AUTOSIZE = RBN_FIRST - 3;
            public const int RBN_CHEVRONPUSHED = RBN_FIRST - 10;

            public const int RB_SETBANDINFO = WM_USER + 6;
            public const int RB_GETRECT = WM_USER + 9;
            public const int RB_INSERTBAND = WM_USER + 10;
            public const int RB_GETBARHEIGHT = WM_USER + 27;

            [StructLayout(LayoutKind.Sequential)]
            public struct REBARBANDINFO
            {
                public int cbSize;
                public int fMask;
                public int fStyle;
                public int clrFore;
                public int clrBack;
                public IntPtr lpText;
                public int cch;
                public int iImage;
                public IntPtr hwndChild;
                public int cxMinChild;
                public int cyMinChild;
                public int cx;
                public IntPtr hbmBack;
                public int wID;
                public int cyChild;
                public int cyMaxChild;
                public int cyIntegral;
                public int cxIdeal;
                public int lParam;
                public int cxHeader;
            }

            public const int RBBIM_CHILD = 0x10;
            public const int RBBIM_CHILDSIZE = 0x20;
            public const int RBBIM_STYLE = 0x1;
            public const int RBBIM_ID = 0x100;
            public const int RBBIM_SIZE = 0x40;
            public const int RBBIM_IDEALSIZE = 0x200;
            public const int RBBIM_TEXT = 0x4;

            public const int RBBS_BREAK = 0x1;
            public const int RBBS_CHILDEDGE = 0x4;
            public const int RBBS_FIXEDBMP = 0x20;
            public const int RBBS_GRIPPERALWAYS = 0x80;
            public const int RBBS_USECHEVRON = 0x200;

            [StructLayout(LayoutKind.Sequential)]
            public struct NMREBARCHEVRON
            {
                public NMHDR hdr;
                public int uBand;
                public int wID;
                public int lParam;
                public RECT rc;
                public int lParamNM;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct IMAGELISTDRAWPARAMS
            {
                public int cbSize;
                public IntPtr himl;
                public int i;
                public IntPtr hdcDst;
                public int x;
                public int y;
                public int cx;
                public int cy;
                public int xBitmap;
                public int yBitmap;
                public int rgbBk;
                public int rgbFg;
                public int fStyle;
                public int dwRop;
                public int fState;
                public int Frame;
                public int crEffect;
            }

            public const int ILD_TRANSPARENT = 0x1;
            public const int ILS_SATURATE = 0x4;

            [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
            public static extern bool ImageList_DrawIndirect(ref IMAGELISTDRAWPARAMS pimldp);

            [StructLayout(LayoutKind.Sequential)]
            public struct DLLVERSIONINFO
            {
                public int cbSize;
                public int dwMajorVersion;
                public int dwMinorVersion;
                public int dwBuildNumber;
                public int dwPlatformID;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct PAINTSTRUCT
            {
                private IntPtr hdc;
                public bool fErase;
                public RECT rcPaint;
                public bool fRestore;
                public bool fIncUpdate;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
                public byte[] rgbReserved;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct SIZE
            {
                public Int32 cx;
                public Int32 cy;

                public SIZE(Int32 cx, Int32 cy)
                {
                    this.cx = cx;
                    this.cy = cy;
                }
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct BLENDFUNCTION
            {
                public byte BlendOp;
                public byte BlendFlags;
                public byte SourceConstantAlpha;
                public byte AlphaFormat;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct SCROLLINFO
            {
                public uint cbSize;
                public uint fMask;
                public int nMin;
                public int nMax;
                public uint nPage;
                public int nPos;
                public int nTrackPos;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct WINDOWPLACEMENT
            {
                public int length;
                public int flags;
                public int showCmd;
                public POINT minPosition;
                public POINT maxPosition;
                public RECT normalPosition;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct TRACKMOUSEEVENT
            {
                public int cbSize;
                public int dwFlags;
                public IntPtr hwndTrack;
                public int dwHoverTime;

            }

            [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
            public struct TVITEM
            {
                public uint mask;
                public IntPtr hItem;
                public uint state;
                public uint stateMask;
                public IntPtr pszText;
                public int cchTextMax;
                public int iImage;
                public int iSelectedImage;
                public int cChildren;
                public IntPtr lParam;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct LVITEM
            {
                public int mask;
                public int iItem;
                public int iSubItem;
                public int state;
                public int stateMask;
                public IntPtr pszText;
                public int cchTextMax;
                public int iImage;
                public IntPtr lParam;
                public int iIndent;
                public int iGroupId;
                public int cColumns;
                public IntPtr puColumns;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct HDITEM
            {
                public uint mask;
                public int cxy;
                public IntPtr pszText;
                public IntPtr hbm;
                public int cchTextMax;
                public int fmt;
                public IntPtr lParam;
                public int iImage;
                public int iOrder;
                public uint type;
                public IntPtr pvFilter;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct HD_HITTESTINFO
            {
                POINT pt;
                uint flags;
                int iItem;
            }
        }

        #endregion User32.dll  struct

        #region User32.dll 函数
        /// <summary>
        /// 该函数检索一指定窗口的客户区域或整个屏幕的显示设备上下文环境的句柄，以后可以在GDI函数中使用该句柄来在设备上下文环境中绘图。hWnd：设备上下文环境被检索的窗口的句柄
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetDC", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);
        /// <summary>
        /// 函数释放设备上下文环境（DC）供其他应用程序使用。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ReleaseDC", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        /// <summary>
        /// 该函数返回桌面窗口的句柄。桌面窗口覆盖整个屏幕。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", SetLastError = true)]
        static public extern IntPtr GetDesktopWindow();
        /// <summary>
        /// 该函数设置指定窗口的显示状态。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        static public extern bool ShowWindow(IntPtr hWnd, short State);
        /// <summary>
        /// 通过发送重绘消息 WM_PAINT 给目标窗体来更新目标窗体客户区的无效区域。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "UpdateWindow", SetLastError = true)]
        static public extern bool UpdateWindow(IntPtr hWnd);
        /// <summary>
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
        static public extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// 该函数改变一个子窗口，弹出式窗口式顶层窗口的尺寸，位置和Z序。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
        static public extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, uint flags);
        /// <summary>
        /// 打开剪切板
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "OpenClipboard", SetLastError = true)]
        static public extern bool OpenClipboard(IntPtr hWndNewOwner);
        /// <summary>
        /// 关闭剪切板
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "CloseClipboard", SetLastError = true)]
        static public extern bool CloseClipboard();
        /// <summary>
        /// 打开清空</summary>
        [DllImport("user32.dll", EntryPoint = "EmptyClipboard", SetLastError = true)]
        static public extern bool EmptyClipboard();
        /// <summary>
        /// 将存放有数据的内存块放入剪切板的资源管理中
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetClipboardData", SetLastError = true)]
        static public extern IntPtr SetClipboardData(uint Format, IntPtr hData);
        /// <summary>
        /// 显示一个MessageBox
        /// </summary>  
        [DllImport("user32.dll", EntryPoint = "MessageBox", SetLastError = true)]
        public static extern IntPtr MessageBox(int hWnd, String text,String caption, uint type);
        /// <summary>
        /// 在一个矩形中装载指定菜单条目的屏幕坐标信息 
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetMenuItemRect", SetLastError = true)]
        static public extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint Item, ref  Win32.RECT rc);
        /// <summary>
        /// 该函数获得一个指定子窗口的父窗口句柄。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetParent", SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);
        /// <summary>
        /// 该函数将指定的消息发送到一个或多个窗口。此函数为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回。　
        /// </summary>
        /// <param name="hWnd">其窗口程序将接收消息的窗口的句柄</param>
        /// <param name="msg">指定被发送的消息</param>
        /// <param name="wParam">指定附加的消息指定信息</param>
        /// <param name="lParam">指定附加的消息指定信息</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref Win32.RECT lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref Win32.POINT lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref Win32.TBBUTTON lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref Win32.TBBUTTONINFO lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref Win32.REBARBANDINFO lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref Win32.TVITEM lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref Win32.LVITEM lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref Win32.HDITEM lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref Win32.HD_HITTESTINFO hti);
        /// <summary>
        /// 该函数将一个消息放入（寄送）到与指定窗口创建的线程相联系消息队列里
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "PostMessage", SetLastError = true)]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "SetWindowsHookEx", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int hookid, Win32.HookProc pfnhook, IntPtr hinst, int threadid);

        [DllImport("user32.dll", EntryPoint = "UnhookWindowsHookEx", SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhook);

        [DllImport("user32.dll", EntryPoint = "CallNextHookEx", SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wparam, IntPtr lparam);
        /// <summary>
        /// 该函数对指定的窗口设置键盘焦点。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetFocus", SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        /// <summary>
        /// 该函数在指定的矩形里写入格式化文本，根据指定的方法对文本格式化（扩展的制表符，字符对齐、折行等）。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "DrawText", SetLastError = true)]
        public extern static int DrawText(IntPtr hdc, string lpString, int nCount, ref Win32.RECT lpRect, int uFormat);
        /// <summary>
        /// 该函数改变指定子窗口的父窗口。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetParent", SetLastError = true)]
        public extern static IntPtr SetParent(IntPtr hChild, IntPtr hParent);
        /// <summary>
        /// 获取对话框中子窗口控件的句柄
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetDlgItem", SetLastError = true)]
        public extern static IntPtr GetDlgItem(IntPtr hDlg, int nControlID);
        /// <summary>
        /// 该函数获取窗口客户区的坐标。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetClientRect", SetLastError = true)]
        public extern static int GetClientRect(IntPtr hWnd, ref Win32.RECT rc);
        /// <summary>
        /// 该函数向指定的窗体添加一个矩形，然后窗口客户区域的这一部分将被重新绘制。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "InvalidateRect", SetLastError = true)]
        public extern static int InvalidateRect(IntPtr hWnd, IntPtr rect, int bErase);
        /// <summary>
        /// 该函数产生对其他线程的控制，如果一个线程没有其他消息在其消息队列里。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "WaitMessage", SetLastError = true)]
        public static extern bool WaitMessage();
        /// <summary>
        /// 该函数为一个消息检查线程消息队列，并将该消息（如果存在）放于指定的结构。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "PeekMessage", SetLastError = true)]
        public static extern bool PeekMessage(ref Win32.MSG msg, int hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);
        /// <summary>
        /// 该函数从调用线程的消息队列里取得一个消息并将其放于指定的结构。此函数可取得与指定窗口联系的消息和由PostThreadMesssge寄送的线程消息。此函数接收一定范围的消息值。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetMessage", SetLastError = true)]
        public static extern bool GetMessage(ref Win32.MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);
        /// <summary>
        /// 该函数将虚拟键消息转换为字符消息。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "TranslateMessage", SetLastError = true)]
        public static extern bool TranslateMessage(ref Win32.MSG msg);
        /// <summary>
        /// 该函数调度一个消息给窗口程序。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "DispatchMessage", SetLastError = true)]
        public static extern bool DispatchMessage(ref Win32.MSG msg);
        /// <summary>
        /// 该函数从一个与应用事例相关的可执行文件（EXE文件）中载入指定的光标资源.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "LoadCursor", SetLastError = true)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, uint cursor);
        /// <summary>
        /// 该函数确定光标的形状。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetCursor", SetLastError = true)]
        public static extern IntPtr SetCursor(IntPtr hCursor);
        /// <summary>
        /// 确定当前焦点位于哪个控件上。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetFocus", SetLastError = true)]
        public static extern IntPtr GetFocus();
        /// <summary>
        /// 该函数从当前线程中的窗口释放鼠标捕获，并恢复通常的鼠标输入处理。捕获鼠标的窗口接收所有的鼠标输入（无论光标的位置在哪里），除非点击鼠标键时，光标热点在另一个线程的窗口中。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture", SetLastError = true)]
        public static extern bool ReleaseCapture();
        /// <summary>
        /// 准备指定的窗口来重绘并将绘画相关的信息放到一个PAINTSTRUCT结构中。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "BeginPaint", SetLastError = true)]
        public static extern IntPtr BeginPaint(IntPtr hWnd, ref Win32.PAINTSTRUCT ps);
        /// <summary>
        /// 标记指定窗口的绘画过程结束,每次调用BeginPaint函数之后被请求
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "EndPaint", SetLastError = true)]
        public static extern bool EndPaint(IntPtr hWnd, ref Win32.PAINTSTRUCT ps);
        /// <summary>
        /// 半透明窗体
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "UpdateLayeredWindow", SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Win32.POINT pptDst, ref Win32.SIZE psize, IntPtr hdcSrc, ref Win32.POINT pprSrc, Int32 crKey, ref Win32.BLENDFUNCTION pblend, Int32 dwFlags);
        /// <summary>
        /// 该函数返回指定窗口的边框矩形的尺寸。该尺寸以相对于屏幕坐标左上角的屏幕坐标给出。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetWindowRect", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Win32.RECT rect);
        /// <summary>
        /// 该函数将指定点的用户坐标转换成屏幕坐标。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ClientToScreen", SetLastError = true)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Win32.POINT pt);
        /// <summary>
        /// 当在指定时间内鼠标指针离开或盘旋在一个窗口上时，此函数寄送消息。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "TrackMouseEvent", SetLastError = true)]
        public static extern bool TrackMouseEvent(ref Win32.TRACKMOUSEEVENT tme);
        /// <summary>
        /// 
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetWindowRgn", SetLastError = true)]
        public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);
        /// <summary>
        /// 该函数检取指定虚拟键的状态。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetKeyState", SetLastError = true)]
        public static extern ushort GetKeyState(int virtKey);
        /// <summary>
        /// 该函数改变指定窗口的位置和尺寸。对于顶层窗口，位置和尺寸是相对于屏幕的左上角的：对于子窗口，位置和尺寸是相对于父窗口客户区的左上角坐标的。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "MoveWindow", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
        /// <summary>
        /// 该函数获得指定窗口所属的类的类名。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetClassName", SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd, out StringBuilder ClassName, int nMaxCount);
        /// <summary>
        /// 该函数改变指定窗口的属性
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        /// <summary>
        /// 该函数检索指定窗口客户区域或整个屏幕的显示设备上下文环境的句柄，在随后的GDI函数中可以使用该句柄在设备上下文环境中绘图。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetDCEx", SetLastError = true)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRegion, uint flags);
        /// <summary>
        /// 获取整个窗口（包括边框、滚动条、标题栏、菜单等）的设备场景 返回值 Long。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetWindowDC", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        /// <summary>
        /// 该函数用指定的画刷填充矩形，此函数包括矩形的左上边界，但不包括矩形的右下边界。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "FillRect", SetLastError = true)]
        public static extern int FillRect(IntPtr hDC, ref Win32.RECT rect, IntPtr hBrush);
        /// <summary>
        /// 该函数返回指定窗口的显示状态以及被恢复的、最大化的和最小化的窗口位置。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetWindowPlacement", SetLastError = true)]
        public static extern int GetWindowPlacement(IntPtr hWnd, ref Win32.WINDOWPLACEMENT wp);
        /// <summary>
        /// 该函数改变指定窗口的标题栏的文本内容
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetWindowText", SetLastError = true)]
        public static extern int SetWindowText(IntPtr hWnd, string text);
        /// <summary>
        /// 该函数将指定窗口的标题条文本（如果存在）拷贝到一个缓存区内。如果指定的窗口是一个控制，则拷贝控制的文本。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetWindowText", SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, out StringBuilder text, int maxCount);
        /// <summary>
        /// 用于得到被定义的系统数据或者系统配置信息.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics", SetLastError = true)]
        static public extern int GetSystemMetrics(int nIndex);
        /// <summary>
        /// 该函数设置滚动条参数，包括滚动位置的最大值和最小值，页面大小，滚动按钮的位置。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SetScrollInfo", SetLastError = true)]
        static public extern int SetScrollInfo(IntPtr hwnd, int bar, ref Win32.SCROLLINFO si, int fRedraw);
        /// <summary>
        /// 该函数显示或隐藏所指定的滚动条。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ShowScrollBar", SetLastError = true)]
        public static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);
        /// <summary>
        /// 该函数可以激活一个或两个滚动条箭头或是使其失效。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "EnableScrollBar", SetLastError = true)]
        public static extern int EnableScrollBar(IntPtr hWnd, uint flags, uint arrows);
        /// <summary>
        /// 该函数将指定的窗口设置到Z序的顶部。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "BringWindowToTop", SetLastError = true)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        /// <summary>
        /// 该函数滚动指定窗体客户区域的目录。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ScrollWindowEx", SetLastError = true)]
        static public extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy, ref Win32.RECT rcScroll, ref Win32.RECT rcClip, IntPtr UpdateRegion, ref Win32.RECT rcInvalidated, uint flags);
        /// <summary>
        /// 该函数确定给定的窗口句柄是否识别一个已存在的窗口。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "IsWindow", SetLastError = true)]
        public static extern int IsWindow(IntPtr hWnd);
        /// <summary>
        /// 该函数将256个虚拟键的状态拷贝到指定的缓冲区中。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState", SetLastError = true)]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        /// <summary>
        /// 该函数将指定的虚拟键码和键盘状态翻译为相应的字符或字符串。该函数使用由给定的键盘布局句柄标识的物理键盘布局和输入语言来翻译代码。
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ToAscii", SetLastError = true)]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
        /// <summary>
        /// 显示和隐藏鼠标指针.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]
        public static extern int ShowCursor(int bShow);
        #endregion
    }
}
