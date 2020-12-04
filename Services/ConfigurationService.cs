using Newtonsoft.Json;
using System.IO;

namespace GachaDiscord.Services
{
    public class ConfigurationService
    {
        public dynamic _config;
        private RateSettings _rateSettings = new RateSettings();
        private PitySettings _pitySettings = new PitySettings();
        private WishSettings _wishSettings = new WishSettings();

        public ConfigurationService()
        {
            _config = JsonConvert.DeserializeObject(File.ReadAllText(""));
            _rateSettings.FiveStar = _config["Rates"]["fiveStar"];
            _rateSettings.FourStar = _config["Rates"]["fourStar"];
            _rateSettings.ThreeStar = _config["Rates"]["threeStar"];
            _pitySettings.IsActivated = _config["Pity"]["isActivated"] ?? false;
            _pitySettings.GuaranteedFive = _config["Pity"]["guaranteedFive"];
            _pitySettings.GuaranteedFour = _config["Pity"]["guaranteedFour"];
            _wishSettings.WishCost = _config["Wish"]["cost"];
        }

        public RateSettings Rates
        {
            get { return _rateSettings; }
            set { _rateSettings = value; }
        }

        public PitySettings Pity
        {
            get { return _pitySettings; }
            set { _pitySettings = value; }
        }

        public WishSettings Wish
        {
            get { return _wishSettings; }
            set { _wishSettings = value; }
        }
    }
}
