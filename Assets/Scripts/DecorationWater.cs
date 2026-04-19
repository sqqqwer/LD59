using System.Collections.Generic;
using UnityEngine;

public class DecorationWater : MonoBehaviour
{
    public List<GameObject> decorationPrefabs;
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float minSpawnDistance = 2f;
    public float maxSpawnDistance = 5f;
    public float offsetRange = 2f;

    private Vector3 direction;
    private float segmentLength;
    private List<GameObject> activeDecorations = new List<GameObject>();
    private float nextSpawnDistance;
    private float totalTraveledDistance = 0f;

    void Start()
    {
        direction = (pointB.position - pointA.position).normalized;
        segmentLength = Vector3.Distance(pointA.position, pointB.position);
        SetNextSpawnDistance();
    }

    void Update()
    {
        float step = speed * (MetaGame.Instance.shipSpeed / 20f) * Time.deltaTime;
        totalTraveledDistance += step;

        for (int i = activeDecorations.Count - 1; i >= 0; i--)
        {
            GameObject deco = activeDecorations[i];
            if (deco == null)
            {
                activeDecorations.RemoveAt(i);
                continue;
            }

            deco.transform.Translate(direction * step, Space.World);

            float traveled = Vector3.Dot(deco.transform.position - pointA.position, direction);
            if (traveled > segmentLength)
            {
                Destroy(deco);
                activeDecorations.RemoveAt(i);
            }
        }

        if (totalTraveledDistance >= nextSpawnDistance)
        {
            SpawnDecoration();
            SetNextSpawnDistance();
            totalTraveledDistance = 0f;
        }
    }

    void SetNextSpawnDistance()
    {
        float speedFactor = Mathf.Clamp01(speed * (MetaGame.Instance.shipSpeed / 20f) / 10f);
        float min = minSpawnDistance * (1f - speedFactor * 0.5f);
        float max = maxSpawnDistance * (1f - speedFactor * 0.5f);
        nextSpawnDistance = Random.Range(min, max);
    }

    void SpawnDecoration()
    {
        if (decorationPrefabs.Count == 0) return;

        GameObject prefab = decorationPrefabs[Random.Range(0, decorationPrefabs.Count)];
        
        Vector3 spawnPos = pointA.position;
        
        Vector3 rightOffset = Vector3.Cross(direction, Vector3.up).normalized;
        float offsetX = Random.Range(-offsetRange, offsetRange);
        float offsetZ = Random.Range(-offsetRange, offsetRange);
        spawnPos += rightOffset * offsetX + Vector3.up * offsetZ;
        
        GameObject deco = Instantiate(prefab, spawnPos, Quaternion.identity);
        
        float randomYaw = Random.Range(0f, 360f);
        deco.transform.rotation = Quaternion.Euler(0, randomYaw, 0);
        
        activeDecorations.Add(deco);
    }
}