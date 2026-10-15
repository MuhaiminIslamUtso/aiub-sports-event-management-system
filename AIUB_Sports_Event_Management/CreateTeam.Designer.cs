using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class CreateTeam : Form
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnBack;
        private Button btnCreateTeam; // Create Team button
        private Label lblTitle;
        private TextBox txtTeamName;
        private TextBox txtSportName;
        private TextBox txtCoachName;
        private NumericUpDown numPlayerCount; // For entering the initial player count

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (UserSession.Role == "Admin") new AdminDashboard().Show();
            else if (UserSession.Role == "Coach") new CoachDashboard().Show();
            else new PlayerDashboard().Show();
        }

        private void btnCreateTeam_Click(object sender, EventArgs e)
        {
            // Check if all fields are filled
            if (string.IsNullOrEmpty(txtTeamName.Text) || string.IsNullOrEmpty(txtSportName.Text) || string.IsNullOrEmpty(txtCoachName.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            // Insert the new team into the database
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    string query = "INSERT INTO Teams (TeamName, SportName, CoachName, PlayerCount) VALUES (@TeamName, @SportName, @CoachName, @PlayerCount)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TeamName", txtTeamName.Text);
                    cmd.Parameters.AddWithValue("@SportName", txtSportName.Text);
                    cmd.Parameters.AddWithValue("@CoachName", txtCoachName.Text);
                    cmd.Parameters.AddWithValue("@PlayerCount", numPlayerCount.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Team created successfully.");
                    this.Hide();
                    new Teams().Show(); // Navigate to Teams view
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating team: " + ex.Message);
            }
        }

        // Custom implementation of placeholder text for TextBox
        private void SetPlaceholderText(TextBox textBox, string placeholder)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = placeholder;
        }

        private void RemovePlaceholderText(TextBox textBox, string placeholder)
        {
            if (textBox.Text == placeholder)
            {
                textBox.Text = "";
                textBox.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTeamName_Enter(object sender, EventArgs e)
        {
            RemovePlaceholderText(txtTeamName, "Enter Team Name");
        }

        private void txtTeamName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTeamName.Text))
            {
                SetPlaceholderText(txtTeamName, "Enter Team Name");
            }
        }

        private void txtSportName_Enter(object sender, EventArgs e)
        {
            RemovePlaceholderText(txtSportName, "Enter Sport Name");
        }

        private void txtSportName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSportName.Text))
            {
                SetPlaceholderText(txtSportName, "Enter Sport Name");
            }
        }

        private void txtCoachName_Enter(object sender, EventArgs e)
        {
            RemovePlaceholderText(txtCoachName, "Enter Coach Name");
        }

        private void txtCoachName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCoachName.Text))
            {
                SetPlaceholderText(txtCoachName, "Enter Coach Name");
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
            this.components = new System.ComponentModel.Container();
            this.btnBack = new Button();
            this.btnCreateTeam = new Button(); // Create Team button
            this.lblTitle = new Label();
            this.txtTeamName = new TextBox();
            this.txtSportName = new TextBox();
            this.txtCoachName = new TextBox();
            this.numPlayerCount = new NumericUpDown(); // Numeric input for Player Count

            ((System.ComponentModel.ISupportInitialize)(this.numPlayerCount)).BeginInit();
            this.SuspendLayout();

            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(20, 20);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 30);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // 
            // btnCreateTeam
            // 
            this.btnCreateTeam.Location = new System.Drawing.Point(325, 350);
            this.btnCreateTeam.Name = "btnCreateTeam";
            this.btnCreateTeam.Size = new System.Drawing.Size(150, 40);
            this.btnCreateTeam.TabIndex = 1;
            this.btnCreateTeam.Text = "Create Team";
            this.btnCreateTeam.UseVisualStyleBackColor = true;
            this.btnCreateTeam.Click += new EventHandler(this.btnCreateTeam_Click);

            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(150, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 40);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Announce a Team for Player and Coach to Join";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // txtTeamName
            // 
            this.txtTeamName.Location = new System.Drawing.Point(325, 80);
            this.txtTeamName.Name = "txtTeamName";
            this.txtTeamName.Size = new System.Drawing.Size(150, 30);
            this.txtTeamName.TabIndex = 3;
            this.txtTeamName.Text = "Enter Team Name"; // Placeholder text
            this.txtTeamName.ForeColor = System.Drawing.Color.Gray;
            this.txtTeamName.Enter += new EventHandler(this.txtTeamName_Enter);
            this.txtTeamName.Leave += new EventHandler(this.txtTeamName_Leave);

            // 
            // txtSportName
            // 
            this.txtSportName.Location = new System.Drawing.Point(325, 130);
            this.txtSportName.Name = "txtSportName";
            this.txtSportName.Size = new System.Drawing.Size(150, 30);
            this.txtSportName.TabIndex = 4;
            this.txtSportName.Text = "Enter Sport Name"; // Placeholder text
            this.txtSportName.ForeColor = System.Drawing.Color.Gray;
            this.txtSportName.Enter += new EventHandler(this.txtSportName_Enter);
            this.txtSportName.Leave += new EventHandler(this.txtSportName_Leave);

            // 
            // txtCoachName
            // 
            this.txtCoachName.Location = new System.Drawing.Point(325, 180);
            this.txtCoachName.Name = "txtCoachName";
            this.txtCoachName.Size = new System.Drawing.Size(150, 30);
            this.txtCoachName.TabIndex = 5;
            this.txtCoachName.Text = "Enter Coach Name"; // Placeholder text
            this.txtCoachName.ForeColor = System.Drawing.Color.Gray;
            this.txtCoachName.Enter += new EventHandler(this.txtCoachName_Enter);
            this.txtCoachName.Leave += new EventHandler(this.txtCoachName_Leave);

            // 
            // numPlayerCount
            // 
            this.numPlayerCount.Location = new System.Drawing.Point(325, 230);
            this.numPlayerCount.Name = "numPlayerCount";
            this.numPlayerCount.Size = new System.Drawing.Size(150, 30);
            this.numPlayerCount.TabIndex = 6;
            this.numPlayerCount.Minimum = 1;
            this.numPlayerCount.Maximum = 50;
            this.numPlayerCount.Value = 1;

            // 
            // CreateTeam Form
            // 
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnCreateTeam);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtTeamName);
            this.Controls.Add(this.txtSportName);
            this.Controls.Add(this.txtCoachName);
            this.Controls.Add(this.numPlayerCount);
            this.Name = "CreateTeam";
            this.Text = "Create Team";
            this.ResumeLayout(false);
            this.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.numPlayerCount)).EndInit();
        }

        #endregion
    }
}
