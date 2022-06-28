using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player"))
        {
            GameManager.isBossStarted = !GameManager.isBossStarted;
        }
    }
}
