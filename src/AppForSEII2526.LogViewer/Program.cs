namespace AppForSEII2526.LogViewer;

class Program
{
    static void Main(string[] args)
    {

        Subscriber subscriber = new Subscriber();
        try
        {
            subscriber.StartConsuming();
            Console.WriteLine("Presione una tecla para salir");
            Console.ReadLine();
        }
        finally { }
    }
}
