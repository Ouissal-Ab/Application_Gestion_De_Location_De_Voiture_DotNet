using Data;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Desktop.Forms;

public partial class MainForm : Form
{
    private readonly DbContextOptions<CarRentalDbContext> _dbOptions;

    public MainForm()
    {
        _dbOptions = new DbContextOptionsBuilder<CarRentalDbContext>()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CarRentalDb;Trusted_Connection=True;TrustServerCertificate=True")
            .Options;
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        LoadVehicles();
        LoadClients();
        LoadRentals();
    }

    private void LoadVehicles()
    {
        try
        {
            using var context = new CarRentalDbContext(_dbOptions);
            dgvVehicles.DataSource = context.Vehicles.ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadClients()
    {
        try
        {
            using var context = new CarRentalDbContext(_dbOptions);
            dgvClients.DataSource = context.Clients.ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading clients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadRentals()
    {
        try
        {
            using var context = new CarRentalDbContext(_dbOptions);
            var rentals = context.Rentals
                .Include(r => r.Client)
                .Include(r => r.Vehicle)
                .ToList();
            dgvRentals.DataSource = rentals.Select(r => new
            {
                r.Id,
                ClientName = $"{r.Client.FirstName} {r.Client.LastName}",
                VehicleInfo = $"{r.Vehicle.Brand} {r.Vehicle.Model}",
                r.StartDate,
                r.EndDate,
                r.TotalAmount,
                r.Status
            }).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading rentals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnAddVehicle_Click(object sender, EventArgs e)
    {
        var form = new VehicleForm(null, _dbOptions);
        if (form.ShowDialog() == DialogResult.OK)
        {
            LoadVehicles();
        }
    }

    private void btnEditVehicle_Click(object sender, EventArgs e)
    {
        if (dgvVehicles.SelectedRows.Count > 0)
        {
            var vehicleId = (int)dgvVehicles.SelectedRows[0].Cells["Id"].Value;
            var form = new VehicleForm(vehicleId, _dbOptions);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadVehicles();
            }
        }
        else
        {
            MessageBox.Show("Please select a vehicle to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnDeleteVehicle_Click(object sender, EventArgs e)
    {
        if (dgvVehicles.SelectedRows.Count > 0)
        {
            var vehicleId = (int)dgvVehicles.SelectedRows[0].Cells["Id"].Value;
            if (MessageBox.Show("Are you sure you want to delete this vehicle?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using var context = new CarRentalDbContext(_dbOptions);
                    var vehicle = context.Vehicles.Find(vehicleId);
                    if (vehicle != null)
                    {
                        context.Vehicles.Remove(vehicle);
                        context.SaveChanges();
                        LoadVehicles();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        else
        {
            MessageBox.Show("Please select a vehicle to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnAddClient_Click(object sender, EventArgs e)
    {
        var form = new ClientForm(null, _dbOptions);
        if (form.ShowDialog() == DialogResult.OK)
        {
            LoadClients();
        }
    }

    private void btnEditClient_Click(object sender, EventArgs e)
    {
        if (dgvClients.SelectedRows.Count > 0)
        {
            var clientId = (int)dgvClients.SelectedRows[0].Cells["Id"].Value;
            var form = new ClientForm(clientId, _dbOptions);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadClients();
            }
        }
        else
        {
            MessageBox.Show("Please select a client to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnDeleteClient_Click(object sender, EventArgs e)
    {
        if (dgvClients.SelectedRows.Count > 0)
        {
            var clientId = (int)dgvClients.SelectedRows[0].Cells["Id"].Value;
            if (MessageBox.Show("Are you sure you want to delete this client?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using var context = new CarRentalDbContext(_dbOptions);
                    var client = context.Clients.Find(clientId);
                    if (client != null)
                    {
                        context.Clients.Remove(client);
                        context.SaveChanges();
                        LoadClients();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        else
        {
            MessageBox.Show("Please select a client to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnRefreshRentals_Click(object sender, EventArgs e)
    {
        LoadRentals();
    }
}

