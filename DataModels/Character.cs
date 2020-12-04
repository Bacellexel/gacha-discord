namespace GachaDiscord.DataModels
{
    public class Character : ISummonable
    {
        public string Rarity;
        public string Icon;
        public string Name;
        public string Element;
        public string Weapon;
        public string Sex;
        public string Nation;
        public string BaseHp;
        public string BaseAttack;
        public string BaseDef;

        public int GetRarity
        {
            get { return int.Parse(Rarity); }
        }
    }
}
