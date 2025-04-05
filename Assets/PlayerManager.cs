using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public IEnumerator SpawnPlayer(GameObject player)
    {
        yield return new WaitForSeconds(0.12f);
        player.SetActive(false);
        yield return new WaitForSeconds(3f);
        player.transform.position = spawnPoint1.position;
        player.SetActive(true);
    }

    public void PlayerDie(GameObject player)
    {
        
        StartCoroutine(SpawnPlayer(player));

    }

 
}
