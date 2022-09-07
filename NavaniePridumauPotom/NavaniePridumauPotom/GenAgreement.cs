using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavaniePridumauPotom
{
    public partial class GenAgreement : Form
    {
        public GenAgreement()
        {
            InitializeComponent();
        }
        public string key;
        public string idUser;
        public string idMain;
        public string fileName;
        public string fileDesig;
        public string idProc;
        public bool sogl = false;

        private void GenAgreement_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "uSERSDataSet.USERS". При необходимости она может быть перемещена или удалена.
            this.uSERSTableAdapter.Fill(this.uSERSDataSet.USERS);
            for (int i = dataGridView1.RowCount-1; i > -1 ; i--) {
                dataGridView1.Rows[i].Height = 100;
                dataGridView1[1, i].Value = Image.FromFile(dataGridView1[dataGridView1.ColumnCount - 1, i].Value.ToString());
                if (key == "I")
                {
                    if (idMain != dataGridView1[dataGridView1.ColumnCount - 2, i].Value.ToString())
                        dataGridView1.Rows.RemoveAt(i);
                }
                else if ((dataGridView1[0, i].Value.ToString() != key)&& key != "ALL")
                    dataGridView1.Rows.RemoveAt(i);
            }
            if (key == "ALL"|| key == "I" || sogl)
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Подтведите согласование файла "+fileDesig+"; "+fileName,"Подтверждение согласования", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SD_PROC.accdb";
                OleDbConnection ConnectBD;
                ConnectBD = new OleDbConnection(ConStr);
                ConnectBD.Open();
                string ComStr = "SELECT (Count(*)+1) AS C FROM SD_APPROVED;";
                OleDbCommand command = new OleDbCommand(ComStr, ConnectBD);
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                string str = reader[0].ToString();

                string ConStr2 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\USERS.accdb";
                OleDbConnection ConnectBD2;
                string ComStr2 = "SELECT USERS.Full_name FROM USERS WHERE USERS.ID = " + idUser+ "; ";
                ConnectBD2 = new OleDbConnection(ConStr2);
                ConnectBD2.Open();
                OleDbCommand command2 = new OleDbCommand(ComStr2, ConnectBD2);
                OleDbDataReader reader2 = command2.ExecuteReader();
                reader2.Read();

                ComStr = "SELECT SD_APPR_REQ.SD_APPR_REQ_ID " +
                "FROM SD_PROC INNER JOIN SD_APPR_REQ ON SD_PROC.SD_PROC_ID = SD_APPR_REQ.ID_SD_PROC " +
                "WHERE(((SD_PROC.SD_PROC_ID) = \""+idProc+"\") AND((SD_APPR_REQ.ID_DEP) = \""+key+"\")); ";
                command = new OleDbCommand(ComStr, ConnectBD);
                reader = command.ExecuteReader();
                reader.Read();
                string str2 = reader[0].ToString();

                ComStr = "INSERT INTO SD_APPROVED VALUES(\""+str+"\",\""+idUser+"\",\""+reader[0].ToString()+ "\",\"" + idProc + "\",False,\""+1+ "\",\"" + DateTime.Now.ToString() + ", " + reader2[0].ToString() + ", Согласование\"); ";
                command = new OleDbCommand(ComStr, ConnectBD);
                reader = command.ExecuteReader();
                ConnectBD.Close();
                ConnectBD2.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Подтведите замечание для файла " + fileDesig + "; " + fileName, "Подтверждение замечания", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SD_PROC.accdb";
                OleDbConnection ConnectBD;
                ConnectBD = new OleDbConnection(ConStr);
                ConnectBD.Open();
                string ComStr = "SELECT (Count(*)+1) AS C FROM SD_APPROVED;";
                OleDbCommand command = new OleDbCommand(ComStr, ConnectBD);
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                string str = reader[0].ToString();

                string ConStr2 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\USERS.accdb";
                OleDbConnection ConnectBD2;
                string ComStr2 = "SELECT USERS.Full_name FROM USERS WHERE USERS.ID = " + idUser + "; ";
                ConnectBD2 = new OleDbConnection(ConStr2);
                ConnectBD2.Open();
                OleDbCommand command2 = new OleDbCommand(ComStr2, ConnectBD2);
                OleDbDataReader reader2 = command2.ExecuteReader();
                reader2.Read();

                ComStr = "SELECT SD_APPR_REQ.SD_APPR_REQ_ID " +
                "FROM SD_PROC INNER JOIN SD_APPR_REQ ON SD_PROC.SD_PROC_ID = SD_APPR_REQ.ID_SD_PROC " +
                "WHERE(((SD_PROC.SD_PROC_ID) = \"" + idProc + "\") AND((SD_APPR_REQ.ID_DEP) = \"" + key + "\")); ";
                command = new OleDbCommand(ComStr, ConnectBD);
                reader = command.ExecuteReader();
                reader.Read();
                string str2 = reader[0].ToString();

                ComStr = "INSERT INTO SD_APPROVED VALUES(\"" + str + "\",\"" + idUser + "\",\"" + reader[0].ToString() + "\",\"" + idProc + "\",False,\"" + 1 + "\",\"" + DateTime.Now.ToString() + ", " + reader2[0].ToString() + ", Согласование\"); ";
                command = new OleDbCommand(ComStr, ConnectBD);
                reader = command.ExecuteReader();



                ComStr = "SELECT (Count(*)+1) AS C FROM SD_REMARK;";
                command = new OleDbCommand(ComStr, ConnectBD);
                reader = command.ExecuteReader();
                reader.Read();
                str = reader[0].ToString();

                ComStr2 = "SELECT USERS.Full_name FROM USERS WHERE USERS.ID = " + idUser + "; ";
                command2 = new OleDbCommand(ComStr2, ConnectBD2);
                reader2 = command2.ExecuteReader();
                reader2.Read();

                ComStr = "INSERT INTO SD_REMARK VALUES(\"" + str + "\",\"" + idProc + "\",\"" + idUser + "\",\"" + 1 + "\",False,\"" + 2 + "\",\"" + DateTime.Now.ToString() + ", " + reader2[0].ToString() + ", Согласование\"); ";
                command = new OleDbCommand(ComStr, ConnectBD);
                reader = command.ExecuteReader();
                ConnectBD.Close();
                ConnectBD2.Close();
            }
        }
    }
}
