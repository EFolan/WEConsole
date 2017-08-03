using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;

namespace WEConsole.Services
{
    class StorageService
    {
        string _storageKey = ConfigurationManager.AppSettings["StorageConnection"];
        CloudStorageAccount StorageAccount;
        CloudBlobClient BlobClient;
        CloudTableClient TableClient;

        public StorageService()
        {
            StorageAccount = CloudStorageAccount.Parse(_storageKey);
            TableClient = StorageAccount.CreateCloudTableClient();
        }

        public CloudTable GetTable(string tableName)
        {
            {
                //table name is AzureSubscriptions         
                CloudTable Table = TableClient.GetTableReference(tableName);
                try
                {
                    if (!Table.Exists())
                    {
                        Console.WriteLine($"Table does not exist. Creating one with name {tableName}");
                    }
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error - {e}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Table.CreateIfNotExistsAsync();
                Console.WriteLine($"Accessed Table: {Table.Name}");
                return Table;
            }
        }        
        public void getquery()
        {
            try
            {
                CloudTable table = TableClient.GetTableReference("deviceData");
                TableOperation retrieveOperation = TableOperation.Retrieve<MessageEntity>("17380", "1501675093450");
                TableResult retrievedResult = table.Execute(retrieveOperation);
                Console.WriteLine(((MessageEntity)retrievedResult.Result).Timestamp);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error - {e}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
    public class MessageEntity : TableEntity
    {
        public MessageEntity(string messsage, string timestamp)
        {
            this.RowKey = timestamp;
            this.PartitionKey= Message;
        }
        public MessageEntity() { }
        public string Message { get; set; }
        public string TimeStamp { get; set; }
        }
    }
