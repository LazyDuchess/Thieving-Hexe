using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseUI;
    public static GameObject staticPauseUI;

    public void Awake()
    {
        staticPauseUI = pauseUI;
    }
    public static void Pause()
    {
        paused = true;
        GameEventsController.PauseGame();
        staticPauseUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public static void Unpause()
    {
        paused = false;
        GameEventsController.UnpauseGame();
        staticPauseUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.controlEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (paused)
                {
                    Unpause();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
}
