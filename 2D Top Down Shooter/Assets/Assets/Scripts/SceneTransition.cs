using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [Header("Loading Parameters")]
    public TextMeshProUGUI loadingPercent;
    public Image loadingProgressBar;


    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false;

    private Animator anim;
    private AsyncOperation loadingSceneOperation;


    public static void SwitchToScene(string sceneName)
    {
        instance.anim.SetTrigger("sceneClosing");

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadingSceneOperation.allowSceneActivation = false;
    }

    private void Start()
    {
        instance = this;

        anim = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation) anim.SetTrigger("sceneOpening");
    }

    private void Update()
    {
        if (loadingSceneOperation != null)
        {
            loadingPercent.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";
            loadingProgressBar.fillAmount = loadingSceneOperation.progress;
        }
    }

    public void OnAnimationOver()
    {
        shouldPlayOpeningAnimation = true;
        instance.loadingSceneOperation.allowSceneActivation = true;
    }
}
