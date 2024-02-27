﻿using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Product.Application.Interfaces;
using Product.Application.KafkaConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Services
{
    public class ProducerService : IProducer
    {
        private readonly IProducer<string, string> _producer;

        private readonly string _topic;
        public ProducerService(IOptions<KafkaProducerConfig> kafkaProducerConfig)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaProducerConfig.Value.BootstrapServers,
                ClientId = kafkaProducerConfig.Value.ClientId,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = kafkaProducerConfig.Value.SaslUsername,
                SaslPassword = kafkaProducerConfig.Value.SaslPassword
            };

            _topic = kafkaProducerConfig.Value.TopicSubscribed;

            _producer = new ProducerBuilder<string, string>(config)
                .Build();
        }

        public async Task ProduceMessage(Guid key, object message)
        {
            try
            {
                var deliveryResult = await _producer.ProduceAsync(_topic, new Message<string, string>
                {
                    Key = JsonConvert.SerializeObject(key),
                    Value = JsonConvert.SerializeObject(message)
                });

                Console.WriteLine($"Produced message to: {deliveryResult.TopicPartitionOffset}");
            }
            catch (ProduceException<string, string> ex)
            {
                Console.WriteLine($"Error producing message: {ex.Error.Reason}");
            }
        }
    }
}
