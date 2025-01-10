using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public event Action OnResume;
    public void OnQuitButtonPressed()
    {
        Application.Quit(); 
    }

    public void OnResumeButtonPressed()
    {
        OnResume?.Invoke();
    }
}
