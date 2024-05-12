using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartButton()
    {
        Debug.Log("Loading...");
        SceneTransition.SwitchToScene("Level");
    }

    public void QuitButton()
    {
        Debug.Log("Aplication is quited");
        Application.Quit();
    }
}
