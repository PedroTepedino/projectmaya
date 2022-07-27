using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationAudioController : MonoBehaviour
{
    public FMODUnity.EventReference PlayerDashSound;
    public FMODUnity.EventReference PlayerHitSound;
    void PlayDashSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(PlayerDashSound);
    }

    void PlayHitSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(PlayerHitSound);
    }
}
