namespace AppForSEII2526.LogViewer;

class Program
{
    static void Main(string[] args)
    {
        string topic = args.Length > 0 ? args[0] : "log.*";

        if (string.IsNullOrWhiteSpace(topic))
        {
            Console.WriteLine("Debe especificar un topic");
            return;
        }

        Subscriber subscriber = new Subscriber(topic);
        try
        {
            Console.WriteLine($"Suscrito al topic: {topic}");
            subscriber.StartConsuming();

            // Mantener en ejecución
            Console.WriteLine("Presione Ctrl+C para salir");
            var exitEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, e) => {
                e.Cancel = true;
                exitEvent.Set();
            };
            exitEvent.WaitOne();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}