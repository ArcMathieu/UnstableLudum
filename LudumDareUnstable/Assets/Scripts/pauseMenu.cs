using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{

    public void PauseON()
    {
        Time.timeScale = 0;
    }

    public void PauseOFF()
    {
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
