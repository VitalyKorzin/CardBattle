using UnityEngine;
using Agava.YandexGames;
using System.Collections.Generic;

public class Leaderboard : Window
{
    [SerializeField] private EntryView _entryViewTemplate;
    [SerializeField] private Transform _content;
    [SerializeField] private string _name;

    private List<EntryView> _entryViews = new List<EntryView>();

    public void SetScore()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        Agava.YandexGames.Leaderboard.GetPlayerEntry(_name, (result) =>
        {
            if (result == null)
                Agava.YandexGames.Leaderboard.SetScore(_name, 30);
            else
                Agava.YandexGames.Leaderboard.SetScore(_name, result.score + 30);
        });
    }

    protected override void LoadData()
    {
        if (YandexGamesSdk.IsInitialized == false)
            return;

        ClearLeaderboard();

        Agava.YandexGames.Leaderboard.GetEntries(_name, (result) =>
        {
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";

                EntryView entryView = Instantiate(_entryViewTemplate, _content);
                entryView.Initialize(name, entry.score, entry.rank);
                _entryViews.Add(entryView);
            }
        });
    }

    private void ClearLeaderboard()
    {
        foreach (EntryView entry in _entryViews)
            Destroy(entry.gameObject);

        _entryViews = new List<EntryView>();
    }
}