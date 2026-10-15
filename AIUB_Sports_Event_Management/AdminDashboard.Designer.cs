using System;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    partial class AdminDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblWelcome;
        private Button btnManageUsers;
        private Button btnManageEvents;
        private Button btnManageInventory;
        private Button btnManageTeams;
        private Button btnManageProfile;
        private Button btnLogout;

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Users().Show();
        }

        private void btnManageEvents_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Events().Show();
        }

        private void btnManageInventory_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Inventory().Show();
        }

        private void btnManageTeams_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Teams().Show();
        }

        private void btnManageProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            new UpdateProfile().Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                UserSession.Username = null;
                UserSession.FullName = null;
                UserSession.Role = null;

                this.Hide();
                new Login().Show();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnManageEvents = new System.Windows.Forms.Button();
            this.btnManageInventory = new System.Windows.Forms.Button();
            this.btnManageTeams = new System.Windows.Forms.Button();
            this.btnManageProfile = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(200, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(500, 40);
            this.lblWelcome.TabIndex = 6;
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWelcome.Text = $"Welcome, {UserSession.FullName}";
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnManageUsers.Location = new System.Drawing.Point(325, 100);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(250, 50);
            this.btnManageUsers.TabIndex = 0;
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 
            // btnManageEvents
            // 
            this.btnManageEvents.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnManageEvents.Location = new System.Drawing.Point(325, 170);
            this.btnManageEvents.Name = "btnManageEvents";
            this.btnManageEvents.Size = new System.Drawing.Size(250, 50);
            this.btnManageEvents.TabIndex = 1;
            this.btnManageEvents.Text = "Manage Events";
            this.btnManageEvents.UseVisualStyleBackColor = true;
            this.btnManageEvents.Click += new System.EventHandler(this.btnManageEvents_Click);
            // 
            // btnManageInventory
            // 
            this.btnManageInventory.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnManageInventory.Location = new System.Drawing.Point(325, 240);
            this.btnManageInventory.Name = "btnManageInventory";
            this.btnManageInventory.Size = new System.Drawing.Size(250, 50);
            this.btnManageInventory.TabIndex = 2;
            this.btnManageInventory.Text = "Manage Inventory";
            this.btnManageInventory.UseVisualStyleBackColor = true;
            this.btnManageInventory.Click += new System.EventHandler(this.btnManageInventory_Click);
            // 
            // btnManageTeams
            // 
            this.btnManageTeams.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnManageTeams.Location = new System.Drawing.Point(325, 310);
            this.btnManageTeams.Name = "btnManageTeams";
            this.btnManageTeams.Size = new System.Drawing.Size(250, 50);
            this.btnManageTeams.TabIndex = 3;
            this.btnManageTeams.Text = "Manage Teams";
            this.btnManageTeams.UseVisualStyleBackColor = true;
            this.btnManageTeams.Click += new System.EventHandler(this.btnManageTeams_Click);
            // 
            // btnManageProfile
            // 
            this.btnManageProfile.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnManageProfile.Location = new System.Drawing.Point(325, 380);
            this.btnManageProfile.Name = "btnManageProfile";
            this.btnManageProfile.Size = new System.Drawing.Size(250, 50);
            this.btnManageProfile.TabIndex = 4;
            this.btnManageProfile.Text = "Manage Profile";
            this.btnManageProfile.UseVisualStyleBackColor = true;
            this.btnManageProfile.Click += new System.EventHandler(this.btnManageProfile_Click);
            //
            // btnLogout
            //
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLogout.Location = new System.Drawing.Point(325, 450);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(250, 50);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // AdminDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.btnManageEvents);
            this.Controls.Add(this.btnManageInventory);
            this.Controls.Add(this.btnManageTeams);
            this.Controls.Add(this.btnManageProfile);
            this.Controls.Add(this.btnLogout);
            this.Name = "AdminDashboard";
            this.Text = "Admin Dashboard";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
