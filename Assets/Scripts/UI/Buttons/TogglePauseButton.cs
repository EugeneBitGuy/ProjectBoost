using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TogglePauseButton : MonoBehaviour
{
    public void TogglePause()
    {
        GameManager.Instance.uiManager.TogglePause();
    }
}
