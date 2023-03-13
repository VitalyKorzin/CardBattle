using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    private delegate T LoadValue<T>(string key);

    protected void SaveIntegerValue(string key, int value)
        => PlayerPrefs.SetInt(key, value);

    protected void SaveStringValue(string key, string value)
        => PlayerPrefs.SetString(key, value);

    protected bool TryLoadIntegerValue(string key, out int result)
        => TryLoadValue(PlayerPrefs.HasKey(key), key, LoadIntValue, out result);

    protected bool TryLoadStringValue(string key, out string result)
        => TryLoadValue(PlayerPrefs.HasKey(key), key, LoadStringValue, out result);

    private bool TryLoadValue<T>(bool condition, string key, LoadValue<T> valueLoading, out T result)
    {
        result = default(T);

        if (condition)
            result = valueLoading(key);

        return condition;
    }

    private int LoadIntValue(string key)
        => PlayerPrefs.GetInt(key);

    private string LoadStringValue(string key)
        => PlayerPrefs.GetString(key);
}