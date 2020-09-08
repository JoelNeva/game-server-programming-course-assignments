public static class MyExtensions
{
    public static Item GetHighestLevelItem(this Player player)
    {
        if (player.Items.Count == 0)
        {
            return null;
        }
        Item highest = player.Items[0];
        foreach (Item item in player.Items)
        {
            if (highest.Level < item.Level)
            {
                highest = item;
            }
        }
        return highest;
    }
}