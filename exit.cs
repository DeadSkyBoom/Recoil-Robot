using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exit : MonoBehaviour
{

    public void Click()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�༭״̬���˳�
#else
        Application.Quit();//���������˳�
#endif
    }
}