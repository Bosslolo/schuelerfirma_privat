namespace Schülerfirma_Admin_Dashboard
{
    partial class Invoices
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Invoices));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Januar 2025",
            "Überfällig",
            "54,10€"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Februar 2025",
            "Bezahlt",
            "19,50€"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "März 2025",
            "Bezahlt",
            "26,85€"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "April 2025",
            "Überfällig",
            "46,20€"}, -1);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceMont = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bookingnrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bruttoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nettoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paymentdueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markPaid = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sendReminder = new System.Windows.Forms.ToolStripMenuItem();
            this.showCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.sendMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.properties = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoicesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.schülerfirmaTestDataSet = new Schülerfirma_Admin_Dashboard.SchülerfirmaTestDataSet();
            this.overviewBox = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.monthyear = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.amount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.o_bookingnumber = new System.Windows.Forms.Label();
            this.o_payment_overdue = new System.Windows.Forms.Label();
            this.o_bruttosumme = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.o_nettosumme = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.o_costs_tea = new System.Windows.Forms.Label();
            this.o_costs_juices = new System.Windows.Forms.Label();
            this.o_costs_chocolate = new System.Windows.Forms.Label();
            this.o_anzahl_tea = new System.Windows.Forms.Label();
            this.o_anzahl_juices = new System.Windows.Forms.Label();
            this.o_anzahl_chocolate = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.o_anzahl_coffee = new System.Windows.Forms.Label();
            this.o_costs_coffee = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.name_lbl_overview = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.invoicesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewtsitem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.profilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datenAktualisierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webOberflächeÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_itslAcc = new System.Windows.Forms.ToolStripMenuItem();
            this.invoicesTableAdapter = new Schülerfirma_Admin_Dashboard.SchülerfirmaTestDataSetTableAdapters.invoicesTableAdapter();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.itslearningImWebÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.actions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invoicesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schülerfirmaTestDataSet)).BeginInit();
            this.overviewBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invoicesBindingSource)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.changeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1103, 590);
            this.splitContainer1.SplitterDistance = 193;
            this.splitContainer1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(172, 20);
            this.button2.TabIndex = 4;
            this.button2.Text = "Ansicht wechseln";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 550);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 28);
            this.button1.TabIndex = 3;
            this.button1.Text = "Übersicht drucken";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dateTimePicker2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Location = new System.Drawing.Point(3, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(187, 112);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zeitraum";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Bis:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Location = new System.Drawing.Point(6, 84);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(175, 18);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Von:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(6, 36);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(175, 18);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(3, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 93);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Anzeigen";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(9, 65);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(69, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Überfällig";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(9, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Bezahlt";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(42, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Alle";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rechnungen";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.overviewBox);
            this.splitContainer2.Size = new System.Drawing.Size(906, 590);
            this.splitContainer2.SplitterDistance = 673;
            this.splitContainer2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn1,
            this.fullName,
            this.invoiceMont,
            this.bookingnrDataGridViewTextBoxColumn,
            this.bruttoDataGridViewTextBoxColumn,
            this.vatDataGridViewTextBoxColumn,
            this.nettoDataGridViewTextBoxColumn,
            this.paymentdueDataGridViewTextBoxColumn});
            this.dataGridView1.ContextMenuStrip = this.actions;
            this.dataGridView1.DataSource = this.invoicesBindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 23;
            this.dataGridView1.Size = new System.Drawing.Size(673, 590);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "status";
            this.dataGridViewTextBoxColumn1.HeaderText = "Status";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // fullName
            // 
            this.fullName.DataPropertyName = "fullName";
            this.fullName.HeaderText = "Name";
            this.fullName.Name = "fullName";
            this.fullName.ReadOnly = true;
            // 
            // invoiceMont
            // 
            this.invoiceMont.DataPropertyName = "invoiceMonth";
            this.invoiceMont.HeaderText = "Monat";
            this.invoiceMont.Name = "invoiceMont";
            this.invoiceMont.ReadOnly = true;
            this.invoiceMont.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.invoiceMont.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bookingnrDataGridViewTextBoxColumn
            // 
            this.bookingnrDataGridViewTextBoxColumn.DataPropertyName = "bookingnr";
            this.bookingnrDataGridViewTextBoxColumn.HeaderText = "Buchungsnummer";
            this.bookingnrDataGridViewTextBoxColumn.Name = "bookingnrDataGridViewTextBoxColumn";
            this.bookingnrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bruttoDataGridViewTextBoxColumn
            // 
            this.bruttoDataGridViewTextBoxColumn.DataPropertyName = "brutto";
            this.bruttoDataGridViewTextBoxColumn.HeaderText = "Bruttobetrag";
            this.bruttoDataGridViewTextBoxColumn.Name = "bruttoDataGridViewTextBoxColumn";
            this.bruttoDataGridViewTextBoxColumn.ReadOnly = true;
            this.bruttoDataGridViewTextBoxColumn.Width = 80;
            // 
            // vatDataGridViewTextBoxColumn
            // 
            this.vatDataGridViewTextBoxColumn.DataPropertyName = "vat";
            this.vatDataGridViewTextBoxColumn.HeaderText = "MwSt.";
            this.vatDataGridViewTextBoxColumn.Name = "vatDataGridViewTextBoxColumn";
            this.vatDataGridViewTextBoxColumn.ReadOnly = true;
            this.vatDataGridViewTextBoxColumn.Width = 50;
            // 
            // nettoDataGridViewTextBoxColumn
            // 
            this.nettoDataGridViewTextBoxColumn.DataPropertyName = "netto";
            this.nettoDataGridViewTextBoxColumn.HeaderText = "Nettobetrag";
            this.nettoDataGridViewTextBoxColumn.Name = "nettoDataGridViewTextBoxColumn";
            this.nettoDataGridViewTextBoxColumn.ReadOnly = true;
            this.nettoDataGridViewTextBoxColumn.Width = 80;
            // 
            // paymentdueDataGridViewTextBoxColumn
            // 
            this.paymentdueDataGridViewTextBoxColumn.DataPropertyName = "payment_due";
            this.paymentdueDataGridViewTextBoxColumn.HeaderText = "Zu zahlen bis";
            this.paymentdueDataGridViewTextBoxColumn.Name = "paymentdueDataGridViewTextBoxColumn";
            this.paymentdueDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // actions
            // 
            this.actions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markPaid,
            this.toolStripSeparator2,
            this.sendReminder,
            this.showCustomer,
            this.sendMessage,
            this.properties});
            this.actions.Name = "markPaid";
            this.actions.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.actions.Size = new System.Drawing.Size(230, 120);
            this.actions.Text = "Aktionen";
            // 
            // markPaid
            // 
            this.markPaid.Image = ((System.Drawing.Image)(resources.GetObject("markPaid.Image")));
            this.markPaid.Name = "markPaid";
            this.markPaid.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.markPaid.Size = new System.Drawing.Size(229, 22);
            this.markPaid.Text = "Als bezahlt markieren";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
            // 
            // sendReminder
            // 
            this.sendReminder.Image = ((System.Drawing.Image)(resources.GetObject("sendReminder.Image")));
            this.sendReminder.Name = "sendReminder";
            this.sendReminder.Size = new System.Drawing.Size(229, 22);
            this.sendReminder.Text = "Mahnung verschicken";
            // 
            // showCustomer
            // 
            this.showCustomer.Image = ((System.Drawing.Image)(resources.GetObject("showCustomer.Image")));
            this.showCustomer.Name = "showCustomer";
            this.showCustomer.Size = new System.Drawing.Size(229, 22);
            this.showCustomer.Text = "Kunden anzeigen";
            // 
            // sendMessage
            // 
            this.sendMessage.Image = ((System.Drawing.Image)(resources.GetObject("sendMessage.Image")));
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.sendMessage.Size = new System.Drawing.Size(229, 22);
            this.sendMessage.Text = "Nachricht schreiben";
            this.sendMessage.Click += new System.EventHandler(this.sendMessage_Click_1);
            // 
            // properties
            // 
            this.properties.Image = ((System.Drawing.Image)(resources.GetObject("properties.Image")));
            this.properties.Name = "properties";
            this.properties.Size = new System.Drawing.Size(229, 22);
            this.properties.Text = "Eigenschaften";
            this.properties.Click += new System.EventHandler(this.properties_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDown = this.actions;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.editToolStripMenuItem.Text = "A&ktionen";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // invoicesBindingSource1
            // 
            this.invoicesBindingSource1.DataMember = "invoices";
            this.invoicesBindingSource1.DataSource = this.schülerfirmaTestDataSet;
            // 
            // schülerfirmaTestDataSet
            // 
            this.schülerfirmaTestDataSet.DataSetName = "SchülerfirmaTestDataSet";
            this.schülerfirmaTestDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // overviewBox
            // 
            this.overviewBox.Controls.Add(this.listView1);
            this.overviewBox.Controls.Add(this.label8);
            this.overviewBox.Controls.Add(this.o_bookingnumber);
            this.overviewBox.Controls.Add(this.o_payment_overdue);
            this.overviewBox.Controls.Add(this.o_bruttosumme);
            this.overviewBox.Controls.Add(this.label15);
            this.overviewBox.Controls.Add(this.label9);
            this.overviewBox.Controls.Add(this.label13);
            this.overviewBox.Controls.Add(this.o_nettosumme);
            this.overviewBox.Controls.Add(this.lbl);
            this.overviewBox.Controls.Add(this.o_costs_tea);
            this.overviewBox.Controls.Add(this.o_costs_juices);
            this.overviewBox.Controls.Add(this.o_costs_chocolate);
            this.overviewBox.Controls.Add(this.o_anzahl_tea);
            this.overviewBox.Controls.Add(this.o_anzahl_juices);
            this.overviewBox.Controls.Add(this.o_anzahl_chocolate);
            this.overviewBox.Controls.Add(this.label10);
            this.overviewBox.Controls.Add(this.label11);
            this.overviewBox.Controls.Add(this.label12);
            this.overviewBox.Controls.Add(this.o_anzahl_coffee);
            this.overviewBox.Controls.Add(this.o_costs_coffee);
            this.overviewBox.Controls.Add(this.label7);
            this.overviewBox.Controls.Add(this.label6);
            this.overviewBox.Controls.Add(this.label5);
            this.overviewBox.Controls.Add(this.name_lbl_overview);
            this.overviewBox.Controls.Add(this.label4);
            this.overviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overviewBox.Location = new System.Drawing.Point(0, 0);
            this.overviewBox.Name = "overviewBox";
            this.overviewBox.Size = new System.Drawing.Size(229, 590);
            this.overviewBox.TabIndex = 0;
            this.overviewBox.TabStop = false;
            this.overviewBox.Text = "Zusammenfassung";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.monthyear,
            this.status,
            this.amount});
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listView1.Location = new System.Drawing.Point(12, 392);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(205, 186);
            this.listView1.TabIndex = 25;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // monthyear
            // 
            this.monthyear.Text = "Monat/Jahr";
            this.monthyear.Width = 72;
            // 
            // status
            // 
            this.status.Text = "Status";
            // 
            // amount
            // 
            this.amount.Text = "Betrag";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 376);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Rechnungen";
            // 
            // o_bookingnumber
            // 
            this.o_bookingnumber.AutoSize = true;
            this.o_bookingnumber.Location = new System.Drawing.Point(9, 324);
            this.o_bookingnumber.Name = "o_bookingnumber";
            this.o_bookingnumber.Size = new System.Drawing.Size(95, 13);
            this.o_bookingnumber.TabIndex = 23;
            this.o_bookingnumber.Text = "Buchungsnummer:";
            // 
            // o_payment_overdue
            // 
            this.o_payment_overdue.AutoSize = true;
            this.o_payment_overdue.Location = new System.Drawing.Point(9, 300);
            this.o_payment_overdue.Name = "o_payment_overdue";
            this.o_payment_overdue.Size = new System.Drawing.Size(131, 13);
            this.o_payment_overdue.TabIndex = 22;
            this.o_payment_overdue.Text = "Zahlung überfällig (Datum)";
            // 
            // o_bruttosumme
            // 
            this.o_bruttosumme.AutoSize = true;
            this.o_bruttosumme.Location = new System.Drawing.Point(169, 250);
            this.o_bruttosumme.Name = "o_bruttosumme";
            this.o_bruttosumme.Size = new System.Drawing.Size(43, 13);
            this.o_bruttosumme.TabIndex = 21;
            this.o_bruttosumme.Text = "39,50 €";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 250);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Bruttosumme";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(144, 228);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "19 % (7,51 €)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 228);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "MwSt.";
            // 
            // o_nettosumme
            // 
            this.o_nettosumme.AutoSize = true;
            this.o_nettosumme.Location = new System.Drawing.Point(169, 212);
            this.o_nettosumme.Name = "o_nettosumme";
            this.o_nettosumme.Size = new System.Drawing.Size(43, 13);
            this.o_nettosumme.TabIndex = 17;
            this.o_nettosumme.Text = "31,99 €";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(9, 212);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(66, 13);
            this.lbl.TabIndex = 16;
            this.lbl.Text = "Nettosumme";
            // 
            // o_costs_tea
            // 
            this.o_costs_tea.AutoSize = true;
            this.o_costs_tea.Location = new System.Drawing.Point(170, 173);
            this.o_costs_tea.Name = "o_costs_tea";
            this.o_costs_tea.Size = new System.Drawing.Size(37, 13);
            this.o_costs_tea.TabIndex = 15;
            this.o_costs_tea.Text = "3,50 €";
            // 
            // o_costs_juices
            // 
            this.o_costs_juices.AutoSize = true;
            this.o_costs_juices.Location = new System.Drawing.Point(170, 150);
            this.o_costs_juices.Name = "o_costs_juices";
            this.o_costs_juices.Size = new System.Drawing.Size(43, 13);
            this.o_costs_juices.TabIndex = 14;
            this.o_costs_juices.Text = "12,00 €";
            // 
            // o_costs_chocolate
            // 
            this.o_costs_chocolate.AutoSize = true;
            this.o_costs_chocolate.Location = new System.Drawing.Point(170, 127);
            this.o_costs_chocolate.Name = "o_costs_chocolate";
            this.o_costs_chocolate.Size = new System.Drawing.Size(37, 13);
            this.o_costs_chocolate.TabIndex = 13;
            this.o_costs_chocolate.Text = "9,00 €";
            // 
            // o_anzahl_tea
            // 
            this.o_anzahl_tea.AutoSize = true;
            this.o_anzahl_tea.Location = new System.Drawing.Point(111, 173);
            this.o_anzahl_tea.Name = "o_anzahl_tea";
            this.o_anzahl_tea.Size = new System.Drawing.Size(13, 13);
            this.o_anzahl_tea.TabIndex = 12;
            this.o_anzahl_tea.Text = "7";
            // 
            // o_anzahl_juices
            // 
            this.o_anzahl_juices.AutoSize = true;
            this.o_anzahl_juices.Location = new System.Drawing.Point(111, 150);
            this.o_anzahl_juices.Name = "o_anzahl_juices";
            this.o_anzahl_juices.Size = new System.Drawing.Size(13, 13);
            this.o_anzahl_juices.TabIndex = 11;
            this.o_anzahl_juices.Text = "8";
            // 
            // o_anzahl_chocolate
            // 
            this.o_anzahl_chocolate.AutoSize = true;
            this.o_anzahl_chocolate.Location = new System.Drawing.Point(111, 127);
            this.o_anzahl_chocolate.Name = "o_anzahl_chocolate";
            this.o_anzahl_chocolate.Size = new System.Drawing.Size(13, 13);
            this.o_anzahl_chocolate.TabIndex = 10;
            this.o_anzahl_chocolate.Text = "9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(111, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Anzahl";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(170, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Kosten";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Getränk";
            // 
            // o_anzahl_coffee
            // 
            this.o_anzahl_coffee.AutoSize = true;
            this.o_anzahl_coffee.Location = new System.Drawing.Point(111, 104);
            this.o_anzahl_coffee.Name = "o_anzahl_coffee";
            this.o_anzahl_coffee.Size = new System.Drawing.Size(19, 13);
            this.o_anzahl_coffee.TabIndex = 6;
            this.o_anzahl_coffee.Text = "10";
            // 
            // o_costs_coffee
            // 
            this.o_costs_coffee.AutoSize = true;
            this.o_costs_coffee.Location = new System.Drawing.Point(169, 104);
            this.o_costs_coffee.Name = "o_costs_coffee";
            this.o_costs_coffee.Size = new System.Drawing.Size(43, 13);
            this.o_costs_coffee.TabIndex = 5;
            this.o_costs_coffee.Text = "15,00 €";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Tee";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Säfte";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Schokolade";
            // 
            // name_lbl_overview
            // 
            this.name_lbl_overview.AutoSize = true;
            this.name_lbl_overview.Location = new System.Drawing.Point(9, 26);
            this.name_lbl_overview.Name = "name_lbl_overview";
            this.name_lbl_overview.Size = new System.Drawing.Size(35, 13);
            this.name_lbl_overview.TabIndex = 1;
            this.name_lbl_overview.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Kaffee";
            // 
            // invoicesBindingSource
            // 
            this.invoicesBindingSource.DataMember = "invoices";
            this.invoicesBindingSource.DataSource = this.schülerfirmaTestDataSet;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewtsitem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.ts_itslAcc});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1103, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.fileToolStripMenuItem.Text = "&Datei";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(177, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewtsitem
            // 
            this.viewtsitem.DropDown = this.changeView;
            this.viewtsitem.Name = "viewtsitem";
            this.viewtsitem.Size = new System.Drawing.Size(59, 20);
            this.viewtsitem.Text = "&Ansicht";
            this.viewtsitem.Click += new System.EventHandler(this.ansichtToolStripMenuItem_Click);
            // 
            // changeView
            // 
            this.changeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem4,
            this.profilToolStripMenuItem});
            this.changeView.Name = "changeView";
            this.changeView.Size = new System.Drawing.Size(125, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem1.Text = "&Übersicht";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem4.Text = "&Kunden";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // profilToolStripMenuItem
            // 
            this.profilToolStripMenuItem.Name = "profilToolStripMenuItem";
            this.profilToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.profilToolStripMenuItem.Text = "&Profil";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datenAktualisierenToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.toolsToolStripMenuItem.Text = "&Werkzeuge";
            // 
            // datenAktualisierenToolStripMenuItem
            // 
            this.datenAktualisierenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("datenAktualisierenToolStripMenuItem.Image")));
            this.datenAktualisierenToolStripMenuItem.Name = "datenAktualisierenToolStripMenuItem";
            this.datenAktualisierenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.datenAktualisierenToolStripMenuItem.Text = "Daten aktualisieren";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.webOberflächeÖffnenToolStripMenuItem,
            this.itslearningImWebÖffnenToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator6,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Hilfe";
            // 
            // webOberflächeÖffnenToolStripMenuItem
            // 
            this.webOberflächeÖffnenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("webOberflächeÖffnenToolStripMenuItem.Image")));
            this.webOberflächeÖffnenToolStripMenuItem.Name = "webOberflächeÖffnenToolStripMenuItem";
            this.webOberflächeÖffnenToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.webOberflächeÖffnenToolStripMenuItem.Text = "Web-Oberfläche öffnen";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStripMenuItem.Image")));
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.searchToolStripMenuItem.Text = "&Suchen";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(208, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.aboutToolStripMenuItem.Text = "&Über ...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ts_itslAcc
            // 
            this.ts_itslAcc.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_itslAcc.DoubleClickEnabled = true;
            this.ts_itslAcc.Name = "ts_itslAcc";
            this.ts_itslAcc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ts_itslAcc.Size = new System.Drawing.Size(162, 20);
            this.ts_itslAcc.Text = "Itsl.-Konto: m.mustermann";
            this.ts_itslAcc.Click += new System.EventHandler(this.ts_itslAcc_Click);
            // 
            // invoicesTableAdapter
            // 
            this.invoicesTableAdapter.ClearBeforeFill = true;
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // itslearningImWebÖffnenToolStripMenuItem
            // 
            this.itslearningImWebÖffnenToolStripMenuItem.Name = "itslearningImWebÖffnenToolStripMenuItem";
            this.itslearningImWebÖffnenToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.itslearningImWebÖffnenToolStripMenuItem.Text = "itslearning im Web öffnen";
            this.itslearningImWebÖffnenToolStripMenuItem.Click += new System.EventHandler(this.itslearningImWebÖffnenToolStripMenuItem_Click);
            // 
            // Invoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 618);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1119, 657);
            this.MinimumSize = new System.Drawing.Size(1119, 657);
            this.Name = "Invoices";
            this.Text = "Rechnungen";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.actions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.invoicesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schülerfirmaTestDataSet)).EndInit();
            this.overviewBox.ResumeLayout(false);
            this.overviewBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invoicesBindingSource)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.changeView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip actions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem sendReminder;
        private System.Windows.Forms.ToolStripMenuItem showCustomer;
        private System.Windows.Forms.ToolStripMenuItem sendMessage;
        private System.Windows.Forms.ToolStripMenuItem properties;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markPaid;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem viewtsitem;
        private System.Windows.Forms.ContextMenuStrip changeView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox overviewBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label name_lbl_overview;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label o_anzahl_coffee;
        private System.Windows.Forms.Label o_costs_coffee;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label o_anzahl_tea;
        private System.Windows.Forms.Label o_anzahl_juices;
        private System.Windows.Forms.Label o_anzahl_chocolate;
        private System.Windows.Forms.Label o_costs_tea;
        private System.Windows.Forms.Label o_costs_juices;
        private System.Windows.Forms.Label o_costs_chocolate;
        private System.Windows.Forms.Label o_nettosumme;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label o_bruttosumme;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label o_payment_overdue;
        private System.Windows.Forms.Label o_bookingnumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader monthyear;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader amount;
        private SchülerfirmaTestDataSet schülerfirmaTestDataSet;
        private System.Windows.Forms.BindingSource invoicesBindingSource;
        private SchülerfirmaTestDataSetTableAdapters.invoicesTableAdapter invoicesTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn namemonthDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem profilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ts_itslAcc;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.ToolStripMenuItem webOberflächeÖffnenToolStripMenuItem;
        private System.Windows.Forms.BindingSource invoicesBindingSource1;
        private System.Windows.Forms.ToolStripMenuItem datenAktualisierenToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceMont;
        private System.Windows.Forms.DataGridViewTextBoxColumn bookingnrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bruttoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nettoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn paymentdueDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem itslearningImWebÖffnenToolStripMenuItem;
    }
}

