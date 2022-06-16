using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public Animator transitionAnimator;

    public void SwitchToScene(string sceneName)
    {
        StartCoroutine(LoadAsyncSceneCoroutine(sceneName));
    }

    IEnumerator LoadAsyncSceneCoroutine(string sceneName)
    {
        transitionAnimator.SetTrigger("ToOut");

        yield return new WaitForSeconds(1.0f);
        Debug.Log("Load "+sceneName);

        SceneManager.LoadScene(sceneName);

        // AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        // while (!operation.isDone)
        // {
        //     Debug.Log(operation.progress);
        // }
    }

}
