using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUi : MonoBehaviour
{
    public void OnTrainingButtonClick()
    {
        // Load the training scene
        SceneManager.LoadScene("TrainingScene");
    }

    public void OnMultiplayerButtonClick()
    {
        // Load the multiplayer scene
        SceneManager.LoadScene("MultiplayerScene");
    }

    public void OnExitButtonClick()
    {
        // Quit the game
        Application.Quit();
    }

}
