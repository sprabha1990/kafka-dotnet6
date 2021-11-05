namespace KafkaConsumer;
public class Consumer<T>
{
    readonly string? _host;
    readonly int _port;
    readonly string? _topic;

    public Consumer()
    {
        _host = "localhost";
        _port = 9092;
        _topic = "producer_logs";
    }

    ConsumerConfig GetConsumerConfig()
    {
        return new ConsumerConfig
        {
            BootstrapServers = $"{_host}:{_port}",
            GroupId = "foo",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    public async Task ConsumeAsync()
    {
        using (var consumer = new ConsumerBuilder<Ignore, T>(GetConsumerConfig())
            .SetValueDeserializer(new CustomValueDeserializer<T>())                        
            .Build())
        {
            consumer.Subscribe(_topic);

            WriteLine($"Subscribed to {_topic}");

            await Task.Run(() =>
            {
                while (true)
                {
                    var consumeResult = consumer.Consume(default(CancellationToken));

                    if(consumeResult.Message.Value is Message result)
                    {
                        WriteLine($"Data Received - {result}");
                    }
                }
            });

            consumer.Close();
        }
    }
}
