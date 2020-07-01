using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool isPausedGame = false;

    [SerializeField] public GameObject pauseUI;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming");
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPausedGame = false;
    }

    public void Pause()
    {
        Debug.Log("pause");
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPausedGame = true;
    }

    public void QuitGame()
    {

        Debug.Log("Quiting game");
        Application.Quit();
    }

}
