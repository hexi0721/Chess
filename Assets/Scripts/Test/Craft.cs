using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour , ITestInterface
{
    public int health
    {
        get;
        set;
    }

    private void Start()
    {
        health = 3;
    }

    public void Damage()
    {
        health -= 1;
        if (health <= 0)
        {
            Debug.Log("Craft Destroyed");
            Destroy(gameObject);
        }
    }


}
