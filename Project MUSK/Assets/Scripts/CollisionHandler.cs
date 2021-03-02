using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] float delay = 1f;

    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] ParticleSystem successEffect;

    AudioSource audioSrc;

    int currentScene;
    int sceneCount;
    bool isTransitioning = false;

    

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        sceneCount = SceneManager.sceneCountInBuildSettings;
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }

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
        isTransitioning = true;
        audioSrc.Stop();
        audioSrc.PlayOneShot(successSound);
        successEffect.Play();
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
        isTransitioning = true;
        audioSrc.Stop();
        audioSrc.PlayOneShot(crashSound);
        crashEffect.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delay);
    }
}
