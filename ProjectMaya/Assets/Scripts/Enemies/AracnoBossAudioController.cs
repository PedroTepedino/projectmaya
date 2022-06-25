using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AracnoBossAudioController : MonoBehaviour
{
    public FMODUnity.EventReference BossIdleSound;
    public FMODUnity.EventReference BossWalkSound;
    public FMODUnity.EventReference BossHoverSound;

    void PlayIdleSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(BossIdleSound, transform.position);
    }

    void PlayWalkSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(BossWalkSound, transform.position);
    }

    void PLayHoverSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(BossHoverSound, transform.position);
    }
}
