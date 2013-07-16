namespace NServiceBus.Unicast.Queuing.Azure
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.WindowsAzure.Storage.Queue;

    public class EnvelopeDeserializationFailed:SerializationException
    {
        public EnvelopeDeserializationFailed(CloudQueueMessage message, Exception ex)
            : base("Failed to deserialize message envelope", ex)
        {
            Message = message;
        }

        public CloudQueueMessage Message { get; private set; }
    }
}