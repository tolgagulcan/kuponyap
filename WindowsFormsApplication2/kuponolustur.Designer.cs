namespace WindowsFormsApplication2
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 459);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 63);
            this.button1.TabIndex = 0;
            this.button1.Text = "OLUŞTUR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rt1
            // 
            this.rt1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rt1.Location = new System.Drawing.Point(128, 12);
            this.rt1.Name = "rt1";
            this.rt1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rt1.Size = new System.Drawing.Size(634, 450);
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
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(803, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 31);
            this.label1.TabIndex = 6;
            this.label1.Text = "para";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(803, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 31);
            this.label2.TabIndex = 7;
            this.label2.Text = "para";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(803, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 31);
            this.label3.TabIndex = 8;
            this.label3.Text = "para";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(768, 285);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 56);
            this.button2.TabIndex = 10;
            this.button2.Text = "kaydet";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(809, 232);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "BİRLEŞTİR";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(794, 408);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 63);
            this.button3.TabIndex = 14;
            this.button3.Text = "OLUŞTUR";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // kuponolustur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 534);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rt1);
            this.Controls.Add(this.ilksecim);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button3;
    }
}
