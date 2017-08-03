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
                catch (Exception e)
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
        public void gettimestamp(string tablename, string partitionkey, string rowkey)
        {
            try
            {
                CloudTable table = TableClient.GetTableReference(tablename);
                TableOperation retrieveOperation = TableOperation.Retrieve<MessageEntity>(partitionkey, rowkey);
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
        public void getquery(string tablename, string partitionkey)
        {
            try
            {
                CloudTable table = TableClient.GetTableReference(tablename);
                TableQuery<MessageEntity> query = new TableQuery<MessageEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionkey));
                int querycount =0;
                foreach (MessageEntity entity in table.ExecuteQuery(query))
                {
                    querycount = querycount + 1;
                }
                Console.WriteLine($"No of items query returned: {querycount}");
                    foreach (MessageEntity entity in table.ExecuteQuery(query))
                {
                    Console.WriteLine("\n Partition Key: " + entity.PartitionKey + "\n RowKey: " +entity.RowKey+ "\n Timestamp (GMT): " +entity.Timestamp+ "\n Message: " +entity.message);
                    Console.ReadKey();
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("End of Query");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error - {e}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        public void temperature(string tablename, string partitionkey)
        {
            CloudTable table = TableClient.GetTableReference(tablename);
            TableQuery<MessageEntity> query = new TableQuery<MessageEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionkey));
            int querycount = 0;
            foreach (MessageEntity entity in table.ExecuteQuery(query))
            {
                querycount = querycount + 1;
            }
            Console.WriteLine($"No of items query returned: {querycount}");
            foreach(MessageEntity entity in table.ExecuteQuery(query))
            {                
                string message = Convert.ToString(entity.message);
                char comma = ',';
                char colon = ':';
                string[] messagecomponents = message.Split(comma);                
                string[] temperatureparts = messagecomponents[2].Split(colon);
                double temperature = Convert.ToDouble(temperatureparts[1]);
                Console.WriteLine($"Temperature: {temperature}");
            }
        }
    }
    public class MessageEntity : TableEntity
    {
        public MessageEntity(string messsage, string timestamp)
        {
            this.RowKey = timestamp;
            this.PartitionKey= message;
        }
        public MessageEntity() { }
        public string message { get; set; }
        public string TimeStamp { get; set; }
        }
    }
