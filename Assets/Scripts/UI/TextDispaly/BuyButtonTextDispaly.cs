using TMPro;
using UnityEngine;

public class BuyButtonTextDispaly : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;

    public void Initialize(Language language)
    {
        _value.text = language switch
        {
            Language.Russian => "Купить",
            Language.English => "Buy",
            Language.Turkish => "Satın almak",
            _ => "Buy",
        };
    }
}