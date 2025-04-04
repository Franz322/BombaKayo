using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    private Transform bomb; // Assign the bomb GameObject in the Inspector
    public Transform carryPosition; // Empty GameObject on player to position bomb while carrying

    private bool isCarrying = false;
    private Rigidbody bombRb;

    public float detectionRange = 3f;
    public float throwForce;

    void Update()
    {
        DetectNearbyBomb();
    }

    public void PickUpBomb()
    {
        if (bomb == null)
            return;

        isCarrying = true;
        bombRb = bomb.GetComponent<Rigidbody>();
        bomb.SetParent(carryPosition);
        bomb.localPosition = Vector3.zero;
        bomb.localRotation = Quaternion.identity;

        if (bombRb != null)
            bombRb.isKinematic = true; // Disable physics while carrying
    }

    
    public void DropBomb()
    {
        if(bomb == null || !isCarrying)
            return;

        Sticky stickyBomb = bomb.GetComponent<Sticky>();
        if (stickyBomb != null )
            stickyBomb.canStick = true;
            
        isCarrying = false;
        bomb.SetParent(null);

        if (bombRb != null)
        {
            bombRb.isKinematic = false; // Enable physics after dropping
            bombRb.AddForce(transform.forward * throwForce, ForceMode.Impulse); // Give a slight push forward
            StartCoroutine(ResetKinematic());
        }

        bomb = null; // Reset detected bomb
    }

    
    private IEnumerator ResetKinematic()
    {
        yield return new WaitForSeconds(2f);
        bombRb.isKinematic = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    void DetectNearbyBomb()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        bomb = null; // Reset detected bomb

        float closestDistance = detectionRange;

        foreach (Collider col in hitColliders)
        {
            if (!col.CompareTag("Bomb")) continue; // Skip already stuck if object is not bomb

            Sticky stickyBomb = col.GetComponent<Sticky>();
            if (stickyBomb != null && stickyBomb.hasStuck) continue; // Skip already stuck bombs

            float distance = Vector3.Distance(transform.position, col.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bomb = col.transform;
            }
        }

    }
}
