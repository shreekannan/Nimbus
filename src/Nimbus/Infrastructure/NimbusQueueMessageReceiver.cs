﻿using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Nimbus.Infrastructure
{
    internal class NimbusQueueMessageReceiver : INimbusMessageReceiver
    {
        private readonly IQueueManager _queueManager;
        private readonly string _queuePath;

        private readonly Lazy<MessageReceiver> _messageReceiver;

        public NimbusQueueMessageReceiver(IQueueManager queueManager, string queuePath)
        {
            _queueManager = queueManager;
            _queuePath = queuePath;

            _messageReceiver = new Lazy<MessageReceiver>(CreateMessageReceiver);
        }

        private MessageReceiver CreateMessageReceiver()
        {
            return _queueManager.CreateMessageReceiver(_queuePath);
        }

        public Task<BrokeredMessage> Receive()
        {
            return _messageReceiver.Value.ReceiveAsync(TimeSpan.FromSeconds(1));
        }

        public override string ToString()
        {
            return _queuePath;
        }
    }
}