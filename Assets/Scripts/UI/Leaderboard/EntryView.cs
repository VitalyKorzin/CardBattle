using TMPro;
using UnityEngine;

public class EntryView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nick;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _rank;

    public void Initialize(string nick, int score, int rank)
    {
        _nick.text = nick;
        _score.text = score.ToString();
        _rank.text = rank.ToString();
    }
}