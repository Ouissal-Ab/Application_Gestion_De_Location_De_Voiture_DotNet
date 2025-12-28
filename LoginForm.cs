using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Desktop.Forms;

public partial class LoginForm : Form
{
    private readonly DbContextOptions<CarRentalDbContext> _dbOptions;

    public LoginForm()
    {
        _dbOptions = new DbContextOptionsBuilder<CarRentalDbContext>()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CarRentalDb;Trusted_Connection=True;TrustServerCertificate=True")
            .Options;
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            using var context = new CarRentalDbContext(_dbOptions);
            var admin = context.Admins.FirstOrDefault(a => a.Username == username && a.Password == password);

            if (admin != null)
            {
                this.Hide();
                var mainForm = new MainForm();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}

