namespace LBLIBRARY.Components
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class DraggableDataGridView : DataGridView
    {
        private bool DelayedMouseDown;
        private Rectangle DragRectangle = Rectangle.Empty;

        public DraggableDataGridView()
        {
            this.AllowDrop = true;
            base.Tag = new Stopwatch();
        }

        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseDown(e);
            if ((e.RowIndex >= 0) && (e.Button == MouseButtons.Right))
            {
                int index = base.CurrentRow.Index;
                List<DataGridViewRow> list = base.SelectedRows.OfType<DataGridViewRow>().ToList<DataGridViewRow>();
                bool selected = base.Rows[e.RowIndex].Selected;
                base.CurrentCell = base.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (((Control.ModifierKeys & Keys.Control) != Keys.None) | selected)
                {
                    foreach (var row in list) row.Selected = true;
                }
                if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
                {
                    for (int i = index; i != e.RowIndex; i += Math.Sign((int) (e.RowIndex - index)))
                    {
                        base.Rows[i].Selected = true;
                    }
                }
            }
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            drgevent.Effect = DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            (base.Tag as Stopwatch).Start();
            int rowIndex = base.HitTest(e.X, e.Y).RowIndex;
            this.DelayedMouseDown = (rowIndex >= 0) && (base.SelectedRows.Contains(base.Rows[rowIndex]) || ((Control.ModifierKeys & Keys.Control) > Keys.None));
            if (!this.DelayedMouseDown)
            {
                base.OnMouseDown(e);
                if (rowIndex >= 0)
                {
                    Size dragSize = SystemInformation.DragSize;
                    this.DragRectangle = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
                }
                else
                {
                    this.DragRectangle = Rectangle.Empty;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Stopwatch tag = base.Tag as Stopwatch;
            tag.Stop();
            if (tag.ElapsedMilliseconds > 0x4b)
            {
                base.DoDragDrop(base.SelectedRows, DragDropEffects.Move);
            }
            tag.Reset();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Stopwatch tag = base.Tag as Stopwatch;
            tag.Stop();
            tag.Reset();
            if (this.DelayedMouseDown)
            {
                this.DelayedMouseDown = false;
                base.OnMouseDown(e);
            }
            base.OnMouseUp(e);
        }

    }
}

