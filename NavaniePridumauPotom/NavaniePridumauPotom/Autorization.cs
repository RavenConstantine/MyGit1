using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavaniePridumauPotom
{
    public partial class Autorization : Form
    {

        string ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\USERS.accdb";
        OleDbConnection ConnectBD;
        public Autorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text)||String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Введите логин и пароль!","Проверьте правильность ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                ConnectBD = new OleDbConnection(ConStr);
                ConnectBD.Open();
                string ComStr = "SELECT USERS.ID, USERS.Department, USERS.Login, USERS.Password FROM USERS WHERE(((USERS.Login) = \"" + textBox1.Text + "\") AND((USERS.Password) = \""+ textBox2.Text + "\")); ";
                OleDbCommand command = new OleDbCommand(ComStr, ConnectBD);
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    MainTable mt = new MainTable();
                    mt.Owner = this;
                    mt.UserId = reader[0].ToString(); 
                    mt.UserDep = reader[1].ToString(); 
                    mt.Show();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Проверьте правильность ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ConnectBD.Close();
            }
        }
    }
}
