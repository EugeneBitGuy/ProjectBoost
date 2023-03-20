using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class UIManager : MonoBehaviour
{
        [SerializeField] private bool allowPause;
        [SerializeField] private List<UIPage> pages = null;
        [SerializeField] private int pausePageIndex;
        [SerializeField] private int clearPageIndex;
        [SerializeField] private int crashPageIndex;
        
        private List<UIElement> _elements;
        private bool isPaused;

        private void OnEnable()
        { 
                SetupGameManagerUIManager();
        }
        private void SetupGameManagerUIManager()
        {
                if (GameManager.Instance != null && GameManager.Instance.uiManager == null)
                {
                        GameManager.Instance.uiManager = this;
                }     
        }
        private void Start()
        { 
                SetupUIElements();
                UpdateUI();
        }

        private void SetupUIElements()
        { 
                _elements = FindObjectsOfType<UIElement>().ToList();
        }
        
        public void UpdateUI()
        {
                foreach (var element in _elements)
                {
                        element.UpdateElement();
                }
        }

        public void GoToPage(int pageIndex)
        {
                if (pageIndex < pages.Count && pages[pageIndex] != null)
                {
                        SetActiveAllPages(false);
                        var page = pages[pageIndex];
                        page.gameObject.SetActive(true);
                }
        }

        public void TogglePause()
        {
                if (allowPause)
                {
                        if (isPaused)
                        {
                                SetActiveAllPages(false);
                                Time.timeScale = 1;
                                isPaused = false;
                        }
                        else
                        {
                                GoToPage(pausePageIndex);
                                Time.timeScale = 0;
                                isPaused = true;
                        }
                }      
        }

        public void SetActiveAllPages(bool state)
        { 
                if (pages != null)
                {
                        foreach (var page in pages)
                        {
                                if(page != null)
                                        page.gameObject.SetActive(state);
                        }
                }
        }
}
