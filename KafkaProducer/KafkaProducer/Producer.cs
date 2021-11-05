namespace KafkaProducer;
public class Producer<T>
{

    readonly string? _host;
    readonly int _port;
    readonly string? _topic;

    public Producer()
    {
        _host = "localhost";
        _port = 9092;
        _topic = "producer_logs";
    }

    ProducerConfig GetProducerConfig()
    {
        return new ProducerConfig
        {
            BootstrapServers = $"{_host}:{_port}"
        };
    }

    public async Task ProduceAsync(T data)
    {
        using (var producer = new ProducerBuilder<Null, T>(GetProducerConfig())
                                             .SetValueSerializer(new CustomValueSerializer<T>())
                                             .Build())
        {
            await producer.ProduceAsync(_topic, new Message<Null, T> { Value = data });
            WriteLine($"{data} published");
        }
    }
}