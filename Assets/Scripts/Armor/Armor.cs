using UnityEngine;

public abstract class Armor : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _value;

    public int Value => _value;
}