using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(TMP_Text))]
public class StartFightTextDisplay : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _fadingDuration;
    [Min(0)]
    [SerializeField] private float _appearanceDuration;
    [Min(0)]
    [SerializeField] private float _startSize;
    [Min(0)]
    [SerializeField] private float _targetSize;
    [Min(0)]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Color _fadingColor;
    [SerializeField] private Color _appearanceColor;

    private readonly float _fadingEndValue = 0f;
    private readonly float _appearanceEndValue = 1f;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        transform.localScale = new Vector3(_startSize, _startSize, _startSize);
    }

    public void Appear()
    {
        _text.DOFade(_appearanceEndValue, _appearanceDuration);
        _text.DOColor(_appearanceColor, _appearanceDuration);
        transform.DOScale(_targetSize, _appearanceDuration);
        StartCoroutine(Move());
    }

    public void Fade()
    {
        _text.DOFade(_fadingEndValue, _fadingDuration);
        _text.DOColor(_fadingColor, _fadingDuration);
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(_fadingDuration);
        enabled = false;
    }

    private IEnumerator Move()
    {
        while (enabled == true)
        {
            transform.Translate(_movementSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }
    }
}