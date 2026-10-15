using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class Signup : Form
    {
        private System.ComponentModel.IContainer components = null;

        private void btnSignup_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) ||
                string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @username", conn);
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Username already exists. Try a different one.");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password, FullName, Role) VALUES (@username, @password, @fullname, @role)", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@fullname", fullName);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Signup successful!");
                    this.Hide();
                    new Login().Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void linkGoToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new Login().Show();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSystemTitle = new Label();
            this.lblFullName = new Label();
            this.txtFullName = new TextBox();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblConfirmPassword = new Label();
            this.txtConfirmPassword = new TextBox();
            this.lblRole = new Label();
            this.cmbRole = new ComboBox();
            this.btnSignup = new Button();
            this.linkGoToLogin = new LinkLabel();

            this.SuspendLayout();

            // Title
            this.lblSystemTitle.Text = "AIUB Sports Event Management System";
            this.lblSystemTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblSystemTitle.Location = new Point(200, 20);
            this.lblSystemTitle.Size = new Size(500, 40);
            this.lblSystemTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Full Name
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new Point(330, 80);
            this.lblFullName.Text = "Full Name:";
            this.txtFullName.Location = new Point(330, 100);
            this.txtFullName.Size = new Size(250, 20);

            // Username
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new Point(330, 140);
            this.lblUsername.Text = "Username:";
            this.txtUsername.Location = new Point(330, 160);
            this.txtUsername.Size = new Size(250, 20);

            // Password
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(330, 200);
            this.lblPassword.Text = "Password:";
            this.txtPassword.Location = new Point(330, 220);
            this.txtPassword.Size = new Size(250, 20);
            this.txtPassword.UseSystemPasswordChar = true;

            // Confirm Password
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new Point(330, 260);
            this.lblConfirmPassword.Text = "Confirm Password:";
            this.txtConfirmPassword.Location = new Point(330, 280);
            this.txtConfirmPassword.Size = new Size(250, 20);
            this.txtConfirmPassword.UseSystemPasswordChar = true;

            // Role
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new Point(330, 320);
            this.lblRole.Text = "Select Role:";
            this.cmbRole.Location = new Point(330, 340);
            this.cmbRole.Size = new Size(250, 20);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new object[] { "Player", "Coach", "Stuff" });
            this.cmbRole.SelectedIndex = 0;

            // Sign Up
            this.btnSignup.Location = new Point(330, 380);
            this.btnSignup.Size = new Size(250, 40);
            this.btnSignup.Text = "Sign Up";
            this.btnSignup.Click += new EventHandler(this.btnSignup_Click);

            // Link to Login
            this.linkGoToLogin.Text = "Already have an account? Login";
            this.linkGoToLogin.Location = new Point(330, 430);
            this.linkGoToLogin.Size = new Size(250, 30);
            this.linkGoToLogin.TextAlign = ContentAlignment.MiddleCenter;
            this.linkGoToLogin.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkGoToLogin_LinkClicked);

            // Signup Form
            this.ClientSize = new Size(900, 550);
            this.BackColor = Color.LightBlue;
            this.Controls.Add(this.lblSystemTitle);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.btnSignup);
            this.Controls.Add(this.linkGoToLogin);
            this.Text = "Signup";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblSystemTitle;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Label lblRole;
        private ComboBox cmbRole;
        private Button btnSignup;
        private LinkLabel linkGoToLogin;
    }
}
