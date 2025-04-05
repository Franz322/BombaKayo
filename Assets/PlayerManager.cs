using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public IEnumerator SpawnPlayer(GameObject player)
    {
        yield return new WaitForSeconds(3f);
        player.transform.position = spawnPoint1.position;
        player.SetActive(true);
    }

    public void PlayerDie(GameObject player)
    {
        player.SetActive(false);
        StartCoroutine(SpawnPlayer(player));

    }

 
}
