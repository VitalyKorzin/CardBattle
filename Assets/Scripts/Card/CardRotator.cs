using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CardRotator : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _rotationDuration;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localRotation = transform.parent.localRotation;
    }

    public void RotateOnSelection()
        => _rectTransform.DOLocalRotate(Vector3.zero, _rotationDuration);

    public void RotateOnDeselection()
        => _rectTransform.DOLocalRotate(Vector3.zero, _rotationDuration);
}