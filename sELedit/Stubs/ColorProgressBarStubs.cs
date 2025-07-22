using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorProgressBar
{
    public class ColorProgressBar : ProgressBar
    {
        public enum FillStyles
        {
            Solid = 0,
            Dashed = 1
        }

        private Color _BarColor = Color.Blue;
        private Color _BorderColor = Color.Black;
        private FillStyles _FillStyle = FillStyles.Solid;

        public Color BarColor
        {
            get { return _BarColor; }
            set { _BarColor = value; Invalidate(); }
        }

        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; Invalidate(); }
        }

        public FillStyles FillStyle
        {
            get { return _FillStyle; }
            set { _FillStyle = value; Invalidate(); }
        }

        public ColorProgressBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = e.ClipRectangle;
            rect.Width = (int)(rect.Width * ((double)Value / Maximum)) - 4;
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rect.Height = rect.Height - 4;
            e.Graphics.FillRectangle(new SolidBrush(_BarColor), 2, 2, rect.Width, rect.Height);
        }
    }
}