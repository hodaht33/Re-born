using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 싱글턴 개체 부모 스크립트
/// </summary>
public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    protected static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                Debug.Assert(instance != null, $"{typeof(T).Name} singleton instance has null value.");
            }
            return instance;
        }

        private set
        {

        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }
}
