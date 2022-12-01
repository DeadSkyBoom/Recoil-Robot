using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar2 : MonoBehaviour
{
    public static HealthBar2 Instance { get; private set; }
    public Image mask;
    float originalSize;
    public Image flame;
    public int positionnumber;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        positionnumber = 5;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    //¸Ä±ä¿í¶È
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
        if (positionnumber > 0)
        {
            flame.transform.Translate(100, 0, 0);
            positionnumber -= 1;
        }
    }
}
