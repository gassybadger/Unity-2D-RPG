using UnityEngine;

public class Singleton<TInstance> : MonoBehaviour
    where TInstance : Singleton<TInstance>
{
    private static TInstance _instance;

    public static TInstance Instance => _instance;


    protected virtual void Awake()
    {
        if (_instance != null && gameObject != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = (TInstance)this;
        DontDestroyOnLoad(gameObject);
        Debug.Log($"{gameObject} Loaded");
    }
}
