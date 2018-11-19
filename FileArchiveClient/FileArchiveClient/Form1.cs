using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileArchiveClient
{
    public partial class Form1 : Form
    {
        Archive archive;
        const string searchingResultsSaveFolder = @".\Znalezione\";
        const string pathToDatabaseFolder = @"..\..\..\..\GUI\GUI_Prototype\bin\Debug\Archive\";

        public Form1()
        {
            InitializeComponent();

            archive = new Archive(pathToDatabaseFolder);
        }

        private void Search_Button_Click(object sender, EventArgs e)
        {
            this.Console_TextBox.Text = "";

            string customerName = this.CustomerData_Name_TextBox.Text;
            string customerMail = this.CustomerData_Mail_TextBox.Text;

            string transactionID = this.TransactionData_TransactionID_TextBox.Text;

            CustomerEntity customer = new CustomerEntity();
            customer.name = customerName;
            customer.mailAddress = customerMail;

            if (System.IO.Directory.Exists(searchingResultsSaveFolder))
                System.IO.Directory.Delete(searchingResultsSaveFolder, true);
            System.IO.Directory.CreateDirectory(searchingResultsSaveFolder);
            List<TransactionEntity> results = archive.GetMatchingArchivisedTransactions(transactionID, customer, searchingResultsSaveFolder);

            if (results == null)
                this.Console_TextBox.Text = "Nie znaleziono żadnej pasującej transakcji.";
            else
            {
                foreach (TransactionEntity result in results)
                {
                    this.Console_TextBox.Text += "Znaleziono pasującą transakcje: " + result.transactionId + Environment.NewLine;
                }
            }
        }

        private void OtworzFolder_Button_Click(object sender, EventArgs e)
        {
			if (!System.IO.Directory.Exists(searchingResultsSaveFolder))
				System.IO.Directory.CreateDirectory(searchingResultsSaveFolder);
            Process.Start("explorer.exe", searchingResultsSaveFolder);
        }
    }
}
