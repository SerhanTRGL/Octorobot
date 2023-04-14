using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadNextScene() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            SceneManager.LoadScene(1);
        } else if (SceneManager.GetActiveScene().buildIndex == 1) {
            SceneManager.LoadScene(2);
        } else {
            SceneManager.LoadScene(0);
        }
        
    }

    public void ExitGame() {
        Application.Quit();
    }
}
