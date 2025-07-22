namespace LBLIBRARY.Components
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class MyMenuRenderer : ToolStripRenderer
    {
        private Color OwnBackColor;
        private Color OwnForeColor;
        private Color OwnSeparatorColor;
        private Color OwnLeftSideColor;
        private Color OwnMouseBackColor;
        private Color OwnMouseBorderColor;
        private Color ItemChecked;

        public MyMenuRenderer(Color co, Color fc, Color sc, Color ls, Color mbc, Color bc, Color ic)
        {
            this.OwnBackColor = co;
            this.OwnForeColor = fc;
            this.OwnSeparatorColor = sc;
            this.OwnLeftSideColor = ls;
            this.OwnMouseBackColor = mbc;
            this.OwnMouseBorderColor = bc;
            this.ItemChecked = ic;
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);
            Rectangle rect = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
            e.Graphics.FillRectangle(new SolidBrush(this.OwnBackColor), rect);
            SolidBrush brush = new SolidBrush(this.OwnLeftSideColor);
            Rectangle rectangle2 = new Rectangle(0, 0, 0x1a, e.AffectedBounds.Height);
            e.Graphics.FillRectangle(brush, rectangle2);
            Rectangle rectangle3 = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rectangle3);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);
            if (e.Item.Selected)
            {
                Rectangle rect = new Rectangle(2, 0, 0x17, 0x17);
                Rectangle rectangle2 = new Rectangle(2, 0, 0x17, 0x17);
                SolidBrush brush = new SolidBrush(Color.Black);
                SolidBrush brush2 = new SolidBrush(this.ItemChecked);
                e.Graphics.FillRectangle(brush, rect);
                e.Graphics.FillRectangle(brush2, rectangle2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
            else
            {
                Rectangle rect = new Rectangle(2, 0, 0x17, 0x17);
                Rectangle rectangle4 = new Rectangle(2, 0, 0x17, 0x17);
                SolidBrush brush = new SolidBrush(Color.White);
                SolidBrush brush4 = new SolidBrush(this.ItemChecked);
                e.Graphics.FillRectangle(brush, rect);
                e.Graphics.FillRectangle(brush4, rectangle4);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);
            if (e.Item.Enabled)
            {
                if (e.Item.IsOnDropDown || !e.Item.Selected)
                {
                    e.Item.ForeColor = this.OwnForeColor;
                }
                else
                {
                    Rectangle rect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
                    e.Graphics.FillRectangle(new SolidBrush(this.OwnMouseBackColor), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Blue)), rect);
                    e.Item.ForeColor = this.OwnForeColor;
                }
                if (e.Item.IsOnDropDown && e.Item.Selected)
                {
                    Rectangle rect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
                    e.Graphics.FillRectangle(new SolidBrush(this.OwnMouseBackColor), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.LightBlue)), rect);
                    e.Item.ForeColor = this.OwnForeColor;
                }
                if ((e.Item as ToolStripMenuItem).DropDown.Visible && !e.Item.IsOnDropDown)
                {
                    Rectangle rect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
                    Rectangle rectangle4 = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
                    e.Graphics.FillRectangle(new SolidBrush(this.OwnBackColor), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rectangle4);
                    e.Item.ForeColor = this.OwnForeColor;
                }
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            SolidBrush brush = new SolidBrush(this.OwnSeparatorColor);
            Rectangle rect = new Rectangle(30, 3, e.Item.Width - 30, 1);
            e.Graphics.FillRectangle(brush, rect);
        }
    }
}

