using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AccessTimer))]
public class PlayerAbility : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private MonoBehaviour _valueBehaviour;
    [SerializeField] private AbilityActionAreaSpawner _areaSpawner;
    [SerializeField] private Image _icon;

    private IPlayerAbility _value;
    private AccessTimer _accessTimer;
    private AbilityActionArea _actionArea;
    private bool _isSelected;
    private Ray _ray;
    private RaycastHit[] _hits;
    private Camera _camera;

    public event UnityAction Used;

    private void OnValidate()
    {
        if (_valueBehaviour && !(_valueBehaviour is IPlayerAbility))
        {
            Debug.LogError(nameof(_valueBehaviour) + " needs to implement " + nameof(IPlayerAbility));
            _valueBehaviour = null;
        }
    }

    private void OnDisable()
    {
        if (_actionArea != null)
            Destroy(_actionArea.gameObject);
    }

    private void Awake()
    {
        _value = (IPlayerAbility)_valueBehaviour;
        _accessTimer = GetComponent<AccessTimer>();
        _camera = Camera.main;
        _icon.sprite = _value.Icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isSelected)
            return;

        if (_accessTimer.Ready)
        {
            _actionArea = _areaSpawner.Spawn();
            _isSelected = true;
            Used?.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isSelected == false)
            return;

        _ray = _camera.ScreenPointToRay(eventData.position);
        _hits = Physics.RaycastAll(_ray);

        foreach (var hit in _hits)
        {
            if (hit.collider.TryGetComponent(out PlayerAbility _))
            {
                Destroy(_actionArea.gameObject);
                _isSelected = false;
                return;
            }
        }

        _actionArea.Use(_value);
        Destroy(_actionArea.gameObject);
        _accessTimer.StartCountingDown();
        _isSelected = false;
    }
}