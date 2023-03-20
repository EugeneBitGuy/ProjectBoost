using System;
using UnityEngine;

namespace Rocket
{
    public class RocketAudioPlayer : MonoBehaviour
    {
        [SerializeField] private RocketCollisionHandler rocketCollisionHandler;
        [SerializeField] private RocketMovement rocketMovement;
        
        [Space]
        
        [SerializeField] private AudioClip engineSound;
        [SerializeField] private AudioClip finishClip;
        [SerializeField] private AudioClip crashClip;

        [SerializeField] private float audioFadeSpeed = 0.1f;
        

        private AudioSource _audioSource;

        private void OnEnable()
        {
            rocketCollisionHandler.OnCrash += PlayCrashSound;
            rocketCollisionHandler.OnFinish += PlayFinishSound;
            rocketCollisionHandler.OnTransition += StopAllSounds;
            rocketMovement.OnEngineStart += PlayEngineSound;
            rocketMovement.OnEngineStop += StopEngineSound;
        }

        private void OnDisable()
        {
            rocketCollisionHandler.OnCrash -= PlayCrashSound;
            rocketCollisionHandler.OnFinish -= PlayFinishSound;
            rocketCollisionHandler.OnTransition -= StopAllSounds;
            rocketMovement.OnEngineStart -= PlayEngineSound;
            rocketMovement.OnEngineStop -= StopEngineSound;
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void PlayCrashSound()
        {
            _audioSource.PlayOneShot(crashClip);
        }

        private void PlayFinishSound()
        {
            _audioSource.PlayOneShot(finishClip);
        }

        private void StopAllSounds()
        {
            _audioSource.Stop();
            _audioSource.volume = .5f;
        }
        
        private void PlayEngineSound()
        {
            _audioSource.volume = 1f;
            if(!_audioSource.isPlaying)
                _audioSource.PlayOneShot(engineSound);
        }
        
        private void StopEngineSound()
        {
            if (_audioSource.volume > 0f)
            {
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0f, Time.fixedDeltaTime * audioFadeSpeed);
            }
            else
            {
                _audioSource.Stop();
            }
        
        }
    }
}
