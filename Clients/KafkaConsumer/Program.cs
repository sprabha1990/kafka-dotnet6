namespace KafkaConsumer;

public class Program
{
    public static async Task Main(string[] args)
    {
        var consumer = new Consumer<Message>();
        await consumer.ConsumeAsync();
    }
}