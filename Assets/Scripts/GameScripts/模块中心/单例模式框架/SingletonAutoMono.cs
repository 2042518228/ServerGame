using UnityEngine;

public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();

                        if (_instance == null)
                        {
                            GameObject obj = new GameObject(typeof(T).Name);
                            _instance = obj.AddComponent<T>();
                           DontDestroyOnLoad(obj);
                        }
                    }
                }
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
}