using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClouldParallax : MonoBehaviour
{

    public float startRangeY = 0f;
    public float finalRangeY = 10f;
    public float minIntervalToSpawn = 1f;
    public float maxIntervalToSpawn = 3f;

    private float countTimeToSpawn = 0;
    private float timeToSpawn = 0;

    private ObjectPooler objectPooler;

    private void Awake()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void Update()
    {
        if(countTimeToSpawn >= timeToSpawn)
        {
            SpawnCloud();
            DefineTimeToSpawn();
        }else
        {
            countTimeToSpawn += Time.deltaTime;
        }
    }

    private void SpawnCloud()
    {
        float positionToSpawnY = Random.Range(startRangeY, finalRangeY);
        float positionToSpawnX = transform.position.x;

        objectPooler.SpawnFromPool("cloud", new Vector2(positionToSpawnX, positionToSpawnY), Quaternion.identity, new Vector3(1, 1, 1));
    }

    private void DefineTimeToSpawn()
    {
        timeToSpawn = Random.Range(minIntervalToSpawn, maxIntervalToSpawn);
        countTimeToSpawn = 0;
    }

}
