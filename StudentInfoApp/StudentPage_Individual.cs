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

        private void StudentPage_Individual_Load(object sender, EventArgs e)
        {
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        studentId, 
                        CONCAT(firstName, ' ', middleName, ' ', lastName) AS FullName, 
                        houseNo, 
                        brgyName, 
                        municipality, 
                        province, 
                        region, 
                        country, 
                        birthdate, 
                        age, 
                        studContactNo, 
                        emailAddress, 
                        guardianFirstName, 
                        guardianLastName, 
                        hobbies, 
                        nickname, 
                        courseId, 
                        yearId 
                    FROM StudentRecordTB 
                    WHERE studentId = @StudentID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", studentId);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        lblStudentID.Text = reader["studentId"].ToString();
                        lblFullName.Text = reader["FullName"].ToString();
                        txtHouseNo.Text = reader["houseNo"].ToString();
                        txtBrgyName.Text = reader["brgyName"].ToString();
                        txtMunicipality.Text = reader["municipality"].ToString();
                        txtProvince.Text = reader["province"].ToString();
                        txtRegion.Text = reader["region"].ToString();
                        txtCountry.Text = reader["country"].ToString();
                        txtBirthdate.Text = reader["birthdate"].ToString();
                        txtAge.Text = reader["age"].ToString();
                        txtStudContactNo.Text = reader["studContactNo"].ToString();
                        txtEmailAddress.Text = reader["emailAddress"].ToString();
                        txtGuardianFirstName.Text = reader["guardianFirstName"].ToString();
                        txtGuardianLastName.Text = reader["guardianLastName"].ToString();
                        txtHobbies.Text = reader["hobbies"].ToString();
                        txtNickname.Text = reader["nickname"].ToString();
                        txtCourseId.Text = reader["courseId"].ToString();
                        txtYearId.Text = reader["yearId"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading data: " + ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtMunicipality_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProvince_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNickname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}