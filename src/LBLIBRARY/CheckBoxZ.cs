namespace LBLIBRARY
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class CheckBoxZ : CheckBox
    {
        private Color clr1;
        private Color clr2;
        private Color color1 = Color.SteelBlue;
        private Color color2 = Color.DarkBlue;
        private Color m_hovercolor1 = Color.Yellow;
        private Color m_hovercolor2 = Color.DarkOrange;
        private int color1Transparent = 150;
        private int color2Transparent = 150;
        private int boxsize = 0x12;
        private int boxlocatx;
        private int boxlocaty;
        private int angle = 90;
        private int textX = 14;
        private int textY = 4;
        private string text = "";

        public CheckBoxZ()
        {
            this.ForeColor = Color.White;
            this.AutoSize = false;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.clr1 = this.color1;
            this.clr2 = this.color2;
            this.color1 = this.m_hovercolor1;
            this.color2 = this.m_hovercolor2;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.color1 = this.clr1;
            this.color2 = this.clr2;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.AutoSize = false;
            this.text = this.Text;
            if ((this.textX == 100) && (this.textY == 0x19))
            {
                this.textX = (base.Width / 3) + 10;
                this.textY = (base.Height / 2) - 1;
            }
            Color color = Color.FromArgb(this.color1Transparent, this.color1);
            Brush brush = new LinearGradientBrush(base.ClientRectangle, color, Color.FromArgb(this.color2Transparent, this.color2), (float) this.angle);
            Point point = new Point(this.textX, this.textY);
            SolidBrush brush2 = new SolidBrush(this.ForeColor);
            e.Graphics.FillRectangle(brush, base.ClientRectangle);
            e.Graphics.DrawString(this.text, this.Font, brush2, (PointF) point);
            Rectangle rectangle = new Rectangle(this.boxlocatx, this.boxlocaty, this.boxsize, this.boxsize);
            ControlPaint.DrawCheckBox(e.Graphics, rectangle, base.Checked ? ButtonState.Checked : ButtonState.Normal);
            brush.Dispose();
        }

        public string DisplayText
        {
            get => 
                this.text;
            set
            {
                this.text = value;
                base.Invalidate();
            }
        }

        public Color StartColor
        {
            get => 
                this.color1;
            set
            {
                this.color1 = value;
                base.Invalidate();
            }
        }

        public Color EndColor
        {
            get => 
                this.color2;
            set
            {
                this.color2 = value;
                base.Invalidate();
            }
        }

        public Color MouseHoverColor1
        {
            get => 
                this.m_hovercolor1;
            set
            {
                this.m_hovercolor1 = value;
                base.Invalidate();
            }
        }

        public Color MouseHoverColor2
        {
            get => 
                this.m_hovercolor2;
            set
            {
                this.m_hovercolor2 = value;
                base.Invalidate();
            }
        }

        public int Transparent1
        {
            get => 
                this.color1Transparent;
            set
            {
                this.color1Transparent = value;
                if (this.color1Transparent <= 0xff)
                {
                    base.Invalidate();
                }
                else
                {
                    this.color1Transparent = 0xff;
                    base.Invalidate();
                }
            }
        }

        public int Transparent2
        {
            get => 
                this.color2Transparent;
            set
            {
                this.color2Transparent = value;
                if (this.color2Transparent <= 0xff)
                {
                    base.Invalidate();
                }
                else
                {
                    this.color2Transparent = 0xff;
                    base.Invalidate();
                }
            }
        }

        public int GradientAngle
        {
            get => 
                this.angle;
            set
            {
                this.angle = value;
                base.Invalidate();
            }
        }

        public int TextLocation_X
        {
            get => 
                this.textX;
            set
            {
                this.textX = value;
                base.Invalidate();
            }
        }

        public int TextLocation_Y
        {
            get => 
                this.textY;
            set
            {
                this.textY = value;
                base.Invalidate();
            }
        }

        public int BoxSize
        {
            get => 
                this.boxsize;
            set
            {
                this.boxsize = value;
                base.Invalidate();
            }
        }

        public int BoxLocation_X
        {
            get => 
                this.boxlocatx;
            set
            {
                this.boxlocatx = value;
                base.Invalidate();
            }
        }

        public int BoxLocation_Y
        {
            get => 
                this.boxlocaty;
            set
            {
                this.boxlocaty = value;
                base.Invalidate();
            }
        }
    }
}

