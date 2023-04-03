using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScript : MonoBehaviour
{
    [SerializeField] private GameObject homeFrog;
    [SerializeField] private Transform playerStart;
    
    private BoxCollider2D boxCollider;
    private bool homeIncreased;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        boxCollider.enabled = false;
        homeFrog.SetActive(true);
    }

    private void OnDisable()
    {
        boxCollider.enabled = true;
        homeFrog.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player" || homeIncreased) return;
        enabled = true;
        col.gameObject.GetComponent<PlayerMovement>().ResetPosition();
        FindObjectOfType<HomeCounter>().IncreaseCounter();
        homeIncreased = true;
    }
}
