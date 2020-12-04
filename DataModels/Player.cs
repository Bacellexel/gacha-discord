using Discord;
using System.Collections.Generic;

namespace GachaDiscord.DataModels
{
    public class Player
    {
        private IUser _user;
        private int _noFourStarPullCounter = 0;
        private int _noFiveStarPullCounter = 0;
        private int _currency = 0;
        private Dictionary<ISummonable, int> _inventory = new Dictionary<ISummonable, int>();

        public IUser User
        {
            get { return _user; }
            set { _user = value; }
        }

        public int NoFourStarPullCounter
        {
            get { return _noFourStarPullCounter; }
            set { _noFourStarPullCounter = value; }
        }

        public int NoFiveStarPullCounter
        {
            get { return _noFiveStarPullCounter; }
            set { _noFiveStarPullCounter = value; }
        }

        public int Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public Dictionary<ISummonable, int> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        /// <summary>
        /// Add the specified ISummonable entity to the player's inventory
        /// </summary>
        /// <param name="item">ISummonable entity to add to the player's inventory</param>
        public void AddItem(ISummonable item)
        {
            bool found = false;
            ISummonable flag = item;

            foreach(var occupiedInventorySlot in _inventory)
            {
                if(occupiedInventorySlot.Key is Weapon && item is Weapon)
                {
                    var inventoryItem = (Weapon)occupiedInventorySlot.Key;
                    var giftedItem = (Weapon)item;

                    if(inventoryItem.Name == giftedItem.Name)
                    {
                        found = true;
                        flag = inventoryItem;
                    }

                }
                if (occupiedInventorySlot.Key is Character && item is Character)
                {
                    var inventoryItem = (Character)occupiedInventorySlot.Key;
                    var giftedItem = (Character)item;

                    if (inventoryItem.Name == giftedItem.Name)
                    {
                        found = true;
                        flag = inventoryItem;
                    }

                }
            }

            if(found && flag != item)
            {
                _inventory[flag]++;
            }
            else
            {
                _inventory.Add(item, 1);
            }
        }
    }
}
