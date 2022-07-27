using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioTrigger : MonoBehaviour
{
    public FMODUnity.EventReference Confirm;
    public FMODUnity.EventReference TabConfirm;
    public FMODUnity.EventReference Select;

    public void PlayButtonHover()
    {
        //FMODUnity.RuntimeManager.PlayOneShot(Select);
    }

    public void PlayButtonClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Confirm);
    }

    public void PlayTabClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(TabConfirm);
    }

}
