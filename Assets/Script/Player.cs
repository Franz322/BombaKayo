using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerManager playerManager;
    private bool isHit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BombExplosion") && !isHit)
        {
            isHit = true;
            playerManager.PlayerDie(gameObject);
            Debug.Log("jahsdkjhasd");
        }
    }

    private void Awake()
    {
        isHit = false;
    }
}
