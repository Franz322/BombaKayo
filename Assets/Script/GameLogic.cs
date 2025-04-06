using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public PlayerManager playerManager;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
       if (playerManager.player1Score > playerManager.player2Score)
        {
            player1.SetActive(true);
        }
        else
        {
            player2.SetActive(true);
        }
        Time.timeScale = 0;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
