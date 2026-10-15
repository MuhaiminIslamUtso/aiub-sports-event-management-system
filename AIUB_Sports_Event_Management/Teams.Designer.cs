using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class Teams : Form
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnBack;
        private Label lblTitle;
        private DataGridView dgvTeams;
        private Button btnDeleteTeam;
        private Button btnCreateTeam;

        private void Teams_Load(object sender, EventArgs e)
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
            new AdminDashboard().Show();
        }

        private void btnDeleteTeam_Click(object sender, EventArgs e)
        {
            if (dgvTeams.SelectedRows.Count > 0)
            {
                int teamId = Convert.ToInt32(dgvTeams.SelectedRows[0].Cells["TeamID"].Value);
                DialogResult result = MessageBox.Show("Are you sure you want to delete this team?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = Database.GetConnection())
                        {
                            conn.Open();
                            string query = "DELETE FROM Teams WHERE TeamID = @TeamID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@TeamID", teamId);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Team deleted successfully.");
                        LoadTeams();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting team: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a team to delete.");
            }
        }

        private void btnCreateTeam_Click(object sender, EventArgs e) 
        {
            this.Hide();
            new CreateTeam().Show(); 
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
            this.btnDeleteTeam = new Button();
            this.btnCreateTeam = new Button();

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
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 40);
            this.lblTitle.Text = "Teams";
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

            // btnDeleteTeam
            this.btnDeleteTeam.Location = new System.Drawing.Point(400, 380);
            this.btnDeleteTeam.Name = "btnDeleteTeam";
            this.btnDeleteTeam.Size = new System.Drawing.Size(150, 40);
            this.btnDeleteTeam.Text = "Delete Team";
            this.btnDeleteTeam.UseVisualStyleBackColor = true;
            this.btnDeleteTeam.Click += new EventHandler(this.btnDeleteTeam_Click);

            // btnCreateTeam
            this.btnCreateTeam.Location = new System.Drawing.Point(200, 380);
            this.btnCreateTeam.Name = "btnCreateTeam";
            this.btnCreateTeam.Size = new System.Drawing.Size(150, 40);
            this.btnCreateTeam.Text = "Create Team";
            this.btnCreateTeam.UseVisualStyleBackColor = true;
            this.btnCreateTeam.Click += new EventHandler(this.btnCreateTeam_Click);

            // Teams Form
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvTeams);
            this.Controls.Add(this.btnDeleteTeam);
            this.Controls.Add(this.btnCreateTeam);
            this.Name = "Teams";
            this.Text = "Teams";
            this.Load += new EventHandler(this.Teams_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvTeams)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
