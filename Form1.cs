using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32; // it's required for reading and writing to the windows registry

namespace SAPI_Unifier
{
	public partial class Form_main : Form
	{
		public Form_main()
		{
			InitializeComponent();
			//MessageBox.Show("Program is Running!"); // to check validity of single instance running. It's work fine :) Thanks https://www.c-sharpcorner.com/UploadFile/f9f215/how-to-restrict-the-application-to-just-one-instance/
			string SAPI5Path = "SOFTWARE\\Microsoft\\SPEECH\\Voices\\Tokens\\";
			string MobilePath = "SOFTWARE\\Microsoft\\Speech_OneCore\\Voices\\Tokens\\";
			string ServerPath = "SOFTWARE\\Microsoft\\Speech Server\\v11.0\\Voices\\Tokens\\";
			string CortanaPath = "SOFTWARE\\Microsoft\\Speech_OneCore\\CortanaVoices\\Tokens\\";

			if (System.Environment.Is64BitOperatingSystem)
			{
				SAPI5Path = "SOFTWARE\\WOW6432Node\\Microsoft\\SPEECH\\Voices\\Tokens\\";
				MobilePath = "SOFTWARE\\WOW6432Node\\Microsoft\\Speech_OneCore\\Voices\\Tokens\\";
				ServerPath = "SOFTWARE\\WOW6432Node\\Microsoft\\Speech Server\\v11.0\\Voices\\Tokens\\";
			}

	        string[] SAPI5Tokens = new string[] { };
		    string[] MobileTokens = new string[] { };
			string[] ServerTokens = new string[] { };
			string[] CortanaTokens = new string[] { };
			
			try
			{
				//// Fix possible inconsistencies in SAPI 5 voices due to some uninstalled mobile or server voices
				RegistryKey key = Registry.LocalMachine.OpenSubKey(SAPI5Path);
		        if (key != null) SAPI5Tokens = key.GetSubKeyNames();
				key = Registry.LocalMachine.OpenSubKey(MobilePath);
				if (key != null) MobileTokens = key.GetSubKeyNames();
				key = Registry.LocalMachine.OpenSubKey(ServerPath);
				if (key != null) ServerTokens = key.GetSubKeyNames();
				key = Registry.LocalMachine.OpenSubKey(CortanaPath);
				if (key != null) CortanaTokens = key.GetSubKeyNames();
			}
			catch
			{
				MessageBox.Show("An Error in the preliminary analysis is occurred!", "Error #1", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			foreach (string SAPI5Token in SAPI5Tokens)
			{
				//MessageBox.Show(SAPI5Token);
				//MessageBox.Show(Array.IndexOf(MobileTokens, SAPI5Token).ToString());
				try
				{
					// Remove uninstalled oneCore voices from the SAPI 5 interface
					if (SAPI5Token.Contains("MSTTS_V110_"))
					{
						if (Array.IndexOf(MobileTokens, SAPI5Token) < 0 & Array.IndexOf(CortanaTokens, SAPI5Token) < 0)
						{
							//MessageBox.Show(SAPI5Token);
							RegistryKey key = Registry.LocalMachine.OpenSubKey(SAPI5Path + "\\" + SAPI5Token);
							string VoiceName = key.GetValue("").ToString();
							textBox_report.Text = VoiceName + " which was an uninstalled OneCore voice is removed from SAPI 5." + Environment.NewLine;
							key = Registry.LocalMachine.OpenSubKey(SAPI5Path, true); // request write permission
							key.DeleteSubKeyTree(SAPI5Token, false); // do not raise an error if the subkey be missing
						}
					}
				}
				catch
				{
					MessageBox.Show("An Error in removing of " + SAPI5Token + " is occurred!", "Error #2", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				try
				{
					// Remove uninstalled SpeechServer voices from the SAPI 5 interface
					if (SAPI5Token.Contains("TTS_MS_"))
					{
						if (Array.IndexOf(ServerTokens, SAPI5Token) < 0)
						{
							//MessageBox.Show(SAPI5Token);
							RegistryKey key = Registry.LocalMachine.OpenSubKey(SAPI5Path + "\\" + SAPI5Token);
							string VoiceName = key.GetValue("").ToString();
							if (VoiceName.Contains("Microsoft Server Speech Text to Speech Voice"))
							{
								textBox_report.Text = VoiceName + " which was an uninstalled Speech Server voice is removed from SAPI 5." + Environment.NewLine;
								key = Registry.LocalMachine.OpenSubKey(SAPI5Path, true);
								key.DeleteSubKeyTree(SAPI5Token, false);
							}
						}
					}
				}
				catch
				{
					MessageBox.Show("An Error in removing of " + SAPI5Token + " is occurred!", "Error #3", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			//// Now that all things become OK it's time to add OneCore voices to SAPI 5 interface
			foreach (string MobileToken in MobileTokens)
			{
				try
				{
					if (Array.IndexOf(SAPI5Tokens, MobileToken) < 0)
					{
						//MessageBox.Show(MobileToken);
						// Create a SAPI5Token with the same name of the MobileToken
						RegistryKey dstkey = Registry.LocalMachine.OpenSubKey(SAPI5Path, true);
						dstkey.CreateSubKey(MobileToken);
						// Copy values from the MobileToken to the created SAPI5Token
						RegistryKey srckey = Registry.LocalMachine.OpenSubKey(MobilePath + "\\" + MobileToken);
						dstkey = Registry.LocalMachine.OpenSubKey(SAPI5Path + "\\" + MobileToken, true);
						foreach (string name in srckey.GetValueNames())
						{
							//MessageBox.Show(name);
							if (name == "")
							{
								textBox_report.Text = textBox_report.Text + srckey.GetValue(name) + " is added from OneCore Speech API to SAPI 5." + Environment.NewLine;
							}
							dstkey.SetValue(name, srckey.GetValue(name), srckey.GetValueKind(name));
						}
						// Create the Attributes subkey for the created voice Token
						dstkey = Registry.LocalMachine.OpenSubKey(SAPI5Path + "\\" + MobileToken, true);
						dstkey.CreateSubKey("Attributes");
						// Copy values from the Attributes subkey of the MobileToken to the created Attributes subkey of SAPI5Token
						srckey = Registry.LocalMachine.OpenSubKey(MobilePath + "\\" + MobileToken + "\\" + "Attributes");
						dstkey = Registry.LocalMachine.OpenSubKey(SAPI5Path + "\\" + MobileToken + "\\" + "Attributes", true);
						foreach (string name in srckey.GetValueNames())
						{
							dstkey.SetValue(name, srckey.GetValue(name), srckey.GetValueKind(name));
						}
					}
				}
				catch
				{
					MessageBox.Show("An Error in adding of " + MobileToken + " is occurred!", "Error #4", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			
			//// Now that all things become OK it's time to add SpeechServer voices to SAPI 5 interface
			foreach (string ServerToken in ServerTokens)
			{
				try
				{
					if (Array.IndexOf(SAPI5Tokens, ServerToken) < 0)
					{
						//MessageBox.Show(MobileToken);
						// Create a SAPI5Token with the same name of the MobileToken
						RegistryKey dstkey = Registry.LocalMachine.OpenSubKey(SAPI5Path, true);
						dstkey.CreateSubKey(ServerToken);
						// Copy values from the MobileToken to the created SAPI5Token
						RegistryKey srckey = Registry.LocalMachine.OpenSubKey(ServerPath + "\\" + ServerToken);
						dstkey = Registry.LocalMachine.OpenSubKey(SAPI5Path + "\\" + ServerToken, true);
						foreach (string name in srckey.GetValueNames())
						{
							//MessageBox.Show(name);
							if (name == "")
							{
								textBox_report.Text = textBox_report.Text + srckey.GetValue(name) + " is added from Speech Server Speech API to SAPI 5." + Environment.NewLine;
							}
							dstkey.SetValue(name, srckey.GetValue(name), srckey.GetValueKind(name));
						}
						// Create the Attributes subkey for the created voice Token
						dstkey = Registry.LocalMachine.OpenSubKey(SAPI5Path + "\\" + ServerToken, true);
						dstkey.CreateSubKey("Attributes");
						// Copy values from the Attributes subkey of the MobileToken to the created Attributes subkey of SAPI5Token
						srckey = Registry.LocalMachine.OpenSubKey(ServerPath + "\\" + ServerToken + "\\" + "Attributes");
						dstkey = Registry.LocalMachine.OpenSubKey(SAPI5Path + "\\" + ServerToken + "\\" + "Attributes", true);
						foreach (string name in srckey.GetValueNames())
						{
							dstkey.SetValue(name, srckey.GetValue(name), srckey.GetValueKind(name));
						}
					}
				}
				catch
				{
					MessageBox.Show("An Error in adding of " + ServerToken + " is occurred!", "Error #5", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

			}

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			if (textBox_report.Text == "")
			{
				textBox_report.Text = "It seems everything is OK. Please don't forget to re-run the SAPI Unifier after installing or uninstalling of any OneCore or Speech Server voices.";
			}
		}

		private void button_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button_about_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Author: Mahmood Taghavi; Version 1.0; License: GPL 3", "About SAPI Unifier", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void linkLabel_website_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// Change the color of the link text by setting LinkVisited
			// to true.
			linkLabel_website.LinkVisited = true;
			//Call the Process.Start method to open the default browser
			//with a URL:
			System.Diagnostics.Process.Start("https://mahmood-taghavi.github.io/SAPI_Unifier/");
		}
	}
}
