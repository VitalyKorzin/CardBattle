using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueText;

    private Camera _camera;
    private Coroutine _fillingJob;
    private int _currentTextValue;

    private void OnEnable() => SubscribeToEvents();

    private void OnDisable() => UnsubscribeFromEvents();

    private void Start()
    {
        _camera = Camera.main;
        _currentTextValue = GetCurrentValue();
        _valueText.text = GetCurrentValue().ToString();
    }

    private void Update() => LookAtCamera();

    protected abstract void SubscribeToEvents();

    protected abstract void UnsubscribeFromEvents();

    protected abstract int GetCurrentValue();

    protected virtual void TryDisableIcon() { }

    protected void OnValueChanged(int newValue)
    {
        if (_fillingJob != null)
            StopCoroutine(_fillingJob);

        _fillingJob = StartCoroutine(Fill(newValue));
    }

    private void LookAtCamera()
    {
        Vector3 worldPosition = transform.position + _camera.transform.rotation * Vector3.forward;
        Vector3 worldUp = _camera.transform.rotation * Vector3.up;
        transform.LookAt(worldPosition, worldUp);
    }

    private IEnumerator Fill(int newValue)
    {
        while (_currentTextValue != newValue)
        {
            if (_currentTextValue > newValue)
                _currentTextValue--;
            else
                _currentTextValue++;

            _valueText.text = _currentTextValue.ToString();
            yield return null;
        }

        TryDisableIcon();
    }
}