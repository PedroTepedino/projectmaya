using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject managerGameObject = new GameObject("GameManager", typeof(GameManager));
                    instance = managerGameObject.GetComponent<GameManager>();
                    DontDestroyOnLoad(managerGameObject);
                }
            }
            return instance;
        }

    }

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private SceneTransitionManager sceneManager;
    [SerializeField] private string menuSceneName;
    
    private bool isGamePaused = false;
    
    [HideInInspector] public static bool isBossStarted = false;

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance == this)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable() {
        playerInput.actions["Pause"].started += ListenToPauseButton;
    }

    private void OnDisable() {
        playerInput.actions["Pause"].started -= ListenToPauseButton;
    }

    private void ListenToPauseButton(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        if (isGamePaused)
        {
            ResumeGame();
        }else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        playerInput.SwitchCurrentActionMap("UI");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        playerInput.SwitchCurrentActionMap("Gameplay");
    }

    public void GoToOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void GoToPause()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void GoToMainMenu()
    {
        sceneManager.SwitchToScene(menuSceneName);
    }


}
