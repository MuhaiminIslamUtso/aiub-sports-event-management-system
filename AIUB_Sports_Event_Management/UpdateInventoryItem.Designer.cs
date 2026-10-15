using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    partial class UpdateInventoryItem : Form
    {
        private System.ComponentModel.IContainer components = null;

        // Controls Declaration
        private Label lblItemID;
        private TextBox txtItemID;
        private Label lblItemName;
        private TextBox txtItemName;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblQuantity;
        private TextBox txtQuantity;
        private Button btnSave;
        private Button btnCancel;

        private int itemId; // Store Item ID

        public UpdateInventoryItem(int itemId, string itemName, string category, int quantityInStock)
        {
            InitializeComponent();

            // Assign values
            this.itemId = itemId;
            txtItemID.Text = itemId.ToString();
            txtItemName.Text = itemName;
            cmbCategory.SelectedItem = category;
            txtQuantity.Text = quantityInStock.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Update Inventory Item";

            // Label - Item ID
            lblItemID = new Label();
            lblItemID.Text = "Item ID:";
            lblItemID.Location = new System.Drawing.Point(30, 30);
            lblItemID.Size = new System.Drawing.Size(100, 20);

            // TextBox - Item ID (Read-only)
            txtItemID = new TextBox();
            txtItemID.Location = new System.Drawing.Point(150, 30);
            txtItemID.Size = new System.Drawing.Size(200, 20);
            txtItemID.ReadOnly = true;

            // Label - Item Name
            lblItemName = new Label();
            lblItemName.Text = "Item Name:";
            lblItemName.Location = new System.Drawing.Point(30, 70);
            lblItemName.Size = new System.Drawing.Size(100, 20);

            // TextBox - Item Name
            txtItemName = new TextBox();
            txtItemName.Location = new System.Drawing.Point(150, 70);
            txtItemName.Size = new System.Drawing.Size(200, 20);

            // Label - Category
            lblCategory = new Label();
            lblCategory.Text = "Category:";
            lblCategory.Location = new System.Drawing.Point(30, 110);
            lblCategory.Size = new System.Drawing.Size(100, 20);

            // ComboBox - Category
            cmbCategory = new ComboBox();
            cmbCategory.Items.AddRange(new object[] {
                "Sports Equipment",
                "Clothing",
                "Accessories",
                "Other"
            });
            cmbCategory.Location = new System.Drawing.Point(150, 110);
            cmbCategory.Size = new System.Drawing.Size(200, 20);

            // Label - Quantity
            lblQuantity = new Label();
            lblQuantity.Text = "Quantity:";
            lblQuantity.Location = new System.Drawing.Point(30, 150);
            lblQuantity.Size = new System.Drawing.Size(100, 20);

            // TextBox - Quantity
            txtQuantity = new TextBox();
            txtQuantity.Location = new System.Drawing.Point(150, 150);
            txtQuantity.Size = new System.Drawing.Size(200, 20);

            // Button - Save
            btnSave = new Button();
            btnSave.Text = "Save Changes";
            btnSave.Location = new System.Drawing.Point(150, 200);
            btnSave.Size = new System.Drawing.Size(100, 30);
            btnSave.Click += new EventHandler(btnSave_Click);

            // Button - Cancel
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new System.Drawing.Point(270, 200);
            btnCancel.Size = new System.Drawing.Size(100, 30);
            btnCancel.Click += new EventHandler(btnCancel_Click);

            // Add controls to form
            this.Controls.Add(lblItemID);
            this.Controls.Add(txtItemID);
            this.Controls.Add(lblItemName);
            this.Controls.Add(txtItemName);
            this.Controls.Add(lblCategory);
            this.Controls.Add(cmbCategory);
            this.Controls.Add(lblQuantity);
            this.Controls.Add(txtQuantity);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string category = cmbCategory.SelectedItem?.ToString() ?? "";
            int quantity = Convert.ToInt32(txtQuantity.Text);

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    string query = "UPDATE Inventory SET Category = @Category, QuantityInStock = @Quantity WHERE ItemID = @ItemID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@ItemID", itemId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item updated successfully.");
                    this.Close();
                    new Inventory().Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            new Inventory().Show();
        }
    }
}
