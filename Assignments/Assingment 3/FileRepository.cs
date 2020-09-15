using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public class FileRepository : IRepository
{
    public Task<Player> Create(NewPlayer player)
    {
        Player newPlayer = new Player
        {
            Id = Guid.NewGuid(),
            Name = player.Name,
            CreationTime = DateTime.Now
        };
        Player[] playersArray = ReadFile();
        List<Player> playerlist = playersArray.ToList();
        playerlist.Add(newPlayer);
        WriteFile(playerlist.ToArray());
        return null;
    }

    public Task<Player> Delete(Guid id)
    {
        Player[] players = ReadFile();
        List<Player> playerlist = players.ToList();
        playerlist.Remove(players.Where(x => x.Id == id).FirstOrDefault());
        WriteFile(playerlist.ToArray());
        return null;
    }

    public Task<Player> Get(Guid id)
    {
        return Task.Run(() =>
        {
            return ReadFile().Where(x => x.Id == id).FirstOrDefault();
        });
    }

    public Task<Player[]> GetAll()
    {
        return Task.Run(() =>
        {
            return ReadFile();
        });
    }

    public Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        Player[] players = ReadFile();
        players.Where(x => x.Id == id).FirstOrDefault().Score = player.Score;
        WriteFile(players);
        return null;
    }

    Player[] ReadFile()
    {
        String jsonString = System.IO.File.ReadAllText(Path.GetFullPath("game-dev.txt"));
        if (jsonString.Equals(""))
        {
            return new Player[0];
        }
        Player[] array = JsonConvert.DeserializeObject<Player[]>(jsonString);
        return array;
    }

    void WriteFile(Player[] players)
    {
        System.IO.File.WriteAllText(Path.GetFullPath("game-dev.txt"), JsonConvert.SerializeObject(players));
    }
}