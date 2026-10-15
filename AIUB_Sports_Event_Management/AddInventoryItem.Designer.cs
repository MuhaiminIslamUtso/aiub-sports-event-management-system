using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    partial class AddInventoryItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Add Inventory Item";

            // Label for Item Name
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblItemName.Text = "Item Name:";
            this.lblItemName.Location = new System.Drawing.Point(30, 30);
            this.lblItemName.Size = new System.Drawing.Size(100, 20);

            // TextBox for Item Name
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtItemName.Location = new System.Drawing.Point(150, 30);
            this.txtItemName.Size = new System.Drawing.Size(200, 20);

            // Label for Category
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblCategory.Text = "Category:";
            this.lblCategory.Location = new System.Drawing.Point(30, 70);
            this.lblCategory.Size = new System.Drawing.Size(100, 20);

            // ComboBox for Category
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbCategory.Items.AddRange(new object[] {
                "Sports Equipment",
                "Clothing",
                "Accessories",
                "Other"});
            this.cmbCategory.Location = new System.Drawing.Point(150, 70);
            this.cmbCategory.Size = new System.Drawing.Size(200, 20);

            // Label for Quantity
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblQuantity.Text = "Quantity:";
            this.lblQuantity.Location = new System.Drawing.Point(30, 110);
            this.lblQuantity.Size = new System.Drawing.Size(100, 20);

            // TextBox for Quantity
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtQuantity.Location = new System.Drawing.Point(150, 110);
            this.txtQuantity.Size = new System.Drawing.Size(200, 20);

            // Label for Price
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblPrice.Text = "Price Per Unit:";
            this.lblPrice.Location = new System.Drawing.Point(30, 150);
            this.lblPrice.Size = new System.Drawing.Size(100, 20);

            // TextBox for Price
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtPrice.Location = new System.Drawing.Point(150, 150);
            this.txtPrice.Size = new System.Drawing.Size(200, 20);

            // Label for Supplier
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblSupplier.Text = "Supplier:";
            this.lblSupplier.Location = new System.Drawing.Point(30, 190);
            this.lblSupplier.Size = new System.Drawing.Size(100, 20);

            // TextBox for Supplier
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.txtSupplier.Location = new System.Drawing.Point(150, 190);
            this.txtSupplier.Size = new System.Drawing.Size(200, 20);

            // Label for Date Purchased
            this.lblDatePurchased = new System.Windows.Forms.Label();
            this.lblDatePurchased.Text = "Date Purchased:";
            this.lblDatePurchased.Location = new System.Drawing.Point(30, 230);
            this.lblDatePurchased.Size = new System.Drawing.Size(100, 20);

            // DatePicker for Date Purchased
            this.dtpDatePurchased = new System.Windows.Forms.DateTimePicker();
            this.dtpDatePurchased.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatePurchased.Location = new System.Drawing.Point(150, 230);
            this.dtpDatePurchased.Size = new System.Drawing.Size(200, 20);

            // Button to Add Item
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.Location = new System.Drawing.Point(150, 270);
            this.btnAddItem.Size = new System.Drawing.Size(100, 30);
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);

            // Button to Cancel
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new System.Drawing.Point(270, 270);
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Adding Controls to the Form
            this.Controls.Add(this.lblItemName);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.lblDatePurchased);
            this.Controls.Add(this.dtpDatePurchased);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnCancel);

            this.Text = "Add Inventory Item";
            this.ClientSize = new System.Drawing.Size(500, 350);
        }

        #endregion

        // Controls Declaration
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label lblDatePurchased;
        private System.Windows.Forms.DateTimePicker dtpDatePurchased;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnCancel;

        // Event Handlers
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string itemName = txtItemName.Text;
            string category = cmbCategory.SelectedItem.ToString();
            int quantity = Convert.ToInt32(txtQuantity.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text);
            string supplier = txtSupplier.Text;
            DateTime datePurchased = dtpDatePurchased.Value;

            // Add the item to the database
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    string query = "INSERT INTO Inventory (ItemName, Category, QuantityInStock, PricePerUnit, Supplier, DatePurchased) " +
                                   "VALUES (@ItemName, @Category, @Quantity, @Price, @Supplier, @DatePurchased)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ItemName", itemName);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Supplier", supplier);
                    cmd.Parameters.AddWithValue("@DatePurchased", datePurchased);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item added successfully.");
                    this.Hide();
                    new Inventory().Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Inventory().Show();  // Show the inventory 
        }
    }
}
