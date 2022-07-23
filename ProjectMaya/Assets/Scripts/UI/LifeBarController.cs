using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


public class LifeBarController : MonoBehaviour
{
    [SerializeField]
    private LifeSystem lifeSystemToListen;
    
    [SerializeField] [BoxGroup("Components")] [Required]
    private Slider barFillImage;

    [SerializeField] [BoxGroup("Components")]
    private Slider barFillDecay;

    [SerializeField] [BoxGroup("Decay")] private float decaySpeed = 0.5f;
    [SerializeField] [BoxGroup("Decay")] private float decayDelay = 0.75f;

    private float curentFill = 1f;
    private Tween decayBarAnimation;

    private void OnEnable() {
        lifeSystemToListen.OnChangeLife += ListenLifeChange;
    }

    private void OnDisable() {
        lifeSystemToListen.OnChangeLife -= ListenLifeChange;
    }

    private void ListenLifeChange()
    {
        curentFill = lifeSystemToListen._currentLife;

        StartCoroutine(LifeBarChange());
    }

    private IEnumerator LifeBarChange()
    {
        barFillImage.value = curentFill;

        yield return new WaitForSeconds(decayDelay);

        barFillDecay.value = curentFill;
    }
}