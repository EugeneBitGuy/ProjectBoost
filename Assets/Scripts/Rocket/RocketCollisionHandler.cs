using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rocket
{
    public class RocketCollisionHandler : MonoBehaviour
    {
        public Action OnCrash;
        public Action OnFinish;
        public Action OnTransition;
        
        [SerializeField] private float sceneLoadingDelay = 2f;

        private int _currentSceneIndex;
        private bool _isTransitioning;
        void Start()
        {
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
            
            OnCrash?.Invoke();

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
            
            OnFinish?.Invoke();
        
            Invoke(nameof(LoadNextLevel), sceneLoadingDelay);
        }

        private void ProcessTransitioning()
        {
            _isTransitioning = true;
            OnTransition.Invoke();
        }

        void LoadNextLevel()
        {
            int nextSceneIndex = _currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
                nextSceneIndex = 0;
        
            SceneManager.LoadScene(nextSceneIndex);
        }
    
    }
}
