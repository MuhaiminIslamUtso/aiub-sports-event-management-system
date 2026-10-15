using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class UpdateProfile : Form
    {
        // UI Controls
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblCurrentPassword;
        private TextBox txtCurrentPassword;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Button btnUpdate;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.Text = "Update Profile";
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Full Name
            lblFullName = new Label() { Text = "Full Name:", Location = new System.Drawing.Point(30, 30), AutoSize = true };
            txtFullName = new TextBox() { Location = new System.Drawing.Point(150, 30), Width = 200 };
            txtFullName.Text = UserSession.FullName; // prefill

            // Current Password
            lblCurrentPassword = new Label() { Text = "Current Password:", Location = new System.Drawing.Point(30, 80), AutoSize = true };
            txtCurrentPassword = new TextBox() { Location = new System.Drawing.Point(150, 80), Width = 200, UseSystemPasswordChar = true };

            // New Password
            lblNewPassword = new Label() { Text = "New Password:", Location = new System.Drawing.Point(30, 130), AutoSize = true };
            txtNewPassword = new TextBox() { Location = new System.Drawing.Point(150, 130), Width = 200, UseSystemPasswordChar = true };

            // Buttons
            btnUpdate = new Button() { Text = "Update", Location = new System.Drawing.Point(150, 190), Width = 90 };
            btnCancel = new Button() { Text = "Cancel", Location = new System.Drawing.Point(260, 190), Width = 90 };

            btnUpdate.Click += btnUpdate_Click;
            btnCancel.Click += (s, e) => {
                this.Hide();
                if (UserSession.Role == "Admin") new AdminDashboard().Show(); // Redirect to admin dashboard
                else if (UserSession.Role == "Coach") new CoachDashboard().Show(); // Redirect to coach dashboard
                else if (UserSession.Role == "Staff") new Staff().Show(); // Redirect to staff dashboard
                else if (UserSession.Role == "Player") new PlayerDashboard().Show(); // Redirect to user dashboard
            };

            this.Controls.AddRange(new Control[] {
                lblFullName, txtFullName,
                lblCurrentPassword, txtCurrentPassword,
                lblNewPassword, txtNewPassword,
                btnUpdate, btnCancel
            });
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string fullName = txtFullName.Text;

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                MessageBox.Show("Please enter your current password.");
                return;
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    // Verify current password
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Username", UserSession.Username);
                    checkCmd.Parameters.AddWithValue("@Password", currentPassword); // Note: Password should be hashed

                    int userExists = (int)checkCmd.ExecuteScalar();

                    if (userExists == 0)
                    {
                        MessageBox.Show("Current password is incorrect.");
                        return;
                    }

                    // Update user
                    string updateQuery = "UPDATE Users SET FullName = @FullName" +
                                         (string.IsNullOrWhiteSpace(newPassword) ? "" : ", Password = @NewPassword") +
                                         " WHERE Username = @Username";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@FullName", fullName);
                    updateCmd.Parameters.AddWithValue("@Username", UserSession.Username);
                    if (!string.IsNullOrWhiteSpace(newPassword))
                        updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);

                    updateCmd.ExecuteNonQuery();

                    // Update session
                    UserSession.FullName = fullName;

                    MessageBox.Show("Profile updated successfully.");
                    this.Hide();
                    if(UserSession.Role == "Admin") new AdminDashboard().Show(); // Redirect to admin dashboard
                    else if (UserSession.Role == "Coach") new CoachDashboard().Show(); // Redirect to coach dashboard
                    else if (UserSession.Role == "Staff") new Staff().Show(); // Redirect to staff dashboard
                    else if (UserSession.Role == "Player") new PlayerDashboard().Show(); // Redirect to user dashboard
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating profile: " + ex.Message);
            }
        }
    }
}
