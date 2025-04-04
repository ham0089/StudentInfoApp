using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentInfoApp
{
    public partial class StudentPage : Form
    {
        private readonly string connectionString = "server=localhost;database=StudentInfoDB;user=root;password=;";

        public StudentPage()
        {
            InitializeComponent();
        }

        private void StudentPage_Load(object sender, EventArgs e)
        {
            LoadStudentRecords();
        }

        private void LoadStudentRecords()
        {
            try
            {
                DataTable studentRecords = GetStudentRecords();
                dgvStudents.DataSource = studentRecords;

                if (!dgvStudents.Columns.Contains("viewButton"))
                {
                    DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn
                    {
                        Name = "viewButton",
                        Text = "VIEW",
                        UseColumnTextForButtonValue = true
                    };
                    dgvStudents.Columns.Add(viewButton);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("A database error occurred while loading student records: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred while loading student records: " + ex.Message);
            }
        }

        private DataTable GetStudentRecords()
        {
            DataTable dt = new DataTable();
            string query = "SELECT studentId, CONCAT(firstName, ' ', lastName) AS FullName FROM StudentRecordTB";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvStudents.Columns["viewButton"].Index && e.RowIndex >= 0)
            {
                int studentId = Convert.ToInt32(dgvStudents.Rows[e.RowIndex].Cells["studentId"].Value);
                StudentPage_Individual studentPage = new StudentPage_Individual(studentId);
                studentPage.Show();
            }
        }
    }
}