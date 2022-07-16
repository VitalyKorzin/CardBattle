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
    [SerializeField] private float _lifetime;
    [SerializeField] private TMP_Text _valueText;

    private readonly float _fadingEndValue = 0f;

    private Camera _camera;
    private int _value;

    private void OnEnable()
    {
        if (_valueText == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    private void Start() => _camera = Camera.main;

    private void Update()
    {
        LookAtCamera();
        Move();
    }

    public void Initialize(int value)
    {
        _value = value;
        StartCoroutine(Display());
    }

    private void LookAtCamera()
    {
        Vector3 worldPosition = transform.position + _camera.transform.rotation * Vector3.forward;
        Vector3 worldUp = _camera.transform.rotation * Vector3.up;
        transform.LookAt(worldPosition, worldUp);
    }

    private void Move()
        => transform.Translate(_movementSpeed * Time.deltaTime * Vector3.up);

    private IEnumerator Display()
    {
        _valueText.text = _value.ToString() + "$";
        _valueText.DOFade(_fadingEndValue, _lifetime);
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }
}