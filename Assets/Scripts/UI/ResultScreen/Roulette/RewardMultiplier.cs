using UnityEngine;

public class RewardMultiplier : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _value;

    public int Value => _value;
}