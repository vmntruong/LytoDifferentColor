namespace PremierePictureBoxApp
{
    partial class Form1
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Test = new System.Windows.Forms.Button();
            this.btn_live = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tB_maxClicks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tB_numberOfClick = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nUpDown_startX = new System.Windows.Forms.NumericUpDown();
            this.nUpDown_startY = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_startX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_startY)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(538, 271);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(159, 36);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(538, 329);
            this.btn_Test.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(122, 31);
            this.btn_Test.TabIndex = 6;
            this.btn_Test.Text = "Import picture";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // btn_live
            // 
            this.btn_live.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_live.Location = new System.Drawing.Point(538, 387);
            this.btn_live.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_live.Name = "btn_live";
            this.btn_live.Size = new System.Drawing.Size(89, 41);
            this.btn_live.TabIndex = 7;
            this.btn_live.Text = "Auto";
            this.btn_live.UseVisualStyleBackColor = true;
            this.btn_live.Click += new System.EventHandler(this.btn_live_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox1.Location = new System.Drawing.Point(67, 26);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(383, 383);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(689, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 9;
            this.button1.Text = "Test";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(657, 399);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(535, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Max Clicks";
            // 
            // tB_maxClicks
            // 
            this.tB_maxClicks.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tB_maxClicks.Location = new System.Drawing.Point(658, 26);
            this.tB_maxClicks.MaxLength = 5;
            this.tB_maxClicks.Name = "tB_maxClicks";
            this.tB_maxClicks.Size = new System.Drawing.Size(102, 45);
            this.tB_maxClicks.TabIndex = 12;
            this.tB_maxClicks.Text = "200";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(537, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Number of Clicks";
            // 
            // tB_numberOfClick
            // 
            this.tB_numberOfClick.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tB_numberOfClick.Location = new System.Drawing.Point(657, 88);
            this.tB_numberOfClick.MaxLength = 5;
            this.tB_numberOfClick.Name = "tB_numberOfClick";
            this.tB_numberOfClick.Size = new System.Drawing.Size(75, 30);
            this.tB_numberOfClick.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(543, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "start X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(543, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "start Y";
            // 
            // nUpDown_startX
            // 
            this.nUpDown_startX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nUpDown_startX.Location = new System.Drawing.Point(618, 170);
            this.nUpDown_startX.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.nUpDown_startX.Name = "nUpDown_startX";
            this.nUpDown_startX.Size = new System.Drawing.Size(79, 30);
            this.nUpDown_startX.TabIndex = 15;
            this.nUpDown_startX.Value = new decimal(new int[] {
            289,
            0,
            0,
            0});
            // 
            // nUpDown_startY
            // 
            this.nUpDown_startY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nUpDown_startY.Location = new System.Drawing.Point(618, 214);
            this.nUpDown_startY.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.nUpDown_startY.Name = "nUpDown_startY";
            this.nUpDown_startY.Size = new System.Drawing.Size(79, 30);
            this.nUpDown_startY.TabIndex = 15;
            this.nUpDown_startY.Value = new decimal(new int[] {
            503,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 472);
            this.Controls.Add(this.nUpDown_startY);
            this.Controls.Add(this.nUpDown_startX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tB_numberOfClick);
            this.Controls.Add(this.tB_maxClicks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_live);
            this.Controls.Add(this.btn_Test);
            this.Controls.Add(this.textBox1);
            this.Location = new System.Drawing.Point(960, 300);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_startX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_startY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.Button btn_live;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB_maxClicks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tB_numberOfClick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nUpDown_startX;
        private System.Windows.Forms.NumericUpDown nUpDown_startY;
    }
}

