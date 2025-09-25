using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UIElements;

public class  Option_music : MonoBehaviour
{
    public GameObject optionPanel;
    public UnityEngine.UI.Image slider;
    public UnityEngine.UI.Slider sound;
    float volume;
    public UnityEngine.UI.Image[] mute;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sound.value = 1f;
        optionPanel.SetActive(false);
        sound.onValueChanged.AddListener(UpdateVolumeUI);
        UpdateVolumeUI(sound.value);
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        sound.value = savedVolume;
        UpdateVolumeUI(savedVolume);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void UpdateVolumeUI(float value)
    {
        volume = value;
        slider.fillAmount = volume;
        mute[0].gameObject.SetActive(volume == 0f);
        mute[1].gameObject.SetActive(volume != 0f);
        MusicManager.Instance.SetVolume(volume);
        PlayerPrefs.SetFloat("volume", volume);
    }
    public void ToggelActive()
    {
        if (optionPanel != null)
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
        }
    }
    public void MuteSound()
    {
        if (volume == 0f)
        {
            sound.value = 1f;

            slider.fillAmount = 1f;

        }
        else
        {
            sound.value = 0f;

            slider.fillAmount = 0f;
        }
    }
}
