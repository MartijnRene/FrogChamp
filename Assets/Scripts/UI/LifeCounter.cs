using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] private List<GameObject> lifeIcons;
    
    private int lives = 3;

    public void LoseALife()
    {
        lives -= 1;
        lifeIcons[^1].gameObject.SetActive(false);
        lifeIcons.RemoveAt(lifeIcons.Count - 1);
        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
