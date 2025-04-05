using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerManager playerManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BombExplosion"))
        {
            playerManager.PlayerDie(gameObject);  
        }
    }

}
