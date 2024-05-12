using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using UnityEditor.PackageManager;
using UnityEngine;

public class spawnRoom : MonoBehaviour
{

    public int salaPraSpawnar;
    //1 = baixo; 2 = cima; 3 = direita; 4 = esquerda;
    public int rand;
    public catalogoDSalas salas;
    public bool spawned = false;
    public bool salaInicial;

    public static bool resetGeralImput;

    void Start()
    {        
        Invoke("spawn", 0.1f);
        resetGeralImput = false;
    }

    public void Update()
    {
        if(resetGeralImput == true)
        {
            Invoke("resetGeral", 0.1f);
            print("sla");
        }
    }

    public void spawn()
    {
        if (spawned == false)
        {
            if (salaPraSpawnar == 1)
            {
                if(!GameObject.FindWithTag("SalasDeFinal") && salaInicial == true)
                {
                    rand = Random.Range(1, salas.cima.Length - 1);
                }
                else if (GameObject.FindWithTag("SalasDeFinal") && salaInicial == false)
                {
                    rand = Random.Range(1, salas.cima.Length);
                }
                else
                {
                    rand = Random.Range(0, salas.cima.Length - 1);
                }

                Instantiate(salas.cima[rand], transform.position, salas.cima[rand].transform.rotation);                
            }
            else if (salaPraSpawnar == 2)
            {
                if(!GameObject.FindWithTag("SalasDeFinal") && salaInicial == true)
                {
                    rand = Random.Range(1, salas.cima.Length - 1);
                }
                else if (GameObject.FindWithTag("SalasDeFinal") && salaInicial == false)
                {
                    rand = Random.Range(1, salas.cima.Length);
                }
                else
                {
                    rand = Random.Range(0, salas.cima.Length - 1);
                }

                Instantiate(salas.baixo[rand], transform.position, salas.baixo[rand].transform.rotation);
            }
            else if (salaPraSpawnar == 3)
            {
                if(!GameObject.FindWithTag("SalasDeFinal") && salaInicial == true)
                {
                    rand = Random.Range(1, salas.cima.Length - 1);
                }
                else if (GameObject.FindWithTag("SalasDeFinal") && salaInicial == false)
                {
                    rand = Random.Range(1, salas.cima.Length);
                }
                else
                {
                    rand = Random.Range(0, salas.cima.Length - 1);
                }

                Instantiate(salas.esquerda[rand], transform.position, salas.esquerda[rand].transform.rotation);
            }
            else if (salaPraSpawnar == 4)
            {
                if(!GameObject.FindWithTag("SalasDeFinal") && salaInicial == true)
                {
                    rand = Random.Range(1, salas.cima.Length - 1);
                }
                else if (GameObject.FindWithTag("SalasDeFinal") && salaInicial == false)
                {
                    rand = Random.Range(1, salas.cima.Length);
                }
                else
                {
                    rand = Random.Range(0, salas.cima.Length - 1);
                }

                Instantiate(salas.direita[rand], transform.position, salas.direita[rand].transform.rotation);
            }            
        }
        spawned = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("spawn point"))
        {
            Destroy(gameObject);
        }
        if(!other.CompareTag("salaCoringa"))
        {
            Instantiate(salas.salaVazia, transform.position, salas.salaVazia.transform.rotation);
        }
    }

    public void resetGeral()
    {
        spawned = false;        
        Invoke("spawn", 0.4f);
    }
}
