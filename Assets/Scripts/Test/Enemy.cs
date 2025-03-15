using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour , ITestInterface
{
    public int health
    {
        get;
        set;
    }

    private void Start()
    {
        health = 5;
    }

    public void Damage()
    {
        health -= 1;
        if (health <= 0)
        {
            Debug.Log("Enemy Destroyed");
            Destroy(gameObject);
        }
    }

}
