using GachaDiscord.DataModels;
using GachaDiscord.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GachaDiscord.Mecanics
{
    
    public class Wish
    {
        private ConfigurationService _config = new ConfigurationService();

        /// <summary>
        /// Pity system is an incremental counter helping the player to pull a better character while he has not had one of this rarity for a set amount of pulls.
        /// </summary>
        /// <param name="player">Player entity</param>
        /// <returns>The rarity of the character to summon. If 1, it means we are doing a random wish according to the base rates.</returns>
        private int CalculatePity(Player player)
        {
            if (player.NoFiveStarPullCounter + 1 == _config.Pity.GuaranteedFive)
            {
                return 5;
            }
            else if (player.NoFourStarPullCounter + 1 == _config.Pity.GuaranteedFour)
            {
                return 4;
            }
            else
            {
                player.NoFiveStarPullCounter++;
                player.NoFourStarPullCounter++;
                return 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">Player Entity</param>
        /// <returns></returns>
        public ISummonable MakeWish(Player player)
        {
            int pity = 0;

            if(_config.Pity.IsActivated)
                 pity = CalculatePity(player);

            if (pity == 5 && _config.Pity.IsActivated)
            {
                return GetPull(player, 5);
            }
            else if (pity == 4 && _config.Pity.IsActivated)
            {
                return GetPull(player, 4);
            }
            else //No pity wish available, so we proceed normally with rates
            {
                var luck = new Random().Next(0, 100*100);

                if (luck <= (_config.Rates.FiveStar * 100))
                {
                    return GetPull(player, 5);
                }
                else if(luck > (_config.Rates.FiveStar * 100) && luck <= (_config.Rates.FourStar * 100))
                {
                    return GetPull(player, 4);
                }
                else 
                {
                    return GetPull(player, 3);
                }
            }
        }

        /// <summary>
        /// Resets the pity if needed, then handle the logic to get the random unity according to the rarity pulled before.
        /// </summary>
        /// <param name="player">Player entity</param>
        /// <param name="starCount">Rarity of the item to summon</param>
        /// <returns>The ISummonable entity the player wins</returns>
        private ISummonable GetPull(Player player, int starCount)
        {
            if (starCount == 4) player.NoFourStarPullCounter = 0;
            if (starCount == 5) player.NoFiveStarPullCounter = 0;

            List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(File.ReadAllText(""));
            List<Weapon> weapons = JsonConvert.DeserializeObject<List<Weapon>>(File.ReadAllText(""));

            List<ISummonable> listOfPossiblePulls = new List<ISummonable>();

            for (int i = 0; i < characters.Count; i++)
            {
                if(characters[i].GetRarity == starCount)
                {
                    listOfPossiblePulls.Add(characters[i]);
                }
            }

            for(int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].GetRarity == starCount)
                {
                    listOfPossiblePulls.Add(weapons[i]);
                }
            }

            var luck = new Random().Next(listOfPossiblePulls.Count);

            for(int i = 0; i < listOfPossiblePulls.Count;i++)
            {
                if (i == luck)
                {
                    if (listOfPossiblePulls[i] is Character)
                    {
                        Character character = (Character)listOfPossiblePulls[i];
                        player.AddItem(character);
                        return character;
                    }
                    if (listOfPossiblePulls[i] is Weapon)
                    {
                        Weapon weapon = (Weapon)listOfPossiblePulls[i];
                        player.AddItem(weapon);
                        return weapon;
                    }
                    //////////////////////////////////////////////////////
                    player.Currency -= _config.Wish.WishCost;
                    //////////////////////////////////////////////////////
                }
            }
            return null;
        }
    }
}
