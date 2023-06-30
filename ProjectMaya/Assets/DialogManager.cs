using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Ink.Runtime;

public class DialogManager : MonoBehaviour
{
    private bool dialogRunning = false;
    private Story currentStory;
    public GameObject targetNPC;
    public TextAsset targetNPCInk;
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

    private void Update()
    {
        
    }

    private void ListenToDialogButton(InputAction.CallbackContext context)
    {
        if (targetNPC!=null && targetNPC.GetComponent<NPC_Interaction>().interact == true)
        {
            if (!isGamePaused && !dialogRunning)
            {
                PauseInGame();
                SetDialogUI();
                StartDialog(targetNPCInk);
            }
            else if (isGamePaused && dialogRunning)
            {
                RunDialog();
            }
            else if(isGamePaused && dialogRunning==false) {ResumeInGame();}
        }else { return; }
    }
    public void PauseInGame()
    {
        
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeInGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    private void SetDialogUI()
    {
        dialogRunning = true;
        dialogUI.SetActive(true);
    }

    public void StartDialog(TextAsset npcInk)
    {
        currentStory = new Story(npcInk.text);
    }

    public void RunDialog()
    {
        if (currentStory.canContinue)
        {
            dialogTextBox.text = currentStory.Continue();
        }
        else { EndDialog(); }
    }

    public void EndDialog()
    {
        dialogRunning = false;
        dialogUI.SetActive(false);
    }

}
