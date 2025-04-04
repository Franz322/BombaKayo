using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    public bool hasStuck = false;
    public bool canStick = false;

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
            Debug.Log("duta");
            hasStuck = true;
        }
    }
}
