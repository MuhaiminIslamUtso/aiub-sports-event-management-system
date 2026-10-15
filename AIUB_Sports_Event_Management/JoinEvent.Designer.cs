using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class JoinEvent : Form
    {
        private Button btnBack;
        private Button btnJoin;
        private DataGridView dgvEvents;
        private Label lblTitle;

        private void JoinEvent_Load(object sender, EventArgs e)
        {
            LoadAvailableEvents();
        }

        private void LoadAvailableEvents()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    string query = @"
                    SELECT 
                        e.EventID,
                        e.EventName,
                        e.Location,
                        e.EventTime,
                        e.CreatedBy,
                        e.CreatedAt,
                        ISNULL((SELECT COUNT(*) FROM EventRegistrations er WHERE er.EventID = e.EventID), 0) AS RegisteredUsersCount,
                        CASE 
                            WHEN EXISTS (
                                SELECT 1 FROM EventRegistrations er 
                                WHERE er.EventID = e.EventID AND er.Username = @Username
                            ) THEN 'Joined'
                            ELSE 'Not Joined'
                        END AS Status
                    FROM Events e
                    ORDER BY e.EventTime";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", UserSession.Username);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvEvents.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading events: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new PlayerDashboard().Show();
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count > 0)
            {
                string status = dgvEvents.SelectedRows[0].Cells["Status"].Value.ToString();
                if (status == "Joined")
                {
                    MessageBox.Show("You have already joined this event.");
                    return;
                }

                int eventId = Convert.ToInt32(dgvEvents.SelectedRows[0].Cells["EventID"].Value);
                DialogResult result = MessageBox.Show("Do you want to join this event?", "Confirm Join", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = Database.GetConnection())
                        {
                            conn.Open();
                            string insertQuery = "INSERT INTO EventRegistrations (Username, EventID) VALUES (@Username, @EventID)";
                            SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                            insertCmd.Parameters.AddWithValue("@Username", UserSession.Username);
                            insertCmd.Parameters.AddWithValue("@EventID", eventId);
                            insertCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("You have successfully joined the event.");
                        LoadAvailableEvents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error joining event: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an event to join.");
            }
        }

        private void InitializeComponent()
        {
            this.btnBack = new Button();
            this.btnJoin = new Button();
            this.dgvEvents = new DataGridView();
            this.lblTitle = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            this.SuspendLayout();

            // Back Button
            this.btnBack.Text = "Back";
            this.btnBack.Location = new System.Drawing.Point(20, 20);
            this.btnBack.Size = new System.Drawing.Size(75, 30);
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // Join Button
            this.btnJoin.Text = "Join Event";
            this.btnJoin.Location = new System.Drawing.Point(325, 350);
            this.btnJoin.Size = new System.Drawing.Size(150, 40);
            this.btnJoin.Click += new EventHandler(this.btnJoin_Click);

            // DataGridView for Events
            this.dgvEvents.Location = new System.Drawing.Point(50, 80);
            this.dgvEvents.Size = new System.Drawing.Size(700, 250);
            this.dgvEvents.ReadOnly = true;
            this.dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvEvents.MultiSelect = false;

            // Title Label
            this.lblTitle.Text = "Available Events to Join";
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(250, 20);
            this.lblTitle.Size = new Size(400, 40);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // JoinEvent Form
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.dgvEvents);
            this.Controls.Add(this.lblTitle);
            this.Text = "Join Event";
            this.Load += new EventHandler(this.JoinEvent_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
