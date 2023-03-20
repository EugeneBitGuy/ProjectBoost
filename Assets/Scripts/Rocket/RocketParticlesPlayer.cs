using System;
using UnityEngine;

namespace Rocket
{
    public class RocketParticlesPlayer : MonoBehaviour
    {
        [SerializeField] private RocketCollisionHandler rocketCollisionHandler;
        [SerializeField] private RocketMovement rocketMovement;
        
        [Space]
        
        [SerializeField] private ParticleSystem mainEngineParticles;
        [SerializeField] private ParticleSystem leftBoosterParticles;
        [SerializeField] private ParticleSystem rightBoosterParticles;
        [SerializeField] private ParticleSystem finishParticles;
        [SerializeField] private ParticleSystem crashParticles;

        private void OnEnable()
        {
            rocketCollisionHandler.OnCrash += PlayCrashParticles;
            rocketCollisionHandler.OnFinish += PlayFinishParticles;
            rocketMovement.OnEngineStart += PlayMainEngineParticles;
            rocketMovement.OnEngineStop += StopMainEngineParticles;
            rocketMovement.OnLeftBoost += PlayLeftBoosterParticles;
            rocketMovement.OnRightBoost += PlayRightBoosterParticles;
            rocketMovement.OnBoostersStop += StopBoosterParticles;
            rocketCollisionHandler.OnTransition += StopBoosterParticles;
            rocketCollisionHandler.OnTransition += StopMainEngineParticles;
        }
        
        private void OnDisable()
        {
            rocketCollisionHandler.OnCrash -= PlayCrashParticles;
            rocketCollisionHandler.OnFinish -= PlayFinishParticles;
            rocketMovement.OnEngineStart -= PlayMainEngineParticles;
            rocketMovement.OnEngineStop -= StopMainEngineParticles;
            rocketMovement.OnLeftBoost -= PlayLeftBoosterParticles;
            rocketMovement.OnRightBoost -= PlayRightBoosterParticles;
            rocketMovement.OnBoostersStop -= StopBoosterParticles;
            rocketCollisionHandler.OnTransition -= StopBoosterParticles;
            rocketCollisionHandler.OnTransition -= StopMainEngineParticles;
        }

        private void StopBoosterParticles()
        {
            leftBoosterParticles.Stop();
            rightBoosterParticles.Stop();
        }

        private void PlayRightBoosterParticles()
        {
            leftBoosterParticles.Stop();
            if(!rightBoosterParticles.isPlaying)
                rightBoosterParticles.Play();
        }

        private void PlayLeftBoosterParticles()
        {
            rightBoosterParticles.Stop();
            if(!leftBoosterParticles.isPlaying)
                leftBoosterParticles.Play();
        }

        private void StopMainEngineParticles()
        {
            
            mainEngineParticles.Stop();
        }

        private void PlayMainEngineParticles()
        {
            if(!mainEngineParticles.isPlaying)
                mainEngineParticles.Play();
        }

        private void PlayFinishParticles()
        {
            finishParticles.Play();
        }

        private void PlayCrashParticles()
        {
            crashParticles.Play();
        }
    }
}