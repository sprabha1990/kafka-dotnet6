namespace KafkaProducer;

public class Program
{
    public static async Task Main(string[] args)
    {
        var producer = new Producer<Message>();

        for(int i=0; i< 25 ; i++)
        {
            await producer.ProduceAsync(new Message
            {
                Data = $"Pushing Data {i} !!",
            });

            await Task.Delay(1000);
        }

        WriteLine("Publish Success!");
        ReadKey();
    }
}