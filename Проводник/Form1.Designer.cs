
namespace Проводник
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ObjectContainer = new System.Windows.Forms.ListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenButton = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyButton = new System.Windows.Forms.ToolStripMenuItem();
            this.CutButton = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameButton = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.PathBox = new System.Windows.Forms.TextBox();
            this.BackButton = new System.Windows.Forms.Button();
            this.ForwardButton = new System.Windows.Forms.Button();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.PropertiesPanel = new System.Windows.Forms.Panel();
            this.InfoOpenedLabel = new System.Windows.Forms.Label();
            this.OpenedLabel = new System.Windows.Forms.Label();
            this.InfoSizeLabel = new System.Windows.Forms.Label();
            this.InfoFullPathLabel = new System.Windows.Forms.Label();
            this.InfoChangedLabel = new System.Windows.Forms.Label();
            this.InfoCreatedLabel = new System.Windows.Forms.Label();
            this.InfoNameLabel = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.FullPathLabel = new System.Windows.Forms.Label();
            this.ChangedLabel = new System.Windows.Forms.Label();
            this.CreatiedLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.DesktopButton = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            this.PropertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ObjectContainer
            // 
            this.ObjectContainer.ContextMenuStrip = this.contextMenuStrip;
            this.ObjectContainer.FormattingEnabled = true;
            this.ObjectContainer.ItemHeight = 15;
            this.ObjectContainer.Location = new System.Drawing.Point(12, 74);
            this.ObjectContainer.Name = "ObjectContainer";
            this.ObjectContainer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ObjectContainer.Size = new System.Drawing.Size(247, 274);
            this.ObjectContainer.TabIndex = 0;
            this.ObjectContainer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ObjectContainer_MouseClick);
            this.ObjectContainer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ObjectContainer_MouseDoubleClick);
            this.ObjectContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ObjectContainer_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenButton,
            this.CopyButton,
            this.CutButton,
            this.PasteButton,
            this.RenameButton,
            this.DeleteButton});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(162, 136);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // OpenButton
            // 
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(161, 22);
            this.OpenButton.Text = "Открыть";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(161, 22);
            this.CopyButton.Text = "Копировать";
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // CutButton
            // 
            this.CutButton.Name = "CutButton";
            this.CutButton.Size = new System.Drawing.Size(161, 22);
            this.CutButton.Text = "Вырезать";
            this.CutButton.Click += new System.EventHandler(this.CutButton_Click);
            // 
            // PasteButton
            // 
            this.PasteButton.Enabled = false;
            this.PasteButton.Name = "PasteButton";
            this.PasteButton.Size = new System.Drawing.Size(161, 22);
            this.PasteButton.Text = "Вставить";
            this.PasteButton.Click += new System.EventHandler(this.PasteButton_Click);
            // 
            // RenameButton
            // 
            this.RenameButton.Name = "RenameButton";
            this.RenameButton.Size = new System.Drawing.Size(161, 22);
            this.RenameButton.Text = "Переименовать";
            this.RenameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(161, 22);
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // PathBox
            // 
            this.PathBox.BackColor = System.Drawing.SystemColors.Window;
            this.PathBox.Location = new System.Drawing.Point(12, 12);
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(776, 23);
            this.PathBox.TabIndex = 1;
            this.PathBox.Enter += new System.EventHandler(this.PathBox_Enter);
            this.PathBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PathBox_KeyDown);
            this.PathBox.Leave += new System.EventHandler(this.PathBox_Leave);
            // 
            // BackButton
            // 
            this.BackButton.Enabled = false;
            this.BackButton.Location = new System.Drawing.Point(12, 41);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(27, 27);
            this.BackButton.TabIndex = 2;
            this.BackButton.Text = "←";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // ForwardButton
            // 
            this.ForwardButton.Enabled = false;
            this.ForwardButton.Location = new System.Drawing.Point(45, 41);
            this.ForwardButton.Name = "ForwardButton";
            this.ForwardButton.Size = new System.Drawing.Size(27, 27);
            this.ForwardButton.TabIndex = 3;
            this.ForwardButton.Text = "→";
            this.ForwardButton.UseVisualStyleBackColor = true;
            this.ForwardButton.Click += new System.EventHandler(this.ForwardButton_Click);
            // 
            // ReloadButton
            // 
            this.ReloadButton.Location = new System.Drawing.Point(232, 41);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(27, 27);
            this.ReloadButton.TabIndex = 4;
            this.ReloadButton.Text = "↻";
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButton_Click);
            // 
            // PropertiesPanel
            // 
            this.PropertiesPanel.Controls.Add(this.InfoOpenedLabel);
            this.PropertiesPanel.Controls.Add(this.OpenedLabel);
            this.PropertiesPanel.Controls.Add(this.InfoSizeLabel);
            this.PropertiesPanel.Controls.Add(this.InfoFullPathLabel);
            this.PropertiesPanel.Controls.Add(this.InfoChangedLabel);
            this.PropertiesPanel.Controls.Add(this.InfoCreatedLabel);
            this.PropertiesPanel.Controls.Add(this.InfoNameLabel);
            this.PropertiesPanel.Controls.Add(this.SizeLabel);
            this.PropertiesPanel.Controls.Add(this.FullPathLabel);
            this.PropertiesPanel.Controls.Add(this.ChangedLabel);
            this.PropertiesPanel.Controls.Add(this.CreatiedLabel);
            this.PropertiesPanel.Controls.Add(this.NameLabel);
            this.PropertiesPanel.Location = new System.Drawing.Point(265, 74);
            this.PropertiesPanel.Name = "PropertiesPanel";
            this.PropertiesPanel.Size = new System.Drawing.Size(523, 274);
            this.PropertiesPanel.TabIndex = 5;
            this.PropertiesPanel.Visible = false;
            // 
            // InfoOpenedLabel
            // 
            this.InfoOpenedLabel.AutoSize = true;
            this.InfoOpenedLabel.Location = new System.Drawing.Point(137, 57);
            this.InfoOpenedLabel.Name = "InfoOpenedLabel";
            this.InfoOpenedLabel.Size = new System.Drawing.Size(31, 15);
            this.InfoOpenedLabel.TabIndex = 11;
            this.InfoOpenedLabel.Text = "Date";
            // 
            // OpenedLabel
            // 
            this.OpenedLabel.AutoSize = true;
            this.OpenedLabel.Location = new System.Drawing.Point(3, 57);
            this.OpenedLabel.Name = "OpenedLabel";
            this.OpenedLabel.Size = new System.Drawing.Size(125, 15);
            this.OpenedLabel.TabIndex = 10;
            this.OpenedLabel.Text = "Последнее открытие:";
            // 
            // InfoSizeLabel
            // 
            this.InfoSizeLabel.AutoSize = true;
            this.InfoSizeLabel.Location = new System.Drawing.Point(137, 87);
            this.InfoSizeLabel.Name = "InfoSizeLabel";
            this.InfoSizeLabel.Size = new System.Drawing.Size(27, 15);
            this.InfoSizeLabel.TabIndex = 9;
            this.InfoSizeLabel.Text = "Size";
            // 
            // InfoFullPathLabel
            // 
            this.InfoFullPathLabel.AutoSize = true;
            this.InfoFullPathLabel.Location = new System.Drawing.Point(137, 72);
            this.InfoFullPathLabel.Name = "InfoFullPathLabel";
            this.InfoFullPathLabel.Size = new System.Drawing.Size(31, 15);
            this.InfoFullPathLabel.TabIndex = 8;
            this.InfoFullPathLabel.Text = "Path";
            // 
            // InfoChangedLabel
            // 
            this.InfoChangedLabel.AutoSize = true;
            this.InfoChangedLabel.Location = new System.Drawing.Point(137, 42);
            this.InfoChangedLabel.Name = "InfoChangedLabel";
            this.InfoChangedLabel.Size = new System.Drawing.Size(31, 15);
            this.InfoChangedLabel.TabIndex = 7;
            this.InfoChangedLabel.Text = "Date";
            // 
            // InfoCreatedLabel
            // 
            this.InfoCreatedLabel.AutoSize = true;
            this.InfoCreatedLabel.Location = new System.Drawing.Point(137, 27);
            this.InfoCreatedLabel.Name = "InfoCreatedLabel";
            this.InfoCreatedLabel.Size = new System.Drawing.Size(31, 15);
            this.InfoCreatedLabel.TabIndex = 6;
            this.InfoCreatedLabel.Text = "Date";
            // 
            // InfoNameLabel
            // 
            this.InfoNameLabel.AutoSize = true;
            this.InfoNameLabel.Location = new System.Drawing.Point(137, 12);
            this.InfoNameLabel.Name = "InfoNameLabel";
            this.InfoNameLabel.Size = new System.Drawing.Size(39, 15);
            this.InfoNameLabel.TabIndex = 5;
            this.InfoNameLabel.Text = "Name";
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(3, 87);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(47, 15);
            this.SizeLabel.TabIndex = 4;
            this.SizeLabel.Text = "Размер";
            // 
            // FullPathLabel
            // 
            this.FullPathLabel.AutoSize = true;
            this.FullPathLabel.Location = new System.Drawing.Point(3, 72);
            this.FullPathLabel.Name = "FullPathLabel";
            this.FullPathLabel.Size = new System.Drawing.Size(83, 15);
            this.FullPathLabel.TabIndex = 3;
            this.FullPathLabel.Text = "Полный путь:";
            // 
            // ChangedLabel
            // 
            this.ChangedLabel.AutoSize = true;
            this.ChangedLabel.Location = new System.Drawing.Point(3, 42);
            this.ChangedLabel.Name = "ChangedLabel";
            this.ChangedLabel.Size = new System.Drawing.Size(59, 15);
            this.ChangedLabel.TabIndex = 2;
            this.ChangedLabel.Text = "Изменён:";
            // 
            // CreatiedLabel
            // 
            this.CreatiedLabel.AutoSize = true;
            this.CreatiedLabel.Location = new System.Drawing.Point(3, 27);
            this.CreatiedLabel.Name = "CreatiedLabel";
            this.CreatiedLabel.Size = new System.Drawing.Size(49, 15);
            this.CreatiedLabel.TabIndex = 1;
            this.CreatiedLabel.Text = "Создан:";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(3, 12);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(34, 15);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Имя:";
            // 
            // DesktopButton
            // 
            this.DesktopButton.Location = new System.Drawing.Point(663, 45);
            this.DesktopButton.Name = "DesktopButton";
            this.DesktopButton.Size = new System.Drawing.Size(125, 23);
            this.DesktopButton.TabIndex = 6;
            this.DesktopButton.Text = "Рабочий стол";
            this.DesktopButton.UseVisualStyleBackColor = true;
            this.DesktopButton.Click += new System.EventHandler(this.DesktopButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 357);
            this.Controls.Add(this.DesktopButton);
            this.Controls.Add(this.PropertiesPanel);
            this.Controls.Add(this.ReloadButton);
            this.Controls.Add(this.ForwardButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.PathBox);
            this.Controls.Add(this.ObjectContainer);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Проводник";
            this.contextMenuStrip.ResumeLayout(false);
            this.PropertiesPanel.ResumeLayout(false);
            this.PropertiesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ObjectContainer;
        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenButton;
        private System.Windows.Forms.ToolStripMenuItem CopyButton;
        private System.Windows.Forms.ToolStripMenuItem CutButton;
        private System.Windows.Forms.ToolStripMenuItem PasteButton;
        private System.Windows.Forms.ToolStripMenuItem RenameButton;
        private System.Windows.Forms.ToolStripMenuItem DeleteButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button ForwardButton;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.Panel PropertiesPanel;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Label FullPathLabel;
        private System.Windows.Forms.Label ChangedLabel;
        private System.Windows.Forms.Label CreatiedLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label InfoSizeLabel;
        private System.Windows.Forms.Label InfoFullPathLabel;
        private System.Windows.Forms.Label InfoChangedLabel;
        private System.Windows.Forms.Label InfoCreatedLabel;
        private System.Windows.Forms.Label InfoNameLabel;
        private System.Windows.Forms.Label InfoOpenedLabel;
        private System.Windows.Forms.Label OpenedLabel;
        private System.Windows.Forms.Button DesktopButton;
    }
}

