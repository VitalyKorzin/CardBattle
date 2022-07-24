using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _fillingDuration;
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private Stickman _stickmen;

    private Camera _camera;
    private Coroutine _fillingJob;
    private int _currentTextValue;

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch (Exception exception)
        {
            enabled = false;
            throw exception;
        }

        _stickmen.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
        => _stickmen.HealthChanged -= OnHealthChanged;

    private void Start()
    {
        _camera = Camera.main;
        _currentTextValue = _stickmen.Health;
        _valueText.text = _stickmen.Health.ToString();
    }

    private void Update() => LookAtCamera();

    private void LookAtCamera()
    {
        Vector3 worldPosition = transform.position + _camera.transform.rotation * Vector3.forward;
        Vector3 worldUp = _camera.transform.rotation * Vector3.up;
        transform.LookAt(worldPosition, worldUp);
    }

    private void OnHealthChanged(int newValue)
    {
        if (_fillingJob != null)
            StopCoroutine(_fillingJob);

        _fillingJob = StartCoroutine(Fill(newValue));
    }

    private IEnumerator Fill(int newValue)
    {
        var delay = new WaitForSeconds(_fillingDuration);

        while (_currentTextValue != newValue)
        {
            if (_currentTextValue > newValue)
                _currentTextValue--;
            else
                _currentTextValue++;

            _valueText.text = _currentTextValue.ToString();
            yield return delay;
        }
    }

    private void Validate()
    {
        if (_valueText == null)
            throw new InvalidOperationException();

        if (_stickmen == null)
            throw new InvalidOperationException();
    }
}