using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DemoManager : MonoBehaviour
{
    private static DemoManager instance = null;
    public static DemoManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DemoManager>();
                if (instance == null)
                {
                    GameObject managerDemoObject = new GameObject("DemoManager", typeof(DemoManager));
                    instance = managerDemoObject.GetComponent<DemoManager>();
                }
            }
            return instance;
        }

    }


    [SerializeField] private GameObject winScreen;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private LifeSystem bossLifeSystem;
    [SerializeField] private SceneTransitionManager sceneManager;
    [SerializeField] private string menuSceneName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        bossLifeSystem.OnDie += ShowWinScreen;
    }

    private void OnDisable()
    {
        bossLifeSystem.OnDie -= ShowWinScreen;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        winScreen.SetActive(false);
        sceneManager.SwitchToScene(menuSceneName);
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        playerInput.SwitchCurrentActionMap("UI");
    }
}
