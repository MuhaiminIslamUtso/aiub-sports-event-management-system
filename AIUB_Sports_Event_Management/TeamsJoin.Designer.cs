using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class TeamsJoin : Form
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnBack;
        private Label lblTitle;
        private DataGridView dgvTeams;
        private Button btnJoinTeam;

        private void TeamsJoin_Load(object sender, EventArgs e)
        {
            LoadTeams();
        }

        private void LoadTeams()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    string query = "SELECT TeamID, TeamName, SportName, CoachName, PlayerCount FROM Teams";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvTeams.DataSource = dt;

                    if (dgvTeams.Columns.Contains("TeamID"))
                        dgvTeams.Columns["TeamID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teams: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (UserSession.Role == "Coach") new CoachDashboard().Show();
            else if (UserSession.Role == "Player") new PlayerDashboard().Show();
        }

        private void btnJoinTeam_Click(object sender, EventArgs e)
        {
            if (dgvTeams.SelectedRows.Count > 0)
            {
                int teamId = Convert.ToInt32(dgvTeams.SelectedRows[0].Cells["TeamID"].Value);
                string teamName = dgvTeams.SelectedRows[0].Cells["TeamName"].Value.ToString();

                DialogResult result = MessageBox.Show($"Do you want to join '{teamName}' as a {UserSession.Role.ToLower()}?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = Database.GetConnection())
                        {
                            conn.Open();

                            if (UserSession.Role == "Coach")
                            {
                                string coachQuery = "UPDATE Teams SET CoachName = @CoachName WHERE TeamID = @TeamID";
                                SqlCommand cmd = new SqlCommand(coachQuery, conn);
                                cmd.Parameters.AddWithValue("@CoachName", UserSession.FullName);
                                cmd.Parameters.AddWithValue("@TeamID", teamId);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("You have successfully joined as the coach.");
                            }
                            else if (UserSession.Role == "Player")
                            {
                                string playerQuery = "UPDATE Teams SET PlayerCount = PlayerCount + 1 WHERE TeamID = @TeamID";
                                SqlCommand cmd = new SqlCommand(playerQuery, conn);
                                cmd.Parameters.AddWithValue("@TeamID", teamId);
                                cmd.ExecuteNonQuery();

                                // Optional: Add user's team name to Users table if you have a Team column
                                string updateUser = "UPDATE Users SET Team = @TeamName WHERE Username = @Username";
                                SqlCommand updateCmd = new SqlCommand(updateUser, conn);
                                updateCmd.Parameters.AddWithValue("@TeamName", teamName);
                                updateCmd.Parameters.AddWithValue("@Username", UserSession.Username);
                                updateCmd.ExecuteNonQuery();

                                MessageBox.Show("You have successfully joined the team.");
                            }
                        }

                        LoadTeams();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error joining team: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a team to join.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnBack = new Button();
            this.lblTitle = new Label();
            this.dgvTeams = new DataGridView();
            this.btnJoinTeam = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTeams)).BeginInit();
            this.SuspendLayout();

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(20, 20);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 30);
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(325, 20);
            this.lblTitle.Size = new System.Drawing.Size(150, 40);
            this.lblTitle.Text = "Join Teams";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // dgvTeams
            this.dgvTeams.Location = new System.Drawing.Point(50, 80);
            this.dgvTeams.Name = "dgvTeams";
            this.dgvTeams.Size = new System.Drawing.Size(700, 280);
            this.dgvTeams.ReadOnly = true;
            this.dgvTeams.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTeams.MultiSelect = false;
            this.dgvTeams.AllowUserToAddRows = false;
            this.dgvTeams.AllowUserToDeleteRows = false;
            this.dgvTeams.AllowUserToResizeRows = false;

            // btnJoinTeam
            this.btnJoinTeam.Location = new System.Drawing.Point(325, 380);
            this.btnJoinTeam.Name = "btnJoinTeam";
            this.btnJoinTeam.Size = new System.Drawing.Size(150, 40);
            this.btnJoinTeam.Text = "Join Team";
            this.btnJoinTeam.UseVisualStyleBackColor = true;
            this.btnJoinTeam.Click += new EventHandler(this.btnJoinTeam_Click);

            // TeamsJoin Form
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvTeams);
            this.Controls.Add(this.btnJoinTeam);
            this.Name = "TeamsJoin";
            this.Text = "TeamsJoin";
            this.Load += new EventHandler(this.TeamsJoin_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvTeams)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
