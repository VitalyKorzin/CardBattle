using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RewardReceivingDisplay : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _movementSpeed;
    [Min(0)]
    [SerializeField] private float _appearanceDuration;
    [Min(0)]
    [SerializeField] private float _deleyBeforeFading;
    [Min(0)]
    [SerializeField] private float _fadingDuration;
    [SerializeField] private TMP_Text _valueText;

    private readonly float _fadingEndValue = 0f;
    private readonly float _appearanceEndValue = 1f;
    private readonly string _currencySymbol = "$";

    private Camera _camera;
    private float _lifetime;

    private void OnEnable()
    {
        if (_valueText == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }
    
    private void Awake()
    {
        _camera = Camera.main;
        _lifetime = _appearanceDuration + _fadingDuration + _deleyBeforeFading;
    }

    private void Update()
    {
        LookAtCamera();
        Move();
    }

    public void Initialize(int value) 
        => StartCoroutine(Display(value));

    private void LookAtCamera()
    {
        Vector3 worldPosition = transform.position + _camera.transform.rotation * Vector3.forward;
        Vector3 worldUp = _camera.transform.rotation * Vector3.up;
        transform.LookAt(worldPosition, worldUp);
    }

    private void Move()
        => transform.Translate(_movementSpeed * Time.deltaTime * Vector3.up);

    private IEnumerator Display(int value)
    {
        _valueText.text = value.ToString() + _currencySymbol;
        _valueText.DOFade(_appearanceEndValue, _appearanceDuration);
        _valueText.DOFade(_fadingEndValue, _fadingDuration).SetDelay(_deleyBeforeFading);
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }
}