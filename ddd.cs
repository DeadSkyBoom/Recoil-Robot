using UnityEngine;
using System.Collections;

public class ddd : MonoBehaviour
{
    static ddd _instance;
    // Use this for initialization
    void Start()
    {

    }
    public static ddd instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ddd>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {

        //�˽ű��������٣�����ÿ�ν����ʼ����ʱ�����жϣ��������ظ���������
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != _instance)
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}