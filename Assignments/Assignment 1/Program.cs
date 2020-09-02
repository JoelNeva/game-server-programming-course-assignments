using System;
using System.Linq;
using System.Threading.Tasks;


namespace Assignment_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You need to give a Station Name as first argument");
            }

            ICityBikeDataFetcher fetcer;
            if (args.Length >= 2 && args[1] != null && args[1] == "offline")
            {
                fetcer = new OfflineCityBikeDataFetcher();
            }
            else if (args.Length >= 2 && args[1] != null && args[1] == "online")
            {
                fetcer = new RealTimeCityBikeDataFetcher();
            }
            else
            {
                Console.WriteLine("You need to give a Mode (offline/online) as second argument");
                return;
            }

            if (args.Length >= 1 && args[0] != null)
            {
                try
                {
                    if (args[0].Any(c => char.IsDigit(c)))
                    {
                        throw new ArgumentException(String.Format("Station name ({0}) is not allowed to contain numbers!", args[0]), args[0]);
                    }
                    Console.WriteLine("Station: {0} | Bikes amount: {1}", args[0], await fetcer.GetBikeCountInStation(args[0]));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("{0} {1}", e.GetType(), e.Message);
                }
            }
        }
    }
}
