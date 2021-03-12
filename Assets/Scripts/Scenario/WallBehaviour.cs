using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour, Poolable
{

    public List<Sprite> sprites;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnObjectSpawn()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
