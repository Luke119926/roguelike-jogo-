using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class inimigoScript : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public GameObject tiro;
    public GameObject tiro1;
    public int vidaMax;

    void Start()
    {
        vidaMax = Random.Range(2, 8);
        print(vidaMax);
    }

    
    void Update()
    {               
        float distance = UnityEngine.Vector2.Distance(transform.position, player.transform.position);
        UnityEngine.Vector2 direction = player.transform.position - transform.position;

        transform.position = UnityEngine.Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        if(vidaMax <= 0)
        {
            Destroy(gameObject);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "tiro")
        {
            vidaMax -= 1;
            print(vidaMax);
        }
    }
}
