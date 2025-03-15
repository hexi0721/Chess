using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.name);

        ITestInterface testInterface = collision.GetComponent<ITestInterface>();
        if (testInterface != null)
        {
            testInterface.Damage();
        }

    }

}
