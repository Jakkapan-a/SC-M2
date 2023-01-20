using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M2_V2._00.Components
{
    public partial class PictureCrop : PictureBox
    {

        Rectangle Rect;
        System.Drawing.Point LocationXY;
        System.Drawing.Point LocationX1Y1;
        bool IsMouseDown = false;

        public PictureCrop()
        {
            InitializeComponent();
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);

        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            LocationXY = e.Location;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                LocationX1Y1 = e.Location;
                Refresh();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                LocationX1Y1 = e.Location;
                Refresh();
                IsMouseDown = false;
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (Rect != null)
            {
                e.Graphics.DrawRectangle(Pens.Red, GetRect());
            }
        }


        public Rectangle GetRect()
        {
            Rect = new Rectangle();
            Rect.X = Math.Min(LocationXY.X, LocationX1Y1.X);
            Rect.Y = Math.Min(LocationXY.Y, LocationX1Y1.Y);
            Rect.Width = Math.Abs(LocationXY.X - LocationX1Y1.X);
            Rect.Height = Math.Abs(LocationXY.Y - LocationX1Y1.Y);
            return Rect;
        }
    }
}
