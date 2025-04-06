using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public GameObject player1;
    public GameObject player2;

    public TextMeshProUGUI player1ScoreUI;
    public TextMeshProUGUI player2ScoreUI;

    public int player1Score = 0;
    public int player2Score = 0;

    private bool isDead = false;

    public float fallThreshold = -10f; 

    private void Start()
    {
        DisplayScore();
    }

    private void Update()
    {
        if (player1.transform.position.y < fallThreshold && !isDead)
        {
            PlayerDie(player1, 1);
        }

        if (player2.transform.position.y < fallThreshold && !isDead)
        {
            PlayerDie(player2, 2);
        }
    }

    private void DisplayScore()
    {
        player1ScoreUI.text = player1Score.ToString();
        player2ScoreUI.text = player2Score.ToString();
    }

    public IEnumerator SpawnPlayer(GameObject player, int playerNumber)
    {
        if (playerNumber == 1)
        {
            player2Score++;
        }
        else
        {
            player1Score++;
        }

        DisplayScore();

        yield return new WaitForSeconds(0.12f);
        player.SetActive(false);

        yield return new WaitForSeconds(2f);

        if (playerNumber == 1)
            player.transform.position = spawnPoint1.position;
        else
            player.transform.position = spawnPoint2.position;

        player.SetActive(true);
        isDead = false;
    }

    public void PlayerDie(GameObject player, int playerNumber)
    {
        if (!isDead)
        {
            isDead = true;
            StartCoroutine(SpawnPlayer(player, playerNumber));
        }
    }
}
