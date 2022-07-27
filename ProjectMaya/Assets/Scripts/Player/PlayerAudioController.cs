using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerAudioController : MonoBehaviour
{ 
    public FMODUnity.EventReference PlayerIdleSoundEvent;
    public FMODUnity.EventReference PlayerMoveSoundEvent;
    public EventReference PlayerDeathSound;

    FMOD.Studio.EventInstance PlayerIdleSound;
    FMOD.Studio.EventInstance PlayerMoveSound;


    public Rigidbody2D rb;

    public LifeSystem ls;

    private void Awake()
    {
        ls = this.GetComponent<LifeSystem>();
    }

    void Start()
    {
        PlayerIdleSound = FMODUnity.RuntimeManager.CreateInstance(PlayerIdleSoundEvent);
        PlayerMoveSound = FMODUnity.RuntimeManager.CreateInstance(PlayerMoveSoundEvent);
    }

    private void OnEnable()
    {
        ls.OnDie += PlayDeath;
    }

    private void OnDisable()
    {
        ls.OnDie -= PlayDeath;
    }

    void Update()
    {
        if (rb.velocity.magnitude > 0.5 && !IsPlaying(PlayerMoveSound))
        {
            PlayerMoveSound.start();
            PlayerIdleSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        if (rb.velocity.magnitude <= 0.5 && IsPlaying(PlayerMoveSound))
        {
            //PlayerMoveSound.release();
            PlayerIdleSound.start();
            PlayerMoveSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    void PlayDeath()
    {
        RuntimeManager.PlayOneShot(PlayerDeathSound);
    }
}
