using System;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    partial class PlayerDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblWelcome;
        private Button btnViewTeam;
        private Button btnViewEvents;
        private Button btnManageProfile;
        private Button btnLogout;

        private void btnViewTeam_Click(object sender, EventArgs e)
        {
            this.Hide();
            new TeamsJoin().Show(); // Navigate to team view
        }

        private void btnViewEvents_Click(object sender, EventArgs e)
        {
            this.Hide();
            new JoinEvent().Show(); // Navigate to team view
        }

        private void btnManageProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            new UpdateProfile().Show(); // Navigate to profile management
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            this.btnViewTeam = new System.Windows.Forms.Button();
            this.btnViewEvents = new System.Windows.Forms.Button();
            this.btnManageProfile = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(250, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(400, 40);
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWelcome.Text = $"Welcome, {UserSession.FullName}";
            // 
            // btnViewTeam
            // 
            this.btnViewTeam.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnViewTeam.Location = new System.Drawing.Point(325, 100);
            this.btnViewTeam.Name = "btnViewTeam";
            this.btnViewTeam.Size = new System.Drawing.Size(250, 50);
            this.btnViewTeam.TabIndex = 0;
            this.btnViewTeam.Text = "View My Team";
            this.btnViewTeam.UseVisualStyleBackColor = true;
            this.btnViewTeam.Click += new System.EventHandler(this.btnViewTeam_Click);
            // 
            // btnViewEvents
            // 
            this.btnViewEvents.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnViewEvents.Location = new System.Drawing.Point(325, 180);
            this.btnViewEvents.Name = "btnViewEvents";
            this.btnViewEvents.Size = new System.Drawing.Size(250, 50);
            this.btnViewEvents.TabIndex = 1;
            this.btnViewEvents.Text = "View Events";
            this.btnViewEvents.UseVisualStyleBackColor = true;
            this.btnViewEvents.Click += new System.EventHandler(this.btnViewEvents_Click);
            // 
            // btnManageProfile
            // 
            this.btnManageProfile.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnManageProfile.Location = new System.Drawing.Point(325, 260);
            this.btnManageProfile.Name = "btnManageProfile";
            this.btnManageProfile.Size = new System.Drawing.Size(250, 50);
            this.btnManageProfile.TabIndex = 2;
            this.btnManageProfile.Text = "Manage Profile";
            this.btnManageProfile.UseVisualStyleBackColor = true;
            this.btnManageProfile.Click += new System.EventHandler(this.btnManageProfile_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLogout.Location = new System.Drawing.Point(325, 340);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(250, 50);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // PlayerDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnViewTeam);
            this.Controls.Add(this.btnViewEvents);
            this.Controls.Add(this.btnManageProfile);
            this.Controls.Add(this.btnLogout);
            this.Name = "PlayerDashboard";
            this.Text = "Player Dashboard";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
