namespace AppForSEII2526.LogViewer;

class Program
{
    static void Main(string[] args)
    {
        string topic = "";

        // Leer el topic de los args o pedirlo por consola
        if (args.Length > 0)
        {
            topic = args[0];
        }
        else
        {
            Console.WriteLine("Ingrese el topic al que desea suscribirse:");
            Console.WriteLine("Ejemplos: log.error, log.info, log.*, log.#");
            topic = Console.ReadLine() ?? "";
        }

        if (string.IsNullOrWhiteSpace(topic))
        {
            Console.WriteLine("Debe especificar un topic");
            return;
        }

        Subscriber subscriber = new Subscriber(topic); // Pasar el topic al constructor
        try
        {
            Console.WriteLine($"Suscrito al topic: {topic}");
            subscriber.StartConsuming();
            Console.WriteLine("Presione una tecla para salir");
            Console.ReadLine();
        }
        finally { }
    }
}  

