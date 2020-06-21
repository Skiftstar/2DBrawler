using System;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnClickedSinglePlayer()
    {
        Debug.Log("LocalPlayer");
    }
    public void OnClickedMultiPlayer()
    {
        Debug.Log("Online");
    }
    public void OnClickedOptions()
    {
        Debug.Log("Options");
    }
    public void OnClickedQuit()
    {
        Application.Quit(0);
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
