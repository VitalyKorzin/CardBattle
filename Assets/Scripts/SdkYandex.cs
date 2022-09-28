using System;
using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using IJunior.TypedScenes;

public class SdkYandex : MonoBehaviour, ISceneLoadHandler<CompletedLevelsCounter>
{
    public event UnityAction VideoAdOpend;
    public event UnityAction Rewarded;
    public event UnityAction Initialized;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize(OnYandexSDKInitialize);
    }

    public void OnSceneLoaded(CompletedLevelsCounter argument)
    {
        if (argument.CanShowInterstitialAd)
        {
            InterstitialAd.Show();
            argument.Clear();
        }
    }

    public void SetLeaderboardScore(int score, string name)
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        Agava.YandexGames.Leaderboard.GetPlayerEntry(name, (result) =>
        {
            if (result == null)
                Agava.YandexGames.Leaderboard.SetScore(name, score);
            else if (result.score < score)
                Agava.YandexGames.Leaderboard.SetScore(name, score);
        });
    }

    public LeaderboardGetEntriesResponse GetLeaderboardEntries(string name)
    {
        LeaderboardGetEntriesResponse response = null;
        Agava.YandexGames.Leaderboard.GetEntries(name, (result) => response = result);
        return response;
    }

    public void OnShowVideoButtonClick() 
        => VideoAd.Show(OnOpenVideoAdCallback, OnRewardedCallback);

    private void OnOpenVideoAdCallback() 
        => VideoAdOpend?.Invoke();

    private void OnRewardedCallback() 
        => Rewarded?.Invoke();

    private void OnYandexSDKInitialize()
    {
        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();

        Initialized?.Invoke();
    }
}