using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AccessTimer : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _duration;
    [SerializeField] private Image _image;

    private readonly uint _startFillingValue = 1;
    private readonly uint _endFillingValue = 0;

    private float _elapsedTime;
    private float _nextValue;

    public bool Ready => _image.fillAmount == _endFillingValue;

    public void StartCountingDown()
    {
        _image.fillAmount = _startFillingValue;
        StartCoroutine(Fill());
    }

    private IEnumerator Fill()
    {
        _elapsedTime = 0f;

        while (_elapsedTime < _duration)
        {
            _nextValue = Mathf.Lerp(_startFillingValue, _endFillingValue, _elapsedTime / _duration);
            _image.fillAmount = _nextValue;
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        _image.fillAmount = _endFillingValue;
    }
}