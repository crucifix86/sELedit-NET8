namespace LBLIBRARY.Components
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TabControlZ : TabControl
    {
        private Color SelectedColor;
        private Color TabDefaultBack;
        private Color TabDefaultFore;
        private Point textloc;
        private Color SelectedTextColor;
        private Color bo;

        public TabControlZ()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            this.DoubleBuffered = base.ResizeRedraw = true;
            base.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        protected virtual void DrawTab(Graphics g, int index, Rectangle r, Color co)
        {
            if (index != -1)
            {
                r.Inflate(-1, -1);
                TextRenderer.DrawText(g, base.TabPages[index].Text, this.Font, r, co, TextFormatFlags.Default);
                g.FillRectangle(new SolidBrush(this.BorderColor), 0, 0x17, base.Width, 2);
                g.FillRectangle(new SolidBrush(this.BorderColor), 0, base.Height - 4, base.Width, 4);
                g.FillRectangle(new SolidBrush(this.BorderColor), 0, 0x19, 4, base.Height);
                g.FillRectangle(new SolidBrush(this.BorderColor), base.Width - 4, 0x19, 4, base.Height);
            }
        }

        protected virtual void DrawTabRectangle(Graphics g, int index, Rectangle r)
        {
            if (index == 0)
            {
                r = new Rectangle(r.Left - 2, r.Top, r.Width + 2, r.Height);
            }
            if (index != base.SelectedIndex)
            {
                r = new Rectangle(r.Left, r.Top + 2, r.Width, r.Height - 2);
            }
            Color color = (index != base.SelectedIndex) ? this.TabDefaultBack : this.SelectedColor;
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, r);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (base.TabCount > 0)
            {
                Rectangle clientRectangle = base.ClientRectangle;
                int bottom = base.GetTabRect(0).Bottom;
                using (SolidBrush brush = new SolidBrush(Color.FromKnownColor(KnownColor.Window)))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(clientRectangle.Left, bottom, clientRectangle.Width, clientRectangle.Height - bottom));
                }
                for (int i = 0; i < base.TabCount; i++)
                {
                    clientRectangle = base.GetTabRect(i);
                    this.DrawTabRectangle(e.Graphics, i, clientRectangle);
                    if (i != base.SelectedIndex)
                    {
                        this.DrawTab(e.Graphics, i, clientRectangle, this.TabDefaultFore);
                    }
                    else
                    {
                        this.DrawTab(e.Graphics, i, clientRectangle, this.SelectedTextColor);
                        clientRectangle.Inflate(-1, -1);
                        ControlPaint.DrawFocusRectangle(e.Graphics, clientRectangle);
                    }
                }
            }
        }

        public override Color BackColor
        {
            get => 
                Color.Transparent;
            set => 
                base.BackColor = Color.Transparent;
        }

        [Category("LBLIBRARY")]
        public Color TabDefaultForeground
        {
            get => 
                this.TabDefaultFore;
            set
            {
                this.TabDefaultFore = value;
                base.Invalidate();
            }
        }

        [Category("LBLIBRARY")]
        public Color TabDefaultBackground
        {
            get => 
                this.TabDefaultBack;
            set
            {
                this.TabDefaultBack = value;
                base.Invalidate();
            }
        }

        [Category("LBLIBRARY")]
        public Point TextPosition
        {
            get => 
                this.textloc;
            set
            {
                this.textloc = value;
                base.Invalidate();
            }
        }

        [Category("LBLIBRARY")]
        public Color SelectedTabBackground
        {
            get => 
                this.SelectedColor;
            set
            {
                this.SelectedColor = value;
                base.Invalidate();
            }
        }

        [Category("LBLIBRARY")]
        public Color SelectedTabForeground
        {
            get => 
                this.SelectedTextColor;
            set
            {
                this.SelectedTextColor = value;
                base.Invalidate();
            }
        }

        [Category("LBLIBRARY")]
        public Color BorderColor
        {
            get => 
                this.bo;
            set
            {
                this.bo = value;
                base.Invalidate();
            }
        }
    }
}

