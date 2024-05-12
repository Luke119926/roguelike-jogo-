using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class catalogoDeArma : MonoBehaviour
{

    public int[] Armas = new int[5];
    //pistola = 1; submetralhadora = 2; pistola dupla = 3; AK-47 = 4; shootgun = 5;
    public int armaEscolhida;

    void Start()
    {
        for(int i = 0; i < Armas.Length; i++)
        {
            Armas[i] = i + 1;
        }
        ReturnRandomGun();
    }

    void ReturnRandomGun()
    {
        armaEscolhida = Random.Range(1, Armas.Length);
    }
}
