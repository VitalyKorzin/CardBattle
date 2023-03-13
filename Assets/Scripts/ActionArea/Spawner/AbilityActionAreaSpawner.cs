using UnityEngine;

public class AbilityActionAreaSpawner : MonoBehaviour
{
    [SerializeField] private AbilityActionArea _template;

    public AbilityActionArea Spawn() 
        => Instantiate(_template, transform.position, Quaternion.identity);
}