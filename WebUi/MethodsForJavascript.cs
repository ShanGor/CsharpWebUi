using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebUi
{
    public partial class MainWin
    {

        //提供给JavaScript调用的方法  
        public void fileSelected(string filePath)
        {
            if(!File.Exists(filePath))
                MessageBox.Show("File does not exist: " + filePath);
        }

        //Given the resource name from front end like file:///xxxx, now get absolute path
        public string ajaxLocalFileRequest(string resource)
        {
            string filePathEscaped = new Uri(resource).AbsolutePath;
            string filePath = callHtml("unescape", filePathEscaped).ToString();
            return File.ReadAllText(filePath);
        }

        //提供给JavaScript调用的方法  
        public void csharpMsgBox(string message)
        {
            MessageBox.Show(message);
        }

        public void htmlReady()
        {
            //TODO, the Ui.html can call it.
        }

        //提供给JavaScript调用的方法  
        public void csharpCallBack(string message)
        {
            callHtml("htmlAction", "Greeting from C#! " + message);
        }


        private Object callHtml(String method, String param)
        {
            //调用JavaScript的messageBox方法，并传入参数  
            object[] objects = new object[1];
            objects[0] = param;
            return this.webBrowser.Document.InvokeScript(method, objects);
        }
    }
}
