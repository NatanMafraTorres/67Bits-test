using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent enemy;
    public GameObject self;
    public Rigidbody singer;
    public Animator anim;
    public BoxCollider box;
    public bool stopped;

    void Start()
    {
        singer = GameObject.Find("Singer").GetComponent<Rigidbody>();
        StartCoroutine(whatever());
    }

    void Update()
    {
        enemy.SetDestination(singer.position);
    }

    private void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.enabled = false;  
            anim.enabled = false;  
            box.enabled = false;  
            stopped = true;
        }
    }

    IEnumerator whatever()
    { 
        while(true)
        {
            yield return new WaitForSeconds(1f);
            if (stopped == true)
            {
                yield return new WaitForSeconds(10f);
                Destroy(self);
            }
        }
    }
}
