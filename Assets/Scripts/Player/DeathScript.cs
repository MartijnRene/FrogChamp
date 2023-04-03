using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public Transform respawnPoint;
    [SerializeField] private AudioSource deathSoundPlayer;
    [SerializeField] private LifeCounter lifeCounter;

    private SpriteRenderer renderer;
    private SpriteHolder changeSprite;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        changeSprite = GetComponent<SpriteHolder>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Death()
    {
        deathSoundPlayer.Play();
        lifeCounter.LoseALife();
        StartCoroutine(Die());
    }
    
    private IEnumerator Die()
    {
        transform.rotation = quaternion.identity;
        renderer.sprite = changeSprite.deathSprite;
        yield return new WaitForSeconds(2);
        Respawn();
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;
        renderer.sprite = changeSprite.normalSprite;
        transform.rotation = quaternion.identity;
        playerMovement.isAlive = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle") && playerMovement.isAlive == true && transform.parent == null)
        {
            playerMovement.isAlive = false;
            Death();
        }
    }
}
