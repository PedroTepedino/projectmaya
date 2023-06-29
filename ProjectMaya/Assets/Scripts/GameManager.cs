using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

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
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private LifeSystem playerLifeSystem;
    [SerializeField] private SceneTransitionManager sceneManager;
    [SerializeField] private string menuSceneName;

    /*[Header("First Button SetterS")]
    [SerializeField] private GameObject pauseFirstButton;
    [SerializeField] private GameObject optionsFirstButton;*/

    private bool isGamePaused = false;

    public static bool isBossStarted = false;
    public static bool GamepadConnected;

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

        GamepadConnected = (Gamepad.all.Count > 0);
        if (GamepadConnected)
        {
            InputSystem.DisableDevice(Keyboard.current);
            InputSystem.DisableDevice(Mouse.current);
        }
        else
        {
            InputSystem.EnableDevice(Keyboard.current);
            InputSystem.EnableDevice(Mouse.current);
        }
    }

    private void OnEnable()
    {
        playerInput.actions["Pause"].started += ListenToPauseButton;
        playerLifeSystem.OnDie += ShowDeathScreen;
    }

    private void OnDisable()
    {
        playerInput.actions["Pause"].started -= ListenToPauseButton;
        playerLifeSystem.OnDie -= ShowDeathScreen;
    }

    private void ListenToPauseButton(InputAction.CallbackContext context)
    {
        if (!context.started || sceneManager.actualSceneName == menuSceneName)
        {
            return;
        }
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
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
        print("Paused");//Edit Owner: León
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        playerInput.SwitchCurrentActionMap("Gameplay");
        print("Unpaused");//Edit Owner: León
    }

    public void GoToPause()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(pauseFirstButton);Edit Owner: León
    }

    public void GoToOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(optionsMenu);//Edit Owner: León
    }

    public void GoToMainMenu()
    {
        sceneManager.SwitchToScene(menuSceneName);
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
        playerInput.SwitchCurrentActionMap("UI");
        //EventSystem.current.SetSelectedGameObject(null);//Edit Owner: León
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        sceneManager.RestartScene();
        //EventSystem.current.SetSelectedGameObject(null);//Edit Owner: León
    }

}
