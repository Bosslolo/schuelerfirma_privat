using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schülerfirma_Admin_Dashboard
{
    public partial class CustomerView : Form
    {
        public CustomerView()
        {
            InitializeComponent();
        }

        private void CustomerView_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'schülerfirmaTestDataSet.customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.schülerfirmaTestDataSet.customers);
            // TODO: This line of code loads data into the 'schülerfirmaTestDataSet.invoices' table. You can move, or remove it, as needed.
            this.invoicesTableAdapter.Fill(this.schülerfirmaTestDataSet.invoices);
        }

        private void o_actions_Click(object sender, EventArgs e)
        {
            actions.Show(System.Windows.Forms.Cursor.Position);
        }

        private void rechnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
            p.WaitForInputIdle();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            changeView.Show(System.Windows.Forms.Cursor.Position);
        }

        private void ts_itslAcc_Click(object sender, EventArgs e)
        {
            AccountSelecterView accountSelecterView = new AccountSelecterView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SearchBox searchBox = new SearchBox();
            searchBox.ShowDialog();
        }
    }
}
