using System;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    partial class Staff
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnManageInventory;
        private Button btnManageProfile;
        private Button btnLogout;
        private Label lblWelcomeStaff;

        private void btnManageInventory_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Inventory().Show(); // Navigate to Inventory management 
        }

        private void btnManageProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            new UpdateProfile().Show(); // Navigate to Profile management
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear the UserSession object and navigate to the login screen
            UserSession.Clear(); 
            MessageBox.Show("You have successfully logged out.");
            this.Hide();
            new Login().Show();
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
            this.btnManageInventory = new System.Windows.Forms.Button();
            this.btnManageProfile = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblWelcomeStaff = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // btnManageInventory
            // 
            this.btnManageInventory.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnManageInventory.Location = new System.Drawing.Point(325, 180);
            this.btnManageInventory.Name = "btnManageInventory";
            this.btnManageInventory.Size = new System.Drawing.Size(250, 50);
            this.btnManageInventory.TabIndex = 0;
            this.btnManageInventory.Text = "Manage Inventory";
            this.btnManageInventory.UseVisualStyleBackColor = true;
            this.btnManageInventory.Click += new System.EventHandler(this.btnManageInventory_Click);

            // 
            // btnManageProfile
            // 
            this.btnManageProfile.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnManageProfile.Location = new System.Drawing.Point(325, 260);
            this.btnManageProfile.Name = "btnManageProfile";
            this.btnManageProfile.Size = new System.Drawing.Size(250, 50);
            this.btnManageProfile.TabIndex = 1;
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
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // 
            // lblWelcomeStaff
            // 
            this.lblWelcomeStaff.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblWelcomeStaff.Location = new System.Drawing.Point(325, 50);
            this.lblWelcomeStaff.Name = "lblWelcomeStaff";
            this.lblWelcomeStaff.Size = new System.Drawing.Size(250, 30);
            this.lblWelcomeStaff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWelcomeStaff.Text = $"Welcome, {UserSession.FullName}"; // Display staff name from session

            // 
            // Staff
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.btnManageInventory);
            this.Controls.Add(this.btnManageProfile);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblWelcomeStaff);
            this.Name = "Staff";
            this.Text = "Staff Dashboard";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
