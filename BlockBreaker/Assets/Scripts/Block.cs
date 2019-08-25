using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockEffectVFX;
    [SerializeField] Sprite[] hitSprites;

    // Cached reference
    Level level;

    // state variables
    int timesHit;

    private void Start() 
    {
        level = FindObjectOfType<Level>();
        if(tag == "Breakable")
        {
            level.CountBreakableBlocks(); 
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit() 
    {
        timesHit++;
        int maxHits = hitSprites.Length +1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitsprite();
        }
    }

    private void ShowNextHitsprite()
    {
        int spriteIndex = timesHit -1; 
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } 
        else
        {
            Debug.LogError("Block sprite is missing from array!" + gameObject.name);  
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerBlockVFX();
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerBlockVFX()
    {
        GameObject blockEffect = Instantiate(blockEffectVFX, transform.position, transform.rotation);
        Destroy(blockEffect, 1f);
    }
}
