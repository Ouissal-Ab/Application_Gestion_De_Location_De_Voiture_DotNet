using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Desktop.Forms;

public partial class VehicleForm : Form
{
    private int? _vehicleId;
    private Vehicle? _vehicle;
    private readonly DbContextOptions<CarRentalDbContext> _dbOptions;

    public VehicleForm(int? vehicleId, DbContextOptions<CarRentalDbContext> dbOptions)
    {
        _vehicleId = vehicleId;
        _dbOptions = dbOptions;
        InitializeComponent();
        LoadVehicle();
    }

    private void LoadVehicle()
    {
        if (_vehicleId.HasValue)
        {
            try
            {
                using var context = new CarRentalDbContext(_dbOptions);
                _vehicle = context.Vehicles.Find(_vehicleId.Value);
                if (_vehicle != null)
                {
                    txtBrand.Text = _vehicle.Brand;
                    txtModel.Text = _vehicle.Model;
                    txtYear.Text = _vehicle.Year.ToString();
                    txtColor.Text = _vehicle.Color;
                    txtLicensePlate.Text = _vehicle.LicensePlate;
                    txtDailyRate.Text = _vehicle.DailyRate.ToString();
                    chkIsAvailable.Checked = _vehicle.IsAvailable;
                    txtDescription.Text = _vehicle.Description ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (_vehicleId.HasValue)
            {
                _vehicle = context.Vehicles.Find(_vehicleId.Value);
            }
            else
            {
                _vehicle = new Vehicle();
                context.Vehicles.Add(_vehicle);
            }

            if (_vehicle != null)
            {
                _vehicle.Brand = txtBrand.Text.Trim();
                _vehicle.Model = txtModel.Text.Trim();
                _vehicle.Year = int.Parse(txtYear.Text);
                _vehicle.Color = txtColor.Text.Trim();
                _vehicle.LicensePlate = txtLicensePlate.Text.Trim();
                _vehicle.DailyRate = decimal.Parse(txtDailyRate.Text);
                _vehicle.IsAvailable = chkIsAvailable.Checked;
                _vehicle.Description = txtDescription.Text.Trim();

                context.SaveChanges();
                MessageBox.Show("Vehicle saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtBrand.Text))
        {
            MessageBox.Show("Please enter a brand.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtModel.Text))
        {
            MessageBox.Show("Please enter a model.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (!int.TryParse(txtYear.Text, out int year) || year < 1900 || year > DateTime.Now.Year + 1)
        {
            MessageBox.Show("Please enter a valid year.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (!decimal.TryParse(txtDailyRate.Text, out decimal rate) || rate <= 0)
        {
            MessageBox.Show("Please enter a valid daily rate.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}

