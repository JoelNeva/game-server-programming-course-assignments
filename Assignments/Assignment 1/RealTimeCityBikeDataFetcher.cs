using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
{
    public async Task<int> GetBikeCountInStation(string stationName)
    {
        HttpClient httpClient = new HttpClient();
        String jsonString = await httpClient.GetStringAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
        BikeRentalStationList list = JsonConvert.DeserializeObject<BikeRentalStationList>(jsonString);
        Station temp = Array.Find(list.stations, x => x.name == stationName);
        if (temp == null)
        {
            throw new NotFoundException(String.Format("Station name ({0}) is not found!", stationName), stationName);
        }
        return temp.bikesAvailable;

    }

}
