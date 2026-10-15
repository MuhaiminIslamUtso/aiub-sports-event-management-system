using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class Inventory : Form
    {
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdateItem;

        private void Inventory_Load(object sender, EventArgs e)
        {
            LoadInventoryData(); 
        }

        private void LoadInventoryData()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT ItemID, ItemName, Category, QuantityInStock, PricePerUnit, Supplier, DatePurchased FROM Inventory";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No data found in the inventory.");
                    }
                    else
                    {
                        dgvInventory.DataSource = dt;

                        // Hide the ItemID column
                        dgvInventory.Columns["ItemID"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory data: " + ex.Message);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (UserSession.Role == "Admin") new AdminDashboard().Show(); // Redirect to admin dashboard
            else if (UserSession.Role == "Staff") new Staff().Show(); // Redirect to staff dashboard
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddInventoryItem().Show();
            LoadInventoryData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                var confirm = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    int itemId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["ItemID"].Value);

                    try
                    {
                        using (SqlConnection conn = Database.GetConnection())
                        {
                            string query = "DELETE FROM Inventory WHERE ItemID = @ItemID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@ItemID", itemId);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Item deleted successfully.");
                            LoadInventoryData(); // Reload data after deletion
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting item: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.");
            }
        }

        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                // Get the selected item's details
                int itemId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["ItemID"].Value);
                string itemName = dgvInventory.SelectedRows[0].Cells["ItemName"].Value.ToString();
                string category = dgvInventory.SelectedRows[0].Cells["Category"].Value.ToString();
                int quantityInStock = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["QuantityInStock"].Value);

                // Create an instance of the UpdateInventoryItem form
                UpdateInventoryItem updateForm = new UpdateInventoryItem(itemId, itemName, category, quantityInStock);

                // Show the UpdateInventoryItem form
                this.Hide();
                updateForm.ShowDialog();

                LoadInventoryData();
            }
            else
            {
                MessageBox.Show("Please select an item to update.");
            }
        }


        private void InitializeComponent()
        {
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdateItem = new System.Windows.Forms.Button();

            // Back Button
            this.btnBack.Text = "Back";
            this.btnBack.Location = new System.Drawing.Point(20, 10);
            this.btnBack.Size = new System.Drawing.Size(100, 35);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // Add Item Button
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.Location = new System.Drawing.Point(130, 10);
            this.btnAddItem.Size = new System.Drawing.Size(100, 35);
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);

            // Inventory Table (DataGridView)
            this.dgvInventory.Location = new System.Drawing.Point(50, 60);
            this.dgvInventory.Size = new System.Drawing.Size(800, 300);
            this.dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventory.MultiSelect = false;
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.AllowUserToOrderColumns = false;
            this.dgvInventory.AllowUserToResizeRows = false;

            // Update Item Button
            this.btnUpdateItem.Text = "Update Item";
            this.btnUpdateItem.Location = new System.Drawing.Point(50, 400);
            this.btnUpdateItem.Size = new System.Drawing.Size(200, 40);
            this.btnUpdateItem.Click += new System.EventHandler(this.btnUpdateItem_Click);

            // Delete Button
            this.btnDelete.Text = "Delete Item";
            this.btnDelete.Location = new System.Drawing.Point(270, 400);
            this.btnDelete.Size = new System.Drawing.Size(200, 40);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // Adding controls to the form
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.dgvInventory);
            this.Controls.Add(this.btnUpdateItem); 
            this.Controls.Add(this.btnDelete);

            this.Text = "Inventory";
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Load += new EventHandler(this.Inventory_Load);
        }
    }
}
