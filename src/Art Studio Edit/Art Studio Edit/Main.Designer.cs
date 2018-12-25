namespace Art_Studio_Edit
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.getuuid_button = new System.Windows.Forms.Button();
            this.browse_button = new System.Windows.Forms.Button();
            this.encode_button = new System.Windows.Forms.Button();
            this.uuid_combobox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.decode_button = new System.Windows.Forms.Button();
            this.mainListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uuid_capture = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // getuuid_button
            // 
            this.getuuid_button.BackColor = System.Drawing.SystemColors.Control;
            this.getuuid_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getuuid_button.ForeColor = System.Drawing.SystemColors.MenuText;
            this.getuuid_button.Location = new System.Drawing.Point(9, 13);
            this.getuuid_button.Name = "getuuid_button";
            this.getuuid_button.Size = new System.Drawing.Size(108, 29);
            this.getuuid_button.TabIndex = 0;
            this.getuuid_button.Text = "Get UUID";
            this.getuuid_button.UseVisualStyleBackColor = false;
            this.getuuid_button.Click += new System.EventHandler(this.getuuid_button_Click);
            // 
            // browse_button
            // 
            this.browse_button.BackColor = System.Drawing.SystemColors.Control;
            this.browse_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browse_button.ForeColor = System.Drawing.SystemColors.MenuText;
            this.browse_button.Location = new System.Drawing.Point(9, 106);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(108, 29);
            this.browse_button.TabIndex = 1;
            this.browse_button.Text = "Browse";
            this.browse_button.UseVisualStyleBackColor = false;
            this.browse_button.Click += new System.EventHandler(this.browse_button_Click);
            // 
            // encode_button
            // 
            this.encode_button.BackColor = System.Drawing.SystemColors.Control;
            this.encode_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.encode_button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.encode_button.Location = new System.Drawing.Point(9, 153);
            this.encode_button.Name = "encode_button";
            this.encode_button.Size = new System.Drawing.Size(137, 30);
            this.encode_button.TabIndex = 4;
            this.encode_button.Text = "Encode";
            this.encode_button.UseVisualStyleBackColor = false;
            this.encode_button.Click += new System.EventHandler(this.encode_button_Click);
            // 
            // uuid_combobox
            // 
            this.uuid_combobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uuid_combobox.FormattingEnabled = true;
            this.uuid_combobox.Location = new System.Drawing.Point(123, 62);
            this.uuid_combobox.Name = "uuid_combobox";
            this.uuid_combobox.Size = new System.Drawing.Size(383, 23);
            this.uuid_combobox.TabIndex = 5;
            this.uuid_combobox.SelectedIndexChanged += new System.EventHandler(this.uuid_combobox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 61);
            this.label3.MinimumSize = new System.Drawing.Size(104, 23);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(104, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "UUID List:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(123, 110);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(383, 21);
            this.textBox3.TabIndex = 9;
            // 
            // decode_button
            // 
            this.decode_button.BackColor = System.Drawing.SystemColors.Control;
            this.decode_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decode_button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.decode_button.Location = new System.Drawing.Point(155, 153);
            this.decode_button.Name = "decode_button";
            this.decode_button.Size = new System.Drawing.Size(137, 30);
            this.decode_button.TabIndex = 10;
            this.decode_button.Text = "Decode";
            this.decode_button.UseVisualStyleBackColor = false;
            this.decode_button.Click += new System.EventHandler(this.decode_button_Click);
            // 
            // mainListBox
            // 
            this.mainListBox.BackColor = System.Drawing.SystemColors.Window;
            this.mainListBox.FormattingEnabled = true;
            this.mainListBox.ItemHeight = 14;
            this.mainListBox.Location = new System.Drawing.Point(300, 153);
            this.mainListBox.Name = "mainListBox";
            this.mainListBox.Size = new System.Drawing.Size(206, 242);
            this.mainListBox.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label4.Location = new System.Drawing.Point(6, 403);
            this.label4.MinimumSize = new System.Drawing.Size(500, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(500, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "AJ Art Converter - a program by cfr0st";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // uuid_capture
            // 
            this.uuid_capture.AutoSize = true;
            this.uuid_capture.BackColor = System.Drawing.SystemColors.Window;
            this.uuid_capture.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uuid_capture.Location = new System.Drawing.Point(311, 13);
            this.uuid_capture.MinimumSize = new System.Drawing.Size(195, 29);
            this.uuid_capture.Name = "uuid_capture";
            this.uuid_capture.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.uuid_capture.Size = new System.Drawing.Size(195, 29);
            this.uuid_capture.TabIndex = 18;
            this.uuid_capture.Text = "[UUID Capture is Off]";
            this.uuid_capture.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Art_Studio_Edit.Properties.Resources.cubes;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(9, 189);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(283, 206);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label1.Location = new System.Drawing.Point(125, 13);
            this.label1.MinimumSize = new System.Drawing.Size(180, 29);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(180, 29);
            this.label1.TabIndex = 19;
            this.label1.Text = "Captures UUID while logging in";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(518, 429);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uuid_capture);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mainListBox);
            this.Controls.Add(this.decode_button);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uuid_combobox);
            this.Controls.Add(this.encode_button);
            this.Controls.Add(this.browse_button);
            this.Controls.Add(this.getuuid_button);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Art Studio Edit v1.1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getuuid_button;
        private System.Windows.Forms.Button browse_button;
        private System.Windows.Forms.Button encode_button;
        private System.Windows.Forms.ComboBox uuid_combobox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button decode_button;
        private System.Windows.Forms.ListBox mainListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label uuid_capture;
        private System.Windows.Forms.Label label1;
    }
}

