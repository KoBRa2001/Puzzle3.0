using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamandItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
