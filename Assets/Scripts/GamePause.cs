using UnityEngine;
using Agava.WebUtility;

public class GamePause : MonoBehaviour
{
    private void OnEnable()
        => WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;

    private void OnDisable()
         => WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

    private void OnInBackgroundChange(bool inBackground)
    {
        AudioListener.pause = inBackground;
        Time.timeScale = inBackground ? 0 : 1;
    }
}