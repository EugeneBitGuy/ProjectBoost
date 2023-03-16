using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float sceneLoadingDelay = 2f;
    [SerializeField] private AudioClip finishClip;
    [SerializeField] private AudioClip crashClip;

    [SerializeField] private ParticleSystem finishParticles;
    [SerializeField] private ParticleSystem crashParticles;
    
    
    private AudioSource _audioSource;
    private Mover _moverComponent;
    private int _currentSceneIndex;

    private bool _isTransitioning;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _moverComponent = GetComponent<Mover>();
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_isTransitioning)
            return;
        
        switch (collision.gameObject.tag)
        {
            case "Finish":
                ProcessCompletion();
                break;
            case "Friendly":
                break;
            default:
                ProcessCrash();
                break;
        }
    }
    private void ProcessCrash()
    {
        ProcessTransitioning();
        _audioSource.PlayOneShot(crashClip);
        crashParticles.Play();

        //todo vfx
        Invoke(nameof(Respawn), sceneLoadingDelay);
    }

    void Respawn()
    {
        SceneManager.LoadScene(_currentSceneIndex);
    }

    private void ProcessCompletion()
    {
        ProcessTransitioning();
        _audioSource.PlayOneShot(finishClip);
        finishParticles.Play();
        
        //todo vfx
        Invoke(nameof(LoadNextLevel), sceneLoadingDelay);
    }

    private void ProcessTransitioning()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.volume = .5f;
        _moverComponent.KillAllParticles();
        _moverComponent.enabled = false;
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = _currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        
        SceneManager.LoadScene(nextSceneIndex);
    }
    
}
