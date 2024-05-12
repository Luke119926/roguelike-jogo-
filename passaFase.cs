using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class passaFase : MonoBehaviour
{


    void start()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            spawnRoom.resetGeralImput = true;
            destroy.destruir = true;
            print(spawnRoom.resetGeralImput);
        }
    }
}
