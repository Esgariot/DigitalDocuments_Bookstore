using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileArchiveClient
{
    public class CustomerEntity
    {
        public string name { get; set; }
        public string mailAddress { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;

            CustomerEntity objEntity = obj as CustomerEntity;

            if (objEntity.name.Equals(this.name) ||
                objEntity.mailAddress.Equals(this.mailAddress))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 602422620;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(mailAddress);
            return hashCode;
        }
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
        private LiteDB.LiteDatabase database;
        private string databasePath;
        private const string transactionsCollectionName = "transactionsEntityGroup";
        private const string xmlFilesFolderPath = "$/files/xmls";
        private const string xpdlFilesFolderPath = "$/files/xpdl";

        public Archive(string pathToDatabase, string databaseName = "BookshopArchive.db")
        {
            this.databasePath = pathToDatabase;
            this.database = new LiteDB.LiteDatabase(this.databasePath + databaseName);
        }

        public List<TransactionEntity> GetMatchingArchivisedTransactions(string transactionId, CustomerEntity customerData, string saveFileFolder)
        {
            LiteDB.LiteCollection<TransactionEntity> transactions =
               this.database.GetCollection<TransactionEntity>(Archive.transactionsCollectionName);

            List<TransactionEntity> results = new List<TransactionEntity>();
            foreach (TransactionEntity transaction in transactions.FindAll())
            {
                if (transaction.transactionId.Equals(transactionId) ||
                    transaction.customer.Equals(customerData))
                    results.Add(transaction);
            }
            if (results.Count == 0)
                return null;

            foreach (TransactionEntity result in results)
            {
                System.IO.Directory.CreateDirectory(saveFileFolder + result.transactionId);

                LiteDB.LiteFileInfo xmlFile = this.database.FileStorage.FindById(Archive.xmlFilesFolderPath + "/" + result.xmlFileName);
                xmlFile.SaveAs(saveFileFolder + result.transactionId + @"\" + result.xmlFileName);

                LiteDB.LiteFileInfo xpdlFile = this.database.FileStorage.FindById(Archive.xpdlFilesFolderPath + "/" + result.xpdlFileName);
                xpdlFile.SaveAs(saveFileFolder + result.transactionId + @"\" + result.xpdlFileName);
            }

            return results;
        }
    }
}
