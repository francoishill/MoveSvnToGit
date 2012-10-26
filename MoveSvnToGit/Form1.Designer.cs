namespace MoveSvnToGit
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
			this.textBoxSvnUrl = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxLocalGitClonedFolder = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxRemoteGitRepo = new System.Windows.Forms.TextBox();
			this.buttonAccept = new System.Windows.Forms.Button();
			this.textBoxMessages = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.numericUpDownStartSvnRevisionNumber = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBoxStandardLayout = new System.Windows.Forms.CheckBox();
			this.checkBoxInitRepo = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartSvnRevisionNumber)).BeginInit();
			this.SuspendLayout();
			// 
			// textBoxSvnUrl
			// 
			this.textBoxSvnUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSvnUrl.Location = new System.Drawing.Point(126, 12);
			this.textBoxSvnUrl.Name = "textBoxSvnUrl";
			this.textBoxSvnUrl.Size = new System.Drawing.Size(440, 20);
			this.textBoxSvnUrl.TabIndex = 1;
			this.textBoxSvnUrl.Tag = "Svn repo url";
			this.textBoxSvnUrl.Text = "svn://path/to repo   OR   http://path/to/repo   OR   c:\\path\\to\\repo";
			this.textBoxSvnUrl.Enter += new System.EventHandler(this.textBox_Enter);
			this.textBoxSvnUrl.Leave += new System.EventHandler(this.textBox_Leave);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(53, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Svn repo url:";
			// 
			// textBoxLocalGitClonedFolder
			// 
			this.textBoxLocalGitClonedFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLocalGitClonedFolder.Location = new System.Drawing.Point(126, 100);
			this.textBoxLocalGitClonedFolder.Name = "textBoxLocalGitClonedFolder";
			this.textBoxLocalGitClonedFolder.Size = new System.Drawing.Size(440, 20);
			this.textBoxLocalGitClonedFolder.TabIndex = 4;
			this.textBoxLocalGitClonedFolder.Tag = "Local Git folder (cloned)";
			this.textBoxLocalGitClonedFolder.Text = "c:\\path\\to\\clone\\svn\\code";
			this.textBoxLocalGitClonedFolder.Enter += new System.EventHandler(this.textBox_Enter);
			this.textBoxLocalGitClonedFolder.Leave += new System.EventHandler(this.textBox_Leave);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 103);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Local Git folder (cloned):";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(33, 134);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(87, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Remote Git repo:";
			// 
			// textBoxRemoteGitRepo
			// 
			this.textBoxRemoteGitRepo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRemoteGitRepo.Location = new System.Drawing.Point(126, 131);
			this.textBoxRemoteGitRepo.Name = "textBoxRemoteGitRepo";
			this.textBoxRemoteGitRepo.Size = new System.Drawing.Size(369, 20);
			this.textBoxRemoteGitRepo.TabIndex = 5;
			this.textBoxRemoteGitRepo.Tag = "Remote Git repo";
			this.textBoxRemoteGitRepo.Text = "git://path/to/git/repo   OR   c:\\path\\to\\git\\repo";
			this.textBoxRemoteGitRepo.Enter += new System.EventHandler(this.textBox_Enter);
			this.textBoxRemoteGitRepo.Leave += new System.EventHandler(this.textBox_Leave);
			// 
			// buttonAccept
			// 
			this.buttonAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAccept.Location = new System.Drawing.Point(501, 331);
			this.buttonAccept.Name = "buttonAccept";
			this.buttonAccept.Size = new System.Drawing.Size(65, 23);
			this.buttonAccept.TabIndex = 0;
			this.buttonAccept.Text = "A&ccept";
			this.buttonAccept.UseVisualStyleBackColor = true;
			this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
			// 
			// textBoxMessages
			// 
			this.textBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMessages.Location = new System.Drawing.Point(12, 164);
			this.textBoxMessages.Multiline = true;
			this.textBoxMessages.Name = "textBoxMessages";
			this.textBoxMessages.ReadOnly = true;
			this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxMessages.Size = new System.Drawing.Size(554, 161);
			this.textBoxMessages.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(26, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(94, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Start from revision:";
			// 
			// numericUpDownStartSvnRevisionNumber
			// 
			this.numericUpDownStartSvnRevisionNumber.Location = new System.Drawing.Point(126, 61);
			this.numericUpDownStartSvnRevisionNumber.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
			this.numericUpDownStartSvnRevisionNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.numericUpDownStartSvnRevisionNumber.Name = "numericUpDownStartSvnRevisionNumber";
			this.numericUpDownStartSvnRevisionNumber.Size = new System.Drawing.Size(92, 20);
			this.numericUpDownStartSvnRevisionNumber.TabIndex = 3;
			this.numericUpDownStartSvnRevisionNumber.Tag = "Start from revision";
			this.numericUpDownStartSvnRevisionNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(33, 39);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Standard layout:";
			// 
			// checkBoxStandardLayout
			// 
			this.checkBoxStandardLayout.AutoSize = true;
			this.checkBoxStandardLayout.Location = new System.Drawing.Point(126, 38);
			this.checkBoxStandardLayout.Name = "checkBoxStandardLayout";
			this.checkBoxStandardLayout.Size = new System.Drawing.Size(231, 17);
			this.checkBoxStandardLayout.TabIndex = 2;
			this.checkBoxStandardLayout.Text = "Standard layout   ( trunk / branches / tags )";
			this.checkBoxStandardLayout.UseVisualStyleBackColor = true;
			// 
			// checkBoxInitRepo
			// 
			this.checkBoxInitRepo.AutoSize = true;
			this.checkBoxInitRepo.Location = new System.Drawing.Point(501, 133);
			this.checkBoxInitRepo.Name = "checkBoxInitRepo";
			this.checkBoxInitRepo.Size = new System.Drawing.Size(64, 17);
			this.checkBoxInitRepo.TabIndex = 9;
			this.checkBoxInitRepo.Text = "Init repo";
			this.checkBoxInitRepo.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(578, 366);
			this.Controls.Add(this.checkBoxInitRepo);
			this.Controls.Add(this.checkBoxStandardLayout);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.numericUpDownStartSvnRevisionNumber);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxMessages);
			this.Controls.Add(this.buttonAccept);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxRemoteGitRepo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxLocalGitClonedFolder);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxSvnUrl);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Move SVN to Git";
			this.Shown += new System.EventHandler(this.Form1_Shown);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartSvnRevisionNumber)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxSvnUrl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxLocalGitClonedFolder;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxRemoteGitRepo;
		private System.Windows.Forms.Button buttonAccept;
		private System.Windows.Forms.TextBox textBoxMessages;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numericUpDownStartSvnRevisionNumber;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBoxStandardLayout;
		private System.Windows.Forms.CheckBox checkBoxInitRepo;
	}
}

