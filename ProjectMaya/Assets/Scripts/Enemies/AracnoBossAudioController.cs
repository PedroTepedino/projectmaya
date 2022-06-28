using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AracnoBossAudioController : MonoBehaviour
{
    public FMODUnity.EventReference BossIdleSound;
    public FMODUnity.EventReference BossWalkSound;
    public FMODUnity.EventReference BossHoverSound;
    public EventReference BossHitSound;

    public LifeSystem ls;

    private void Awake()
    {
        ls = this.GetComponentInParent<LifeSystem>();
    }
    private void OnEnable()
    {
        ls.OnDie += PlayHit;
    }

    private void OnDisable()
    {
        ls.OnDie -= PlayHit;
    }

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

    void PlayHit()
    {
        RuntimeManager.PlayOneShot(BossHitSound, transform.position);
    }
}
