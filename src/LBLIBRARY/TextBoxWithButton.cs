namespace LBLIBRARY
{
    using LBLIBRARY.Components;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TextBoxWithButton : Panel
    {
        private TextBox te = new TextBox();
        private ButtonC bt = new ButtonC();
        private Bitmap ButtonImage;
        private Point ButtonImagePosition;

        [Category("Действие")]
        public event EventHandler Button_Click
        {
            add
            {
                this.bt.Click += value;
            }
            remove
            {
                this.bt.Click += null;
            }
        }

        [Category("Действие")]
        public event EventHandler Button_MouseEnter
        {
            add
            {
                this.bt.MouseEnter += value;
            }
            remove
            {
                this.bt.MouseEnter += null;
            }
        }

        [Category("Действие")]
        public event EventHandler Button_MouseLeave
        {
            add
            {
                this.bt.MouseLeave += value;
            }
            remove
            {
                this.bt.MouseLeave += null;
            }
        }

        public TextBoxWithButton()
        {
            base.BorderStyle = BorderStyle.FixedSingle;
            base.Height = 0x16;
            this.te.BorderStyle = BorderStyle.None;
            this.te.Multiline = true;
            this.te.Height = base.Height;
            this.bt.Height = 0x16;
            this.bt.Width = 0x13;
            this.bt.Location = new Point(base.Width - 0x15, 2);
            this.bt.borderColor = this.te.BackColor;
            this.bt.EndColor = this.te.BackColor;
            this.bt.MouseClickColor1 = this.te.BackColor;
            this.bt.MouseClickColor2 = this.te.BackColor;
            this.bt.MouseHoverColor1 = this.te.BackColor;
            this.bt.MouseHoverColor2 = this.te.BackColor;
            this.bt.StartColor = this.te.BackColor;
            this.bt.BorderWidth = 0;
            base.Controls.Add(this.te);
            base.Controls.Add(this.bt);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.te.Height = base.Height;
            this.te.Width = base.Width - 0x12;
            this.bt.Location = new Point(base.Width - 0x15, 2);
        }

        [Category("Внешний вид")]
        public Font TextBoxFont
        {
            get => 
                this.te.Font;
            set => 
                this.te.Font = value;
        }

        [Category("Внешний вид")]
        public string TextBoxText
        {
            get => 
                this.te.Text;
            set => 
                this.te.Text = value;
        }

        [Category("Внешний вид")]
        public Bitmap Image
        {
            get => 
                this.ButtonImage;
            set
            {
                this.ButtonImage = value;
                this.bt.Image = value;
            }
        }

        [Category("Внешний вид")]
        public Point ButtonIm_Position
        {
            get => 
                this.ButtonImagePosition;
            set
            {
                this.ButtonImagePosition = value;
                this.bt.Image_Location = value;
            }
        }
    }
}

