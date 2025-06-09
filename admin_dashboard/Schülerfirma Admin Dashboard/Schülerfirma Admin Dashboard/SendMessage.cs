using itslearningTest;
using Newtonsoft.Json.Linq;
using Schülerfirma_Admin_Dashboard.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace Schülerfirma_Admin_Dashboard
{
    public partial class SendMessage : Form
    {
        private static List<string> uploaded_files = new List<string>();
        Itslearning itsl = new Itslearning();
        public SendMessage()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Datei auswählen …";
            openFileDialog.Filter = "Erlaubte Dateinamenerweiterungen|*.aac;*.adt;*.adts;*.accdb;*.accde;*.accdr;*.accdt;*.aif;*.aifc;*.aiff;*.aspx;*.avi;*.bmp;*.cab;*.cda;*.csv;*.dif;*.dll;*.doc;*.docm;*.docx;*.dot;*.dotx;*.eml;*.eps;*.flv;*.gif;*.htm;*.html;*.ini;*.iso;*.jar;*.jpg;*.jpeg;*.m4a;*.mdb;*.mid;*.midi;*.mov;*.mp3;*.mp4;*.mpeg;*.mpg;*.mui;*.pdf;*.png;*.pot;*.potm;*.potx;*.ppam;*.pps;*.ppsm;*.ppsx;*.ppt;*.pptm;*.pptx;*.psd;*.pst;*.pub;*.rar;*.rtf;*.sldm;*.sldx;*.swf;*.sys;*.tif;*.tiff;*.tmp;*.txt;*.vob;*.vsd;*.vsdm;*.vsdx;*.vss;*.vssm;*.vst;*.vstm;*.vstx;*.wav;*.wbk;*.wks;*.wma;*.wmd;*.wmv;*.wmz;*.wms;*.wpd;*.wp5;*.xla;*.xlam;*.xll;*.xlm;*.xls;*.xlsm;*.xlsx;*.xlt;*.xltm;*.xltx;*.xps;*.zip|Alle Dateien (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.CheckPathExists = true; // Ensure the path exists
            openFileDialog.CheckFileExists = true; // Ensure the file exists
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                attachmentsList.Items.Add(openFileDialog.SafeFileName);
                uploaded_files.Add(openFileDialog.FileName.ToString()); // Add the file path to the list of files to be uploaded
            }
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            searchResultBox.Location = new Point(15, 45);
            searchResultBox.Visible = true;
            searchResultBox.Items.Clear();
            string at = await itsl.SEasyAccessToken();
            // If there is already something in the textBox, search only for the entered text after the last semicolon 
            if (recipientsBox.Text.Length > 0)
            {
                string[] parts = recipientsBox.Text.Split(';');
                string lastPart = parts.LastOrDefault()?.Trim();
                if (!string.IsNullOrEmpty(lastPart))
                {
                    JArray results = await itsl.JAGetCustomArray(at, $"personal/instantmessages/recipients/search/v1?searchText={lastPart}");
                    searchResultBox.Items.Clear(); // Clear previous results
                    foreach (JObject result in results)
                    {
                        string label = result["Label"]?.ToString() + " [" + result["Id"] + "]";
                        searchResultBox.Items.Add(label);
                        // recipientsBox.AppendText($"{label}\n");
                    }
                }
            }
        }

        private async void SendMessage_Load(object sender, EventArgs e)
        {
            // Clear the attachment list
            attachmentsList.Items.Clear();

            // Check if the user has access to the Instant Messaging System and the ability to send messages as an individual
            string at = await itsl.SEasyAccessToken();
            JObject hasAccess = await itsl.JOGetCustomObject(at, "personal/instantmessages/permissions/v1");
            if (hasAccess["CanAccessInstantMessageSystem"].ToString() == "false")
            {
                MessageBox.Show("Sie haben keinen Zugriff auf das Instant Messaging System.", "Zugriff verweigert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            if (hasAccess["CanUseSendAsIndividualMessages"].ToString() == "false")
            {
                MessageBox.Show("Sie haben keinen Zugriff auf das Senden von Nachrichten als Einzelperson.", "Zugriff verweigert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            // Load predefined message templates
            templateBox.Items.Clear();
            ResourceManager vorlagen = new ResourceManager(typeof(Resources));
            foreach (DictionaryEntry entry in PredefinedMessages.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true))
            {
                string resourceKey = entry.Key.ToString();
                // object resource = entry.Value;
                // string resource = entry.Value.ToString();
                templateBox.Items.Add(resourceKey);
            }
        }

        private void searchResultBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected in the searchResultBox 
            // and append it to the recipientsBox, replace the entered text after the ; with the selected item and add it after the existing 
            // recipients in the recipientsBox
            if (searchResultBox.SelectedItem == null)
                return; // Exit if no item is selected
            // Replace the last part of the recipientsBox text after the last semicolon with the selected item
            string[] recipients = recipientsBox.Text.Split(';');
            if (recipients.Length > 0 && !string.IsNullOrWhiteSpace(recipients[recipients.Length - 1]))
            {
                // If the last part is not empty, replace it
                recipients[recipients.Length - 1] = searchResultBox.SelectedItem.ToString();
            }
            else
            {
                // If the last part is empty, just append the selected item
                Array.Resize(ref recipients, recipients.Length + 1);
                recipients[recipients.Length - 1] = searchResultBox.SelectedItem.ToString();
            }
            recipientsBox.Text = string.Join(";", recipients.Where(r => !string.IsNullOrWhiteSpace(r))) + ";"; // Join the recipients with semicolons and add a semicolon at the end

            // searchResultBox.SelectedItem.ToString();

            // recipientsBox.AppendText(searchResultBox.SelectedItem.ToString() + ";"); // Add a new line before appending the selected item
            recipientsBox.SelectionStart = recipientsBox.Text.Length; // Move the cursor to the end of the text
            recipientsBox.ScrollToCaret(); // Scroll to the end of the text
            searchResultBox.Visible = false; // Hide the search result box after selection
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected item from the templateBox and set the value from the resx file to the text of the messageBox
            if (templateBox.SelectedItem != null)
            {
                string selectedItem = templateBox.SelectedItem.ToString();
                messageBox.Text = PredefinedMessages.ResourceManager.GetString(selectedItem, CultureInfo.CurrentCulture);
            }
        }

        private async void sendMsg_Click(object sender, EventArgs e)
        {
            string at = await itsl.SEasyAccessToken();
            if (uploaded_files != null)
            {
                JArray response = await itsl.JAUploadFileAttachments(at, uploaded_files);
            }
            
        }
    }
}
