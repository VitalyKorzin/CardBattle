using UnityEngine;
using DG.Tweening;

public class PointerRotator : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _delayBeforeRotation;
    [Min(0)]
    [SerializeField] private float _rotationDuration;
    [SerializeField] private Vector3 _targetRotation;

    private void OnEnable() 
        => Rotate();

    public void Rotate() 
        => GetComponent<RectTransform>().DOLocalRotate(_targetRotation, _rotationDuration).SetDelay(_delayBeforeRotation);
}