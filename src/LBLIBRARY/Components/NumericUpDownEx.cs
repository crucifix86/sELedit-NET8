namespace LBLIBRARY.Components
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class NumericUpDownEx : Panel
    {
        private bool IsBlack = true;
        private bool ThousandsSeparator;
        private int DecimalPlaces;
        private ButtonC Plus;
        private ButtonC Minus;
        private TextBoxEx Textbox;

        [Category("OwnEvents")]
        public event EventHandler TextBoxDoubleClick
        {
            add
            {
                this.Textbox.DoubleClick += value;
            }
            remove
            {
                this.Textbox.DoubleClick += null;
            }
        }

        [Category("OwnEvents")]
        public event EventHandler TextBoxEnter
        {
            add
            {
                this.Textbox.Enter += value;
            }
            remove
            {
                this.Textbox.Enter += null;
            }
        }

        [Category("OwnEvents")]
        public event KeyEventHandler TextBoxKeyDown
        {
            add
            {
                this.Textbox.KeyDown += value;
            }
            remove
            {
                this.Textbox.KeyDown += null;
            }
        }

        [Category("OwnEvents")]
        public event EventHandler TextBoxLeave
        {
            add
            {
                this.Textbox.Leave += value;
            }
            remove
            {
                this.Textbox.Leave += null;
            }
        }

        [Category("OwnEvents")]
        public event EventHandler TextBoxTextChanged
        {
            add
            {
                this.Textbox.TextChanged += value;
            }
            remove
            {
                this.Textbox.TextChanged += null;
            }
        }

        public NumericUpDownEx()
        {
            ButtonC nc1 = new ButtonC();
            nc1.BackColor = Color.Transparent;
            nc1.BorderColor = Color.Transparent;
            nc1.BorderWidth = 1;
            nc1.ButtonShape = ButtonC.ButtonsShapes.Rect;
            nc1.Text = "▴";
            nc1.StartColor = Color.DodgerBlue;
            nc1.EndColor = Color.MidnightBlue;
            nc1.FlatStyle = FlatStyle.Flat;
            nc1.Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold);
            nc1.ForeColor = Color.Black;
            nc1.GradientAngle = 90;
            nc1.MouseClickColor1 = Color.Turquoise;
            nc1.MouseClickColor2 = Color.Turquoise;
            nc1.MouseHoverColor1 = Color.Turquoise;
            nc1.MouseHoverColor2 = Color.DarkSlateGray;
            nc1.Name = "button1";
            nc1.ShowButtontext = true;
            nc1.Size = new Size(13, 9);
            nc1.TabIndex = 1;
            nc1.TextLocation_X = -1;
            nc1.TextLocation_Y = -5;
            nc1.Transparent1 = 250;
            nc1.Transparent2 = 250;
            nc1.UseVisualStyleBackColor = true;
            nc1.Location = new Point(0x8e, 1);
            nc1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.Plus = nc1;
            ButtonC nc2 = new ButtonC();
            nc2.BackColor = Color.Transparent;
            nc2.BorderColor = Color.Transparent;
            nc2.BorderWidth = 1;
            nc2.ButtonShape = ButtonC.ButtonsShapes.Rect;
            nc2.Text = "▾";
            nc2.EndColor = Color.MidnightBlue;
            nc2.FlatStyle = FlatStyle.Flat;
            nc2.Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold);
            nc2.ForeColor = Color.Black;
            nc2.GradientAngle = 90;
            nc2.MouseClickColor1 = Color.Turquoise;
            nc2.MouseClickColor2 = Color.Turquoise;
            nc2.MouseHoverColor1 = Color.Turquoise;
            nc2.MouseHoverColor2 = Color.DarkSlateGray;
            nc2.Name = "buttonZ1";
            nc2.ShowButtontext = true;
            nc2.Size = new Size(13, 9);
            nc2.StartColor = Color.DodgerBlue;
            nc2.TabIndex = 4;
            nc2.TextLocation_X = -1;
            nc2.TextLocation_Y = -3;
            nc2.Transparent1 = 250;
            nc2.Transparent2 = 250;
            nc2.UseVisualStyleBackColor = true;
            nc2.Location = new Point(0x8e, 10);
            nc2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.Minus = nc2;
            TextBoxEx ex1 = new TextBoxEx();
            ex1.Multiline = true;
            ex1.BorderStyle = BorderStyle.None;
            ex1.Location = new Point(0, 0);
            ex1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            ex1.ForeColor = Color.Wheat;
            ex1.Size = new Size(0x8e, 0x1c);
            ex1.Font = new Font("Segue UI", 12f, FontStyle.Regular);
            ex1.Text = "0";
            ex1.BackColor = Color.Black;
            ex1.Name = "Textbox";
            this.Textbox = ex1;
            base.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;
            base.Size = new Size(0x9e, 0x15);
            base.Controls.Add(this.Textbox);
            base.Controls.Add(this.Plus);
            base.Controls.Add(this.Minus);
            this.Plus.Click += new EventHandler(this.Plus_Click);
            this.Minus.Click += new EventHandler(this.Minus_Click);
            this.Textbox.SetButtonsNames(this.Plus.Name, this.Minus.Name);
            this.Increment = 1M;
        }

        private void Minus_Click(object sender, EventArgs e)
        {
            this.Textbox.Focus();
            this.Textbox.SelectionLength = 0;
            try
            {
                this.Textbox.Text = (Convert.ToDecimal(this.Textbox.Text) - this.Increment).ToString();
            }
            catch
            {
                this.Textbox.Text = (this.Textbox.LastValue - this.Increment).ToString();
            }
            if ((this.DecimalPlaces != 0) && this.ThousandsSeparator)
            {
                this.Textbox.Text = string.Format(this.Textbox.FormatedText, double.Parse(this.Textbox.Text.Replace("\x00a0", "")));
            }
            try
            {
                if (Convert.ToDecimal(this.Textbox.Text) < this.MinimalValue)
                {
                    this.Textbox.Text = this.MinimalValue.ToString();
                }
            }
            catch
            {
            }
        }

        private void Plus_Click(object sender, EventArgs e)
        {
            this.Textbox.Focus();
            this.Textbox.SelectionLength = 0;
            try
            {
                this.Textbox.Text = (Convert.ToDecimal(this.Textbox.Text) + this.Increment).ToString();
            }
            catch
            {
                this.Textbox.Text = (this.Textbox.LastValue + this.Increment).ToString();
            }
            if ((this.DecimalPlaces != 0) && this.ThousandsSeparator)
            {
                this.Textbox.Text = string.Format(this.Textbox.FormatedText, double.Parse(this.Textbox.Text.Replace("\x00a0", "")));
            }
        }

        public decimal Increment { get; set; }

        public string TextBoxName
        {
            get => 
                this.Textbox.Name;
            set => 
                this.Textbox.Name = value;
        }

        public bool SetBlack
        {
            get => 
                this.IsBlack;
            set
            {
                if (value)
                {
                    this.BackColor = Color.FromArgb(0x23, 0x23, 0x23);
                    this.Textbox.BackColor = Color.Black;
                    this.Textbox.ForeColor = Color.FromArgb(0xc0, 0xff, 0xff);
                    this.Minus.ForeColor = Color.Silver;
                    this.Plus.ForeColor = Color.Silver;
                    this.Minus.StartColor = Color.FromArgb(20, 20, 20);
                    this.Minus.EndColor = Color.FromArgb(20, 20, 20);
                    this.Plus.StartColor = Color.FromArgb(20, 20, 20);
                    this.Plus.EndColor = Color.FromArgb(20, 20, 20);
                    this.Plus.BorderColor = Color.FromArgb(20, 20, 80);
                    this.Minus.BorderColor = Color.FromArgb(20, 20, 20);
                }
                else
                {
                    this.BackColor = Color.White;
                    this.Textbox.BackColor = SystemColors.Window;
                    this.Textbox.ForeColor = SystemColors.ControlText;
                    this.Minus.ForeColor = SystemColors.ControlText;
                    this.Plus.ForeColor = SystemColors.ControlText;
                    this.Minus.StartColor = Color.FromArgb(220, 220, 220);
                    this.Plus.StartColor = Color.FromArgb(220, 220, 220);
                    this.Minus.EndColor = Color.FromArgb(220, 220, 220);
                    this.Plus.EndColor = Color.FromArgb(220, 220, 220);
                    this.Plus.BorderColor = Color.Gray;
                    this.Minus.BorderColor = Color.Gray;
                }
                this.IsBlack = value;
            }
        }

        public bool SetThousandsSeparator
        {
            get => 
                this.ThousandsSeparator;
            set
            {
                this.ThousandsSeparator = value;
                this.Textbox.ThousandsSeparator = value;
            }
        }

        public int SetDecimalPlaces
        {
            get => 
                this.DecimalPlaces;
            set
            {
                this.DecimalPlaces = value;
                this.Textbox.SetDecimalPlaces = value;
            }
        }

        public Color TextBoxForeColor
        {
            get => 
                this.Textbox.ForeColor;
            set => 
                this.Textbox.ForeColor = value;
        }

        public Color TextBoxBackGroundColor
        {
            get => 
                this.Textbox.BackColor;
            set => 
                this.Textbox.BackColor = value;
        }

        public Font TextBoxFont
        {
            get => 
                this.Textbox.Font;
            set
            {
                this.Textbox.Font = value;
                TextRenderer.MeasureText("1", this.Font);
            }
        }

        public decimal Value
        {
            get
            {
                if (this.Textbox.DecimalPlaces <= 0)
                {
                    return Convert.ToDecimal(this.Textbox.Text);
                }
                if (!this.Textbox.Text.Contains(","))
                {
                    return Convert.ToDecimal(this.Textbox.Text);
                }
                char[] separator = new char[] { ',' };
                string[] strArray = this.Textbox.Text.Split(separator);
                return Convert.ToDecimal(strArray[0] + "," + strArray[1]);
            }
            set
            {
                this.Textbox.Text = value.ToString();
                this.Textbox.FormatValue();
            }
        }

        public decimal MinimalValue
        {
            get => 
                this.Textbox.MinimalValue;
            set => 
                this.Textbox.MinimalValue = value;
        }

        public decimal MaximalValue
        {
            get => 
                this.Textbox.MaximalValue;
            set => 
                this.Textbox.MaximalValue = value;
        }
    }
}

