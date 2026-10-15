using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class Login : Form
    {
        private System.ComponentModel.IContainer components = null;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Role, FullName FROM Users WHERE Username = @username AND Password = @password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader["Role"].ToString();
                            string name = reader["FullName"].ToString();
                            UserSession.Username = username;
                            UserSession.FullName = name;
                            UserSession.Role = role;
                            MessageBox.Show($"Welcome {name}! You are logged in as {role}.");
                            this.Hide();

                            if (UserSession.Role == "Admin")
                                new AdminDashboard().Show();
                            else if (UserSession.Role == "Coach")
                                new CoachDashboard().Show();
                            else if (UserSession.Role == "Player")
                                new PlayerDashboard().Show();
                            else if (UserSession.Role == "Staff")
                                new Staff().Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed: " + ex.Message);
            }
        }

        private void linkGoToSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new Signup().Show();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSystemTitle = new Label();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.linkGoToSignup = new LinkLabel();

            this.SuspendLayout();

            // Title
            this.lblSystemTitle.Text = "AIUB Sports Event Management System";
            this.lblSystemTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblSystemTitle.Location = new Point(200, 40);
            this.lblSystemTitle.Size = new Size(500, 40);
            this.lblSystemTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Username
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new Point(330, 150);
            this.lblUsername.Text = "Username:";
            this.txtUsername.Location = new Point(330, 170);
            this.txtUsername.Size = new Size(250, 22);

            // Password
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(330, 210);
            this.lblPassword.Text = "Password:";
            this.txtPassword.Location = new Point(330, 230);
            this.txtPassword.Size = new Size(250, 22);
            this.txtPassword.UseSystemPasswordChar = true;

            // Login Button
            this.btnLogin.Location = new Point(330, 270);
            this.btnLogin.Size = new Size(250, 40);
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // Signup Link
            this.linkGoToSignup.Text = "Don't have an account? Sign up";
            this.linkGoToSignup.Location = new Point(330, 320);
            this.linkGoToSignup.Size = new Size(250, 30);
            this.linkGoToSignup.TextAlign = ContentAlignment.MiddleCenter;
            this.linkGoToSignup.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkGoToSignup_LinkClicked);

            // Form settings
            this.ClientSize = new Size(900, 600);
            this.BackColor = Color.LightBlue;
            this.Controls.Add(this.lblSystemTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.linkGoToSignup);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblSystemTitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private LinkLabel linkGoToSignup;
    }
}
