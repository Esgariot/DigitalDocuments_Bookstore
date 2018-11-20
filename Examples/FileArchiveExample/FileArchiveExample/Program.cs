using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileArchiveExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToDatabaseFolder = @".\";
            string pathToFilesFolder = @"..\..\..\Tools\";

            FileStream xmlFile = File.Open(pathToFilesFolder + "PurchaseOrderTemplate.xml",
                System.IO.FileMode.Open);

            FileStream xpdlFile = File.Open(pathToFilesFolder + "XPDL.txt",
                System.IO.FileMode.Open);

            CustomerEntity customer = new CustomerEntity();
            customer.name = "k1";
            customer.mailAddress = "k1@gmail.com";

            string transactionId = "ABC8296_30742581";

            Archive archive = new Archive(pathToDatabaseFolder);
            archive.ArchiviseTransaction(transactionId, customer, xmlFile, xpdlFile);

            List<TransactionEntity> results = archive.GetMatchingArchivisedTransactions(transactionId, customer, pathToDatabaseFolder);
        }
    }
}
