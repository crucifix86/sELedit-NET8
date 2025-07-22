namespace LBLIBRARY.Components
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    public class DateTimePickerA : DateTimePicker
    {
        private Color _backDisabledColor;
        private Color OwnForeColor;
        private Color OwnBorderColor;
        private bool IsBlack;
        private Color DropDownButtonBackColor = Color.FromArgb(0xe5, 0xf1, 0xfb);
        private Brush DropDownButtonForeColor = Brushes.Black;
        private Brush DropDownButtonMouseMoveBackColor = Brushes.LightBlue;
        private Graphics g;

        public DateTimePickerA()
        {
            base.SetStyle(ControlStyles.UserPaint, true);
            this._backDisabledColor = Color.FromKnownColor(KnownColor.Control);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Rectangle rect = new Rectangle(base.ClientRectangle.Width - 0x11, 1, 0x11, 0x13);
            base.OnMouseMove(e);
            if ((e.X >= (base.ClientRectangle.Width - 0x11)) && (e.X < (base.ClientRectangle.Width - 1)))
            {
                this.g.FillRectangle(this.DropDownButtonMouseMoveBackColor, rect);
                this.g.DrawString("▾", new Font("Segui UI", 14f, FontStyle.Regular), this.DropDownButtonForeColor, new PointF((float) rect.X, (float) (rect.Y + 1)));
            }
            else
            {
                this.g.FillRectangle(new SolidBrush(this.DropDownButtonBackColor), rect);
                this.g.DrawString("▾", new Font("Segui UI", 14f, FontStyle.Regular), this.DropDownButtonForeColor, new PointF((float) rect.X, (float) (rect.Y + 1)));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Brush brush;
            ComboBoxState hot;
            this.g = base.CreateGraphics();
            Rectangle bounds = new Rectangle(base.ClientRectangle.Width - 0x11, 0, 0x11, 20);
            if (base.Enabled)
            {
                brush = new SolidBrush(this.BackColor);
                hot = ComboBoxState.Hot;
            }
            else
            {
                brush = new SolidBrush(this._backDisabledColor);
                hot = ComboBoxState.Disabled;
            }
            this.g.FillRectangle(brush, 0, 0, base.ClientRectangle.Width, base.ClientRectangle.Height);
            this.g.DrawString(this.Text, this.Font, new SolidBrush(this.OwnForeColor), (float) 0f, (float) 2f);
            ComboBoxRenderer.DrawDropDownButton(this.g, bounds, hot);
            this.g.FillRectangle(new SolidBrush(this.DropDownButtonBackColor), bounds);
            this.g.DrawRectangle(new Pen(this.OwnBorderColor), 0, 0, base.Width - 1, base.Height - 1);
            this.g.DrawString("▾", new Font("Segui UI", 14f, FontStyle.Regular), this.DropDownButtonForeColor, new PointF((float) bounds.X, (float) (bounds.Y + 1)));
            brush.Dispose();
        }

        [Category("OwnProperties"), Browsable(true)]
        public override Color BackColor
        {
            get => 
                base.BackColor;
            set => 
                base.BackColor = value;
        }

        [Category("OwnProperties")]
        public Color SetColor
        {
            get => 
                this.OwnForeColor;
            set => 
                this.OwnForeColor = value;
        }

        [Category("OwnProperties")]
        public Color BackDisabledColor
        {
            get => 
                this._backDisabledColor;
            set => 
                this._backDisabledColor = value;
        }

        [Category("OwnProperties")]
        public Color SetBorderColor
        {
            get => 
                this.OwnBorderColor;
            set => 
                this.OwnBorderColor = value;
        }

        [Category("OwnProperties")]
        public bool SetColorBlack
        {
            get => 
                this.IsBlack;
            set
            {
                if (value)
                {
                    this.IsBlack = true;
                    this.BackColor = Color.FromArgb(20, 20, 20);
                    this.OwnBorderColor = Color.FromArgb(120, 120, 120);
                    this.OwnForeColor = Color.FromArgb(0xeb, 0xeb, 0xeb);
                    this.DropDownButtonBackColor = Color.FromArgb(20, 20, 20);
                    this.DropDownButtonForeColor = new SolidBrush(Color.FromArgb(0xeb, 0xeb, 0xeb));
                    this.DropDownButtonMouseMoveBackColor = new SolidBrush(Color.FromArgb(50, 50, 50));
                }
                else
                {
                    this.IsBlack = false;
                    this.BackColor = Color.White;
                    this.OwnBorderColor = Color.FromArgb(150, 150, 150);
                    this.OwnForeColor = SystemColors.ControlText;
                    this.DropDownButtonBackColor = Color.FromArgb(0xe5, 0xf1, 0xfb);
                    this.DropDownButtonForeColor = new SolidBrush(Color.FromArgb(70, 70, 70));
                    this.DropDownButtonMouseMoveBackColor = new SolidBrush(Color.LightBlue);
                }
                base.Invalidate();
            }
        }
    }
}

