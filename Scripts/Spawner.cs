using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Rigidbody Enemy;
    void Start()
    {
        StartCoroutine(whatever());
    }

    IEnumerator whatever()
    { 
            while(true)
            {
                yield return new WaitForSeconds(5f);
                Instantiate(Enemy, transform.position, transform.rotation);
            }
    }
}

