using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIUBSportsEventManager
{
    public partial class CreateEvent : Form
    {

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrEmpty(txtEventName.Text) || string.IsNullOrEmpty(txtLocation.Text) || dtpEventTime.Value == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            try
            {
                // Get data from form fields
                string eventName = txtEventName.Text;
                string location = txtLocation.Text;
                DateTime eventTime = dtpEventTime.Value;
                string createdBy = UserSession.Username; // Automatically set CreatedBy to the logged-in user's username

                // SQL query to insert event into the database
                using (SqlConnection conn = Database.GetConnection())
                {
                    string query = "INSERT INTO Events (EventName, Location, EventTime, CreatedBy) VALUES (@EventName, @Location, @EventTime, @CreatedBy)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@EventName", eventName);
                    cmd.Parameters.AddWithValue("@Location", location);
                    cmd.Parameters.AddWithValue("@EventTime", eventTime);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Event created successfully.");
                    NavigateToEventsPage(); // Navigate to Events page after successful creation
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating event: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigateToEventsPage(); // Navigate to Events page if canceled
        }

        private void NavigateToEventsPage()
        {
            // Navigate back to the Events page
            this.Hide();
            Events eventsPage = new Events(); // Assuming Events is your main form for managing events
            eventsPage.Show();
        }

        #region Designer Code

        private Label lblEventName;
        private TextBox txtEventName;
        private Label lblLocation;
        private TextBox txtLocation;
        private Label lblEventTime;
        private DateTimePicker dtpEventTime;
        private Button btnCreate;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.lblEventName = new Label();
            this.txtEventName = new TextBox();
            this.lblLocation = new Label();
            this.txtLocation = new TextBox();
            this.lblEventTime = new Label();
            this.dtpEventTime = new DateTimePicker();
            this.btnCreate = new Button();
            this.btnCancel = new Button();

            // Form properties
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Text = "Create Event";

            // Event Name Label
            this.lblEventName.Text = "Event Name:";
            this.lblEventName.Location = new System.Drawing.Point(50, 50);
            this.lblEventName.Size = new System.Drawing.Size(120, 25);

            // Event Name TextBox
            this.txtEventName.Location = new System.Drawing.Point(200, 50);
            this.txtEventName.Size = new System.Drawing.Size(600, 25);

            // Location Label
            this.lblLocation.Text = "Location:";
            this.lblLocation.Location = new System.Drawing.Point(50, 100);
            this.lblLocation.Size = new System.Drawing.Size(120, 25);

            // Location TextBox
            this.txtLocation.Location = new System.Drawing.Point(200, 100);
            this.txtLocation.Size = new System.Drawing.Size(600, 25);

            // Event Time Label
            this.lblEventTime.Text = "Event Time:";
            this.lblEventTime.Location = new System.Drawing.Point(50, 150);
            this.lblEventTime.Size = new System.Drawing.Size(120, 25);

            // Event Time DateTimePicker
            this.dtpEventTime.Location = new System.Drawing.Point(200, 150);
            this.dtpEventTime.Size = new System.Drawing.Size(600, 25);

            // Create Button
            this.btnCreate.Text = "Create Event";
            this.btnCreate.Location = new System.Drawing.Point(200, 300);
            this.btnCreate.Size = new System.Drawing.Size(200, 40);
            this.btnCreate.Click += new EventHandler(this.btnCreate_Click);

            // Cancel Button
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new System.Drawing.Point(450, 300);
            this.btnCancel.Size = new System.Drawing.Size(200, 40);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // Add controls to form
            this.Controls.Add(this.lblEventName);
            this.Controls.Add(this.txtEventName);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lblEventTime);
            this.Controls.Add(this.dtpEventTime);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnCancel);
        }

        #endregion
    }
}
