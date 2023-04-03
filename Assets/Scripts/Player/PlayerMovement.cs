using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioSource jumpSoundPlayer;

    private SpriteHolder changeSprite;
    private DeathScript deathScript;
    private SpriteRenderer renderer;
    
    private bool isRunning;
    public bool isAlive = true;

    private void Awake()
    {
        changeSprite = GetComponent<SpriteHolder>();
        deathScript = GetComponent<DeathScript>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isAlive) return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!isRunning) Rotate(Quaternion.Euler(0,0,0));
            if (transform.position.y != 7) Move(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isRunning) Rotate(Quaternion.Euler(0,0,90));
            if (transform.position.x != -6) Move(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isRunning) Rotate(Quaternion.Euler(0,0,180));
            if (transform.position.y != -7) Move(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (!isRunning) Rotate(Quaternion.Euler(0,0,270));
            if (transform.position.x != 6) Move(Vector2.right);
        }
    }

    private void Move(Vector2 direction)
    {
        if (isRunning) return;
        Vector3 destination = (Vector2)transform.position + direction;

        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));
        
        StartCoroutine(Jump(destination));
        jumpSoundPlayer.Play();

        if (obstacle && !platform && isAlive)
        {
            isAlive = false;
            deathScript.Death();
            transform.SetParent(null);
            return;
        }

        transform.SetParent(platform ? platform.transform : null);
    }

    private IEnumerator Jump(Vector2 destination)
    {
        isRunning = true;
        
        float time = 0f;
        float duration = 0.15f;
        Vector2 position = transform.position;

        renderer.sprite = changeSprite.jumpSprite;
        
        while (time < duration)
        {
            float progress = time / duration;
            transform.position = Vector2.Lerp(position, destination, progress);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
        if (isAlive) renderer.sprite = changeSprite.normalSprite;
        isRunning = false;
    }
    
    private void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void ResetPosition()
    {
        isRunning = false;
        isAlive = true;
        StopAllCoroutines();
        transform.position = deathScript.respawnPoint.position;
        transform.SetParent(null);
    }
}
