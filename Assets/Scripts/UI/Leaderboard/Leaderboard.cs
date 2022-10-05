using UnityEngine;
using Agava.YandexGames;
using System.Collections.Generic;

public class Leaderboard : Window
{
    [SerializeField] private ScoreSaver _saver;
    [SerializeField] private EntryView _entryViewTemplate;
    [SerializeField] private Transform _content;
    [SerializeField] private string _name;

    private List<EntryView> _entryViews = new List<EntryView>();

    protected override void LoadData()
    {
        if (YandexGamesSdk.IsInitialized == false)
            return;

        Authorize();
        SetScore();
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

    private void Authorize()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();
    }

    private void SetScore()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        Agava.YandexGames.Leaderboard.SetScore(_name, _saver.LoadScore());
    }

    private void ClearLeaderboard()
    {
        foreach (EntryView entry in _entryViews)
            Destroy(entry.gameObject);

        _entryViews = new List<EntryView>();
    }
}