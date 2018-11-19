namespace FileArchiveClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CustomerData_Mail_TextBox = new System.Windows.Forms.TextBox();
            this.CustomerData_Name_TextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TransactionData_TransactionID_TextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Search_Button = new System.Windows.Forms.Button();
            this.Console_Label = new System.Windows.Forms.Label();
            this.Console_TextBox = new System.Windows.Forms.TextBox();
            this.OtworzFolder_Button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(78, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dane klienta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(3, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Imie i nazwisko:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(3, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Email:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.CustomerData_Mail_TextBox);
            this.panel1.Controls.Add(this.CustomerData_Name_TextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(5, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 140);
            this.panel1.TabIndex = 5;
            // 
            // CustomerData_Mail_TextBox
            // 
            this.CustomerData_Mail_TextBox.Location = new System.Drawing.Point(4, 95);
            this.CustomerData_Mail_TextBox.Name = "CustomerData_Mail_TextBox";
            this.CustomerData_Mail_TextBox.Size = new System.Drawing.Size(256, 20);
            this.CustomerData_Mail_TextBox.TabIndex = 5;
            // 
            // CustomerData_Name_TextBox
            // 
            this.CustomerData_Name_TextBox.Location = new System.Drawing.Point(4, 51);
            this.CustomerData_Name_TextBox.Name = "CustomerData_Name_TextBox";
            this.CustomerData_Name_TextBox.Size = new System.Drawing.Size(256, 20);
            this.CustomerData_Name_TextBox.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.TransactionData_TransactionID_TextBox);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(274, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(266, 140);
            this.panel2.TabIndex = 6;
            // 
            // TransactionData_TransactionID_TextBox
            // 
            this.TransactionData_TransactionID_TextBox.Location = new System.Drawing.Point(6, 50);
            this.TransactionData_TransactionID_TextBox.Name = "TransactionData_TransactionID_TextBox";
            this.TransactionData_TransactionID_TextBox.Size = new System.Drawing.Size(256, 20);
            this.TransactionData_TransactionID_TextBox.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(78, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Dane transakcji";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(3, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Numer transakcji:";
            // 
            // Search_Button
            // 
            this.Search_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Search_Button.Location = new System.Drawing.Point(426, 155);
            this.Search_Button.Name = "Search_Button";
            this.Search_Button.Size = new System.Drawing.Size(114, 25);
            this.Search_Button.TabIndex = 7;
            this.Search_Button.Text = "Wyszukaj";
            this.Search_Button.UseVisualStyleBackColor = true;
            this.Search_Button.Click += new System.EventHandler(this.Search_Button_Click);
            // 
            // Console_Label
            // 
            this.Console_Label.AutoSize = true;
            this.Console_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Console_Label.Location = new System.Drawing.Point(2, 196);
            this.Console_Label.Name = "Console_Label";
            this.Console_Label.Size = new System.Drawing.Size(0, 16);
            this.Console_Label.TabIndex = 6;
            // 
            // Console_TextBox
            // 
            this.Console_TextBox.Location = new System.Drawing.Point(5, 155);
            this.Console_TextBox.Multiline = true;
            this.Console_TextBox.Name = "Console_TextBox";
            this.Console_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Console_TextBox.Size = new System.Drawing.Size(415, 57);
            this.Console_TextBox.TabIndex = 8;
            // 
            // OtworzFolder_Button
            // 
            this.OtworzFolder_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.OtworzFolder_Button.Location = new System.Drawing.Point(426, 186);
            this.OtworzFolder_Button.Name = "OtworzFolder_Button";
            this.OtworzFolder_Button.Size = new System.Drawing.Size(114, 25);
            this.OtworzFolder_Button.TabIndex = 9;
            this.OtworzFolder_Button.Text = "Otwórz folder";
            this.OtworzFolder_Button.UseVisualStyleBackColor = true;
            this.OtworzFolder_Button.Click += new System.EventHandler(this.OtworzFolder_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 215);
            this.Controls.Add(this.OtworzFolder_Button);
            this.Controls.Add(this.Console_TextBox);
            this.Controls.Add(this.Console_Label);
            this.Controls.Add(this.Search_Button);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Archiwum transakcji";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox CustomerData_Mail_TextBox;
        private System.Windows.Forms.TextBox CustomerData_Name_TextBox;
        private System.Windows.Forms.TextBox TransactionData_TransactionID_TextBox;
        private System.Windows.Forms.Button Search_Button;
        private System.Windows.Forms.Label Console_Label;
        private System.Windows.Forms.TextBox Console_TextBox;
        private System.Windows.Forms.Button OtworzFolder_Button;
    }
}

