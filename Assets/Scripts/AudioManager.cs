using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip gameOver;
    private AudioSource audioSource;

    public AudioMixer mainMixer;

    [Header("Sliders")]
    public Slider sliderVolMaster;
    public Slider sliderVolMusic;
    public Slider sliderVolSortilege;
    public Slider sliderVolEnemy;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        sliderSetup(sliderVolMaster);
        sliderSetup(sliderVolMusic);
        sliderSetup(sliderVolSortilege);
        sliderSetup(sliderVolEnemy);

        sliderVolMaster.onValueChanged.AddListener(sliderVolMaster_onValueChanged);
        sliderVolMusic.onValueChanged.AddListener(sliderVolMusic_onValueChanged);
        sliderVolSortilege.onValueChanged.AddListener(sliderVolSortilege_onValueChanged);
        sliderVolEnemy.onValueChanged.AddListener(sliderVolEnemy_onValueChanged);
    }


    void sliderSetup(Slider volumeSlider)
    {
        volumeSlider.minValue = 0.001f;
        volumeSlider.maxValue = 1.6f;

        volumeSlider.value = 1f;
    }


    void sliderVolMaster_onValueChanged(float value)
    {
        setVolume("volMain", value);
    }

    void sliderVolMusic_onValueChanged(float value)
    {
        setVolume("volMusic", value);
    }

    void sliderVolSortilege_onValueChanged(float value)
    {
        setVolume("volSortilege", value);
    }

    void sliderVolEnemy_onValueChanged(float value)
    {
        setVolume("volEnemies", value);
    }

    void setVolume(string paramName, float value)
    {
        // Modifier le volume du group Music
        mainMixer.SetFloat(paramName, Mathf.Log(value) * 20f);
    }

    public void GameOverSound()
    {
        audioSource.PlayOneShot(gameOver);
    }
}
