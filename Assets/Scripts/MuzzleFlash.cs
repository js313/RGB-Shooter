using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public GameObject flashHolder;
    public Sprite[] flashSprites;
    public SpriteRenderer[] spriteRenderers;
    public float flashTime;

    void Start()
    {
        Deactivate();
    }
    public void Activate()
    {
        flashHolder.SetActive(true);

        int flashSpriteIndex = Random.Range(0, flashSprites.Length);
        foreach (var renderer in spriteRenderers)
        {
            renderer.sprite = flashSprites[flashSpriteIndex];
        }

        Invoke(nameof(Deactivate), flashTime);
    }

    public void Deactivate()
    {
        flashHolder.SetActive(false);
    }
}
