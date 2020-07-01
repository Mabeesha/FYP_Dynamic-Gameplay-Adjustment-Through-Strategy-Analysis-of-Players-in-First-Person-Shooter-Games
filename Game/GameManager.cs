using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public GameObject completeLevelUI;
    public GameObject miniMap;
    public GameObject player;


    public void EndGame()
    {
        if (!gameHasEnded)
        {
            Debug.Log("Game Over");
            gameHasEnded = true;
            player.SetActive(false); 
            //Invoke("Restart",2f);
        }
        
    } 

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        miniMap.SetActive(false);
        //player.SetActive(false);

        Debug.Log("Level Completed!");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
