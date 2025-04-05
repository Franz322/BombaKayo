using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public SphereCollider bombCollider;
    public GameObject bombVFX;
    public float bombCountDown;
    private IEnumerator BombCountDown()
    {
        yield return new WaitForSeconds(bombCountDown);
        bombCollider.enabled = true;
        Instantiate(bombVFX, transform.position, bombVFX.transform.rotation);

        yield return new WaitForSeconds(0.1f);
        bombCollider.enabled = false;
        Destroy(gameObject);
    }

    public void TriggerBomb()
    {
        StartCoroutine(BombCountDown());
    }
}
