using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class NextButton : ResultDisplayElement
{
    public override void Appear(float endValue, float duration) 
        => GetComponent<TMP_Text>().DOFade(endValue, duration);
}