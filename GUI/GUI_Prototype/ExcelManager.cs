using System.IO;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace GUI_Prototype
{
    class ExcelManager
    {
        static void openfile()
        {
            string mySheet = Path.Combine(Utils.excelDirPath, Utils.excelFileName); //@"C:\Users\Tomasz\Desktop\studia\DC\dok1.xlsx";
            var excelApp = new Excel.Application();
            excelApp.Visible = true;

            Excel.Workbooks books = excelApp.Workbooks;

            Excel.Workbook sheet = books.Open(mySheet);
        }
        static void MergeExcels()
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            app.Workbooks.Add("");
            app.Workbooks.Add(@"C:\Users\Tomasz\Desktop\studia\DC\dok1.xlsx");
            app.Workbooks.Add(@"C:\Users\Tomasz\Desktop\studia\DC\dok2.xlsx");
            for (int i = 2; i <= app.Workbooks.Count; i++)
            {
                for (int j = 1; j <= app.Workbooks[i].Worksheets.Count; j++)
                {
                    Excel.Worksheet ws = app.Workbooks[i].Worksheets[j];
                    ws.Copy(app.Workbooks[1].Worksheets[1]);
                }
            }
        }
        static void Main(string[] args)
        {
            //openfile();
            MergeExcels();
        }
    }
}