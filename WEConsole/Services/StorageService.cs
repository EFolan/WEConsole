﻿using System;
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
            //table name is AzureSubscriptions
            CloudTable Table = TableClient.GetTableReference(tableName);
            Table.CreateIfNotExistsAsync();
            Console.WriteLine($"Accessed Table: {Table.Name}");
            return Table;
        }

    }
}