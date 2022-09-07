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
using Excel = Microsoft.Office.Interop.Excel;

namespace NavaniePridumauPotom
{
    public partial class MainTable : Form
    {
        string ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SD_PROC.accdb";
        OleDbConnection ConnectBD;
        public string UserId;
        public string UserDep;
        public MainTable()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddProc ap = new AddProc();
            ap.UserId = UserId;
            ap.Owner = this;
            ap.ShowDialog();
        }

        private void MainTable_Load(object sender, EventArgs e)
        {
            this.Owner.Visible = false;
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sD_PROCDataSet.SD_PROC". При необходимости она может быть перемещена или удалена.
            this.sD_PROCTableAdapter1.Fill(this.sD_PROCDataSet1.SD_PROC);
            DGVUpdate();
        }

        private void MainTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1 && dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
            {
                GenAgreement ga = new GenAgreement();
                switch (e.ColumnIndex)
                {
                    case 4:
                        ga.key = "ALL";
                        break;
                    case 5:
                        ga.key = "I";
                        ga.idMain = dataGridView1[dataGridView1.ColumnCount - 3, e.RowIndex].Value.ToString();
                        break;
                    case 6:
                        ga.key = "11";
                        break;
                    case 7:
                        ga.key = "21";
                        break;
                    case 8:
                        ga.key = "22";
                        break;
                    case 9:
                        ga.key = "23";
                        break;
                    case 10:
                        ga.key = "24";
                        break;
                    case 11:
                        ga.key = "31";
                        break;
                    case 12:
                        ga.key = "41";
                        break;
                    case 13:
                        ga.key = "42";
                        break;
                    case 14:
                        ga.key = "51";
                        break;
                    case 15:
                        ga.key = "HP";
                        break;
                    case 16:
                        ga.key = "MK";
                        break;
                    case 17:
                        ga.key = "TK";
                        break;
                    case 18:
                        ga.key = "NK";
                        break;
                    case 19:
                        ga.key = "OGK";
                        break;
                }
                ga.idUser = UserId;
                if (UserDep!=ga.key)
                    ga.sogl = true;
                ga.fileName = dataGridView1[3, e.RowIndex].Value.ToString();
                ga.fileDesig = dataGridView1[0, e.RowIndex].Value.ToString();
                ga.idProc = dataGridView1[dataGridView1.ColumnCount - 1, e.RowIndex].Value.ToString();
                ga.ShowDialog();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DGVUpdate();
        }
        public void DGVUpdate()
        {
            sD_PROCDataSet1.SD_PROC.Clear();
            sD_PROCTableAdapter1.Fill(sD_PROCDataSet1.SD_PROC);
            ConnectBD = new OleDbConnection(ConStr);
            ConnectBD.Open();
            for (int i = dataGridView1.Rows.Count - 1; i > -1; i--)
            {
                if (dataGridView1[dataGridView1.Columns.Count - 2, i].Value.ToString() == "True")
                {
                    dataGridView1.Rows.RemoveAt(i);
                    continue;
                }
                string ComStr =
                    "SELECT SD_APPR_REQ.ID_DEP, " +
                    "CStr(EXISTS( " +
                    "SELECT SD_APPROVED_ID " +
                    "FROM SD_APPROVED " +
                    "WHERE(SD_APPROVED.ID_SD_APPR_REQ = SD_APPR_REQ.SD_APPR_REQ_ID) AND((SD_APPROVED.PR_A) = False))) " +
                    "AS[APPROVED], " +
                    "CStr(EXISTS( " +
                    "SELECT SD_APPROVED_ID " +
                    "FROM SD_APPROVED " +
                    "WHERE(SD_APPROVED.ID_SD_APPR_REQ = SD_APPR_REQ.SD_APPR_REQ_ID) AND((SD_APPROVED.PR_A) = False) AND(SD_APPROVED.ID_USER = SD_APPR_REQ.ID_USER))) " +
                    "AS[APPROVED_ACCORDANCE], " +
                    "CStr(EXISTS( " +
                    "SELECT SD_REMARK_ID " +
                    "FROM SD_APPROVED INNER JOIN SD_REMARK ON SD_APPROVED.ID_USER = SD_REMARK.ID_USER " +
                    "WHERE(SD_APPROVED.ID_SD_APPR_REQ = SD_APPR_REQ.SD_APPR_REQ_ID) AND((SD_APPROVED.PR_A) = False) AND((SD_REMARK.PR_A) = False) AND ((SD_REMARK.ID_SD_PROC)=\"" + dataGridView1[dataGridView1.Columns.Count - 1, i].Value.ToString() + "\"))) " +
                    "AS[REMARK] " +
                    "FROM SD_PROC INNER JOIN SD_APPR_REQ ON SD_PROC.SD_PROC_ID = SD_APPR_REQ.ID_SD_PROC " +
                    "WHERE(((SD_PROC.SD_PROC_ID) = \"" + dataGridView1[dataGridView1.Columns.Count - 1, i].Value.ToString() + "\") AND((SD_APPR_REQ.PR_A) = False)); ";
                OleDbCommand command = new OleDbCommand(ComStr, ConnectBD);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int? n = null;
                    switch (reader[0].ToString())
                    {
                        case "11":
                            dataGridView1[6, i].Value = "11";
                            n = 6;
                            break;
                        case "21":
                            dataGridView1[7, i].Value = "21";
                            n = 7;
                            break;
                        case "22":
                            dataGridView1[8, i].Value = "22";
                            n = 8;
                            break;
                        case "23":
                            dataGridView1[9, i].Value = "23";
                            n = 9;
                            break;
                        case "24":
                            dataGridView1[10, i].Value = "24";
                            n = 10;
                            break;
                        case "31":
                            dataGridView1[11, i].Value = "31";
                            n = 11;
                            break;
                        case "41":
                            dataGridView1[12, i].Value = "41";
                            n = 12;
                            break;
                        case "42":
                            dataGridView1[13, i].Value = "42";
                            n = 13;
                            break;
                        case "51":
                            dataGridView1[14, i].Value = "51";
                            n = 14;
                            break;
                        case "HP":
                            dataGridView1[15, i].Value = "Хозяева помещения";
                            n = 15;
                            break;
                        case "MK":
                            dataGridView1[16, i].Value = "Мат. контроль";
                            n = 16;
                            break;
                        case "TK":
                            dataGridView1[17, i].Value = "Тех. контроль";
                            n = 17;
                            break;
                        case "NK":
                            dataGridView1[18, i].Value = "Нормоконтроль";
                            n = 18;
                            break;
                        case "OGK":
                            dataGridView1[19, i].Value = "ОГК";
                            n = 19;
                            break;
                    }
                    if (n != null)
                    {
                        string[] strr = { reader[1].ToString() , reader[2].ToString() , reader[3].ToString() };
                        dataGridView1[(int)n, i].Style.BackColor = Color.Gray;
                        if (reader[1].ToString() != "0" )
                            dataGridView1[(int)n, i].Style.BackColor = Color.Blue;
                        if (reader[2].ToString() != "0")
                            dataGridView1[(int)n, i].Style.BackColor = Color.Green;
                        if (reader[3].ToString() != "0")
                            dataGridView1[(int)n, i].Style.BackColor = Color.Red;
                    }
                }
                string [] str = sD_PROCDataSet1.SD_PROC.Rows[i][3].ToString().Split('§');
                dataGridView1[0, i].Value = str[0];
                dataGridView1[1, i].Value = sD_PROCDataSet1.SD_PROC.Rows[i][1].ToString();
                dataGridView1[3, i].Value = str[1];
                dataGridView1[4, i].Value = "Все";
                switch (sD_PROCDataSet1.SD_PROC.Rows[i][4].ToString())
                {
                    case "0":
                        dataGridView1[4, i].Style.BackColor = Color.Red;
                        break;
                    case "1":
                        dataGridView1[4, i].Style.BackColor = Color.Gray;
                        break;
                    case "2":
                        dataGridView1[4, i].Style.BackColor = Color.Yellow;
                        break;
                    case "3":
                        dataGridView1[4, i].Style.BackColor = Color.Blue;
                        break;
                    case "4":
                        dataGridView1[4, i].Style.BackColor = Color.Green;
                        break;
                }
                dataGridView1[5, i].Value = "Согласующий";
                dataGridView1[5, i].Style.BackColor = Color.DarkGreen;
                dataGridView1[dataGridView1.ColumnCount - 5, i].Value = sD_PROCDataSet1.SD_PROC.Rows[i][6].ToString();
                dataGridView1[dataGridView1.ColumnCount - 4, i].Value = "Печать";
                
            }
            ConnectBD.Close();


            if (дляСогласованияСоМнойToolStripMenuItem.Checked)
            {
                int n=6;
                switch (UserDep)
                {
                    case "11":
                        n = 6;
                        break;
                    case "21":
                        n = 7;
                        break;
                    case "22":
                        n = 8;
                        break;
                    case "23":
                        n = 9;
                        break;
                    case "24":
                        n = 10;
                        break;
                    case "31":
                        n = 11;
                        break;
                    case "41":
                        n = 12;
                        break;
                    case "42":
                        n = 13;
                        break;
                    case "51":
                        n = 14;
                        break;
                    case "HP":
                        n = 15;
                        break;
                    case "MK":
                        n = 16;
                        break;
                    case "TK":
                        n = 17;
                        break;
                    case "NK":
                        n = 18;
                        break;
                    case "OGK":
                        n = 19;
                        break;
                }
                for (int i = dataGridView1.RowCount - 1; i > -1; i--)
                {
                    if(dataGridView1[n,i].Value==null)
                        dataGridView1.Rows.RemoveAt(i);
                }
            }
            if (поданныеМнойToolStripMenuItem.Checked)
            {
                for (int i = dataGridView1.RowCount - 1; i > -1; i--)
                {
                    if (dataGridView1[dataGridView1.ColumnCount - 3, i].Value.ToString() != UserId)
                        dataGridView1.Rows.RemoveAt(i);
                }
            }
            if (наСогласованииToolStripMenuItem.Checked)
            {
                for (int i = dataGridView1.RowCount - 1; i > -1; i--)
                {
                    if (dataGridView1[4, i].Style.BackColor != Color.Yellow)
                        dataGridView1.Rows.RemoveAt(i);
                }
            }
            if (вОтделеToolStripMenuItem.Checked)
            {
                for (int i = dataGridView1.RowCount - 1; i > -1; i--)
                {
                    if (dataGridView1[4, i].Style.BackColor != Color.Gray)
                        dataGridView1.Rows.RemoveAt(i);
                }
            }
        }

        private void дляСогласованияСоМнойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            дляСогласованияСоМнойToolStripMenuItem.Checked = !дляСогласованияСоМнойToolStripMenuItem.Checked;
        }

        private void поданныеМнойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            поданныеМнойToolStripMenuItem.Checked = !поданныеМнойToolStripMenuItem.Checked;
        }

        private void наСогласованииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            наСогласованииToolStripMenuItem.Checked = !наСогласованииToolStripMenuItem.Checked;
        }

        private void вОтделеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            вОтделеToolStripMenuItem.Checked = !вОтделеToolStripMenuItem.Checked;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ConnectBD = new OleDbConnection(ConStr);
            ConnectBD.Open();
            string ConStr2 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\USERS.accdb";
            OleDbConnection ConnectBD2;
            string ComStr2 = "SELECT USERS.Full_name FROM USERS WHERE USERS.ID = " + UserId + "; ";
            ConnectBD2 = new OleDbConnection(ConStr2);
            ConnectBD2.Open();
            OleDbCommand command2 = new OleDbCommand(ComStr2, ConnectBD2);
            OleDbDataReader reader2 = command2.ExecuteReader();
            reader2.Read();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++) {
                string ComStr = "UPDATE SD_PROC SET SD_PROC.PR_A = True, SD_PROC.USER_DATE = \""+ DateTime.Now.ToString() + ", " + reader2[0].ToString() + ", Удаление\" WHERE(([SD_PROC].[SD_PROC_ID] = \"" + dataGridView1.SelectedRows[i].Cells[dataGridView1.ColumnCount - 1].Value.ToString() + "\")); "; 
                OleDbCommand command = new OleDbCommand(ComStr, ConnectBD);
                OleDbDataReader reader = command.ExecuteReader();
            }
            ConnectBD.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook wBook;
            Excel.Worksheet xlSheet;
            wBook = xlApp.Workbooks.Add();
            xlApp.Columns.ColumnWidth = 20;
            xlSheet = (Excel.Worksheet)wBook.Sheets[1];
            xlSheet.Name = "Процедуры согласования";
            Color c = dataGridView1.Rows[0].Cells[0].Style.BackColor;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count-4; j++)
                {
                    if(dataGridView1.Rows[i].Cells[j].Value!=null)
                    xlApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    if (dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.Red||dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.Blue||dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.Gray||dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.Green||dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.Yellow)
                        xlApp.Cells[i + 2, j + 1].Interior.Color = dataGridView1.Rows[i].Cells[j].Style.BackColor;
                }
            }
            xlApp.Visible = true;
        }
    }
}
