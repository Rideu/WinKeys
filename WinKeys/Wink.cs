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

namespace WinKeys
{
    public partial class WinKeys : Form
    {
        static string keybuf = "";
        static Timer keyt = new Timer { Interval = 6 };
        static MouseHook ms_listener;
        static KeyboardHook kb_listener;

        #region Procs
        static private bool
            rmh, lmh;

        static int
            rmc, lmc;

        private static void Ms_listener_RightButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            rmh = false;
        }

        private static void Ms_listener_LeftButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            lmh = false;
        }

        private static void Ms_listener_RightButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            rmc++;
            rmh = true;
        }

        private static void _listener_LeftButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            lmc++;
            lmh = true;
        }

        private static void Kb_listener_KeyDown(KeyboardHook.VKeys key)
        {
            //keybuf += key.ToString();
            //if (key == KeyboardHook.VKeys.LSHIFT)
            //    lmc++;
        }
        #endregion

        IEnumerable<string> moduleWindowNames;
        bool whiteListInited;
        Timer moduleRefresher = new Timer { Interval = 5000 };

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

            moduleRefresher.Tick += ModuleRefresher_Tick;

            moduleRefresher.Start();

            if (Settings.Default.lockon_name != null)
                moduleWindowNames = Settings.Default.lockon_name.OfType<string>();

            //Timer.Tick
            {

                var procs = Process.GetProcesses().Where(n => n.MainWindowTitle.Length > 1).Select(n => n.MainWindowTitle).ToArray();
                checkedWhiteListBox.Items.AddRange(procs);

                var its = checkedWhiteListBox.Items.OfType<string>();

                if (moduleWindowNames != null)
                {
                    var ocs = moduleWindowNames.Intersect(its);

                    for (int i = 0; i < checkedWhiteListBox.Items.Count; i++)
                    {
                        if (ocs.Contains(checkedWhiteListBox.Items[i].ToString()))
                        {
                            checkedWhiteListBox.SetItemChecked(i, true);
                        }
                    }

                }
            }
            whiteListInited = true;

            //#if !DEBUG
            //ms_listener = new MouseHook();
            //ms_listener.Install();
            //ms_listener.LeftButtonDown += _listener_LeftButtonUp;
            //ms_listener.LeftButtonUp += Ms_listener_LeftButtonUp;
            //ms_listener.RightButtonDown += Ms_listener_RightButtonDown;
            //ms_listener.RightButtonUp += Ms_listener_RightButtonUp;

            kb_listener = new KeyboardHook();
            kb_listener.Install();
            kb_listener.KeyDown += Kb_listener_KeyDown;
            labelHeader.MouseMove += WinHeader_MouseMove;
            labelHeader.MouseDown += WinHeader_MouseDown;
            buttonClose.Click += buttonClose_Click;
            buttonMinimize.Click += buttonMinimize_Click;

            ModuleHook.ActiveWindowChanged += ModuleHook_ActiveWindowChanged;
            //#endif
        }

        private void ModuleRefresher_Tick(object sender, EventArgs e)
        {
            whiteListInited = false;

            checkedWhiteListBox.Items.Clear();

            if (Settings.Default.lockon_name != null)
                moduleWindowNames = Settings.Default.lockon_name.OfType<string>();

            var procs = Process.GetProcesses().Where(n => n.MainWindowTitle.Length > 1).Select(n => n.MainWindowTitle).ToArray();
            checkedWhiteListBox.Items.AddRange(procs);

            var its = checkedWhiteListBox.Items.OfType<string>();

            if (moduleWindowNames != null)
            {
                var ocs = moduleWindowNames.Intersect(its);

                for (int i = 0; i < checkedWhiteListBox.Items.Count; i++)
                {
                    if (ocs.Contains(checkedWhiteListBox.Items[i].ToString()))
                    {
                        checkedWhiteListBox.SetItemChecked(i, true);
                    }
                }

            }
            whiteListInited = true;
        }

        private void checkedWhiteListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!whiteListInited) return;

            var ents = Settings.Default.lockon_name;

            if (e.NewValue == CheckState.Checked)
            {
                ents.Add(checkedWhiteListBox.Items[e.Index].ToString());
            }
            else
            {
                ents.Remove(checkedWhiteListBox.Items[e.Index].ToString());
            }

            Settings.Default.lockon_name = ents;
            Settings.Default.Save();
        }

        private void ModuleHook_ActiveWindowChanged(object sender, string e)
        {
            Lock = !Settings.Default.lockon_name.Contains(e);
        }

        /// <summary>
        /// Lock key logging 
        /// </summary>
        public bool Lock = false;

        private void Keyt_Tick(object sender, EventArgs e)
        {

            keybuf = labelKeys.Text = labelKeys.Text = labelKeys.Text = "";

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
                keybuf += "LButton";
            if (rmh)
                keybuf += "RButton";

            labelKeys.Text = keybuf;
        }



        private void WinKeys_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ms_listener.Uninstall();
            kb_listener.Uninstall();
            keyt.Stop();
        }


        private void WinKeys_SizeChanged(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                Settings.Default.winkeys_bounds_point = Bounds.Location;
                Settings.Default.winkeys_bounds_size = Bounds.Size;
                Settings.Default.Save();
            });
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


        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84)
            {
                var cp = MousePosition;
                var cursor = this.PointToClient(cp);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (TopBorder.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (LeftBorder.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (RightBorder.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (BottomBorder.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
    }
}
