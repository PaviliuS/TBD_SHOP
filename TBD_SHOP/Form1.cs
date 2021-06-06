using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TBD_RES
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        private SqlCommand command = null;
        private void view(string str)
        {
            dataAdapter = new SqlDataAdapter(str, sqlConnection);
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            dataAdapter.Dispose();
            dataSet.Dispose();
        }
        private void procedure(string str)
        {
            command = new SqlCommand(str, sqlConnection);
            if (command.ExecuteNonQuery().ToString() == "-1")
            {
                MessageBox.Show("Ошибка!");
            }
            else
            {
                MessageBox.Show("Успешно!");
            }
            command.Dispose();
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SHOPDB"].ConnectionString);
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключение к БД установлено");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            procedure($"EXECUTE p_sales @new_product = N'{textBox3.Text}', @new_client = N'{textBox4.Text}', @new_extras = N'{textBox5.Text}', @new_employee = N'{textBox6.Text}';");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            procedure($"EXECUTE p_clients @new_name_1 = N'{textBox7.Text}', @new_name_2 = N'{textBox8.Text}', @new_name_3 = N'{textBox9.Text}', @new_phone = N'{textBox10.Text}', @new_passport = N'{textBox11.Text}', @new_position = N'{textBox12.Text}';");

        }
        private void button3_Click(object sender, EventArgs e)
        {
            procedure($"EXECUTE p_extras @new_name = N'{textBox13.Text}', @new_price = N'{textBox14.Text}';");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            view("select * from v_clients");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            view("select * from v_extras");
        }
        private void button6_Click(object sender, EventArgs e)
        {
            view("select * from v_employees");
        }
        private void button7_Click(object sender, EventArgs e)
        {
            view("select * from v_products");
        }
        private void button8_Click(object sender, EventArgs e)
        {
            view("select * from v_c_positions");
        }
        private void button9_Click(object sender, EventArgs e)
        {
            view("select * from v_sales");
        }
        private void button10_Click(object sender, EventArgs e)
        {
            view("select * from v_sum");
        }
    }
}