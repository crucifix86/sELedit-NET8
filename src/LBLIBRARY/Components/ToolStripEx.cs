namespace LBLIBRARY.Components
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ToolStripEx : ToolStrip
    {
        private Color OwnBackColor;
        private Color OwnForeColor;
        private Color OwnSeparatorColor;
        private Color OwnLeftSideColor;
        private Color OwnMouseBackColor;
        private Color OwnMouseBorderColor;
        private Color ItemChecked;
        private Color ArrowColor;
        private bool ColorIsBlack;

        [Category("Внешний вид")]
        public bool SetColorBlack
        {
            get => 
                this.ColorIsBlack;
            set
            {
                this.ColorIsBlack = value;
                if (value)
                {
                    this.OwnBackColor = Color.FromArgb(20, 20, 20);
                    this.OwnForeColor = Color.FromArgb(0xe1, 0xe1, 0xe1);
                    base.BackColor = Color.FromArgb(20, 20, 20);
                    this.OwnSeparatorColor = Color.FromArgb(60, 60, 60);
                    this.OwnLeftSideColor = Color.FromArgb(0x19, 0x19, 0x19);
                    this.OwnMouseBackColor = Color.FromArgb(0x23, 0x23, 0x23);
                    this.OwnMouseBorderColor = Color.Black;
                    this.ItemChecked = Color.FromArgb(70, 70, 70);
                    this.ArrowColor = Color.FromArgb(0x7d, 0x7d, 0x7d);
                }
                else
                {
                    this.OwnBackColor = Color.White;
                    this.OwnForeColor = Color.Black;
                    base.BackColor = Color.FromArgb(0xff, 0xf5, 0xf5, 0xf5);
                    this.OwnSeparatorColor = Color.FromArgb(150, 150, 150);
                    this.OwnLeftSideColor = Color.FromArgb(0xf2, 0xf2, 0xf2);
                    this.OwnMouseBackColor = Color.FromArgb(0xff, 0xc3, 0xea, 0xf6);
                    this.OwnMouseBorderColor = Color.Blue;
                    this.ItemChecked = Color.LightSkyBlue;
                    this.ArrowColor = SystemColors.ControlText;
                }
                base.Renderer = new MyToolStripRenderer(this.OwnBackColor, this.OwnForeColor, this.OwnSeparatorColor, this.OwnLeftSideColor, this.OwnMouseBackColor, this.OwnMouseBorderColor, this.ItemChecked, this.ArrowColor);
            }
        }
    }
}

