using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using NServiceBus.Unicast.Queuing;
using NUnit.Framework;

namespace NServiceBus.Unicast.Queueing.Azure.Tests
{
    public abstract class AzureQueueFixture
    {
        protected AzureMessageQueue queue;
        protected CloudQueueClient client;
        protected CloudQueue nativeQueue;


        protected virtual string QueueName
        {
            get
            {
                return "testqueue";
            }
        }

        protected virtual bool PurgeOnStartup { get{ return false;} }

        protected virtual int SecondsToWaitForMessage { get { return 1; } }

        [SetUp]
        public void Setup()
        {
            client = CloudStorageAccount.DevelopmentStorageAccount.CreateCloudQueueClient();
       
            nativeQueue = client.GetQueueReference(QueueName);

            nativeQueue.CreateIfNotExist();
            nativeQueue.Clear();

            
            queue = new AzureMessageQueue(client);

            queue.Init(QueueName, PurgeOnStartup, SecondsToWaitForMessage);
        }

        protected void AddTestMessage()
        {
            AddTestMessage(new QueuedMessage());
        }

        protected void AddTestMessage(QueuedMessage messageToAdd)
        {
            queue.Send(messageToAdd,QueueName,false);
        }

    }
}