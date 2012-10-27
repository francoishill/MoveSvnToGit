using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using SharedClasses;

namespace MoveSvnToGit
{
	public partial class Form1 : Form
	{
		/* Inststructions to make use of svn://localhost
		 * In TortoiseSVN\bin (or Subversion\bin) forlder run svnserve.exe as follows:
		 * svnserve.exe --daemon --root c:\francois\dev\repos\vsprojects
		 * then can use svn url (for git-svn):
		 * svn://localhost/windowsstartupmanager
		 */

		Dictionary<TextBox, string> textboxesWithInitialWatermarkText = new Dictionary<TextBox, string>();
		List<MoveFromSvnToGit> foldersToMove = new List<MoveFromSvnToGit>();

		public Form1()
		{
			InitializeComponent();

			ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
			buttonAllInFolder.Visible = Directory.Exists(@"C:\Francois\Dev\VSprojects");

			textboxesWithInitialWatermarkText.Add(textBoxSvnUrl, textBoxSvnUrl.Text);
			textboxesWithInitialWatermarkText.Add(textBoxLocalGitClonedFolder, textBoxLocalGitClonedFolder.Text);
			textboxesWithInitialWatermarkText.Add(textBoxRemoteGitRepo, textBoxRemoteGitRepo.Text);

			//file:///C:/Francois/Dev/repos/vsprojects/windowsstartupmanager/trunk
			textBoxSvnUrl.Text = @"svn://localhost/windowsstartupmanager/trunk";
			checkBoxStandardLayout.Checked = false;//Because we only check out the Trunc url we dont use StandardLayout
			numericUpDownStartSvnRevisionNumber.Value = 1;
			textBoxLocalGitClonedFolder.Text = @"C:\Francois\Other\tmp\testGit\WindowsStartupManager";
			textBoxRemoteGitRepo.Text = @"C:\Francois\Other\tmp\testGit\WindowsStartupManager_repo";
			checkBoxInitRepo.Checked = true;

			/*textBoxSvnUrl.Text = "svn://iserver.gls.co.za/GLS/Version6/GLSCore6";
			numericUpDownStartSvnRevisionNumber.Value = 3828;
			textBoxLocalGitClonedFolder.Text = @"C:\ProgrammingGit\test\GLSCore6";
			textBoxRemoteGitRepo.Text = @"C:\ProgrammingGit\_repos\GLSCore6";*/
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			if (!File.Exists(MoveFromSvnToGit.GitExePath))
			{
				UserMessages.ShowErrorMessage("Cannot find git EXE (click OK to exit): " + MoveFromSvnToGit.GitExePath);
				Application.Exit();
			}
			else if (!File.Exists(MoveFromSvnToGit.SvnExePath))
			{
				UserMessages.ShowErrorMessage("Cannot find svn EXE (click OK to exit): " + MoveFromSvnToGit.SvnExePath);
				Application.Exit();
			}
			else if (!File.Exists(MoveFromSvnToGit.SvnserveExePath))
			{
				UserMessages.ShowErrorMessage("Cannot find svnserve EXE (click OK to exit): " + MoveFromSvnToGit.SvnserveExePath);
				Application.Exit();
			}
		}

		private void textBox_Enter(object sender, EventArgs e)
		{
			var textboxesKeys = textboxesWithInitialWatermarkText.Keys.ToArray();
			for (int i = 0; i < textboxesKeys.Length; i++)
				if (sender == textboxesKeys[i] && textboxesKeys[i].Text == textboxesWithInitialWatermarkText[textboxesKeys[i]])
					textboxesKeys[i].Text = null;
		}

		private void textBox_Leave(object sender, EventArgs e)
		{
			var textboxesKeys = textboxesWithInitialWatermarkText.Keys.ToArray();
			for (int i = 0; i < textboxesKeys.Length; i++)
				if (sender == textboxesKeys[i] && string.IsNullOrWhiteSpace(textboxesKeys[i].Text))
					textboxesKeys[i].Text = textboxesWithInitialWatermarkText[textboxesKeys[i]];
		}

		//static bool isbusy = false;
		private void buttonAccept_Click(object sender, EventArgs e)
		{
			//if (isbusy)
			//{
			//    UserMessages.ShowWarningMessage("Please wait, already busy.");
			//    return;
			//}
			//isbusy = true;

			foreach (var tb in textboxesWithInitialWatermarkText.Keys)
				if (tb.Text == textboxesWithInitialWatermarkText[tb] || string.IsNullOrWhiteSpace(tb.Text))
				{
					UserMessages.ShowWarningMessage("Invalid path for " + tb.Tag.ToString());
					tb.Focus();
					return;
				}

			string svnservePath = null;
			if (textBoxSvnUrl.Text.StartsWith("svn://localhost", StringComparison.InvariantCultureIgnoreCase)
				|| textBoxSvnUrl.Text.StartsWith("file:", StringComparison.InvariantCultureIgnoreCase)
				|| textBoxSvnUrl.Text[1] == ':' /*it is a file path like c:\.. or d:\...*/)
			{
				svnservePath = FileSystemInterop.SelectFolder("Please select the PARENT FOLDER of the local SVN repo, will be required to start svnserve.exe. Git-Svn cannot use local repo paths.");
				if (svnservePath == null)
				{
					UserMessages.ShowWarningMessage("Operation cancelled to move svn url: " + textBoxSvnUrl.Text);
					return;
				}
			}
			MoveFromSvnToGit tmpMove = new MoveFromSvnToGit(
				textBoxSvnUrl.Text, checkBoxStandardLayout.Checked, (int)numericUpDownStartSvnRevisionNumber.Value,
				textBoxLocalGitClonedFolder.Text, textBoxRemoteGitRepo.Text, checkBoxInitRepo.Checked,
				svnservePath);
			if (!tmpMove.ValidateAll(delegate { numericUpDownStartSvnRevisionNumber.Focus(); }))
				return;
			tmpMove.MoveNow(AppendMessage, false, true, true);
		}

		private void AppendMessage(string message)
		{
			Action<string> appendAction = (ms) =>
			{
				textBoxMessages.Text += GetCurrentDateString() + ms + Environment.NewLine;
				textBoxMessages.SelectionStart = textBoxMessages.Text.Length;
				textBoxMessages.SelectionLength = 0;
				textBoxMessages.ScrollToCaret();
			};
			if (this.InvokeRequired)
				this.Invoke(appendAction, message);
			else
				appendAction(message);
		}

		private string GetCurrentDateString()
		{
			return "[" + DateTime.Now.ToString("HH:mm:ss") + "] ";
		}

		private void buttonAllInFolder_Click(object sender, EventArgs e)
		{
			string rootSvnFolder = FileSystemInterop.SelectFolder(
				"Select a root folder to search for all subversion controlled (svn checkouts) subfolders (will look for .svn folder inside each).",
				@"C:\Francois\Dev\VSprojects",
				Environment.SpecialFolder.MyComputer, this);
			if (rootSvnFolder == null) return;
			string destinationFolderForGitClones = FileSystemInterop.SelectFolder(
				"Select a root destination folder to place Git version controlled folders (git cloned)",
				@"c:\francois\tmp\SvnToGit\ClonedFromSvn",
				Environment.SpecialFolder.MyComputer, this);
			if (destinationFolderForGitClones == null) return;
			string destinationRootForGitRemoteRepos = FileSystemInterop.SelectFolder(
				"Select a root destination folder where the Git repos will be initialized (the 'remote' repos)",
				@"c:\francois\tmp\SvnToGit\_gitRepos",
				Environment.SpecialFolder.MyComputer, this);
			if (destinationRootForGitRemoteRepos == null) return;
			MoveFromSvnToGit.MoveAllValidSvnCheckoutsInFolderToGitClones(
				rootSvnFolder,
				destinationFolderForGitClones,
				destinationRootForGitRemoteRepos,
				AppendMessage,
				UserMessages.Confirm("Do you want to automatically close svnserve.exe processes when we require to start one for local svn repos? (Choose no to be prompted every time)."));
		}
	}
}
