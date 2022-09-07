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
    public partial class AddProc : Form
    {
        string ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SD_PROC.accdb";
        OleDbConnection ConnectBD;
        string ConStr2 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\USERS.accdb";
        OleDbConnection ConnectBD2;
        public string UserId;
        public AddProc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectBD = new OleDbConnection(ConStr);
            ConnectBD.Open();
            ConnectBD2 = new OleDbConnection(ConStr2);
            ConnectBD2.Open();
            string ComStr = "SELECT (Count(*)+1) AS C FROM SD_PROC;";
            string ComStr2 = "SELECT USERS.Full_name FROM USERS WHERE USERS.ID = "+UserId+"; ";
            OleDbCommand command = new OleDbCommand(ComStr, ConnectBD);
            OleDbCommand command2 = new OleDbCommand(ComStr2, ConnectBD2);
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            OleDbDataReader reader2 = command2.ExecuteReader();
            reader2.Read();
            string str = reader[0].ToString();
            ComStr = "INSERT INTO SD_PROC VALUES(\"" + str + "\", CDate(\"" + DateTime.Now.ToString() + "\"), \""+UserId+"\", \""+textBox1.Text+ "§"+textBox2.Text+ "§"+textBox3.Text + "\", \"1\", False, \""+textBox4.Text+"\", \"" + DateTime.Now.ToString() + ", " + reader2[0].ToString() + ", Создание процедуры\"); ";
            command = new OleDbCommand(ComStr, ConnectBD);
            command.ExecuteReader();
            ComStr = "SELECT (Count(*)+1) AS C FROM SD_APPR_REQ;";
            command = new OleDbCommand(ComStr, ConnectBD); 
            reader = command.ExecuteReader();
            reader.Read();
            int i = int.Parse(reader[0].ToString());
            if (checkBox1.Checked)
            {
                AddAppr(i,"11",str);
                i++;
            }
            if (checkBox2.Checked)
            {
                AddAppr(i, "21", str);
                i++;
            }
            if (checkBox3.Checked)
            {
                AddAppr(i, "22", str);
                i++;
            }
            if (checkBox4.Checked)
            {
                AddAppr(i, "23", str);
                i++;
            }
            if (checkBox5.Checked)
            {
                AddAppr(i, "24", str);
                i++;
            }
            if (checkBox6.Checked)
            {
                AddAppr(i, "31", str);
                i++;
            }
            if (checkBox7.Checked)
            {
                AddAppr(i, "41", str);
                i++;
            }
            if (checkBox8.Checked)
            {
                AddAppr(i, "42", str);
                i++;
            }
            if (checkBox9.Checked)
            {
                AddAppr(i, "51", str);
                i++;
            }
            AddAppr(i, "HP", str);
            i++;
            AddAppr(i, "MK", str);
            i++;
            AddAppr(i, "TK", str);
            i++;
            AddAppr(i, "NK", str);
            i++;
            AddAppr(i, "OGK", str);
            ConnectBD.Close();
            ConnectBD2.Close();
            MainTable mt = this.Owner as MainTable; 
            if (mt != null)
            {
                mt.DGVUpdate();
            }
        }
        private void AddAppr(int i, string dep, string idProc)
        {
            ConnectBD = new OleDbConnection(ConStr);
            ConnectBD.Open();
            ConnectBD2 = new OleDbConnection(ConStr2);
            ConnectBD2.Open();
            string ComStr2 = "SELECT USERS.ID FROM USERS WHERE(((USERS.Job_title) = \"Начальник отдела\") AND((USERS.Department) = \""+dep+"\")); ";
            OleDbCommand command2 = new OleDbCommand(ComStr2, ConnectBD2);
            OleDbDataReader reader2 = command2.ExecuteReader();
            reader2.Read();
            string ComStr = "INSERT INTO SD_APPR_REQ VALUES(\"" + i.ToString() + "\",\"" + idProc + "\",\"" + reader2[0].ToString() + "\",\"" + dep + "\",\"\",False,\"\",\"" + DateTime.Now.ToString() + ", " + reader2[0].ToString() + ", Создание процедуры\"); ";
            OleDbCommand  command = new OleDbCommand(ComStr, ConnectBD);
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            ConnectBD.Close();
            ConnectBD2.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text)||String.IsNullOrWhiteSpace(textBox2.Text)||String.IsNullOrWhiteSpace(textBox3.Text))
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox3.Text = openFileDialog1.FileName;
        }
    }
}
