using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace DCproj
{
    class Excel
    {
        public string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;
        private readonly int col = 6;

        public Excel(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[sheet];
        }

        public string ReadCell(int i, int j)
        {
            i++;
            j++;
            if (ws.Cells[i, j].Value2 != null)
                return ws.Cells[i, j].Value2;
            else
                return "";
        }

        public int NumOfRows()
        {
            int i = 0;
            bool end = false;
            if (ws.Cells[1, 1].Value2 == null)
                return 0;

            while (end != true)
            {
                i++;
                if (ws.Cells[i, 1].Value2 == null && ws.Cells[i+1, 1].Value2 == null)
                {
                    end = true;
                }
            }
            return i;
        }

        public int NumOfHeaderRows()
        {
            int i = 0;
            bool end = false;
            if (ws.Cells[1, 1].Value2 == null)
                return 0;

            while (end != true)
            {
                i++;
                if (ws.Cells[i, 1].Value2 == "Products")
                {
                    i++;
                    end = true;
                }
            }
            return i;
        }

        public string[,] ReadRange(int starti, int startj, int endi, int endj)
        {
            Range range = (Range)ws.Range[ws.Cells[starti, startj], ws.Cells[endi, endj]];
            object[,] holder = range.Value2;
            string[,] returnString = new string[endi - starti, endj - startj];
            for(int i=1; i<=endi-starti; i++)
            {
                for (int j = 1; j <= endj - startj; j++)
                {
                    if (holder[i, j] != null)
                        returnString[i - 1, j - 1] = holder[i, j].ToString();
                    else
                        returnString[i - 1, j - 1] = "";
                }
            }
            return returnString;
        }


        public void WriteRange(int starti, int startj, int endi, int endj, string[,] writeString)
        {
            Range range = (Range)ws.Range[ws.Cells[starti, startj], ws.Cells[endi, endj]];
            range.Value2 = writeString;
        }

        public void WriteCell(int i, int j, string value)
        {
            i++;
            j++;
            ws.Cells[i, j].Value2 = value;
        }

        public void Save()
        {
            wb.Save();
        }

        public void SaveAs(string path)
        {
            wb.SaveAs(path);
        }

        public void Close()
        {
            wb.Close();
        }
    }
}
