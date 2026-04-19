using System.Collections.Generic;
using UnityEngine;

public class BlackWallWater : MonoBehaviour
{
    public List<GameObject> objectPrefabs;
    public float objectWidth = 1f;
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private List<GameObject> pool = new List<GameObject>();
    private Vector3 direction;
    private int objectCount;

    void Start()
    {
        direction = (pointB.position - pointA.position).normalized;
        float segmentLength = Vector3.Distance(pointA.position, pointB.position);
        objectCount = Mathf.CeilToInt(segmentLength / objectWidth);
        
        for (int i = 0; i < objectCount; i++)
        {
            GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Count)];
            GameObject obj = Instantiate(prefab);
            float t = (float)i / (objectCount - 1);
            obj.transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
            obj.SetActive(true);
            pool.Add(obj);
        }
    }

    void Update()
    {
        foreach (GameObject obj in pool)
        {
            Vector3 newPosition = obj.transform.position + direction * speed * (MetaGame.Instance.shipSpeed / 20f) * Time.deltaTime;
            
            if (Vector3.Dot(newPosition - pointA.position, direction) > Vector3.Dot(pointB.position - pointA.position, direction))
            {
                obj.transform.position = pointA.position;
                float randomScale = Random.Range(0.9f, 1.1f);
                obj.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            }
            else
            {
                obj.transform.position = newPosition;
            }
        }
    }
}
