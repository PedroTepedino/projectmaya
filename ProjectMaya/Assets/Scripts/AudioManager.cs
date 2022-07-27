using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject managerGameObject = new GameObject("AudioManager", typeof(AudioManager));
                    instance = managerGameObject.GetComponent<AudioManager>();
                    DontDestroyOnLoad(managerGameObject);
                }
            }
            return instance;
        }

    }

    FMOD.Studio.Bus master;
    FMOD.Studio.Bus music;
    FMOD.Studio.Bus sfx;
    float masterVolume = 1f;
    float musicVolume = 1f;
    float sfxVolume = 1f;
    bool masterMute = false;
    bool musicMute = false;
    bool sfxMute = false;

     private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance == this)
        {
            Destroy(this.gameObject);
        }

        music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        sfx = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        master.setVolume(masterVolume);
        master.setMute(masterMute);
        music.setVolume(musicVolume);
        music.setMute(musicMute);
        sfx.setVolume(sfxVolume);
        sfx.setMute(sfxMute);
    }

    public void MasterVolumeLevel (float newMasterVolumeValue)
    {
        master.setVolume(newMasterVolumeValue);
    }

    public void MusicVolumeLevel (float newMusicVolumeValue)
    {
        music.setVolume(newMusicVolumeValue);
    }

    public void SFXVolumeLevel (float newSFXVolumeValue)
    {
        sfx.setVolume(newSFXVolumeValue);
    }

    public void MasterMute (bool newMasterMuteValue)
    {
        master.setMute(newMasterMuteValue);
    }

    public void MusicMute (bool newMusicMuteValue)
    {
        music.setMute(newMusicMuteValue);
    }

    public void SFXMute (bool newSFXMuteValue)
    {
        sfx.setMute(newSFXMuteValue);
    }

    
}
