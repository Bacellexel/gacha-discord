namespace GachaDiscord.DataModels
{
    public class Weapon : ISummonable
    {
        public string Name;
        public string Type;
        public string Rarity;
        public string ATK;
        public string Secondary;
        public string Passive;
        public string Bonus;

        public int GetRarity
        {
            get { return int.Parse(Rarity); }
        }
    }
}
