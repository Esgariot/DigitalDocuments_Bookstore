using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCproj
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //OpenFile();
            Excel excelA = new Excel(@"C:\Users\Tomasz\Desktop\studia\DC\A.csv", 1);
            Excel excelB = new Excel(@"C:\Users\Tomasz\Desktop\studia\DC\B.csv", 1);//dest
            int numOfHeaderRows = excelA.NumOfHeaderRows();
            int numOfRowsA = excelA.NumOfRows();
            int numOfRowsB = excelB.NumOfRows();
            string[,] readString = excelA.ReadRange(numOfHeaderRows+1, 1, numOfRowsA, 7);
            excelB.WriteRange(numOfRowsB, 1, numOfRowsA- (numOfHeaderRows + 1) + numOfRowsB - 1, 6, readString);
            excelB.SaveAs(@"C:\Users\Tomasz\Desktop\studia\DC\TestFiles\Test1.csv");
            excelA.Close();
            excelB.Close();

        }

        public void OpenFile()
        {
            Excel excel = new Excel(@"C:\Users\Tomasz\Desktop\studia\DC\Sample.xlsx", 1);

            MessageBox.Show(excel.ReadCell(0, 0));
        }


    }
}
