using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SharedClasses;
using System.Diagnostics;

namespace MoveSvnToGit
{
	public partial class Form1 : Form
	{
		Dictionary<TextBox, string> textboxesWithInitialWatermarkText = new Dictionary<TextBox, string>();

		/* Inststructions to make use of svn://localhost
		 * In TortoiseSVN\bin (or Subversion\bin) forlder run svnserve.exe as follows:
		 * svnserve.exe --daemon --root c:\francois\dev\repos\vsprojects
		 * then can use svn url (for git-svn):
		 * svn://localhost/windowsstartupmanager
		 */

		public Form1()
		{
			InitializeComponent();

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

		private const string GitExePath = @"C:\Program Files (x86)\Git\bin\git.exe";
		private void Form1_Shown(object sender, EventArgs e)
		{
			if (!File.Exists(GitExePath))
			{
				UserMessages.ShowErrorMessage("Cannot find git EXE (click OK to exit): " + GitExePath);
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

		private bool ValidateAll()
		{
			foreach (var tb in textboxesWithInitialWatermarkText.Keys)
				if (tb.Text == textboxesWithInitialWatermarkText[tb] || string.IsNullOrWhiteSpace(tb.Text))
				{
					UserMessages.ShowWarningMessage("Invalid path for " + tb.Tag.ToString());
					tb.Focus();
					return false;
				}
			if (numericUpDownStartSvnRevisionNumber.Value < 0)
			{
				UserMessages.ShowWarningMessage("Enter valid valud for " + numericUpDownStartSvnRevisionNumber.Tag.ToString());
				numericUpDownStartSvnRevisionNumber.Focus();
				return false;
			}
			if (textBoxSvnUrl.Text.StartsWith("svn://localhost", StringComparison.InvariantCultureIgnoreCase)
				&& Process.GetProcessesByName("svnserve").Length == 0)
			{
				UserMessages.ShowWarningMessage("SvnUrl starts with 'svn://localhost', but no svnserve Processes found, this command will hence fail, please start svnserve first."
					+ Environment.NewLine
					+ Environment.NewLine + @"Most likely the svnserve.exe will sit in ProgramFiles\TortoiseSVN\bin or ProgramFile\Subversion\bin"
					+ Environment.NewLine
					+ Environment.NewLine + @"Working example is:"
					+ Environment.NewLine + @"svnserve.exe --daemon --root c:\francois\dev\repos\vsprojects"
					+ Environment.NewLine + "And then svn url can be:"
					+ Environment.NewLine + "svn://localhost/windowsstartupmanager/trunk");
				return false;
			}
			if (checkBoxInitRepo.Checked)//If requested to INIT the Git repo, check not already existing
			{
				string gitReportToInit = textBoxRemoteGitRepo.Text;
				if (gitReportToInit.Contains("://"))
				{
					UserMessages.ShowWarningMessage("Found the text '://' for the Remote Git repo, meaning  the repository is not local, not currently supported to init a remote git repo");
					return false;
				}
				if (Directory.Exists(gitReportToInit) && (Directory.GetFiles(gitReportToInit).Length > 0 || Directory.GetDirectories(gitReportToInit).Length > 0))
				{
					UserMessages.ShowWarningMessage("The Remote Git repo to INIT is a local directory but it is not empty, please ensure it is empty or the directory does not exist.");
					return false;
				}
				if (!Directory.Exists(gitReportToInit))
				{
					try
					{
						Directory.CreateDirectory(gitReportToInit);
						if (!Directory.Exists(gitReportToInit))
						{
							UserMessages.ShowWarningMessage("Unable to create Remote Git repo directory, cannot continue: " + gitReportToInit);
							return false;
						}
					}
					catch (Exception exc)
					{
						UserMessages.ShowWarningMessage("Unable to create Remote Git repo directory, cannot continue: " + exc.Message);
						return false;
					}
				}
			}
			return true;
		}

		bool isbusy = false;
		private void buttonAccept_Click(object sender, EventArgs e)
		{
			if (!ValidateAll())
				return;

			if (isbusy)
			{
				UserMessages.ShowWarningMessage("Please wait, already busy.");
				return;
			}

			isbusy = true;
			ThreadingInterop.DoAction(delegate
			{
				string remoteName = Path.GetFileNameWithoutExtension(textBoxRemoteGitRepo.Text);

				if (!Directory.Exists(textBoxLocalGitClonedFolder.Text))
					Directory.CreateDirectory(textBoxLocalGitClonedFolder.Text);

				List<string> commandListArguments = new List<string>();

				/*commandListArguments.Add(string.Format("svn init \"{0}\" \"{1}\"", textBoxSvnUrl.Text, textBoxLocalGitClonedFolder.Text));
				commandListArguments.Add(string.Format("svn fetch"));*/
				
				commandListArguments.Add(string.Format("svn clone{0} -r{1}:HEAD \"{2}\" \"{3}\"", checkBoxStandardLayout.Checked ? " -s" : "", numericUpDownStartSvnRevisionNumber.Value, textBoxSvnUrl.Text, textBoxLocalGitClonedFolder.Text));

				if (checkBoxInitRepo.Checked)
					commandListArguments.Add(string.Format("init --bare \"{0}\"", textBoxRemoteGitRepo.Text));
				//git remote add [-t <branch>] [-m <master>] [-f] [--tags|--no-tags] [--mirror=<fetch|push>] <name> <url>
				commandListArguments.Add(string.Format("remote add \"{0}\" \"{1}\"", remoteName, textBoxRemoteGitRepo.Text));
				commandListArguments.Add(string.Format("push \"{0}\" master", remoteName));

				bool allSuccess = true;
				foreach (var args in commandListArguments)
					if (!RunGitCommand(args, textBoxLocalGitClonedFolder.Text))
					{
						allSuccess = false;
						break;
					}

				if (allSuccess)
					Process.Start("explorer", textBoxLocalGitClonedFolder.Text);

				isbusy = false;
			},
			false);
		}

		private bool RunGitCommand(string arguments, string workingDir)
		{
			int exitCode;
			bool result = ProcessesInterop.StartAndWaitProcessRedirectOutput(
				new System.Diagnostics.ProcessStartInfo(GitExePath, arguments) { WorkingDirectory = workingDir },
				(obj, output) => { if (output != null) AppendMessage(output); },
				(obj, error) => { if (error != null) AppendMessage("ERROR: " + error); },
				out exitCode);
			if (result) AppendMessage("SUCCESS: arguments = " + arguments);
			return result;
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
	}
}
