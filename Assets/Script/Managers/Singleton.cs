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
                // ���̾Űâ�� �ִ°��߿� ã�� 
                _instance = FindObjectOfType<T>();

                // ã�Ҵµ��� ������ ? ���� ����� 
                if (_instance == null) 
                {
                    // T Ÿ������ ������Ʈ ���� , �̸��� T�� 
                    GameObject _obj = new GameObject(typeof(T).Name, typeof(T));
                    _instance = _obj.AddComponent<T>();
                }
            }
            return _instance;
        }
        
    }

    // �������� ������ awake ����
    protected abstract void Singleton_Awake();

    private void Awake()
    {
        // ������ �����ǵ� awake���� ����
        Singleton_Awake();

        // dontdestoryOnLoad ���� 
        // F_SettingDontDestroy();
    }

    // DontDestory�� �ֵ鸸 ������ awake�� �־��ֱ� 
    protected void F_SettingDontDestroy() 
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
