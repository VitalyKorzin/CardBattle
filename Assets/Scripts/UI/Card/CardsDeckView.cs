using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CardsDeckView : MonoBehaviour
{
    [SerializeField] private int _maximumCount = 5;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private float _degree = 90f;

    private readonly float _offsetSum = 0.5f;
    private List<Vector3> _targetCardsPositions = new List<Vector3>();
    private List<CardView> _cards = new List<CardView>();
    private RectTransform _rectTransform;
    private Coroutine _rotationJob;
    private float _degreeStep;
    private float _offsetX = 1f;
    private float _offsetY = 0f;
    private float _currentRotationAngle;
    private float _targetRotationAngle;
    private float _angleStep;
    private float _selectedCardAngle;

    public int MaximumCount => _maximumCount;

    private void Awake()
    {
        _degreeStep = _degree / _maximumCount;
        _targetRotationAngle = _degree / 2f;
        _selectedCardAngle = -_targetRotationAngle;
        _currentRotationAngle = -(_targetRotationAngle / (MaximumCount - 1));
        _angleStep = -_currentRotationAngle;
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.rotation = Quaternion.Euler(0, 0, _currentRotationAngle);
        SetTargetCardsPositions(_maximumCount);
    }

    public void Add(CardView card)
    {
        card.Destroyed += OnCardDestroyed;
        card.Selected += OnCardSelected;
        _cards.Add(card);
        StartRotate(_angleStep);
        card.StartMove(_targetCardsPositions[_targetCardsPositions.Count - (_cards.Count - 1) - 1]);
        card.StartRotate(-(GetCardAngle(_maximumCount) * _cards.Count));
    }

    private void OnCardSelected(CardView card)
    {
        card.Selected -= OnCardSelected;
        card.StartRotate(_selectedCardAngle);
    }

    private void OnCardDestroyed(CardView card)
    {
        card.Destroyed -= OnCardDestroyed;
        _cards.Remove(card);
        _degree -= _degreeStep;
        _offsetX += _offsetSum;
        _offsetY += _offsetSum;
        _targetCardsPositions.Clear();
        SetTargetCardsPositions(_cards.Count);

        for (var i = 0; i < _cards.Count; i++)
        {
            _cards[i].StartMove(_targetCardsPositions[_targetCardsPositions.Count - i - 1]);
            _cards[i].StartRotate(GetCardAngle(_cards.Count) * (i + 1) * -1f);
        }
    }

    private void StartRotate(float angleStep)
    {
        _currentRotationAngle += angleStep;

        if (_rotationJob != null)
            StopCoroutine(_rotationJob);

        _rotationJob = StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (_rectTransform.rotation.z != _currentRotationAngle)
        {
            _rectTransform.rotation = Quaternion.Lerp(_rectTransform.rotation, Quaternion.Euler(0, 0, _currentRotationAngle), _rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void SetTargetCardsPositions(int count)
    {
        for (var i = 0; i < count; i++)
        {
            float positionX = _radius * Mathf.Cos(_degree * Mathf.Deg2Rad / count * (i + _offsetX));
            float positionY = _radius * Mathf.Sin(_degree * Mathf.Deg2Rad / count * (i + _offsetY));
            _targetCardsPositions.Add(new Vector3(positionX, positionY));
        }
    }

    private float GetCardAngle(int cardsCount) 
        => _targetRotationAngle / ((cardsCount / 2f) + _offsetSum);
}