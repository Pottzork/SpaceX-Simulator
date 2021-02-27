using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentScene;
    int sceneCount;
    [SerializeField]
    float delay = 1f;
    

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Here is safe to crash");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", delay);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    private void LoadNextScene()
    {
        int firstScene = 0;
        int nextSceneIndex = currentScene + 1;
        
        if (nextSceneIndex == sceneCount)
        {
            SceneManager.LoadScene(firstScene);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
       
    }

    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delay);

    }
}
