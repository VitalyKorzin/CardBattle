using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using IJunior.TypedScenes;

public class SdkYandex : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    public event UnityAction VideoAdOpend;
    public event UnityAction Rewarded;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();
    }

    public void OnShowVideoButtonClick() 
        => VideoAd.Show(OnOpenVideoAdCallback, OnRewardedCallback);

    private void OnOpenVideoAdCallback() 
        => VideoAdOpend?.Invoke();

    private void OnRewardedCallback() 
        => Rewarded?.Invoke();

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        if (argument.CanShowInterstitialAd)
        {
            InterstitialAd.Show();
            argument.Clear();
        }
    }
}