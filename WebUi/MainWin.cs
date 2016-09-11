using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace WebUi
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class MainWin : Form
    {

        public MainWin()
        {
            /*//Below are for force to use IE11, but not really need if I set it at HTML.
            int ieVersion = Int32.Parse(ConfigHelper.GetSetting("IE.Version"));

            string addressWidth = Detect3264();
            string registryStr;
            if (addressWidth.Contains("64"))
            {  //For 64bit system.
                Console.WriteLine("Using 64bit System");
                registryStr = ConfigHelper.GetSetting("IE.Registry.64b");
            }
            else
            {  //For 32bit system
                Console.WriteLine("Using 32bit System");
                registryStr = ConfigHelper.GetSetting("IE.Registry.32b");
            }

            var appName = Process.GetCurrentProcess().MainModule.ModuleName;
            Console.WriteLine("App Name: " + appName);
            Registry.SetValue(registryStr, appName, ieVersion, RegistryValueKind.DWord);*/

            InitializeComponent();

            webBrowser.ScriptErrorsSuppressed = true;
            string url = ConfigHelper.GetSetting("ui.url");
            if (!url.Contains('/') && !url.Contains(':') && !url.Contains('\\'))
            {
                url = new FileInfo(Path.Combine(Application.StartupPath, url)).FullName;
                if (!File.Exists(url))
                {
                    MessageBox.Show(this, "File does not exist:" + url + "!", "Fatal Error!");
                    return;
                }
                else
                {
                    Console.WriteLine("Get the URL: " + url + " successfully!");
                }
            }
            this.webBrowser.Url = new Uri(url);
            this.webBrowser.ObjectForScripting = this;

            this.webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
        }

        //When the UI document loaded completed, do something automatically.
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Change the title to the html title
            this.Text = this.webBrowser.Document.Title;
        }

        public static string Detect3264()
        {
            ConnectionOptions oConn = new ConnectionOptions();
            System.Management.ManagementScope oMs = new System.Management.ManagementScope(@"\\localhost", oConn);
            System.Management.ObjectQuery oQuery = new System.Management.ObjectQuery("select AddressWidth from Win32_Processor");
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            ManagementObjectCollection oReturnCollection = oSearcher.Get();
            string addressWidth = null;

            foreach (ManagementObject oReturn in oReturnCollection)
            {
                addressWidth = oReturn["AddressWidth"].ToString();
            }

            return addressWidth;
        }
    }
}
