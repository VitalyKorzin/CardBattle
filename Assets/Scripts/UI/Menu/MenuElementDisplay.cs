using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class MenuElementDisplay : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Menu _menu;

    private RectTransform _rectTransform;

    private void OnEnable()
        => _menu.GameBegun += OnGameBegun;

    private void OnDisable()
        => _menu.GameBegun -= OnGameBegun;

    private void Awake()
        => _rectTransform = GetComponent<RectTransform>();

    private void OnGameBegun()
        => _rectTransform.DOAnchorPos(_targetPosition, _duration);
}