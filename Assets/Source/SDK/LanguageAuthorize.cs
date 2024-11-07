using UnityEngine;
using YG;

public class LanguageAuthorize : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += Load;

    private void OnDisable() => YandexGame.GetDataEvent -= Load;

    private void Awake()
    {
        if (YandexGame.SDKEnabled == true)
            Load();
    }

    public void Load()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Russian");
        }
        else if (YandexGame.EnvironmentData.language == "en")
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
        }
        else if (YandexGame.EnvironmentData.language == "tr")
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Turkish");
        }
        else
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
        }
    }
}
