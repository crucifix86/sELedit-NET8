namespace LBLIBRARY.Components
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class TextBoxEx : TextBox
    {
        private string First = "";
        private string Second = "";
        private bool allowSpace;
        public string FormatedText;
        public bool ThousandsSeparator;
        public int DecimalPlaces;
        private bool BackSpace;
        public decimal LastValue;

        public void FormatValue()
        {
            if (this.ThousandsSeparator && (this.DecimalPlaces != 0))
            {
                this.Text = string.Format(this.FormatedText, decimal.Parse(this.Text.Replace("\x00a0", "")));
            }
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        internal static extern IntPtr GetFocus();
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            try
            {
                this.LastValue = Convert.ToDecimal(this.Text);
            }
            catch
            {
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Control & (e.KeyCode == Keys.A))
            {
                base.SelectAll();
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (e.KeyChar != '\b')
            {
                this.BackSpace = true;
                NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
                string numberDecimalSeparator = numberFormat.NumberDecimalSeparator;
                string numberGroupSeparator = numberFormat.NumberGroupSeparator;
                string negativeSign = numberFormat.NegativeSign;
                string str4 = e.KeyChar.ToString();
                if (!char.IsDigit(e.KeyChar) && (!str4.Equals(numberDecimalSeparator) && (!str4.Equals(numberGroupSeparator) && (!str4.Equals(negativeSign) && ((e.KeyChar != '\b') && (!this.allowSpace || (e.KeyChar != ' ')))))))
                {
                    e.Handled = true;
                }
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            Control control = FromHandle(GetFocus());
            try
            {
                if (Convert.ToDecimal(this.Text) < this.MinimalValue)
                {
                    this.Text = this.MinimalValue.ToString();
                }
                if (Convert.ToDecimal(this.Text) > this.MaximalValue)
                {
                    this.Text = this.MaximalValue.ToString();
                }
            }
            catch
            {
            }
            if (control.Name.ToString().Length != 0)
            {
                string str = control.Name.ToString();
                if ((str != this.First) && (str != this.Second))
                {
                    if (string.IsNullOrWhiteSpace(this.Text))
                    {
                        this.Text = "0," + new string('0', this.DecimalPlaces);
                    }
                    if (this.Text.Count(z => z == '-') > 1)
                    {
                        this.Text = this.LastValue.ToString();
                    }
                    base.OnLeave(e);
                    if (this.ThousandsSeparator && (this.DecimalPlaces != 0))
                    {
                        if (this.Text.Contains(","))
                        {
                            char[] separator = new char[] { ',' };
                            string[] strArray = this.Text.Split(separator);
                            this.Text = strArray[0] + "," + strArray[1];
                        }
                        this.Text = string.Format(this.FormatedText, decimal.Parse(this.Text.Replace("\x00a0", "")));
                    }
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if ((this.DecimalPlaces == 0) && this.Text.Contains(","))
            {
                this.Text = this.Text.Remove(this.Text.IndexOf(','), 1);
            }
        }

        public void SetButtonsNames(string s1, string s2)
        {
            this.First = s1;
            this.Second = s2;
        }

        public decimal MinimalValue { get; set; }

        public decimal MaximalValue { get; set; }

        public int SetDecimalPlaces
        {
            get => 
                this.DecimalPlaces;
            set
            {
                this.DecimalPlaces = value;
                if (value == 0)
                {
                    char[] separator = new char[] { ',' };
                    this.Text = this.Text.Split(separator)[0];
                }
                else
                {
                    this.Text = "0," + new string('0', value);
                    this.FormatedText = "{0:#,##0.";
                    for (int i = 0; i < value; i++)
                    {
                        this.FormatedText = this.FormatedText + "0";
                    }
                    this.FormatedText = this.FormatedText + "}";
                }
            }
        }

    }
}

