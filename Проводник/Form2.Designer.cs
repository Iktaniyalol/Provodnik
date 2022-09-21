
namespace Проводник
{
    partial class RenameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OrPathLabel = new System.Windows.Forms.Label();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OriginalFilePathLapel = new System.Windows.Forms.Label();
            this.NewFileNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OrPathLabel
            // 
            this.OrPathLabel.AutoSize = true;
            this.OrPathLabel.Location = new System.Drawing.Point(12, 21);
            this.OrPathLabel.Name = "OrPathLabel";
            this.OrPathLabel.Size = new System.Drawing.Size(124, 15);
            this.OrPathLabel.TabIndex = 0;
            this.OrPathLabel.Text = "Оригинальный путь: ";
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Location = new System.Drawing.Point(12, 66);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(456, 23);
            this.FileNameTextBox.TabIndex = 1;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(12, 107);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(169, 57);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(299, 107);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(169, 57);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OriginalFilePathLapel
            // 
            this.OriginalFilePathLapel.AutoSize = true;
            this.OriginalFilePathLapel.Location = new System.Drawing.Point(142, 21);
            this.OriginalFilePathLapel.Name = "OriginalFilePathLapel";
            this.OriginalFilePathLapel.Size = new System.Drawing.Size(41, 15);
            this.OriginalFilePathLapel.TabIndex = 4;
            this.OriginalFilePathLapel.Text = "*path*";
            // 
            // NewFileNameLabel
            // 
            this.NewFileNameLabel.AutoSize = true;
            this.NewFileNameLabel.Location = new System.Drawing.Point(12, 48);
            this.NewFileNameLabel.Name = "NewFileNameLabel";
            this.NewFileNameLabel.Size = new System.Drawing.Size(136, 15);
            this.NewFileNameLabel.TabIndex = 5;
            this.NewFileNameLabel.Text = "Новое название файла:";
            // 
            // RenameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 177);
            this.Controls.Add(this.NewFileNameLabel);
            this.Controls.Add(this.OriginalFilePathLapel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.OrPathLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "RenameForm";
            this.Text = "Переименование";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label OrPathLabel;
        private System.Windows.Forms.TextBox FileNameTextBox;
        private System.Windows.Forms.Button OkButton;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label OriginalFilePathLapel;
        private System.Windows.Forms.Label NewFileNameLabel;
    }
}