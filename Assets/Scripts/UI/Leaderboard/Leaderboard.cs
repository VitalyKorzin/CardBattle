using System;
using UnityEngine;
using Agava.YandexGames;
using System.Collections.Generic;

public class Leaderboard : Window
{
    [SerializeField] private SdkYandex _sdkYandex;
    [SerializeField] private EntryView _entryViewTemplate;
    [SerializeField] private Transform _content;
    [SerializeField] private string _name;

    private List<EntryView> _entryViews = new List<EntryView>();
    private LeaderboardGetEntriesResponse _response;

    public void Set(int score) 
        => _sdkYandex.SetLeaderboardScore(score, _name);

    protected override void LoadData()
    {
        ClearLeaderboard();
        _response = _sdkYandex.GetLeaderboardEntries(_name);

        if (_response != null)
        {
            foreach (var entry in _response.entries)
            {
                string name = entry.player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = "Anonymos";

                EntryView entryView = Instantiate(_entryViewTemplate, _content);
                entryView.Initialize(name, entry.score, entry.rank);
                _entryViews.Add(entryView);
            }
        }
    }

    private void ClearLeaderboard()
    {
        foreach (EntryView entry in _entryViews)
            Destroy(entry.gameObject);

        _entryViews = new List<EntryView>();
    }
}