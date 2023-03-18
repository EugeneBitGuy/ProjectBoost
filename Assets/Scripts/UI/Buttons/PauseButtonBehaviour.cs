using DefaultNamespace;
using UnityEngine;

public class PauseButtonBehaviour : MonoBehaviour
{
    public void TogglePause()
    {
        GameManager.Instance.uiManager.TogglePause();
    }
}
