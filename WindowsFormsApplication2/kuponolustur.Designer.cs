﻿namespace WindowsFormsApplication2
{
    partial class kuponolustur
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
            this.button1 = new System.Windows.Forms.Button();
            this.rt1 = new System.Windows.Forms.RichTextBox();
            this.ilksecim = new WindowsFormsApplication2.UserControl1();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 459);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 63);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rt1
            // 
            this.rt1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rt1.Location = new System.Drawing.Point(128, 12);
            this.rt1.Name = "rt1";
            this.rt1.Size = new System.Drawing.Size(634, 582);
            this.rt1.TabIndex = 4;
            this.rt1.Text = "";
            // 
            // ilksecim
            // 
            this.ilksecim.AutoSize = true;
            this.ilksecim.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ilksecim.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.ilksecim.Location = new System.Drawing.Point(12, 11);
            this.ilksecim.Name = "ilksecim";
            this.ilksecim.Size = new System.Drawing.Size(101, 429);
            this.ilksecim.TabIndex = 1;
            this.ilksecim.Load += new System.EventHandler(this.ilksecim_Load_1);
            // 
            // kuponolustur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 534);
            this.Controls.Add(this.rt1);
            this.Controls.Add(this.ilksecim);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Name = "kuponolustur";
            this.ShowIcon = false;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.kuponolustur_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Button button1;
        private UserControl1 ilksecim;
        private System.Windows.Forms.RichTextBox rt1;
    }
}
