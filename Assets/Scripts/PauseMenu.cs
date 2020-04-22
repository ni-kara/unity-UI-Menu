using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject optionScreen;
    public GameObject pauseScreen;

    public string mainMenuScene;

    private bool isPaused;

    public GameObject loadingScreen;
    public GameObject loadingIcon;
    public Text loadingText;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseUnpause();
    }

    public void PauseUnpause() 
    {
        if (!isPaused)
        {
            pauseScreen.SetActive(true);
            isPaused = true;

            Time.timeScale = 0f;
        }
        else 
        {
            pauseScreen.SetActive(false);
            isPaused = false;

            Time.timeScale = 1f;
        }
    }

    public void OpenOptions() =>    
        optionScreen.SetActive(true);
    
    public void CloseOptions() =>
        optionScreen.SetActive(false);
    
    public void QuitOptions() =>    
        StartCoroutine(LoadMain());    

    public IEnumerator LoadMain() 
    {
        loadingScreen.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuScene);

        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                loadingText.text = "Press any key to continue";
                loadingIcon.SetActive(false);

                if (Input.anyKeyDown)
                {
                    Time.timeScale = 1f;
                    asyncLoad.allowSceneActivation = true; 
                }
            }
            yield return null;
        }
    }
}
