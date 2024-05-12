using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{

    public static bool destruir;
    public GameObject sala;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("salas") || other.CompareTag("salaCoringa"))
        {
            Destroy(other.gameObject);
        }
    }   
    void Update()
    {
        if(destruir == true)
        {
            Destroy(sala);
        }
    }
}
