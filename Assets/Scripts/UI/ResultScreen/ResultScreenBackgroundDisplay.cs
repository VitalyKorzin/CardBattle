using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ResultScreenBackgroundDisplay : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _colorChangeDuration;
    [SerializeField] private Color _targetColor;

    public void Display()
        => GetComponent<Image>().DOColor(_targetColor, _colorChangeDuration);
}