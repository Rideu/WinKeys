using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinKeysС
{
    public partial class MousePad : UserControl
    {
        Graphics gdi;
        public MousePad()
        {
            InitializeComponent();
            gdi = this.CreateGraphics();
            center = new Point(Size.Width / 2, Size.Height / 2);
            DoubleBuffered = true;
        }

        Point center;
        Point delta;

        public void SetDelta(Point d)
        {
            delta = d;
            var tgt = new Point(center.X + delta.X, center.Y + delta.Y);
            var rect = new Rectangle(tgt.X - 2, tgt.Y - 2, 4, 4);
            Refresh();
            gdi.DrawLine(Pens.White, center, tgt);
            gdi.DrawRectangle(Pens.White, rect);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

        }
    }
}
