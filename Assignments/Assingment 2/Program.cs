using System;
using System.Collections.Generic;
using System.Linq;

namespace Assingment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Teht1();
            //Teht2();
            //Teht3();
            //Teht4();
            //Teht5();
            //Teht6();
            //Teht7();
        }

        static void Teht1()
        {
            Player[] players = new Player[1000000];
            HashSet<Guid> bigHeapOGuids = new HashSet<Guid>();
            bool duplicates = false;
            for (long i = 0; i < players.Length; i++)
            {
                players[i] = new Player();
                Guid g = Guid.NewGuid();

                if (!bigHeapOGuids.Contains(g))
                {
                    bigHeapOGuids.Add(g);
                    players[i].Id = g;
                }
                else
                {
                    duplicates = true;
                    Console.WriteLine("Duplicate found");
                }
            }
            if (!duplicates)
            {
                Console.WriteLine("No duplicates found");
            }
        }

        static void Teht2()
        {
            Player player = GeneratePlayer();
            Console.WriteLine(player.GetHighestLevelItem().Level);
        }

        static void Teht3()
        {
            Player player = GeneratePlayer();

            Item[] arrayedList = GetItems(player);

            Console.WriteLine("GetItems:");
            int round = 0;
            do
            {
                for (int i = 0; i < arrayedList.Length; i++)
                {
                    Console.WriteLine(arrayedList[i].Level);
                }
                if (round > 0)
                {
                    break;
                }
                Console.WriteLine("GetItemsWithLinq:");
                arrayedList = GetItemsWithLinq(player);
                round++;
            } while (true);
        }
        static void Teht4()
        {
            Player player = GeneratePlayer();
            Player nullPlayer = new Player() { Items = new List<Item>() };

            Console.WriteLine("FirstItem player: " + FirstItem(player));
            Console.WriteLine("FirstItem nullplayer: " + FirstItem(nullPlayer));
            Console.WriteLine("FirstItemWithLinq player: " + FirstItemWithLinq(player));
            Console.WriteLine("FirstItemWithLinq nullplayer: " + FirstItemWithLinq(nullPlayer));
        }

        static void Teht5()
        {
            Player player = GeneratePlayer();
            ProcessEachItem(player, PrintItem);
        }

        static void Teht6()
        {
            Player player = GeneratePlayer();
            ProcessEachItem(player, item => Console.WriteLine("Item ID: " + item.Id + " Item Level: " + item.Level));
        }

        static void Teht7()
        {
            List<Player> players = new List<Player>();
            List<PlayerForAnotherGame> playersForAnotherGames = new List<PlayerForAnotherGame>();

            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                int c = random.Next(1000);
                players.Add(new Player() { Score = c });
                playersForAnotherGames.Add(new PlayerForAnotherGame() { Score = c });
            }

            Player[] top10players = new Game<Player>(players).GetTop10Players();
            PlayerForAnotherGame[] top10ofAnotherGame = new Game<PlayerForAnotherGame>(playersForAnotherGames).GetTop10Players();

            Console.WriteLine("Results from Game: ");
            foreach (Player player in top10players)
            {
                Console.WriteLine(player.Score);
            }
            Console.WriteLine("Results from another Game: ");
            foreach (PlayerForAnotherGame player in top10ofAnotherGame)
            {
                Console.WriteLine(player.Score);
            }
        }


        static void ProcessEachItem(Player player, Action<Item> process)
        {
            foreach (Item item in player.Items)
            {
                process(item);
            }
        }

        static void PrintItem(Item item)
        {
            Console.WriteLine("Item ID: " + item.Id + " Item Level: " + item.Level);
        }
        static Player GeneratePlayer()
        {
            Player player = new Player()
            {
                Items = new List<Item>()
                {
                    new Item(){Level = 345,Id = Guid.NewGuid()},
                    new Item(){Level = 24,Id = Guid.NewGuid()},
                    new Item(){Level = 724,Id = Guid.NewGuid()},
                    new Item(){Level = 23,Id = Guid.NewGuid()},
                    new Item(){Level = 43,Id = Guid.NewGuid()},
                    new Item(){Level = 62,Id = Guid.NewGuid()}
                }
            };
            return player;
        }

        public static Item[] GetItems(Player player)
        {
            Item[] array = new Item[player.Items.Count];
            int i = 0;
            foreach (Item item in player.Items)
            {
                array[i++] = item;
            }
            return array;
        }

        public static Item[] GetItemsWithLinq(Player player)
        {
            return player.Items.ToArray();
        }

        public static Item FirstItem(Player player)
        {
            if (player.Items == null || player.Items.Count() == 0)
            {
                return null;
            }
            return player.Items[0];
        }

        public static Item FirstItemWithLinq(Player player)
        {
            return player.Items.FirstOrDefault();
        }
    }
}


