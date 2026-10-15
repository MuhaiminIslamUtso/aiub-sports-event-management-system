using System;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    partial class CoachDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblWelcome;
        private Button btnManageUsers;
        private Button btnManageTeams;
        private Button btnManageProfile;
        private Button btnLogout;

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Users().Show();
        }

        private void btnManageTeams_Click(object sender, EventArgs e)
        {
            this.Hide();
            new TeamsJoin().Show();
        }

        private void btnManageProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            new UpdateProfile().Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserSession.Username = null;
            UserSession.FullName = null;
            UserSession.Role = null;

            this.Hide();
            new Login().Show();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnManageUsers = new System.Windows.Forms.Button();
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
            this.lblWelcome.TabIndex = 4;
            this.lblWelcome.Text = $"Welcome, {UserSession.FullName}";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageUsers.Location = new System.Drawing.Point(325, 100);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(250, 50);
            this.btnManageUsers.TabIndex = 0;
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 
            // btnManageTeams
            // 
            this.btnManageTeams.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageTeams.Location = new System.Drawing.Point(325, 170);
            this.btnManageTeams.Name = "btnManageTeams";
            this.btnManageTeams.Size = new System.Drawing.Size(250, 50);
            this.btnManageTeams.TabIndex = 1;
            this.btnManageTeams.Text = "Manage Teams";
            this.btnManageTeams.UseVisualStyleBackColor = true;
            this.btnManageTeams.Click += new System.EventHandler(this.btnManageTeams_Click);
            // 
            // btnManageProfile
            // 
            this.btnManageProfile.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageProfile.Location = new System.Drawing.Point(325, 240);
            this.btnManageProfile.Name = "btnManageProfile";
            this.btnManageProfile.Size = new System.Drawing.Size(250, 50);
            this.btnManageProfile.TabIndex = 2;
            this.btnManageProfile.Text = "Manage Profile";
            this.btnManageProfile.UseVisualStyleBackColor = true;
            this.btnManageProfile.Click += new System.EventHandler(this.btnManageProfile_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogout.Location = new System.Drawing.Point(325, 310);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(250, 50);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // CoachDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.btnManageTeams);
            this.Controls.Add(this.btnManageProfile);
            this.Controls.Add(this.btnLogout);
            this.Name = "CoachDashboard";
            this.Text = "Coach Dashboard";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
