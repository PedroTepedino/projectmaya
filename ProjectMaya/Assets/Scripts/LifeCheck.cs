using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeCheck : MonoBehaviour
{
    [SerializeField] private LifeSystem _lifeSystemToListen;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Update() {
        textMesh.text = _lifeSystemToListen._currentLife.ToString();
    }
}
