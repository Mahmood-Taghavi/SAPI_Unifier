using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading; // this is used for single instance running by using the Mutex

namespace SAPI_Unifier
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		//[STAThread]
		private static Mutex mutex = null;
		static void Main()
		{
			const string appName = "SAPI_Unifier";
			bool createdNew;
			mutex = new Mutex(true, appName, out createdNew);
			if (!createdNew)
			{
				//app is already running! Exiting the application  
				return;
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form_main());
		}
	}
}
