using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    public bool hasStuck = false;
    public bool canStick = false;

    public float timeBeforeDestroy = 5f;  // Time in seconds before the object is destroyed after being thrown

    private void Start()
    {
        // Optional: If you want to destroy the object immediately after being thrown, you can use this line.
        // StartCoroutine(DestroyAfterTime(timeBeforeDestroy)); // Optional, only if you want to destroy after a delay.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasStuck) return;

        if (!canStick) return;

        if (other.CompareTag("Player"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            gameObject.transform.SetParent(other.transform, true);
            hasStuck = true;


            StartCoroutine(DestroyAfterTime(timeBeforeDestroy));
        }
    }

    

    private IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
