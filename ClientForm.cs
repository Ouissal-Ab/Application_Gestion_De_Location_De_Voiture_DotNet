using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Desktop.Forms;

public partial class ClientForm : Form
{
    private int? _clientId;
    private Client? _client;
    private readonly DbContextOptions<CarRentalDbContext> _dbOptions;

    public ClientForm(int? clientId, DbContextOptions<CarRentalDbContext> dbOptions)
    {
        _clientId = clientId;
        _dbOptions = dbOptions;
        InitializeComponent();
        LoadClient();
    }

    private void LoadClient()
    {
        if (_clientId.HasValue)
        {
            try
            {
                using var context = new CarRentalDbContext(_dbOptions);
                _client = context.Clients.Find(_clientId.Value);
                if (_client != null)
                {
                    txtFirstName.Text = _client.FirstName;
                    txtLastName.Text = _client.LastName;
                    txtEmail.Text = _client.Email;
                    txtPhone.Text = _client.Phone;
                    txtPassword.Text = _client.Password;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateInput())
            return;

        try
        {
            using var context = new CarRentalDbContext(_dbOptions);
            if (_clientId.HasValue)
            {
                _client = context.Clients.Find(_clientId.Value);
            }
            else
            {
                _client = new Client();
                context.Clients.Add(_client);
            }

            if (_client != null)
            {
                _client.FirstName = txtFirstName.Text.Trim();
                _client.LastName = txtLastName.Text.Trim();
                _client.Email = txtEmail.Text.Trim();
                _client.Phone = txtPhone.Text.Trim();
                _client.Password = txtPassword.Text;

                context.SaveChanges();
                MessageBox.Show("Client saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtFirstName.Text))
        {
            MessageBox.Show("Please enter first name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtLastName.Text))
        {
            MessageBox.Show("Please enter last name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
        {
            MessageBox.Show("Please enter a valid email.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show("Please enter a password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}

