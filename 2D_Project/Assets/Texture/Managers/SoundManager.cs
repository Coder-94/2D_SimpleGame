using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager  instance;
    public AudioMixer           mixer;
    public Slider               slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //음량조절
    public void SetBGM(float sliderValue)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.Save();
    }
    public void SetSE(float sliderValue)
    {
        mixer.SetFloat("SE", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.Save();
    }
    public void SetMaster(float sliderValue)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.Save();
    }
}
