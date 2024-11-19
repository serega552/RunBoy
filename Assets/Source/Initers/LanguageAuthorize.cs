using UnityEngine;
using YG;

namespace Initers
{
    public class LanguageAuthorize : MonoBehaviour
    {
        private const string Ru = "ru";
        private const string En = "en";
        private const string Tr = "tr";

        private void OnEnable() => YandexGame.GetDataEvent += Load;

        private void OnDisable() => YandexGame.GetDataEvent -= Load;

        private void Awake()
        {
            if (YandexGame.SDKEnabled == true)
                Load();
        }

        public void Load()
        {
            switch (YandexGame.EnvironmentData.language)
            {
                case Ru:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Russian");
                    break;
                case En:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
                    break;
                case Tr:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Turkish");
                    break;
                default:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
                    break;
            }
        }
    }
}