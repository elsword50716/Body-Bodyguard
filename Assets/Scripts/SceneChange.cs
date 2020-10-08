using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Slider progressBar;

    AsyncOperation async;
    float progressValue = 0;
    string nextSceneName;

    void Start()
    {
        nextSceneName = GameDataManager.nextSceneName;
        StartCoroutine(SceneLoad());
    }

    
    void Update()
    {
        progressBar.value = progressValue;
    }

    IEnumerator SceneLoad()
    {
        async = SceneManager.LoadSceneAsync($"{nextSceneName}");

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            while (progressValue < async.progress)
            {
                ++progressValue;
                yield return new WaitForEndOfFrame();
            }

        }

        progressValue = 1;
        async.allowSceneActivation = true;//先load純場景
        //SceneManager.LoadSceneAsync($"{nextSceneName}_Scene_Functions", LoadSceneMode.Additive);//在load功能
    }
}
