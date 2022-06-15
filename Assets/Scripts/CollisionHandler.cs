using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    Movement mv;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashedSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;
    bool isTransitioning;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mv = GetComponent<Movement>();
        isTransitioning = false;
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys(){
        if(Input.GetKeyDown(KeyCode.L))
            LoadNextLevel();
        else if (Input.GetKeyDown(KeyCode.C))
            collisionDisabled = !collisionDisabled;
    }

    private void OnCollisionEnter(Collision other) {
        if(!isTransitioning || !collisionDisabled)
            switch(other.gameObject.tag){
                case "Friendly":
                    Debug.Log("Friendly");
                    break;
                case "Finish":
                    mv.enabled = false;
                    audioSource.PlayOneShot(successSound); 
                    isTransitioning = true;
                    successParticles.Play();
                    Invoke("StartSuccessSequence", levelLoadDelay);
                    break;

                default:
                    mv.enabled = false;
                    Invoke("ReloadLevel", levelLoadDelay);
                    crashParticles.Play();
                    audioSource.PlayOneShot(crashedSound); 
                    isTransitioning = true;
                    break;
            }
    }

    void StartSuccessSequence(){
        audioSource.Stop();
        LoadNextLevel();
    }

    void StartCrashSequence(){
        audioSource.Stop();
        ReloadLevel();
    }

    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
        
    }

    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0);

        else
            SceneManager.LoadScene(currentSceneIndex+1);
    }
}
