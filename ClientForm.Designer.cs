namespace BackOffice.Desktop.Forms;

partial class ClientForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblFirstName;
    private Label lblLastName;
    private Label lblEmail;
    private Label lblPhone;
    private Label lblPassword;
    private TextBox txtFirstName;
    private TextBox txtLastName;
    private TextBox txtEmail;
    private TextBox txtPhone;
    private TextBox txtPassword;
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
        this.lblFirstName = new Label();
        this.lblLastName = new Label();
        this.lblEmail = new Label();
        this.lblPhone = new Label();
        this.lblPassword = new Label();
        this.txtFirstName = new TextBox();
        this.txtLastName = new TextBox();
        this.txtEmail = new TextBox();
        this.txtPhone = new TextBox();
        this.txtPassword = new TextBox();
        this.btnSave = new Button();
        this.btnCancel = new Button();
        this.SuspendLayout();

        // lblFirstName
        this.lblFirstName.AutoSize = true;
        this.lblFirstName.Location = new System.Drawing.Point(20, 20);
        this.lblFirstName.Name = "lblFirstName";
        this.lblFirstName.Size = new System.Drawing.Size(60, 13);
        this.lblFirstName.Text = "First Name:";

        // txtFirstName
        this.txtFirstName.Location = new System.Drawing.Point(120, 17);
        this.txtFirstName.Name = "txtFirstName";
        this.txtFirstName.Size = new System.Drawing.Size(250, 20);
        this.txtFirstName.TabIndex = 0;

        // lblLastName
        this.lblLastName.AutoSize = true;
        this.lblLastName.Location = new System.Drawing.Point(20, 50);
        this.lblLastName.Name = "lblLastName";
        this.lblLastName.Size = new System.Drawing.Size(61, 13);
        this.lblLastName.Text = "Last Name:";

        // txtLastName
        this.txtLastName.Location = new System.Drawing.Point(120, 47);
        this.txtLastName.Name = "txtLastName";
        this.txtLastName.Size = new System.Drawing.Size(250, 20);
        this.txtLastName.TabIndex = 1;

        // lblEmail
        this.lblEmail.AutoSize = true;
        this.lblEmail.Location = new System.Drawing.Point(20, 80);
        this.lblEmail.Name = "lblEmail";
        this.lblEmail.Size = new System.Drawing.Size(35, 13);
        this.lblEmail.Text = "Email:";

        // txtEmail
        this.txtEmail.Location = new System.Drawing.Point(120, 77);
        this.txtEmail.Name = "txtEmail";
        this.txtEmail.Size = new System.Drawing.Size(250, 20);
        this.txtEmail.TabIndex = 2;

        // lblPhone
        this.lblPhone.AutoSize = true;
        this.lblPhone.Location = new System.Drawing.Point(20, 110);
        this.lblPhone.Name = "lblPhone";
        this.lblPhone.Size = new System.Drawing.Size(41, 13);
        this.lblPhone.Text = "Phone:";

        // txtPhone
        this.txtPhone.Location = new System.Drawing.Point(120, 107);
        this.txtPhone.Name = "txtPhone";
        this.txtPhone.Size = new System.Drawing.Size(200, 20);
        this.txtPhone.TabIndex = 3;

        // lblPassword
        this.lblPassword.AutoSize = true;
        this.lblPassword.Location = new System.Drawing.Point(20, 140);
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Size = new System.Drawing.Size(56, 13);
        this.lblPassword.Text = "Password:";

        // txtPassword
        this.txtPassword.Location = new System.Drawing.Point(120, 137);
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.PasswordChar = '*';
        this.txtPassword.Size = new System.Drawing.Size(200, 20);
        this.txtPassword.TabIndex = 4;

        // btnSave
        this.btnSave.Location = new System.Drawing.Point(120, 180);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(100, 30);
        this.btnSave.TabIndex = 5;
        this.btnSave.Text = "Save";
        this.btnSave.UseVisualStyleBackColor = true;
        this.btnSave.Click += new EventHandler(this.btnSave_Click);

        // btnCancel
        this.btnCancel.Location = new System.Drawing.Point(230, 180);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(100, 30);
        this.btnCancel.TabIndex = 6;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

        // ClientForm
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 230);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.txtPassword);
        this.Controls.Add(this.txtPhone);
        this.Controls.Add(this.txtEmail);
        this.Controls.Add(this.txtLastName);
        this.Controls.Add(this.txtFirstName);
        this.Controls.Add(this.lblPassword);
        this.Controls.Add(this.lblPhone);
        this.Controls.Add(this.lblEmail);
        this.Controls.Add(this.lblLastName);
        this.Controls.Add(this.lblFirstName);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "ClientForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Client";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}

