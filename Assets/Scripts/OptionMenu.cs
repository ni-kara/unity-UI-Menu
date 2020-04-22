using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Toggle fullscreen;
    public Toggle vsync;

    [System.Serializable]
    public struct ResItem {
        public int horizontal;
        public int vertical;
    };
    public ResItem[] resolutions;

    public Text resLabel;

    private int selectedResolution;

    public AudioMixer mixer;
    
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public Text masterLabel;
    public Text musicLabel;
    public Text sfxLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        //load system properties
        fullscreen.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
            vsync.isOn = false;
        else
            vsync.isOn = true;

        // search for resolution in list
        bool foundRes = false;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true; 
                selectedResolution = i;
                UpdateResolutionLabel();
            }      
        }

        if (!foundRes)        
            resLabel.text = Screen.width + " X " + Screen.height;

        if (PlayerPrefs.HasKey("MasterVol"))
        {
            mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            masterSlider.value = PlayerPrefs.GetFloat("MasterVol");
            masterLabel.text = (masterSlider.value + 80).ToString();
        }

        if (PlayerPrefs.HasKey("MusicVol"))
        {
            mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
            musicLabel.text = (musicSlider.value + 80).ToString();
        }

        if (PlayerPrefs.HasKey("SFXVol"))
        {
            mixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVol");
            sfxLabel.text = (sfxSlider.value + 80).ToString();
        }
    }

    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0)
            selectedResolution = 0;

        UpdateResolutionLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Length - 1)
            selectedResolution = resolutions.Length-1;

        UpdateResolutionLabel();
    }

    private void UpdateResolutionLabel() =>
        resLabel.text = resolutions[selectedResolution].horizontal + " X " + resolutions[selectedResolution].vertical;
    
    public void ApplyGraphics(){
        
        if(vsync.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        //set resolution
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreen.isOn);
    }

    public void SetMasterVol() 
    {
        masterLabel.text = (masterSlider.value+80).ToString();
        mixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }

    public void SetMusicVol()
    {
        musicLabel.text = (musicSlider.value + 80).ToString();
        mixer.SetFloat("MusicVol", musicSlider.value);
        
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void SetSFXVol()
    {
        sfxLabel.text = (sfxSlider.value + 80).ToString();
        mixer.SetFloat("SFXVol", sfxSlider.value);
        
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }
}
