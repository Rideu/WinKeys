using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinKeys.Helper;
using WinKeysС;

namespace WinKeys
{
    public partial class WinKeys : Form
    {
        static string
            keybuf = "",
            msbuf = "";

        static Point mouseOld;
        static Point mouseDelta;
        static Timer keyt = new Timer { Interval = 6 };
        static MouseHook ms_listener;
        static KeyboardHook kb_listener;

        #region Procs
        static private bool
            rmh, lmh;

        static int
            rmc, lmc;


        private static void LeftButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct) =>
            lmh = false;


        private static void LeftButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            lmc++; lmh = true;
        }

        private static void RightButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct) =>
            rmh = false;

        private static void RightButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            rmc++; rmh = true;
        }

        private static void Kb_listener_KeyDown(KeyboardHook.VKeys key)
        {
            //keybuf += key.ToString();
            //if (key == KeyboardHook.VKeys.LSHIFT)
            //    lmc++;
        }
        #endregion

        List<string> checkedWindows = new List<string>();

        public WinKeys()
        {
            InitializeComponent();
            var winkbounds = new Rectangle(Settings.Default.winkeys_bounds_point, Settings.Default.winkeys_bounds_size);
            if (winkbounds != default)
            {
                this.Bounds = winkbounds;
            }
            keyt.Tick += Keyt_Tick;
            keyt.Start();

            if (Settings.Default.savedWindows == null)
                Settings.Default.savedWindows = new StringCollection();

            checkedWindows = Settings.Default.savedWindows.OfType<string>().ToList();

            buttonRefresh.Click += (s, e) =>
            {
                var wins = GetActiveWindows().Union(checkedWindows).ToArray();
                var items = checkedWhiteListBox.Items;

                items.Clear();
                items.AddRange(wins);

                for (int i = 0; i < items.Count; i++)
                {
                    var win = items[i].ToString();
                    if (checkedWindows.Contains(win))
                    {
                        checkedWhiteListBox.SetItemChecked(i, true);
                    }
                }
            };

            kb_listener = new KeyboardHook();
            kb_listener.KeyDown += Kb_listener_KeyDown;

            ms_listener = new MouseHook();
            ms_listener.MouseMove += Ms_listener_MouseMove;
            ms_listener.LeftButtonDown += LeftButtonDown;
            ms_listener.LeftButtonUp += LeftButtonUp;
            ms_listener.RightButtonDown += RightButtonDown;
            ms_listener.RightButtonUp += RightButtonUp;

            ms_listener.Install();
            kb_listener.Install();

            ModuleHook.ActiveWindowChanged += ModuleHook_ActiveWindowChanged;
        }

        private void Ms_listener_MouseMove(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            var mouseNew = new Point(mouseStruct.pt.x, mouseStruct.pt.y);
            mouseDelta = new Point(mouseNew.X - mouseOld.X, mouseNew.Y - mouseOld.Y);
            mouseOld = mouseNew;

        }

        static List<string> GetActiveWindows()
        {
            return Process.GetProcesses().Where(n => n.MainWindowTitle.Length > 1).Select(n => n.MainWindowTitle).ToList();
        }

        void SaveWindows()
        {

            Settings.Default.savedWindows.Clear();
            Settings.Default.savedWindows.AddRange(checkedWindows.ToArray());
            Settings.Default.Save();
        }

        private void checkedWhiteListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var ci = e.Index;
            var i = checkedWhiteListBox.Items[ci].ToString();
            if (e.NewValue == CheckState.Checked && !checkedWindows.Contains(i))
                checkedWindows.Add(i);
            else if (e.NewValue == CheckState.Unchecked)
                checkedWindows.RemoveAll(n => n == i);

            SaveWindows();
        }

        private void ModuleHook_ActiveWindowChanged(object sender, string e)
        {
            Lock = !(checkedWindows.Contains(e) || e == "WinKeys");
            if (!Lock)
            {
                mousePad.SetDelta(default);
            }
        }

        public bool Lock = false;

        private void Keyt_Tick(object sender, EventArgs e)
        {

            msbuf = keybuf = labelKeys.Text = labelKeys.Text = labelKeys.Text = "";

            if (Lock) return;

            foreach (var k in kb_listener.PressedKeys)
            {
                keybuf += k.ToString().Select((n, i) => (i == 0 ? n.ToString().ToUpper() : n.ToString())).Aggregate((n, m) => n + m) + " ";
            }

            //for (int i = 0; i < 256; i++)
            //{
            //    var s = GetAsyncKeyState(i);
            //    if (s == 0 && i > 2)
            //    {
            //        keybuf += ((Keys)i).ToString() + " ";
            //    }
            //}
            if (lmh)
                msbuf += "LButton ";
            if (rmh)
                msbuf += "RButton";

            if (mouseDelta.X != 0 || mouseDelta.Y != 0)
            {
                //msbuf += $"{mouseDelta.X} {mouseDelta.Y}";
                mousePad.SetDelta(mouseDelta);
            }

            labelKeys.Text = keybuf;
            labelMouse.Text = msbuf;
        }



        private void WinKeys_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ms_listener.Uninstall();
            kb_listener.Uninstall();
            keyt.Stop();
        }


        private void WinKeys_SizeChanged(object sender, EventArgs e)
        {
            Settings.Default.winkeys_bounds_point = Bounds.Location;
            Settings.Default.winkeys_bounds_size = Bounds.Size;
            Settings.Default.Save();
        }

        private void WinKeys_Load(object sender, EventArgs e)
        {

        }

        Point lastLocation;

        private void WinHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Location = new Point((Location.X - lastLocation.X) + MousePosition.X, (Location.Y - lastLocation.Y) + MousePosition.Y);
                Location = new Point(MousePosition.X - lastLocation.X, MousePosition.Y - lastLocation.Y);
            }
        }

        private void WinHeader_MouseDown(object sender, MouseEventArgs e)
        {
            lastLocation = PointToClient(MousePosition);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private const int
        HTLEFT = 10,
        HTRIGHT = 11,
        HTTOP = 12,
        HTTOPLEFT = 13,
        HTTOPRIGHT = 14,
        HTBOTTOM = 15,
        HTBOTTOMLEFT = 16,
        HTBOTTOMRIGHT = 17;

        const int BorderSize = 5;


        Rectangle TopBorder { get { return new Rectangle(0, 0, this.ClientSize.Width, BorderSize); } }
        Rectangle BottomBorder { get { return new Rectangle(0, this.ClientSize.Height - BorderSize, this.ClientSize.Width, BorderSize); } }
        Rectangle RightBorder { get { return new Rectangle(this.ClientSize.Width - BorderSize, 0, BorderSize, this.ClientSize.Height); } }
        Rectangle LeftBorder { get { return new Rectangle(0, 0, BorderSize, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, BorderSize, BorderSize); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - BorderSize, 0, BorderSize, BorderSize); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - BorderSize, BorderSize, BorderSize); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - BorderSize, this.ClientSize.Height - BorderSize, BorderSize, BorderSize); } }


        //protected override void WndProc(ref Message message)
        //{
        //    base.WndProc(ref message);

        //    //if (message.Msg == 0x84)
        //    //{
        //    //    var cp = MousePosition;
        //    //    var cursor = this.PointToClient(cp);

        //    //    if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
        //    //    else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
        //    //    else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
        //    //    else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

        //    //    else if (TopBorder.Contains(cursor)) message.Result = (IntPtr)HTTOP;
        //    //    else if (LeftBorder.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
        //    //    else if (RightBorder.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
        //    //    else if (BottomBorder.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
        //    //}
        //}
    }
}
