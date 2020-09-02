using System;
using System.Linq;
using System.Threading.Tasks;


namespace Assignment_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ICityBikeDataFetcher fetcer = new RealTimeCityBikeDataFetcher();
            if (args.Length >= 2 && args[1] != null && args[1] == "offline")
            {
                fetcer = new OfflineCityBikeDataFetcher();
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
