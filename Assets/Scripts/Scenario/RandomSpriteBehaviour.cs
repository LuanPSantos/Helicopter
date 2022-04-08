using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteBehaviour : MonoBehaviour, Poolable
{

    public List<Sprite> sprites;

    public SpriteRenderer spriteRenderer;

    public void OnObjectSpawn()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
