using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    public float scale = 1.05f;
    void Start()
    {
        Renderer backgound = GetComponent<Renderer>();
        
        float xCameraSize = Camera.main.orthographicSize * Camera.main.aspect * 2;
        float yCameraSize = Camera.main.orthographicSize * 2;

        float xScale = xCameraSize / backgound.bounds.size.x * scale;
        float yScale = yCameraSize / backgound.bounds.size.y * scale;

        backgound.transform.localScale = new Vector3(Mathf.Max(xScale, yScale), Mathf.Max(xScale, yScale), 1);
        backgound.enabled = true;
    }
}
