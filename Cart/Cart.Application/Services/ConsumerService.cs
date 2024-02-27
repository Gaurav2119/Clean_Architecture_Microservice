using Cart.Application.Interfaces;
using Cart.Application.KafkaConfiguration;
using Cart.Domain.Entities;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Services
{
    public class ConsumerService : IConsumer
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<ConsumerService> _logger;
        private readonly string _topic;
        private readonly ICartService _cartService;

        public ConsumerService(IOptions<KafkaConsumerConfig> kafkaConsumerConfig, ILogger<ConsumerService> logger, ICartService cartService)
        {
            _logger = logger;

            var config = new ConsumerConfig
            {
                GroupId = kafkaConsumerConfig.Value.GroupId,
                BootstrapServers = kafkaConsumerConfig.Value.BootstrapServers,
                ClientId = kafkaConsumerConfig.Value.ClientId,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SaslUsername = kafkaConsumerConfig.Value.SaslUsername,
                SaslPassword = kafkaConsumerConfig.Value.SaslPassword
            };

            _topic = kafkaConsumerConfig.Value.TopicSubscribed ?? throw new Exception("Kafka topic is null. Check config file");

            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _cartService = cartService;
        }

        public void ConsumeMessage(CancellationToken cts)
        {
            _consumer.Subscribe(_topic);

            cts.Register(() =>
            {
                _logger.LogInformation("Kafka Consumer is stopping.");
                _consumer.Close();
            });

            try
            {
                while (!cts.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(cts);

                        if (consumeResult.IsPartitionEOF)
                        {
                            _logger.LogInformation($"Reached end of partition: {consumeResult.TopicPartition}");
                            continue;
                        }

                        _logger.LogInformation($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");

                        // Deserialize the message
                        dynamic? kafkaKey = JsonConvert.DeserializeObject(consumeResult.Message.Key);
                        dynamic? kafkaMessage = JsonConvert.DeserializeObject(consumeResult.Message.Value);

                        if (kafkaKey != null && kafkaMessage != null)
                        {
                            // Extract key and message and create an Order object
                            var cartItem = new CartList(new Guid(kafkaKey), kafkaMessage?.ProductName, kafkaMessage?.Quantity, kafkaMessage?.UnitPrice);

                            _cartService.Create(cartItem);
                        }

                        _consumer.Commit();

                    }

                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"Error occurred: {ex.Error.Reason}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }
            finally
            {
                _consumer.Close();
            }
        }

        public void RunInBackground()
        {
            CancellationToken cancellationToken = new CancellationToken();
            Task.Run(() => ConsumeMessage(cancellationToken));
        }
    }
}
