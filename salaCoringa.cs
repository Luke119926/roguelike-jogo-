using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class salaCoringa : MonoBehaviour
{

    public GameObject Porta;
    public GameObject salaCoringaDFinal;
    public Transform myTrans;

    void  OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "spawn point")
        {
            Destroy(Porta);
        }
    }

    void Update()
    {
        if(!GameObject.FindWithTag("SalasDeFinal"))
        {
            Instantiate(salaCoringaDFinal, myTrans.position, myTrans.rotation);
            Destroy(gameObject);
        }
    }
}
