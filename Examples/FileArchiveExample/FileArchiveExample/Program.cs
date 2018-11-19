using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiteDB;

namespace FileArchiveExample
{
    public class CustomerEntity
    {
        public string name { get; set; }
        public string mailAddress { get; set; }
    }

    public class TransactionEntity
    {
        public Guid id { get; set; }
        public string transactionId { get; set; }
        public CustomerEntity customer { get; set; }
        public string xmlFileName { get; set; }
        public string xpdlFileName { get; set; }
    }

    public class Archive
    {
        private LiteDatabase database;
        private string databasePath;
        private const string transactionsCollectionName = "transactionsEntityGroup";
        private const string xmlFilesFolderPath = "$/files/xmls";
        private const string xpdlFilesFolderPath = "$/files/xpdl";

        public Archive(string pathToDatabase, string databaseName = "BookshopArchive.db")
        {
            this.databasePath = pathToDatabase;
            this.database = new LiteDatabase(databaseName);
        }

        public void ArchiviseTransaction(string transactionId, CustomerEntity customerData, FileStream xmlFile, FileStream xpdlFile)
        {
            FileInfo xmlFileInfo = new FileInfo(xmlFile.Name);
            FileInfo xpdlFileInfo = new FileInfo(xpdlFile.Name);

            TransactionEntity transactionData = new TransactionEntity();
            transactionData.id = Guid.NewGuid();
            transactionData.transactionId = transactionId;
            transactionData.customer = customerData;
            transactionData.xmlFileName =  transactionData.id + "_" + xmlFileInfo.Name;
            transactionData.xpdlFileName = transactionData.id + "_" + xpdlFileInfo.Name;

            var transactions =
                this.database.GetCollection<TransactionEntity>(Archive.transactionsCollectionName);

            transactions.Insert(transactionData);
            this.database.FileStorage.Upload(Archive.xmlFilesFolderPath + "/" + transactionData.xmlFileName, 
                transactionData.xmlFileName, xmlFile);
            this.database.FileStorage.Upload(Archive.xpdlFilesFolderPath + "/" + transactionData.xpdlFileName, 
                transactionData.xpdlFileName, xpdlFile);
        }

        public TransactionEntity GetArchivisedTransaction(string transactionId, CustomerEntity customerData)
        {
            LiteCollection<TransactionEntity> transactions =
               this.database.GetCollection<TransactionEntity>(Archive.transactionsCollectionName);

            var results = transactions.Find(x =>
                (x.transactionId.Equals(transactionId) /*|| x.customer.Equals(customerData)*/)
            );

            TransactionEntity result = results.First();

            System.IO.Directory.CreateDirectory(this.databasePath + result.transactionId);

            LiteFileInfo xmlFile = this.database.FileStorage.FindById(Archive.xmlFilesFolderPath + "/" + result.xmlFileName);
            xmlFile.SaveAs(this.databasePath + result.transactionId + @"\" + result.xmlFileName);

            LiteFileInfo xpdlFile = this.database.FileStorage.FindById(Archive.xpdlFilesFolderPath + "/" + result.xpdlFileName);
            xpdlFile.SaveAs(this.databasePath + result.transactionId + @"\" + result.xpdlFileName);

            return result;
        }
    }

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
            customer.name = "Krzysztof Jarzyna";
            customer.mailAddress = "szef.wszystkich@szefow.pl";

            string transactionId = "JPA8296_30742581";

            Archive archive = new Archive(pathToDatabaseFolder);
            archive.ArchiviseTransaction(transactionId, customer, xmlFile, xpdlFile);

            TransactionEntity result = archive.GetArchivisedTransaction(transactionId, customer);

        }
    }
}
