using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HubOne.PS
{
    /// <summary>
    /// Represents the form that is used to authorise against WorkflowMax
    /// </summary>
    public partial class KeyForm : Form
    {
        /// <summary>
        /// The Account Key
        /// </summary>
        public string AccountKey { get; set; }

        /// <summary>
        /// Form Constructor
        /// </summary>
        public KeyForm()
        {
            InitializeComponent();
            ButtonRetrieveWorkflowMaxKey.Click +=ButtonRetrieveWorklowMaxKey_Click;
            ButtonOk.Click += ButtonOk_Click;
            LoadBrowser();
        }

        private void LoadBrowser()
        {
            WebBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser_Navigated);
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            var queryString = WebBrowser1.Url.Query;
            if(queryString.Contains("accountKey"))
            {
                var accountKey = queryString.Substring(queryString.IndexOf("=", System.StringComparison.Ordinal) + 1).Replace("&apiUrl=api.workflowmax.com", "");
                WebBrowser1.Visible = false;
                WebBrowser1.Height = 0;
                this.Height = 240;
                AccountKey = accountKey;
                ButtonRetrieveWorkflowMaxKey.Visible = false;
                ButtonOk.Visible = true;
            }
            else
            {
                WebBrowser1.Visible = true;
                WebBrowser1.Height = 660;
                this.Height = 870;
            }
        }

        void ButtonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ButtonRetrieveWorklowMaxKey_Click(object sender, EventArgs eventArgs)
        {
            ShowProgress("Retrieving WorkflowMax Token...", true);
            ButtonRetrieveWorkflowMaxKey.Visible = false;
            var tokenResponse = GetWorkflowMaxToken();
            if (!tokenResponse.IsError && tokenResponse.ResponseValue != String.Empty)
            {
                GetWorkflowMaxAccountKey(tokenResponse.ResponseValue);
            }
            else
            {
                ShowProgress("An error has occurred: " + tokenResponse.ErrorMessage, true);
            }
        }

        private void ShowProgress(string message, bool show)
        {
            TextBoxProgress.Text = message;
            TextBoxProgress.Visible = show;
        }

        private CommonClasses.WebResponse GetWorkflowMaxToken()
        {
            try
            {
                #region Get Token
                var request = (HttpWebRequest) WebRequest.Create(new Uri("https://api.workflowmax.com/max.api/token"));
                request.Method = "POST";
                request.Accept = "*/*";
                request.ContentType = "application/xml";

                const string strReq = @"<Application><ApiKey>14C10292983D48CE86E1AA1FE0F8DDFE</ApiKey></Application>";
                byte[] bytes = Encoding.UTF8.GetBytes(strReq);
                request.ContentLength = bytes.Length;
                using (Stream putStream = request.GetRequestStream())
                {
                    putStream.Write(bytes, 0, bytes.Length);
                }

                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        if (response.StatusDescription == "OK")
                        {
                            var tokenResponse = reader.ReadToEnd();
                            var xmlResponse = XElement.Parse(tokenResponse);
                            var xElement = xmlResponse.Element("Token");
                            if (xElement != null)
                            {
                                return new CommonClasses.WebResponse() {ResponseValue = xElement.Value};
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                return new CommonClasses.WebResponse() {IsError = true, ErrorMessage = ex.Message};
            }

            return new CommonClasses.WebResponse() {IsError = true, ErrorMessage = "An unknown error has occurred", Method = "GetWorkflowMaxToken"};
        }

        /// <summary>
        /// Retrieve the key
        /// </summary>
        /// <param name="token"></param>
        private void GetWorkflowMaxAccountKey(string token)
        {
            ShowProgress("Retrieving WorkflowMax Account Key...", true);
            const string redirectUrl = "http://www.gusha.com.au";
            var wfmxUrl = "https://my.workflowmax.com/my/authorise.aspx?token=" + token + "&callbackUrl=" +  HttpUtility.UrlEncode(redirectUrl);
            WebBrowser1.Navigate(new Uri(wfmxUrl));
            ShowProgress("", false);
        }

        /// <summary>
        /// Common classes
        /// </summary>
        public class CommonClasses
        {
            /// <summary>
            /// Common WebResponse Items
            /// </summary>
            [SerializableAttribute]
            public class WebResponse
            {
                public bool IsError { get; set; }
                public string ResponseValue { get; set; }
                public string ErrorMessage { get; set; }
                public string Method { get; set; }
            }
        }
    }
}
