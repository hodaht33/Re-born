using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 싱글턴 개체 부모 스크립트
/// </summary>
public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                Debug.Assert(instance != null, $"{typeof(T).Name}타입인스턴스가 널값을 가짐");
            }

            return instance;
        }

        private set
        {

        }
    }
    
    /* 상속받는 자식에 복붙
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    */
}
