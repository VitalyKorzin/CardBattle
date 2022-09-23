using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RaycastTarget))]
public class Menu : MonoBehaviour, IPointerClickHandler
{
    private RaycastTarget _raycastTarget;

    public event UnityAction GameBegun;

    private void Awake() 
        => _raycastTarget = GetComponent<RaycastTarget>();

    public void OnPointerClick(PointerEventData eventData)
    {
        GameBegun?.Invoke();
        _raycastTarget.raycastTarget = false;
    }
}