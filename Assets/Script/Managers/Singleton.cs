using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _instance;

    public static T Instnace
    {
        get 
        {
            if (_instance == null)
            { 
                // 하이어러키창에 있는거중에 찾기 
                _instance = FindObjectOfType<T>();

                // 찾았는데도 없으면 ? 새로 만들기 
                if (_instance == null) 
                {
                    // T 타입으로 오브젝트 생성 , 이름도 T로 
                    GameObject _obj = new GameObject(typeof(T).Name, typeof(T));
                    _instance = _obj.AddComponent<T>();
                }
            }
            return _instance;
        }
        
    }

    // 하위에서 실행할 awake 동작
    protected abstract void Singleton_Awake();

    private void Awake()
    {
        // 하위의 재정의된 awake동작 실행
        Singleton_Awake();

        // dontdestoryOnLoad 설정 
        // F_SettingDontDestroy();
    }

    // DontDestory할 애들만 하위의 awake에 넣어주기 
    protected void F_SettingDontDestroy() 
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
