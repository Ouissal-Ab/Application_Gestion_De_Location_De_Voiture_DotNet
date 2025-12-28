namespace BackOffice.Desktop.Forms;

partial class VehicleForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblBrand;
    private Label lblModel;
    private Label lblYear;
    private Label lblColor;
    private Label lblLicensePlate;
    private Label lblDailyRate;
    private Label lblDescription;
    private TextBox txtBrand;
    private TextBox txtModel;
    private TextBox txtYear;
    private TextBox txtColor;
    private TextBox txtLicensePlate;
    private TextBox txtDailyRate;
    private TextBox txtDescription;
    private CheckBox chkIsAvailable;
    private Button btnSave;
    private Button btnCancel;

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
        this.lblBrand = new Label();
        this.lblModel = new Label();
        this.lblYear = new Label();
        this.lblColor = new Label();
        this.lblLicensePlate = new Label();
        this.lblDailyRate = new Label();
        this.lblDescription = new Label();
        this.txtBrand = new TextBox();
        this.txtModel = new TextBox();
        this.txtYear = new TextBox();
        this.txtColor = new TextBox();
        this.txtLicensePlate = new TextBox();
        this.txtDailyRate = new TextBox();
        this.txtDescription = new TextBox();
        this.chkIsAvailable = new CheckBox();
        this.btnSave = new Button();
        this.btnCancel = new Button();
        this.SuspendLayout();

        // lblBrand
        this.lblBrand.AutoSize = true;
        this.lblBrand.Location = new System.Drawing.Point(20, 20);
        this.lblBrand.Name = "lblBrand";
        this.lblBrand.Size = new System.Drawing.Size(38, 13);
        this.lblBrand.Text = "Brand:";

        // txtBrand
        this.txtBrand.Location = new System.Drawing.Point(120, 17);
        this.txtBrand.Name = "txtBrand";
        this.txtBrand.Size = new System.Drawing.Size(250, 20);
        this.txtBrand.TabIndex = 0;

        // lblModel
        this.lblModel.AutoSize = true;
        this.lblModel.Location = new System.Drawing.Point(20, 50);
        this.lblModel.Name = "lblModel";
        this.lblModel.Size = new System.Drawing.Size(39, 13);
        this.lblModel.Text = "Model:";

        // txtModel
        this.txtModel.Location = new System.Drawing.Point(120, 47);
        this.txtModel.Name = "txtModel";
        this.txtModel.Size = new System.Drawing.Size(250, 20);
        this.txtModel.TabIndex = 1;

        // lblYear
        this.lblYear.AutoSize = true;
        this.lblYear.Location = new System.Drawing.Point(20, 80);
        this.lblYear.Name = "lblYear";
        this.lblYear.Size = new System.Drawing.Size(32, 13);
        this.lblYear.Text = "Year:";

        // txtYear
        this.txtYear.Location = new System.Drawing.Point(120, 77);
        this.txtYear.Name = "txtYear";
        this.txtYear.Size = new System.Drawing.Size(100, 20);
        this.txtYear.TabIndex = 2;

        // lblColor
        this.lblColor.AutoSize = true;
        this.lblColor.Location = new System.Drawing.Point(20, 110);
        this.lblColor.Name = "lblColor";
        this.lblColor.Size = new System.Drawing.Size(34, 13);
        this.lblColor.Text = "Color:";

        // txtColor
        this.txtColor.Location = new System.Drawing.Point(120, 107);
        this.txtColor.Name = "txtColor";
        this.txtColor.Size = new System.Drawing.Size(150, 20);
        this.txtColor.TabIndex = 3;

        // lblLicensePlate
        this.lblLicensePlate.AutoSize = true;
        this.lblLicensePlate.Location = new System.Drawing.Point(20, 140);
        this.lblLicensePlate.Name = "lblLicensePlate";
        this.lblLicensePlate.Size = new System.Drawing.Size(75, 13);
        this.lblLicensePlate.Text = "License Plate:";

        // txtLicensePlate
        this.txtLicensePlate.Location = new System.Drawing.Point(120, 137);
        this.txtLicensePlate.Name = "txtLicensePlate";
        this.txtLicensePlate.Size = new System.Drawing.Size(150, 20);
        this.txtLicensePlate.TabIndex = 4;

        // lblDailyRate
        this.lblDailyRate.AutoSize = true;
        this.lblDailyRate.Location = new System.Drawing.Point(20, 170);
        this.lblDailyRate.Name = "lblDailyRate";
        this.lblDailyRate.Size = new System.Drawing.Size(61, 13);
        this.lblDailyRate.Text = "Daily Rate:";

        // txtDailyRate
        this.txtDailyRate.Location = new System.Drawing.Point(120, 167);
        this.txtDailyRate.Name = "txtDailyRate";
        this.txtDailyRate.Size = new System.Drawing.Size(100, 20);
        this.txtDailyRate.TabIndex = 5;

        // chkIsAvailable
        this.chkIsAvailable.AutoSize = true;
        this.chkIsAvailable.Location = new System.Drawing.Point(120, 200);
        this.chkIsAvailable.Name = "chkIsAvailable";
        this.chkIsAvailable.Size = new System.Drawing.Size(75, 17);
        this.chkIsAvailable.Text = "Available";
        this.chkIsAvailable.UseVisualStyleBackColor = true;
        this.chkIsAvailable.TabIndex = 6;

        // lblDescription
        this.lblDescription.AutoSize = true;
        this.lblDescription.Location = new System.Drawing.Point(20, 230);
        this.lblDescription.Name = "lblDescription";
        this.lblDescription.Size = new System.Drawing.Size(63, 13);
        this.lblDescription.Text = "Description:";

        // txtDescription
        this.txtDescription.Location = new System.Drawing.Point(120, 227);
        this.txtDescription.Multiline = true;
        this.txtDescription.Name = "txtDescription";
        this.txtDescription.Size = new System.Drawing.Size(250, 80);
        this.txtDescription.TabIndex = 7;

        // btnSave
        this.btnSave.Location = new System.Drawing.Point(120, 320);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(100, 30);
        this.btnSave.TabIndex = 8;
        this.btnSave.Text = "Save";
        this.btnSave.UseVisualStyleBackColor = true;
        this.btnSave.Click += new EventHandler(this.btnSave_Click);

        // btnCancel
        this.btnCancel.Location = new System.Drawing.Point(230, 320);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(100, 30);
        this.btnCancel.TabIndex = 9;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

        // VehicleForm
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 370);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.txtDescription);
        this.Controls.Add(this.chkIsAvailable);
        this.Controls.Add(this.txtDailyRate);
        this.Controls.Add(this.txtLicensePlate);
        this.Controls.Add(this.txtColor);
        this.Controls.Add(this.txtYear);
        this.Controls.Add(this.txtModel);
        this.Controls.Add(this.txtBrand);
        this.Controls.Add(this.lblDescription);
        this.Controls.Add(this.lblDailyRate);
        this.Controls.Add(this.lblLicensePlate);
        this.Controls.Add(this.lblColor);
        this.Controls.Add(this.lblYear);
        this.Controls.Add(this.lblModel);
        this.Controls.Add(this.lblBrand);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "VehicleForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Vehicle";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}

