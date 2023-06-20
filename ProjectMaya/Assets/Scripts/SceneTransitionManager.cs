using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public Animator transitionAnimator;
    public string actualSceneName;

    public void SwitchToScene(string sceneName)
    {
        StartCoroutine(LoadAsyncSceneCoroutine(sceneName));
    }

    public void RestartScene()
    {
        StartCoroutine(LoadAsyncSceneCoroutine(SceneManager.GetActiveScene().name));
    }

    IEnumerator LoadAsyncSceneCoroutine(string sceneName)
    {
        transitionAnimator.SetTrigger("ToOut");

        yield return new WaitForSeconds(1.0f);
        Debug.Log("Load " + sceneName);

        SceneManager.LoadScene(sceneName);

        actualSceneName = SceneManager.GetActiveScene().name;

        // AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // while (!operation.isDone)
        // {
        //     Debug.Log(operation.progress);
        // }
    }

}
