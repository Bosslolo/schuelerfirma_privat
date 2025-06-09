using itslearningTest;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace Schülerfirma_Admin_Dashboard
{
    public partial class Invoices : Form
    {
        Itslearning itsl = new Itslearning();
        public Invoices()
        {
            InitializeComponent();
            // Check itslearning and login user            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(dataGridView1_MouseDown);
            this.markPaid.Click += new System.EventHandler(this.markPaid_Click);
            this.sendMessage.Click += new System.EventHandler(this.sendMessage_Click);
        }

        private void sendMessage_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = dataGridView1.HitTest(e.X, e.Y);
                dataGridView1.ClearSelection();
                dataGridView1.Rows[hti.RowIndex].Selected = true;
            }
        }

        private void markPaid_Click(object sender, EventArgs e)
        {
            Int32 rowToDelete = dataGridView1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (rowToDelete != -1)
            {
                dataGridView1.Rows.RemoveAt(rowToDelete);
                dataGridView1.ClearSelection();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Cut"));
                m.MenuItems.Add(new MenuItem("Copy"));
                m.MenuItems.Add(new MenuItem("Paste"));

                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                m.Show(dataGridView1, new Point(e.X, e.Y));

            }
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void sendMessage_Click_1(object sender, EventArgs e)
        {
            SendMessage sendMessage = new SendMessage();
            sendMessage.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private void ansichtToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            changeView.Show(Cursor.Position);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            string at = Schülerfirma_Admin_Dashboard.Properties.Settings.Default.access_token;
            string rt = Schülerfirma_Admin_Dashboard.Properties.Settings.Default.refresh_token; 
            System.Windows.Forms.MessageBox.Show("Access Token: " + at + "\nRefresh Token: " + rt, "Token Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Clipboard.SetText(at + "\n\n" + rt);
#endif
            string access_token = await itsl.SEasyAccessToken();
            System.Windows.Forms.MessageBox.Show("Access Token: " + access_token, "Token Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Clipboard.SetText(access_token);
            // TODO: This line of code loads data into the 'schülerfirmaTestDataSet.invoices' table. You can move, or remove it, as needed.
            this.invoicesTableAdapter.Fill(this.schülerfirmaTestDataSet.invoices);
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            CustomerView customerView = new CustomerView();
            customerView.Show();
        }

        private void properties_Click(object sender, EventArgs e)
        {
            PropertyView propertyView = new PropertyView();
            propertyView.Show();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void ts_itslAcc_Click(object sender, EventArgs e)
        {
            AccountSelecterView accountSelecterView = new AccountSelecterView();
            accountSelecterView.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDialog.ShowDialog();
        }

        private async void itslearningImWebÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(await itsl.SASSOUrl("https://csh.itslearning.com/DashboardMenu.aspx", await itsl.SEasyAccessToken()));
            Process.Start(sInfo);
        }
    }
}
