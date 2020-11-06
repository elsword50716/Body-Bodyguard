using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    AsyncOperation async;
    string nextSceneName;

    void Start()
    {
        nextSceneName = GameDataManager.nextSceneName;
        StartCoroutine(SceneLoad());
    }


    void Update()
    {
        
        // if(Keyboard.current[Key.Space].wasPressedThisFrame){
        //     async.allowSceneActivation = true;
        // }
    }

    IEnumerator SceneLoad()
    {
        int displayProgress = 0;
        int toProgress = 0;
        async = SceneManager.LoadSceneAsync($"{nextSceneName}");

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            toProgress = (int)async.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                progressBar.value = displayProgress;
                progressText.SetText($"Loading...{displayProgress}%");
                yield return new WaitForEndOfFrame();
            }

        }

        toProgress = 100;
        while(displayProgress < toProgress){
            ++displayProgress;
            progressBar.value = displayProgress;
            progressText.SetText($"Loading...{displayProgress}%");
            yield return new WaitForEndOfFrame();
        }
        async.allowSceneActivation = true;//先load純場景
        //SceneManager.LoadSceneAsync($"{nextSceneName}_Scene_Functions", LoadSceneMode.Additive);//在load功能
    }
}
