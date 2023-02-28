using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Collider spawnArea;
    public int numItemsToSpawn = 1;
    public float yOffset = 0.1f;

    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        // Spawn the specified number of items
        for (int i = 0; i < numItemsToSpawn; i++)
        {
            // Get a random point within the collider bounds
            Vector3 spawnPoint = GetRandomPointInCollider(spawnArea);

            // Offset the spawn point to be on top of the ground mesh
            spawnPoint.y = GetHeightAtPoint(spawnPoint) + yOffset;

            // Spawn the item at the spawn point
            Instantiate(itemPrefab, spawnPoint, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 randomPoint = Vector3.zero;

        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            randomPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        return randomPoint;
    }

    private float GetHeightAtPoint(Vector3 point)
    {
        float height = 0f;
        RaycastHit hit;

        // Cast a ray downwards to get the height at the point
        if (Physics.Raycast(point + Vector3.up * 100f, Vector3.down, out hit))
        {
            height = hit.point.y;
        }

        return height;
    }
}