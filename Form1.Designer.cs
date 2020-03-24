namespace SAPI_Unifier
{
	partial class Form_main
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
			this.textBox_report = new System.Windows.Forms.TextBox();
			this.label_info = new System.Windows.Forms.Label();
			this.button_exit = new System.Windows.Forms.Button();
			this.button_about = new System.Windows.Forms.Button();
			this.linkLabel_website = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// textBox_report
			// 
			this.textBox_report.Location = new System.Drawing.Point(18, 38);
			this.textBox_report.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.textBox_report.Multiline = true;
			this.textBox_report.Name = "textBox_report";
			this.textBox_report.ReadOnly = true;
			this.textBox_report.Size = new System.Drawing.Size(822, 187);
			this.textBox_report.TabIndex = 1;
			// 
			// label_info
			// 
			this.label_info.AutoSize = true;
			this.label_info.Location = new System.Drawing.Point(18, 14);
			this.label_info.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label_info.Name = "label_info";
			this.label_info.Size = new System.Drawing.Size(814, 20);
			this.label_info.TabIndex = 0;
			this.label_info.Text = "This program unifies installed Microsoft OneCore voices and Microsoft Speech Serv" +
    "er voices with Microsoft SAPI 5.";
			// 
			// button_exit
			// 
			this.button_exit.Location = new System.Drawing.Point(628, 235);
			this.button_exit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button_exit.Name = "button_exit";
			this.button_exit.Size = new System.Drawing.Size(112, 35);
			this.button_exit.TabIndex = 4;
			this.button_exit.Text = "Exit";
			this.button_exit.UseVisualStyleBackColor = true;
			this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
			// 
			// button_about
			// 
			this.button_about.Location = new System.Drawing.Point(102, 235);
			this.button_about.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button_about.Name = "button_about";
			this.button_about.Size = new System.Drawing.Size(156, 35);
			this.button_about.TabIndex = 2;
			this.button_about.Text = "About SAPI Unifier";
			this.button_about.UseVisualStyleBackColor = true;
			this.button_about.Click += new System.EventHandler(this.button_about_Click);
			// 
			// linkLabel_website
			// 
			this.linkLabel_website.AutoSize = true;
			this.linkLabel_website.Location = new System.Drawing.Point(361, 242);
			this.linkLabel_website.Name = "linkLabel_website";
			this.linkLabel_website.Size = new System.Drawing.Size(149, 20);
			this.linkLabel_website.TabIndex = 3;
			this.linkLabel_website.TabStop = true;
			this.linkLabel_website.Text = "Visit project website";
			this.linkLabel_website.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_website_LinkClicked);
			// 
			// Form_main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(852, 285);
			this.Controls.Add(this.linkLabel_website);
			this.Controls.Add(this.button_about);
			this.Controls.Add(this.button_exit);
			this.Controls.Add(this.label_info);
			this.Controls.Add(this.textBox_report);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Form_main";
			this.Text = "SAPI Unifier";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_report;
		private System.Windows.Forms.Label label_info;
		private System.Windows.Forms.Button button_exit;
		private System.Windows.Forms.Button button_about;
		private System.Windows.Forms.LinkLabel linkLabel_website;
	}
}

