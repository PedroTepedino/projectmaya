using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{ 
    public FMODUnity.EventReference PlayerDashSound;
    public FMODUnity.EventReference PlayerHitSound;
    public FMODUnity.EventReference PlayerIdleSoundEvent;
    public FMODUnity.EventReference PlayerMoveSoundEvent;

    FMOD.Studio.EventInstance PlayerIdleSound;
    FMOD.Studio.EventInstance PlayerMoveSound;

    public Rigidbody2D rb;

    void Start()
    {
        PlayerIdleSound = FMODUnity.RuntimeManager.CreateInstance(PlayerIdleSoundEvent);
        PlayerMoveSound = FMODUnity.RuntimeManager.CreateInstance(PlayerMoveSoundEvent);
    }

    void Update()
    {
        if (rb.velocity.magnitude > 0 && !IsPlaying(PlayerMoveSound))
        {
            PlayerMoveSound.start();
            PlayerIdleSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        if (rb.velocity.magnitude == 0 && IsPlaying(PlayerMoveSound))
        {
            //PlayerMoveSound.release();
            PlayerIdleSound.start();
            PlayerMoveSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    void PlayDashSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(PlayerDashSound, transform.position);
    }

    void PlayHitSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(PlayerHitSound, transform.position);
    }

    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
