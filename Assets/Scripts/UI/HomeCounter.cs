using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeCounter : MonoBehaviour
{
    private int amountOfHomes;
    private int homesFilled;

    private void Start()
    {
        homesFilled = 0;
        amountOfHomes = FindObjectsOfType<HomeScript>().Length;
    }

    public void IncreaseCounter()
    {
        homesFilled += 1;

        if (homesFilled >= amountOfHomes)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
