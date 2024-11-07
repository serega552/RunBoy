using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        public int Money = 50;
        public int Diamond = 0;
        public float MaxEnergy = 50;
        public float Record = 0;
        public int CountEnergyBoost = 0;
        public int CountUpgradeEnergyBoost = 1;
        public int CountSpeedBoost = 0;
        public int CountUpgradeSpeedBoost = 1;
        public int CountMoneyBoost = 0;
        public int CountUpgradeMoneyBoost = 1;
        public int CountUpgradeEnergy = 1;
        public int SelectedDance;
        public int SelectedSkin;
        public List<int> BoughtSkins = new List<int>();
        public List<int> BoughtDances = new List<int>();
        public List<float> AmountDailyProgreses = new List<float>();
        public List<float> AmountWeeklyProgreses = new List<float>();
        public List<float> AmountDistanceProgreses = new List<float>();
        public int StartWeeklyTime = 3;
        public int StartDailyTime = 1;
        public float WorldSound = 0.5f;

        public SavesYG()
        {
        }
    }
}

