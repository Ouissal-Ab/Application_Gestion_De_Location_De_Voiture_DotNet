namespace BackOffice.Desktop.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitle;
    private Label lblUsername;
    private Label lblPassword;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
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
        this.lblTitle = new Label();
        this.lblUsername = new Label();
        this.lblPassword = new Label();
        this.txtUsername = new TextBox();
        this.txtPassword = new TextBox();
        this.btnLogin = new Button();
        this.btnCancel = new Button();
        this.SuspendLayout();

        // lblTitle
        this.lblTitle.AutoSize = true;
        this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
        this.lblTitle.Location = new System.Drawing.Point(100, 30);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(200, 24);
        this.lblTitle.Text = "Admin Login";

        // lblUsername
        this.lblUsername.AutoSize = true;
        this.lblUsername.Location = new System.Drawing.Point(50, 80);
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Size = new System.Drawing.Size(60, 13);
        this.lblUsername.Text = "Username:";

        // txtUsername
        this.txtUsername.Location = new System.Drawing.Point(120, 77);
        this.txtUsername.Name = "txtUsername";
        this.txtUsername.Size = new System.Drawing.Size(200, 20);
        this.txtUsername.TabIndex = 0;

        // lblPassword
        this.lblPassword.AutoSize = true;
        this.lblPassword.Location = new System.Drawing.Point(50, 120);
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Size = new System.Drawing.Size(56, 13);
        this.lblPassword.Text = "Password:";

        // txtPassword
        this.txtPassword.Location = new System.Drawing.Point(120, 117);
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.PasswordChar = '*';
        this.txtPassword.Size = new System.Drawing.Size(200, 20);
        this.txtPassword.TabIndex = 1;

        // btnLogin
        this.btnLogin.Location = new System.Drawing.Point(120, 160);
        this.btnLogin.Name = "btnLogin";
        this.btnLogin.Size = new System.Drawing.Size(90, 30);
        this.btnLogin.TabIndex = 2;
        this.btnLogin.Text = "Login";
        this.btnLogin.UseVisualStyleBackColor = true;
        this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

        // btnCancel
        this.btnCancel.Location = new System.Drawing.Point(230, 160);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(90, 30);
        this.btnCancel.TabIndex = 3;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

        // LoginForm
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 250);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnLogin);
        this.Controls.Add(this.txtPassword);
        this.Controls.Add(this.txtUsername);
        this.Controls.Add(this.lblPassword);
        this.Controls.Add(this.lblUsername);
        this.Controls.Add(this.lblTitle);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "LoginForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Car Rental - Admin Login";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}

