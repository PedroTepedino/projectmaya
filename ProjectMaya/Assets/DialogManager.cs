using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public GameObject targetNPC = null;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private TextMeshProUGUI dialogTextBox;   
    [SerializeField] private bool isGamePaused;
    private void OnEnable()
    {
        playerInput.actions["Interact"].started += ListenToDialogButton;
    }

    private void OnDisable()
    {
        playerInput.actions["Interact"].started -= ListenToDialogButton; 
    }

    private void ListenToDialogButton(InputAction.CallbackContext context)
    {
        if (targetNPC!=null && targetNPC.GetComponent<NPC_Interaction>().interact == true)
        {
            if (isGamePaused){ResumeInGame();}
            else if (!isGamePaused){PauseInGame();}
        }else { return; }
    }
    public void PauseInGame()
    {
        dialogUI.SetActive(true);
        SetDialogText();
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeInGame()
    {
        dialogUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    private void SetDialogText()
    {
        dialogTextBox.text = targetNPC.GetComponent<NPC_Interaction>().desiredText.text;
    }
}
