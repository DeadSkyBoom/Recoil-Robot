using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music_control : MonoBehaviour
{
    public AudioSource music;
    public Slider slider;
    
    // Start is called before the first frame update
    private void Start()
    {
        music.volume = PlayerPrefs.GetFloat("Volume");
        slider.value = music.volume;
    }

    // Update is called once per frame
   
    void Update()
    {

    }
    public void con_sound()
    {
        music.volume = slider.value;
        PlayerPrefs.SetFloat("Volume", slider.value);
    }
}
