using System;
using System.IO;
using System.Threading.Tasks;

public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
{
    public async Task<int> GetBikeCountInStation(string stationName)
    {
        await Task.Delay(0);
        string[] lines = File.ReadAllLines("bikedata.txt");

        Station[] stations = new Station[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] current = lines[i].Split(" : ");
            stations[i] = new Station();
            stations[i].name = current[0];
            stations[i].bikesAvailable = int.Parse(current[1]);
        }

        Station temp = Array.Find(stations, x => x.name.Equals(stationName));
        if (temp == null)
        {
            throw new NotFoundException(String.Format("Station name ({0}) is not found!", stationName), stationName);
        }
        return temp.bikesAvailable;
    }

}












