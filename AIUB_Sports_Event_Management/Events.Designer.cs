using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class Events : Form
    {
        private void Events_Load(object sender, EventArgs e)
        {
            LoadEvents();
        }

        private void LoadEvents()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    // SQL Query to get events along with the registered users count
                    string query = @"
                SELECT 
                    e.EventID, 
                    e.EventName, 
                    e.Location, 
                    e.EventTime, 
                    e.CreatedBy, 
                    e.CreatedAt, 
                    ISNULL(COUNT(er.Username), 0) AS RegisteredUsersCount
                FROM 
                    Events e
                LEFT JOIN 
                    EventRegistrations er ON e.EventID = er.EventID
                GROUP BY 
                    e.EventID, 
                    e.EventName, 
                    e.Location, 
                    e.EventTime, 
                    e.CreatedBy, 
                    e.CreatedAt
                ORDER BY 
                    e.EventTime";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind data to DataGridView
                    dgvEvents.DataSource = dt;

                    // Optionally, hide EventID if it's not needed
                    if (dgvEvents.Columns.Contains("EventID"))
                        dgvEvents.Columns["EventID"].Visible = false;  // Hide internal ID
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading events: " + ex.Message);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); 
            new AdminDashboard().Show(); 
        }

        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count > 0)
            {
                int eventId = Convert.ToInt32(dgvEvents.SelectedRows[0].Cells["EventID"].Value);
                var confirm = MessageBox.Show("Delete selected event?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = Database.GetConnection())
                        {
                            string query = "DELETE FROM Events WHERE EventID = @id";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id", eventId);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Event deleted.");
                            LoadEvents();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting event: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Select an event to delete.");
            }
        }

        private void btnCreateEvent_Click(object sender, EventArgs e)
        {
            this.Hide();
            new CreateEvent().Show();
        }

        #region Designer

        private Label lblTitle;
        private Button btnBack;
        private Button btnCreateEvent;
        private DataGridView dgvEvents;
        private Button btnDeleteEvent;

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.btnBack = new Button();
            this.btnCreateEvent = new Button();
            this.dgvEvents = new DataGridView();
            this.btnDeleteEvent = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            this.SuspendLayout();

            // Title Label
            this.lblTitle.Text = "Manage Events";
            this.lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkBlue;
            this.lblTitle.Location = new Point(250, 10);
            this.lblTitle.Size = new Size(400, 40);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Back Button
            this.btnBack.Text = "Back";
            this.btnBack.Location = new Point(20, 15);
            this.btnBack.Size = new Size(80, 30);
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // Create Event Button
            this.btnCreateEvent.Text = "Create Event";
            this.btnCreateEvent.Font = new Font("Segoe UI", 11F);
            this.btnCreateEvent.Location = new Point(50, 70);
            this.btnCreateEvent.Size = new Size(150, 40);
            this.btnCreateEvent.Click += new EventHandler(this.btnCreateEvent_Click);

            // Delete Event Button
            this.btnDeleteEvent.Text = "Delete Selected Event";
            this.btnDeleteEvent.Font = new Font("Segoe UI", 11F);
            this.btnDeleteEvent.Location = new Point(220, 70);
            this.btnDeleteEvent.Size = new Size(200, 40);
            this.btnDeleteEvent.Click += new EventHandler(this.btnDeleteEvent_Click);

            // DataGridView
            this.dgvEvents.Location = new Point(50, 130);
            this.dgvEvents.Size = new Size(800, 400);
            this.dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvEvents.MultiSelect = false;
            this.dgvEvents.ReadOnly = true;
            this.dgvEvents.AllowUserToAddRows = false;
            this.dgvEvents.AllowUserToDeleteRows = false;
            this.dgvEvents.AllowUserToResizeRows = false;

            // Events Form
            this.ClientSize = new Size(900, 600);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnCreateEvent);
            this.Controls.Add(this.btnDeleteEvent);
            this.Controls.Add(this.dgvEvents);
            this.Text = "Events";
            this.Load += new EventHandler(this.Events_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
