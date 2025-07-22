namespace LBLIBRARY.Components
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public class UpdatesForm : Form
    {
        private bool isTopPanelDragged;
        private bool isWindowMaximized;
        private Point offset;
        private Size _normalWindowSize;
        private Point _normalWindowLocation = Point.Empty;
        private int Language = 1;
        private IContainer components;
        private Panel TopPanel;
        private ButtonC ClosseWindow;
        private Label WindowTextLabel;
        private ToolTip toolTip1;
        private OpenFileDialog DialogOpenTask;
        private RichTextBox UpdatesBox;
        private ComboBoxA comboBoxEx1;

        public UpdatesForm()
        {
            this.InitializeComponent();
        }

        private void _CloseButton_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void ChangeIndex(object sender, EventArgs e)
        {
            this.UpdatesBox.Text = "";
            if (this.comboBoxEx1.SelectedIndex == 0)
            {
                this.Language = 1;
                this.WindowTextLabel.Text = "История обновлений";
                this.SetText(File.ReadAllLines(Application.StartupPath + @"\Changelog_Ru.txt", Encoding.GetEncoding(0x4e3)).ToList<string>());
            }
            else
            {
                this.Language = 2;
                this.WindowTextLabel.Text = "Updates history";
                this.SetText(File.ReadAllLines(Application.StartupPath + @"\Changelog_En.txt", Encoding.GetEncoding(0x4e3)).ToList<string>());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.TopPanel = new Panel();
            this.comboBoxEx1 = new ComboBoxA();
            this.WindowTextLabel = new Label();
            this.ClosseWindow = new ButtonC();
            this.toolTip1 = new ToolTip(this.components);
            this.DialogOpenTask = new OpenFileDialog();
            this.UpdatesBox = new RichTextBox();
            this.TopPanel.SuspendLayout();
            base.SuspendLayout();
            this.TopPanel.BackColor = Color.FromArgb(220, 120, 80);
            this.TopPanel.Controls.Add(this.comboBoxEx1);
            this.TopPanel.Controls.Add(this.WindowTextLabel);
            this.TopPanel.Controls.Add(this.ClosseWindow);
            this.TopPanel.Dock = DockStyle.Top;
            this.TopPanel.Location = new Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new Size(730, 0x21);
            this.TopPanel.TabIndex = 4;
            this.TopPanel.MouseDown += new MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new MouseEventHandler(this.TopPanel_MouseUp);
            this.comboBoxEx1.ArrowColor = Color.Black;
            this.comboBoxEx1.BackColor = Color.FromArgb(0xff, 0xe0, 0xc0);
            this.comboBoxEx1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FlatStyle = FlatStyle.Flat;
            this.comboBoxEx1.FormattingEnabled = true;
            object[] items = new object[] { "Russian", "English" };
            this.comboBoxEx1.Items.AddRange(items);
            this.comboBoxEx1.Location = new Point(0, 0);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.SelectionColor = Color.FromArgb(0xc0, 0xff, 0xff);
            this.comboBoxEx1.SetColorBlack = ComboBoxA.MyEnum.None;
            this.comboBoxEx1.SetFont = new Font("Microsoft Sans Serif", 9f);
            this.comboBoxEx1.Size = new Size(0x80, 0x15);
            this.comboBoxEx1.TabIndex = 2;
            this.comboBoxEx1.SelectedIndexChanged += new EventHandler(this.ChangeIndex);
            this.WindowTextLabel.AutoSize = true;
            this.WindowTextLabel.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.WindowTextLabel.ForeColor = Color.White;
            this.WindowTextLabel.Location = new Point(270, 5);
            this.WindowTextLabel.Name = "WindowTextLabel";
            this.WindowTextLabel.Size = new Size(200, 0x18);
            this.WindowTextLabel.TabIndex = 1;
            this.WindowTextLabel.Text = "История обновлений";
            this.WindowTextLabel.MouseDown += new MouseEventHandler(this.WindowTextLabel_MouseDown);
            this.WindowTextLabel.MouseMove += new MouseEventHandler(this.WindowTextLabel_MouseMove);
            this.WindowTextLabel.MouseUp += new MouseEventHandler(this.WindowTextLabel_MouseUp);
            this.ClosseWindow.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.ClosseWindow.BackColor = Color.Transparent;
            this.ClosseWindow.BorderColor = Color.Transparent;
            this.ClosseWindow.BorderWidth = 0;
            this.ClosseWindow.ButtonShape = ButtonC.ButtonsShapes.Rect;
            this.ClosseWindow.Text = "X";
            this.ClosseWindow.EndColor = Color.FromArgb(220, 120, 80);
            this.ClosseWindow.FlatStyle = FlatStyle.Flat;
            this.ClosseWindow.Font = new Font("Microsoft YaHei UI", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.ClosseWindow.ForeColor = Color.White;
            this.ClosseWindow.GradientAngle = 90;
            this.ClosseWindow.Image = null;
            this.ClosseWindow.Image_Location = new Point(0, 0);
            this.ClosseWindow.ImageToHeight = false;
            this.ClosseWindow.Location = new Point(0x2bb, 2);
            this.ClosseWindow.MouseClickColor1 = Color.FromArgb(220, 120, 80);
            this.ClosseWindow.MouseClickColor2 = Color.FromArgb(220, 120, 80);
            this.ClosseWindow.MouseHoverColor1 = Color.FromArgb(220, 120, 80);
            this.ClosseWindow.MouseHoverColor2 = Color.FromArgb(220, 120, 80);
            this.ClosseWindow.Name = "ClosseWindow";
            this.ClosseWindow.ShowButtontext = true;
            this.ClosseWindow.Size = new Size(0x1f, 30);
            this.ClosseWindow.StartColor = Color.FromArgb(220, 120, 80);
            this.ClosseWindow.TabIndex = 0;
            this.ClosseWindow.Text = "X";
            this.ClosseWindow.TextLocation_X = 6;
            this.ClosseWindow.TextLocation_Y = 5;
            this.toolTip1.SetToolTip(this.ClosseWindow, "Close");
            this.ClosseWindow.Transparent1 = 250;
            this.ClosseWindow.Transparent2 = 250;
            this.ClosseWindow.UseVisualStyleBackColor = true;
            this.ClosseWindow.Click += new EventHandler(this._CloseButton_Click);
            this.DialogOpenTask.FileName = "Tasks";
            this.DialogOpenTask.Filter = "Tasks.data|*.data|All Files|*.*";
            this.UpdatesBox.BackColor = Color.WhiteSmoke;
            this.UpdatesBox.BorderStyle = BorderStyle.None;
            this.UpdatesBox.Cursor = Cursors.SizeNS;
            this.UpdatesBox.Location = new Point(3, 0x21);
            this.UpdatesBox.Name = "UpdatesBox";
            this.UpdatesBox.ReadOnly = true;
            this.UpdatesBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.UpdatesBox.Size = new Size(0x2d7, 0x1b6);
            this.UpdatesBox.TabIndex = 5;
            this.UpdatesBox.Text = "";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.WhiteSmoke;
            base.ClientSize = new Size(730, 0x1d9);
            base.Controls.Add(this.UpdatesBox);
            base.Controls.Add(this.TopPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "UpdatesForm";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "My App";
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            base.ResumeLayout(false);
        }

        public void SetText(List<string> Text)
        {
            if (this.Language == 1)
            {
                foreach (string str in Text)
                {
                    this.UpdatesBox.Text = str.StartsWith("В") ? (this.UpdatesBox.Text + $"						     {str}\n") : (this.UpdatesBox.Text + str + "\n");
                }
                foreach (string str2 in this.UpdatesBox.Lines)
                {
                    if (str2.StartsWith("\t\t\t\t\t\t     В"))
                    {
                        this.UpdatesBox.Select(this.UpdatesBox.Text.IndexOf(str2), str2.Length);
                        this.UpdatesBox.SelectionColor = Color.Green;
                        this.UpdatesBox.SelectionFont = new Font("Segui UI", 12f);
                    }
                }
                this.UpdatesBox.AutoScrollOffset = new Point(0, 0x3e8);
                this.UpdatesBox.Select(0, 0);
            }
            else
            {
                foreach (string str3 in Text)
                {
                    this.UpdatesBox.Text = str3.StartsWith("V") ? (this.UpdatesBox.Text + $"						     {str3}\n") : (this.UpdatesBox.Text + str3 + "\n");
                }
                foreach (string str4 in this.UpdatesBox.Lines)
                {
                    if (str4.StartsWith("\t\t\t\t\t\t     V"))
                    {
                        this.UpdatesBox.Select(this.UpdatesBox.Text.IndexOf(str4), str4.Length);
                        this.UpdatesBox.SelectionColor = Color.Green;
                        this.UpdatesBox.SelectionFont = new Font("Segui UI", 12f);
                    }
                }
                this.UpdatesBox.AutoScrollOffset = new Point(0, 0x3e8);
                this.UpdatesBox.Select(0, 0);
            }
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                this.isTopPanelDragged = false;
            }
            else
            {
                this.isTopPanelDragged = true;
                Point point = base.PointToScreen(new Point(e.X, e.Y));
                Point point2 = new Point {
                    X = base.Location.X - point.X,
                    Y = base.Location.Y - point.Y
                };
                this.offset = point2;
            }
            if (e.Clicks == 2)
            {
                this.isTopPanelDragged = false;
            }
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isTopPanelDragged)
            {
                Point point = this.TopPanel.PointToScreen(new Point(e.X, e.Y));
                point.Offset(this.offset);
                base.Location = point;
                if (((base.Location.X > 2) || (base.Location.Y > 2)) && (base.WindowState == FormWindowState.Maximized))
                {
                    base.Location = this._normalWindowLocation;
                    base.Size = this._normalWindowSize;
                    this.isWindowMaximized = false;
                }
            }
        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            this.isTopPanelDragged = false;
            if ((base.Location.Y <= 5) && !this.isWindowMaximized)
            {
                this._normalWindowSize = base.Size;
                this._normalWindowLocation = base.Location;
                Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
                base.Location = new Point(0, 0);
                base.Size = new Size(workingArea.Width, workingArea.Height);
                this.isWindowMaximized = true;
            }
        }

        private void WindowTextLabel_MouseDown(object sender, MouseEventArgs e)
        {
            this.TopPanel_MouseDown(sender, e);
        }

        private void WindowTextLabel_MouseMove(object sender, MouseEventArgs e)
        {
            this.TopPanel_MouseMove(sender, e);
        }

        private void WindowTextLabel_MouseUp(object sender, MouseEventArgs e)
        {
            this.TopPanel_MouseUp(sender, e);
        }

        public int SetLanguage
        {
            get => 
                this.Language;
            set
            {
                this.Language = value;
                if (this.Language == 1)
                {
                    this.comboBoxEx1.SelectedIndex = 0;
                }
                else
                {
                    this.comboBoxEx1.SelectedIndex = 1;
                }
            }
        }
    }
}

