using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool paused = false;
    [SerializeField]
    GameObject pauseMenu;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            togglePause();
    }

    void togglePause()
    {
        if (Time.timeScale == 0f)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            paused = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
    }
}