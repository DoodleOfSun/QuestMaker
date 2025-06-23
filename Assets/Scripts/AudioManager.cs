using UnityEngine;
using UnityEngine.UI;   

public class AudioManager : MonoBehaviour
{
    private AudioSource backgroundMusicSource;
    private AudioSource clickSoundSource;
    private Slider bgmSlider;
    private Slider sfxSlider;
    private float bgmVolume = 1.0f;
    private float sfxVolume = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMusicSource = GameObject.Find("AudioSource_BGM").GetComponent<AudioSource>();
        clickSoundSource = GameObject.Find("AudioSource_SFX").GetComponent<AudioSource>();

        backgroundMusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.optionPanel.activeSelf)
        {
            bgmSlider = GameObject.Find("Slider_BGM").GetComponent<Slider>();
            sfxSlider = GameObject.Find("Slider_SFX").GetComponent<Slider>();
            bgmVolume = bgmSlider.value;
            sfxVolume = sfxSlider.value;
        }

        backgroundMusicSource.volume = bgmVolume;
        clickSoundSource.volume = sfxVolume;
    }

    public void PlayClickSound()
    {
        clickSoundSource.Play();
    }
}
