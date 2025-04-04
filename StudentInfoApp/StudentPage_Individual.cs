using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentInfoApp
{
    public partial class StudentPage_Individual : Form
    {
        private readonly string connectionString = "server=localhost;database=StudentInfoDB;user=root;password=;";
        private int studentId;

        public StudentPage_Individual(int studentId)
        {
            InitializeComponent();
            this.studentId = studentId;
        }

        public StudentPage_Individual()
        {
        }

        private void StudentPage_Individual_Load(object sender, EventArgs e)
        {
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT StudentID, CONCAT(FirstName, ' ', MiddleName, ' ', LastName) AS FullName, Address, Age, Contact, Email FROM StudentRecordTB WHERE StudentID = @StudentID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", studentId);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtFirstName.Text = reader["FirstName"].ToString();
                        txtLastName.Text = reader["LastName"].ToString();
                        txtMiddleName.Text = reader["MiddleName"].ToString();
                        txtAge.Text = reader["Age"].ToString();
                        txtContact.Text = reader["Contact"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        lblStudentID.Text = reader["StudentID"].ToString();
                        lblFullName.Text = reader["FullName"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading data: " + ex.Message);
                }
            }
        }
    }
}
