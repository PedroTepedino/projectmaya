using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC_Interaction : MonoBehaviour
{
    
    public bool interact;
    private TextMeshPro tMesh;
    public TextAsset npcInkJSON;
    [SerializeField] private GameObject dialogManager;
    [Header("NPC Dialog Variables")]
    [SerializeField] private GameObject npcUI;
    [SerializeField] private GameObject npcPortrait;
    
    // Start is called before the first frame update
    void Start()
    {
        tMesh = GetComponentInChildren<TextMeshPro>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            tMesh.text = "Q/Gamepad B";
            interact = true;
            dialogManager.GetComponent<DialogManager>().targetNPC = this.gameObject;
            dialogManager.GetComponent<DialogManager>().targetNPCInk = npcInkJSON;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            tMesh.text = "";
            interact = false;
            dialogManager.GetComponent<DialogManager>().targetNPC = null;
            dialogManager.GetComponent<DialogManager>().targetNPCInk = null;
        }
    }
}
