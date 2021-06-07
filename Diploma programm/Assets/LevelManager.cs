using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider loadingSlider;
    public Text loadingText;

    private float loadingWait;



	public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));

    }
	
    IEnumerator LoadAsynchronously (string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);
     
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            
            loadingSlider.value = progress;
            loadingText.text = progress * 100f + "%";
            Debug.Log(progress);
            StartCoroutine(WaitForSecondsSceneLoading());
            yield return null;
        }
    }

    private IEnumerator WaitForSecondsSceneLoading()
    {
        yield return new WaitForSecondsRealtime(10);    
    }
}
