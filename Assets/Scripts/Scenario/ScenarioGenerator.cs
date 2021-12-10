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

        int startSpacePositionY = GetSpaceStartPosition(x);
        int finalSpacePositionY = startSpacePositionY + GetSpaceSize(x, startSpacePositionY);

        for (float y = wallStartPositionY; y < wallStartPositionY + wallSize; y += blockSize)
        {
            if(y <= startSpacePositionY || y > finalSpacePositionY)
            {
                objectPooler.SpawnFromPool("wall", new Vector3(x, y, 0f), Quaternion.identity, new Vector3(1,1));
            }            
        }
    }

    private int GetSpaceSize(float xCoordRef, int startSpacePositionY)
    {
        
        int maxSpaceSize = wallSize - (2 * borderOffset) - Mathf.Abs(startSpacePositionY);

        float noise = GetXPerlinNoise(xCoordRef);

        int spaceSize = (int) (noise * maxSpaceSize);

        if(spaceSize < minSpaceSize)
        {
            spaceSize = minSpaceSize;
        }

        return spaceSize;
    }

    private int GetSpaceStartPosition(float yCoordRef)
    {
        int minSpaceStartPosition = wallStartPositionY + borderOffset;
        int maxSpaceStartPosition = wallStartPositionY + wallSize - minSpaceSize - borderOffset;

        int startPosition = (int) (GetYPerlinNoise(yCoordRef) * (maxSpaceStartPosition + minSpaceStartPosition)/2 );

        return Mathf.Clamp(startPosition, minSpaceStartPosition, maxSpaceStartPosition);
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

    private float GetYPerlinNoise(float coordRef)
    {
        float yCoord = CalculatePerlinNoiseCoord(coordRef, yNoiseScale);

        return Mathf.PerlinNoise(0f, yCoord);
    }

    private float GetXPerlinNoise(float coordRef)
    {
        float xCoord = CalculatePerlinNoiseCoord(coordRef, xNoiseScale);

        return Mathf.PerlinNoise(xCoord, 0f);
    }

    private float CalculatePerlinNoiseCoord(float coordRef, float noiseScale)
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
