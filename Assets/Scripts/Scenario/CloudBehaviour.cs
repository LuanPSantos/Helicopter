using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour, Poolable
{
    public float speed;
    public float minScale = 3f;
    public float maxScale = 6f;
    public float cloudSpeedFactor = 10;

    public List<Sprite> sprites;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnObjectSpawn()
    {
        RandomReset();
    }

    void Update()
    {
        Fly();
    }

    private void Fly()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void RandomReset()
    {
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale * ScenarioGenerator.oneNegativeOrPositive(), scale * ScenarioGenerator.oneNegativeOrPositive());
        speed = scale / cloudSpeedFactor;
        spriteRenderer.color = new Color(255, 255, 255, scale / maxScale);
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
