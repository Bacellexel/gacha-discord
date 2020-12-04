using GachaDiscord.DataModels;
using GachaDiscord.Mecanics;
using GachaDiscord.Services;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GachaDiscord.Modules
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        private ConfigurationService _config = new ConfigurationService();

        [Command("wish")]
        public async Task SayAsync()
        {
            Wish wish = new Wish();
            List<Player> players = Global.Players;

            Player me = players.Find(x => x.User == Context.User);

            if (!players.Any(player => player.User == Context.User))
            {
                me = new Player
                {
                    User = Context.User,
                    Currency = 100
                };
                players.Add(me);
            }

            if (me.Currency < _config.Wish.WishCost)
            {
                await ReplyAsync("Not enough currency");
                return;
            }
            
            var pulled = wish.MakeWish(me);

            Character character = new Character();
            Weapon weapon = new Weapon();

            if (pulled is Weapon)
            {
                weapon = (Weapon)pulled;
                await ReplyAsync($"You got {weapon.Name}({weapon.GetRarity}*)");
            }
            if (pulled is Character)
            {
                character = (Character)pulled;
                await ReplyAsync($"You got {character.Name}({character.GetRarity}*)");
            }
        }
    }
}
