using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class Users : Form
    {

        private void Users_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    string query = "SELECT UserID, Username, FullName, Role FROM Users";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    dgvUsers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }

        private void btnUpdateRole_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0 && cmbRoles.SelectedItem != null)
            {
                int userID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);
                string newRole = cmbRoles.SelectedItem.ToString();

                try
                {
                    using (SqlConnection conn = Database.GetConnection())
                    {
                        string query = "UPDATE Users SET Role = @role WHERE UserID = @userID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@role", newRole);
                        cmd.Parameters.AddWithValue("@userID", userID);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Role updated successfully.");
                        LoadUsers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating role: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a user and role to update.");
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                int userID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);

                var confirm = MessageBox.Show("Are you sure to delete this user?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = Database.GetConnection())
                        {
                            string query = "DELETE FROM Users WHERE UserID = @userID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@userID", userID);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("User deleted successfully.");
                            LoadUsers();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting user: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (UserSession.Role == "Admin") new AdminDashboard().Show(); // Redirect to admin dashboard
            else if (UserSession.Role == "Coach") new CoachDashboard().Show(); // Redirect to coach dashboard
            else if (UserSession.Role == "Staff") new Staff().Show(); // Redirect to staff dashboard
            else if (UserSession.Role == "Player") new PlayerDashboard().Show(); // Redirect to user dashboard
        }

        #region Designer Code

        private Label lblTitle;
        private DataGridView dgvUsers;
        private ComboBox cmbRoles;
        private Label lblRole;
        private Button btnUpdateRole;
        private Button btnDeleteUser;
        private Button btnBack;

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.dgvUsers = new DataGridView();
            this.cmbRoles = new ComboBox();
            this.lblRole = new Label();
            this.btnUpdateRole = new Button();
            this.btnDeleteUser = new Button();
            this.btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();

            // Title Label
            this.lblTitle.Text = "User Management";
            this.lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkBlue;
            this.lblTitle.Location = new Point(300, 10);
            this.lblTitle.Size = new Size(400, 40);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // DataGridView
            this.dgvUsers.Location = new Point(50, 70);
            this.dgvUsers.Size = new Size(800, 300);
            this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false; 
            this.dgvUsers.AllowUserToOrderColumns = false; 
            this.dgvUsers.AllowUserToResizeRows = false;

            // Role Label
            this.lblRole.Text = "Change Role:";
            this.lblRole.Font = new Font("Segoe UI", 12F);
            this.lblRole.Location = new Point(50, 390);
            this.lblRole.Size = new Size(110, 30);

            // Role ComboBox
            this.cmbRoles.Location = new Point(170, 390);
            this.cmbRoles.Size = new Size(200, 30);
            this.cmbRoles.Font = new Font("Segoe UI", 12F);
            this.cmbRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRoles.Items.AddRange(new object[] { "Admin", "Staff", "Coach", "Player" });

            // Update Role Button
            this.btnUpdateRole.Text = "Update Role";
            this.btnUpdateRole.Font = new Font("Segoe UI", 12F);
            this.btnUpdateRole.Location = new Point(400, 390);
            this.btnUpdateRole.Size = new Size(200, 40);
            this.btnUpdateRole.Click += new EventHandler(this.btnUpdateRole_Click);

            // Delete Button
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.Font = new Font("Segoe UI", 12F);
            this.btnDeleteUser.Location = new Point(620, 390);
            this.btnDeleteUser.Size = new Size(200, 40);
            this.btnDeleteUser.Click += new EventHandler(this.btnDeleteUser_Click);

            //  Back Button
            this.btnBack.Text = "Back";
            this.btnBack.Font = new Font("Segoe UI", 12F);
            this.btnBack.Location = new Point(20, 10);
            this.btnBack.Size = new Size(100, 35);
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // Users Form
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 600);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.cmbRoles);
            this.Controls.Add(this.btnUpdateRole);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDeleteUser);
            this.Name = "Users";
            this.Text = "Manage Users";
            this.Load += new EventHandler(this.Users_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
