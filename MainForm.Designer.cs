namespace BackOffice.Desktop.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private TabControl tabControl;
    private TabPage tabVehicles;
    private TabPage tabClients;
    private TabPage tabRentals;
    private DataGridView dgvVehicles;
    private DataGridView dgvClients;
    private DataGridView dgvRentals;
    private Button btnAddVehicle;
    private Button btnEditVehicle;
    private Button btnDeleteVehicle;
    private Button btnAddClient;
    private Button btnEditClient;
    private Button btnDeleteClient;
    private Button btnRefreshRentals;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.tabControl = new TabControl();
        this.tabVehicles = new TabPage();
        this.tabClients = new TabPage();
        this.tabRentals = new TabPage();
        this.dgvVehicles = new DataGridView();
        this.dgvClients = new DataGridView();
        this.dgvRentals = new DataGridView();
        this.btnAddVehicle = new Button();
        this.btnEditVehicle = new Button();
        this.btnDeleteVehicle = new Button();
        this.btnAddClient = new Button();
        this.btnEditClient = new Button();
        this.btnDeleteClient = new Button();
        this.btnRefreshRentals = new Button();
        this.tabControl.SuspendLayout();
        this.tabVehicles.SuspendLayout();
        this.tabClients.SuspendLayout();
        this.tabRentals.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvVehicles)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvRentals)).BeginInit();
        this.SuspendLayout();

        // tabControl
        this.tabControl.Controls.Add(this.tabVehicles);
        this.tabControl.Controls.Add(this.tabClients);
        this.tabControl.Controls.Add(this.tabRentals);
        this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabControl.Location = new System.Drawing.Point(0, 0);
        this.tabControl.Name = "tabControl";
        this.tabControl.SelectedIndex = 0;
        this.tabControl.Size = new System.Drawing.Size(1000, 600);
        this.tabControl.TabIndex = 0;

        // tabVehicles
        this.tabVehicles.Controls.Add(this.btnDeleteVehicle);
        this.tabVehicles.Controls.Add(this.btnEditVehicle);
        this.tabVehicles.Controls.Add(this.btnAddVehicle);
        this.tabVehicles.Controls.Add(this.dgvVehicles);
        this.tabVehicles.Location = new System.Drawing.Point(4, 22);
        this.tabVehicles.Name = "tabVehicles";
        this.tabVehicles.Padding = new System.Windows.Forms.Padding(3);
        this.tabVehicles.Size = new System.Drawing.Size(992, 574);
        this.tabVehicles.TabIndex = 0;
        this.tabVehicles.Text = "Vehicles";
        this.tabVehicles.UseVisualStyleBackColor = true;

        // dgvVehicles
        this.dgvVehicles.AllowUserToAddRows = false;
        this.dgvVehicles.AllowUserToDeleteRows = false;
        this.dgvVehicles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvVehicles.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvVehicles.Location = new System.Drawing.Point(3, 3);
        this.dgvVehicles.Name = "dgvVehicles";
        this.dgvVehicles.ReadOnly = true;
        this.dgvVehicles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvVehicles.Size = new System.Drawing.Size(986, 520);
        this.dgvVehicles.TabIndex = 0;

        // btnAddVehicle
        this.btnAddVehicle.Location = new System.Drawing.Point(10, 530);
        this.btnAddVehicle.Name = "btnAddVehicle";
        this.btnAddVehicle.Size = new System.Drawing.Size(100, 30);
        this.btnAddVehicle.TabIndex = 1;
        this.btnAddVehicle.Text = "Add Vehicle";
        this.btnAddVehicle.UseVisualStyleBackColor = true;
        this.btnAddVehicle.Click += new EventHandler(this.btnAddVehicle_Click);

        // btnEditVehicle
        this.btnEditVehicle.Location = new System.Drawing.Point(120, 530);
        this.btnEditVehicle.Name = "btnEditVehicle";
        this.btnEditVehicle.Size = new System.Drawing.Size(100, 30);
        this.btnEditVehicle.TabIndex = 2;
        this.btnEditVehicle.Text = "Edit Vehicle";
        this.btnEditVehicle.UseVisualStyleBackColor = true;
        this.btnEditVehicle.Click += new EventHandler(this.btnEditVehicle_Click);

        // btnDeleteVehicle
        this.btnDeleteVehicle.Location = new System.Drawing.Point(230, 530);
        this.btnDeleteVehicle.Name = "btnDeleteVehicle";
        this.btnDeleteVehicle.Size = new System.Drawing.Size(100, 30);
        this.btnDeleteVehicle.TabIndex = 3;
        this.btnDeleteVehicle.Text = "Delete Vehicle";
        this.btnDeleteVehicle.UseVisualStyleBackColor = true;
        this.btnDeleteVehicle.Click += new EventHandler(this.btnDeleteVehicle_Click);

        // tabClients
        this.tabClients.Controls.Add(this.btnDeleteClient);
        this.tabClients.Controls.Add(this.btnEditClient);
        this.tabClients.Controls.Add(this.btnAddClient);
        this.tabClients.Controls.Add(this.dgvClients);
        this.tabClients.Location = new System.Drawing.Point(4, 22);
        this.tabClients.Name = "tabClients";
        this.tabClients.Padding = new System.Windows.Forms.Padding(3);
        this.tabClients.Size = new System.Drawing.Size(992, 574);
        this.tabClients.TabIndex = 1;
        this.tabClients.Text = "Clients";
        this.tabClients.UseVisualStyleBackColor = true;

        // dgvClients
        this.dgvClients.AllowUserToAddRows = false;
        this.dgvClients.AllowUserToDeleteRows = false;
        this.dgvClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvClients.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvClients.Location = new System.Drawing.Point(3, 3);
        this.dgvClients.Name = "dgvClients";
        this.dgvClients.ReadOnly = true;
        this.dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvClients.Size = new System.Drawing.Size(986, 520);
        this.dgvClients.TabIndex = 0;

        // btnAddClient
        this.btnAddClient.Location = new System.Drawing.Point(10, 530);
        this.btnAddClient.Name = "btnAddClient";
        this.btnAddClient.Size = new System.Drawing.Size(100, 30);
        this.btnAddClient.TabIndex = 1;
        this.btnAddClient.Text = "Add Client";
        this.btnAddClient.UseVisualStyleBackColor = true;
        this.btnAddClient.Click += new EventHandler(this.btnAddClient_Click);

        // btnEditClient
        this.btnEditClient.Location = new System.Drawing.Point(120, 530);
        this.btnEditClient.Name = "btnEditClient";
        this.btnEditClient.Size = new System.Drawing.Size(100, 30);
        this.btnEditClient.TabIndex = 2;
        this.btnEditClient.Text = "Edit Client";
        this.btnEditClient.UseVisualStyleBackColor = true;
        this.btnEditClient.Click += new EventHandler(this.btnEditClient_Click);

        // btnDeleteClient
        this.btnDeleteClient.Location = new System.Drawing.Point(230, 530);
        this.btnDeleteClient.Name = "btnDeleteClient";
        this.btnDeleteClient.Size = new System.Drawing.Size(100, 30);
        this.btnDeleteClient.TabIndex = 3;
        this.btnDeleteClient.Text = "Delete Client";
        this.btnDeleteClient.UseVisualStyleBackColor = true;
        this.btnDeleteClient.Click += new EventHandler(this.btnDeleteClient_Click);

        // tabRentals
        this.tabRentals.Controls.Add(this.btnRefreshRentals);
        this.tabRentals.Controls.Add(this.dgvRentals);
        this.tabRentals.Location = new System.Drawing.Point(4, 22);
        this.tabRentals.Name = "tabRentals";
        this.tabRentals.Padding = new System.Windows.Forms.Padding(3);
        this.tabRentals.Size = new System.Drawing.Size(992, 574);
        this.tabRentals.TabIndex = 2;
        this.tabRentals.Text = "Rentals";
        this.tabRentals.UseVisualStyleBackColor = true;

        // dgvRentals
        this.dgvRentals.AllowUserToAddRows = false;
        this.dgvRentals.AllowUserToDeleteRows = false;
        this.dgvRentals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvRentals.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvRentals.Location = new System.Drawing.Point(3, 3);
        this.dgvRentals.Name = "dgvRentals";
        this.dgvRentals.ReadOnly = true;
        this.dgvRentals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvRentals.Size = new System.Drawing.Size(986, 520);
        this.dgvRentals.TabIndex = 0;

        // btnRefreshRentals
        this.btnRefreshRentals.Location = new System.Drawing.Point(10, 530);
        this.btnRefreshRentals.Name = "btnRefreshRentals";
        this.btnRefreshRentals.Size = new System.Drawing.Size(100, 30);
        this.btnRefreshRentals.TabIndex = 1;
        this.btnRefreshRentals.Text = "Refresh";
        this.btnRefreshRentals.UseVisualStyleBackColor = true;
        this.btnRefreshRentals.Click += new EventHandler(this.btnRefreshRentals_Click);

        // MainForm
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1000, 600);
        this.Controls.Add(this.tabControl);
        this.Name = "MainForm";
        this.Text = "Car Rental - Admin Panel";
        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        this.ResumeLayout(false);
        this.tabControl.ResumeLayout(false);
        this.tabVehicles.ResumeLayout(false);
        this.tabClients.ResumeLayout(false);
        this.tabRentals.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dgvVehicles)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvRentals)).EndInit();
    }
}

