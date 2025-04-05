using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    [Header("Bomb Settings")]
    public GameObject bombPrefab1;
    public GameObject bombPrefab2;
    public GameObject bombPrefab3;

    public float spawnInterval = 2f;

    [Header("Spawn Area")]
    public float spawnRadius = 10f;
    public float spawnHeightOffset = 0f;

    private float timer;

    // To track how many bombs of each type are currently active
    private int bomb1Count = 0;
    private int bomb2Count = 0;
    private int bomb3Count = 0;

    private const int maxBombs = 3;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBomb();
            timer = 0f;
        }
    }

    void SpawnBomb()
    {
        // Randomly choose one of the 3 bomb prefabs
        GameObject selectedBomb = ChooseRandomBomb();

        // Check if we can spawn the selected bomb (make sure it has fewer than 3 instances)
        if (CanSpawnBomb(selectedBomb))
        {
            // Get a random point inside a unit sphere and scale it to the radius
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;

            // Keep everything on the horizontal plane (optional: set Y to fixed offset)
            randomOffset.y = spawnHeightOffset;

            // Calculate the spawn position
            Vector3 spawnPos = transform.position + randomOffset;

            // Spawn the selected bomb at the calculated position
            GameObject spawnedBomb = Instantiate(selectedBomb, spawnPos, Quaternion.identity);

            // Add the spawned bomb to the appropriate counter
            if (selectedBomb == bombPrefab1)
                bomb1Count++;
            else if (selectedBomb == bombPrefab2)
                bomb2Count++;
            else if (selectedBomb == bombPrefab3)
                bomb3Count++;
        }
    }

    bool CanSpawnBomb(GameObject bomb)
    {
        // Check if we can spawn the bomb by checking the active count for each prefab
        if (bomb == bombPrefab1 && bomb1Count < maxBombs)
            return true;
        if (bomb == bombPrefab2 && bomb2Count < maxBombs)
            return true;
        if (bomb == bombPrefab3 && bomb3Count < maxBombs)
            return true;

        return false;
    }

    GameObject ChooseRandomBomb()
    {
        // Randomly pick one of the three bomb prefabs
        int randomIndex = Random.Range(0, 3);  // Randomly select an index between 0 and 2

        if (randomIndex == 0)
            return bombPrefab1;
        else if (randomIndex == 1)
            return bombPrefab2;
        else
            return bombPrefab3;
    }

    // This will be called when a bomb is destroyed, to decrease the active count
    public void OnBombDestroyed(GameObject bomb)
    {
        if (bomb == bombPrefab1)
            bomb1Count--;
        else if (bomb == bombPrefab2)
            bomb2Count--;
        else if (bomb == bombPrefab3)
            bomb3Count--;
    }

    // This will be called to visualize the spawn area as a wire cube in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Calculate the center of the spawn area (it should be the same as the object's position)
        Vector3 center = transform.position + new Vector3(0, spawnHeightOffset, 0);

        // Create a size based on the spawnRadius for X and Z, with a fixed Y size of 0.1f for visualization
        Vector3 size = new Vector3(spawnRadius * 2, 0.1f, spawnRadius * 2); // 2x spawnRadius for full coverage

        // Draw the wire cube representing the spawn area
        Gizmos.DrawWireCube(center, size);
    }
}
