using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class CardView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _selectionOffset = 2f;

    private RectTransform _rectTransform;
    private Coroutine _movementJob;
    private Coroutine _rotationJob;

    public event UnityAction<CardView> Destroyed;
    public event UnityAction<CardView> Selected;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        float targetPositionX = _rectTransform.localPosition.x + _selectionOffset;
        float targetPositionY = _rectTransform.localPosition.y + _selectionOffset;
        var targetPosition = new Vector2(targetPositionX, targetPositionY);
        StartMove(targetPosition);
        Selected?.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }

    public void StartMove(Vector3 targetPosition)
    {
        if (_movementJob != null)
            StopCoroutine(_movementJob);

        _movementJob = StartCoroutine(Move(targetPosition));
    }

    public void StartRotate(float angle)
    {
        if (_rotationJob != null)
            StopCoroutine(_rotationJob);

        _rotationJob = StartCoroutine(Rotate(angle));
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        while (_rectTransform.localPosition != targetPosition)
        {
            _rectTransform.localPosition = Vector3.Lerp(_rectTransform.localPosition, targetPosition, _movementSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Rotate(float angle)
    {
        while (_rectTransform.localRotation.z != angle)
        {
            _rectTransform.localRotation = Quaternion.Lerp(_rectTransform.localRotation, Quaternion.Euler(0, 0, angle), _rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}