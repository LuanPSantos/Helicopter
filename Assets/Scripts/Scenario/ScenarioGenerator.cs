using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioGenerator : MonoBehaviour
{
    public int wallStartPositionY;
    public int wallSize;
    public int offsetColumnPositionX;
    public int borderOffset;
    public int minSpaceSize;
    public float xNoiseScale;
    public float yNoiseScale;
    public int blockSize = 4;
    public int heartSpawnChance = 20;

    public GameplayManager gameplayManager;

    private int lastXPosition = 0;

    private ObjectPooler objectPooler;

    private void Start()
    {
        HelicopterBehaviour.onChangeXPosition += GenerateColumn;
        objectPooler = ObjectPooler.Instance;
    }

    private void OnDestroy()
    {
        HelicopterBehaviour.onChangeXPosition -= GenerateColumn;
    }

    private void GenerateColumn(float xRef)
    {
        int x = CalculateX(xRef);

        TrackLastXPosition(x);        

        if (x != lastXPosition + blockSize) return;
        lastXPosition += blockSize;

        int spaceStartPosition = GetSpaceStartPosition();
        int spaceSize = GetSpaceSize(spaceStartPosition);
        int giftPosition = int.MinValue;
        if (Random.Range(0, 100) < heartSpawnChance) 
        {
            giftPosition = Random.Range(spaceStartPosition, spaceStartPosition + spaceSize);
        }

        for (float y = wallStartPositionY; y <= wallStartPositionY + wallSize; y += blockSize)
        {
            if(y < spaceStartPosition || y > spaceStartPosition + spaceSize || wallStartPositionY + wallSize == y || wallStartPositionY == y)
            {
                objectPooler.SpawnFromPool("wall", new Vector3(x, y, 0f), Quaternion.identity, new Vector3(1, 1));

            }  else
            {
                if (y == giftPosition)
                {
                    GameObject item = objectPooler.SpawnFromPool("gift", new Vector3(x, y, 0f), Quaternion.identity, new Vector3(1, 1));
                    item.GetComponent<HeartBehaviour>().SetVisible(true);
                }
            }          
        }
    }

    private int GetSpaceSize(int minSpaceStartPosition)
    {
        int maxSpaceEndPosition = wallStartPositionY + wallSize;

        int spaceSize = (int) (GetXPerlinNoise() * (maxSpaceEndPosition - minSpaceStartPosition));

        return Mathf.Clamp(spaceSize, minSpaceSize, maxSpaceEndPosition - minSpaceStartPosition);
    }

    private int GetSpaceStartPosition()
    {
        int minSpaceStartPosition = wallStartPositionY;
        int maxSpaceEndPosition = wallStartPositionY + wallSize;

        int startPosition = (int) (GetYPerlinNoise() * (maxSpaceEndPosition - minSpaceStartPosition));

        return Mathf.Clamp(startPosition + minSpaceStartPosition, minSpaceStartPosition, maxSpaceEndPosition);
    }

    private void TrackLastXPosition(float x)
    {
        if (lastXPosition == 0)
        {
            lastXPosition = (int)x - blockSize;
        }
    }

    private int CalculateX(float xRef)
    {
        return Mathf.RoundToInt(xRef) + offsetColumnPositionX;
    }

    private float GetYPerlinNoise()
    {
        float yCoord = CalculatePerlinNoiseCoord(yNoiseScale);

        return Mathf.PerlinNoise(0f, yCoord);
    }

    private float GetXPerlinNoise()
    {
        float xCoord = CalculatePerlinNoiseCoord(xNoiseScale);

        return Mathf.PerlinNoise(xCoord, 0f);
    }

    private float CalculatePerlinNoiseCoord(float noiseScale)
    {
        return Time.time * noiseScale;
    }

    public static int oneNegativeOrPositive()
    {
        if (((int)Time.time) % 2 == 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
