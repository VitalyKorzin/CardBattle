using UnityEngine;
using Agava.YandexGames;
using System.Collections;
using UnityEngine.Events;

public class Localization : MonoBehaviour
{
    public event UnityAction<Language> LanguageChanged;

    private IEnumerator Start()
    {
        if (YandexGamesSdk.IsInitialized)
            LoadLocalization();

        while (YandexGamesSdk.IsInitialized == false)
        {
            yield return new WaitForSeconds(0.25f);

            if (YandexGamesSdk.IsInitialized)
                LoadLocalization();
        }
    }

    private void LoadLocalization()
    {
        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case "ru":
                LanguageChanged?.Invoke(Language.Russian);
                break;
            case "en":
                LanguageChanged?.Invoke(Language.English);
                break;
            case "tr":
                LanguageChanged?.Invoke(Language.Turkish);
                break;
            default:
                LanguageChanged?.Invoke(Language.English);
                break;
        }
    }
}