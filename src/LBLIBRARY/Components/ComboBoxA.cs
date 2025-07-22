namespace LBLIBRARY.Components
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ComboBoxA : ComboBox
    {
        private Color BorderColor;
        private Font TextFont = new Font("Segue UI", 9f, FontStyle.Regular);
        private MyEnum IsBlack;

        public ComboBoxA()
        {
            base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            base.FlatStyle = FlatStyle.Flat;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectionColor = Color.Red;
            this.BorderColor = Color.Black;
            this.ArrowColor = Color.Black;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.DrawItem += new DrawItemEventHandler(this.AdvancedComboBox_DrawItem);
        }

        private void AdvancedComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                ComboBox box = sender as ComboBox;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(this.SelectionColor), e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(box.BackColor), e.Bounds);
                }
                e.Graphics.DrawString(box.Items[e.Index].ToString(), this.TextFont, new SolidBrush(box.ForeColor), (PointF) new Point(1, e.Bounds.Y - 1));
                e.DrawFocusRectangle();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int verticalScrollBarWidth = SystemInformation.VerticalScrollBarWidth;
            Color controlLightLight = SystemColors.ControlLightLight;
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height), controlLightLight, SystemColors.ControlDark, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height));
            brush.Dispose();
            Pen pen = new Pen(SystemColors.ButtonShadow, 1f);
            e.Graphics.DrawRectangle(pen, new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height).X, new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height).Y, new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height).Width - 2, new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height).Height - 2);
            pen.Dispose();
            SolidBrush brush2 = new SolidBrush(this.ArrowColor);
            Point[] points = new Point[] { new Point(base.Width - 15, (base.Height / 2) - 2), new Point(base.Width - 4, (base.Height / 2) - 2), new Point(base.Width - 10, (base.Height / 2) + 4) };
            e.Graphics.FillPolygon(brush2, points);
            brush2.Dispose();
            if (base.SelectedItem != null)
            {
                e.Graphics.DrawString(this.Text, this.TextFont, new SolidBrush(this.ForeColor), (float) 1f, (float) 2f);
            }
            e.Graphics.DrawRectangle(new Pen(this.BorderColor, 2f), 0f, 0f, (float) base.Width, (float) base.Height);
        }

        public System.Windows.Forms.DrawMode DrawMode { get; set; }

        public Color SelectionColor { get; set; }

        public Color ArrowColor { get; set; }

        public Font SetFont
        {
            get => 
                this.TextFont;
            set => 
                this.TextFont = value;
        }

        public MyEnum SetColorBlack
        {
            get => 
                this.IsBlack;
            set
            {
                if (value == MyEnum.True)
                {
                    this.BackColor = Color.FromArgb(20, 20, 20);
                    this.ArrowColor = Color.Silver;
                    this.SelectionColor = Color.Green;
                    this.ForeColor = Color.FromArgb(0xeb, 0xeb, 0xeb);
                    this.BorderColor = Color.FromArgb(80, 80, 80);
                    this.IsBlack = MyEnum.True;
                }
                else if (value != MyEnum.False)
                {
                    this.IsBlack = MyEnum.None;
                }
                else
                {
                    this.BackColor = SystemColors.Window;
                    this.ArrowColor = Color.FromArgb(70, 70, 70);
                    this.SelectionColor = Color.FromArgb(0x80, 0x80, 0xff);
                    this.ForeColor = SystemColors.WindowText;
                    this.BorderColor = Color.FromArgb(70, 70, 70);
                    this.IsBlack = MyEnum.False;
                }
                base.Invalidate();
            }
        }

        public enum MyEnum
        {
            True,
            False,
            None
        }
    }
}

