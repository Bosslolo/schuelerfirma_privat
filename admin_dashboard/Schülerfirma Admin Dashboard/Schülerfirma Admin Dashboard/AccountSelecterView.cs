using itslearningTest;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schülerfirma_Admin_Dashboard
{
    public partial class AccountSelecterView : Form
    {
        Itslearning itsl = new Itslearning();
        public AccountSelecterView()
        {
            InitializeComponent();
            this.Size = new Size(430, 262);
            this.MaximumSize = new Size(430, 262); 
            this.MinimumSize = new Size(430, 262);
            string access_token = Properties.Settings.Default.access_token;
            string refresh_token = Properties.Settings.Default.refresh_token;
            if (string.IsNullOrEmpty(access_token) || string.IsNullOrEmpty(refresh_token))
            {
                nologinpnl.Location = new Point(0, 0);
                loginpnl.Visible = false;
                nologinpnl.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://127.0.0.1:5000/login");
            Process.Start(sInfo);
        }

        private async Task loginbtn_ClickAsync(object sender, EventArgs e)
        {
            

        }

        private async void loginbtn_Click(object sender, EventArgs e)
        {
            loginpnl.Visible = true;
            nologinpnl.Location = new Point(412, 0);
            JObject info = await SignInAsync();
            if (info == null)
            {
                MessageBox.Show("Login failed. Please check your credentials.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            itsl_account.Text = info["FullName"].ToString() + " (" + info["PersonId"].ToString() + ")";
            instant_messaging_access.Text = info["CanAccessInstantMessageSystem"].ToString() == "true" ? "Nein" : "Ja";
            itsl_image.Load(info["ProfileImageUrl"].ToString());
        }
        public static class AccessToken
        {
            public static string Token { get; set; } = String.Empty;
            public static string RefreshToken { get; set; } = String.Empty;
        }

            private async Task<JObject> SignInAsync()
        {
            // [rWyl{Access Token}...E1b2;3437{Refresh Token}...iza5;2025-05-27 18:14:49.433670]
            string creds = credsBox.Text;
            // Remove the first and last character.
            creds = creds.Trim('[', ']');
            string[] parts = creds.Split(';');
            creds = String.Empty;
            /*for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();
                // Remove b' prefix and trailing apostrophe if present
                if (parts[i].StartsWith("b'"))
                { parts[i] = parts[i].Substring(2); }
                if (parts[i].EndsWith("'"))
                { parts[i] = parts[i].Substring(0, parts[i].Length - 1); }
            } */
            string at = parts[0];
            Properties.Settings.Default.access_token = at; // Store the access token in settings for later use. 
            Properties.Settings.Default.Save();
            AccessToken.Token = at; // Store the access token for later use. 
            string rt = parts[1];
            Properties.Settings.Default.refresh_token = rt; // Store the refresh token in settings for later use.
            Properties.Settings.Default.Save();
            AccessToken.RefreshToken = rt; // Store the refresh token for later use.
            string time_created = parts[2];
            // Decrypt the things.

            // Call the itslearning api to get the user's information.
            JObject info = await itsl.JOGetCustomObject(at, "personal/person/v1");
            return info;
        }

        private async void AccountSelecterView_Load(object sender, EventArgs e)
        {
            string rt = Schülerfirma_Admin_Dashboard.Properties.Settings.Default.refresh_token;
            if (rt == String.Empty)
            {
                nologinpnl.Location = new Point(0, 0);
                loginpnl.Visible = false;
                nologinpnl.Visible = true;
                return;
            }
            string access_token = await itsl.SEasyAccessToken();

            if(string.IsNullOrEmpty(access_token))
            {
                await SignInAsync();
                return;
            }
            JObject info = await itsl.JOGetCustomObject(access_token, "personal/person/v1");
            loginpnl.Visible = true;
            nologinpnl.Location = new Point(412, 0);
            if (info == null)
            {
                MessageBox.Show("Login failed. Please check your credentials.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            itsl_account.Text = info["FullName"].ToString() + " (" + info["PersonId"].ToString() + ")";
            instant_messaging_access.Text = info["CanAccessInstantMessageSystem"].ToString() == "true" ? "Nein" : "Ja";
            itsl_image.Load(info["ProfileImageUrl"].ToString());
        }
    }
}
