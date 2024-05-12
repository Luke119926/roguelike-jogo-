using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [Header("movimento")]
    public Rigidbody2D rig;
    public float speed;

    [Header("tiro")]
    public catalogoDeArma catalogoDeArmas;
    public GameObject Tiro;
    public Transform tiroPos;
    public Transform tiroPosPistolaDupla1;
    public Transform tiroPosPistolaDupla2;
    public float antiSpan;


    [Header("Shotgun")]

    public GameObject shotgunTiroPrefab;
    public Transform shotgunTiro1;
    public Transform shotgunTiro2;
    public Transform shotgunTiro3;

    [Header("Recuo")]
    public float recuoForce = 10f;
    public float recuoDuration = 0.1f;

    private bool isRecoiling = false;
    private Vector2 recoilDirection; 

   
    [Header("Dash")]
     // Variáveis relacionadas ao dash
    public float dashDistance = 5.0f;
    public float dashDuration = 0.5f;
    public float dashDelay = 0f;
    private bool isDashing = false;
    public float DashAntiSpan;

    [Header("AK")]
    public float akDelayT1 = 0.1f;
    public float akDelayT2 = 0.1f;
    private int tiroAK = 1;

    [Header("Pistola Dupla")]
    public float antiSpanP1;
    public float antiSpanP2;
    private bool Pistola1 = false;
    private bool Pistola2 = true;

    [Header("vida")]
    public int vidaMax = 3;
    public GameObject inimigo;
    public bool sla;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        catalogoDeArmas = GetComponent<catalogoDeArma>();
        sla = true;
    }

    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if (isRecoiling == true)
        {
            rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y);
        }
        else
        {
            rig.velocity = new Vector2(hor * speed, ver * speed);
        }

        if(vidaMax <= 0)
        {
            if(sla == true)
            {
                print("você morreu");
                sla = false;
            }  
        }

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         
        Vector2 direction = (Vector2)((worldMousePos - transform.position));
        direction.Normalize();

        // Verificar se o jogador está pressionando a tecla de dash (por exemplo, a tecla "Shift").
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !isRecoiling && DashAntiSpan <= 0)
        {
            // Iniciar o dash.
            StartCoroutine(PerformDash());
            Debug.Log("dashou");
        }
        else
        {
            DashAntiSpan -= Time.deltaTime;
        }

        if (catalogoDeArmas.armaEscolhida == 1)
        {
            if (Input.GetMouseButtonDown(0) && antiSpan <= 0)
            {
                GameObject bullet = (GameObject)Instantiate(Tiro, tiroPos.position, tiroPos.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * 20;
                antiSpan = 0.2f;
            }
            else
            {
                antiSpan -= Time.deltaTime;
            }
        }
        else if (catalogoDeArmas.armaEscolhida == 2)
        {
            if (Input.GetMouseButton(0) && antiSpan <= 0)
            {
                GameObject bullet = (GameObject)Instantiate(Tiro, tiroPos.position, tiroPos.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * 20;
                antiSpan = 0.12f;
            }
            else
            {
                antiSpan -= Time.deltaTime;
            }
        }
        else if (catalogoDeArmas.armaEscolhida == 3)
        {
            if (Input.GetMouseButtonDown(0) && antiSpanP2 <= 0 && Pistola2 == true)
            {
                GameObject bullet = (GameObject)Instantiate(Tiro, tiroPosPistolaDupla1.position, tiroPosPistolaDupla1.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * 20;
              
                antiSpanP1 = 0.15f; 
                Pistola2 = false;
                Pistola1 = true;              
            }
            else
            {
                antiSpanP2 -= Time.deltaTime;
            }

            if (Input.GetMouseButtonDown(0) && antiSpanP1 <= 0 && Pistola1 == true)
            {
                GameObject bullet = (GameObject)Instantiate(Tiro, tiroPosPistolaDupla2.position, tiroPosPistolaDupla2.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * 20;

                antiSpanP2 = 0.15f;
                Pistola2 = true;
                Pistola1 = false;
            }
            else
            {
                antiSpanP1 -= Time.deltaTime;
            }

            
        }
        else if (catalogoDeArmas.armaEscolhida == 4)
        {            
            if (Input.GetMouseButtonDown(0) && antiSpan <= 0 && tiroAK == 1)
            {
                GameObject bullet = (GameObject)Instantiate(Tiro, tiroPos.position, tiroPos.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * 20;

                akDelayT1 = 0.15f;
                akDelayT2 = 0.3f; 
                tiroAK = 2;
            }

            else if(akDelayT1 <= 0 && tiroAK == 2)
            {
                GameObject bullet = (GameObject)Instantiate(Tiro, tiroPos.position, tiroPos.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * 20;
            
                tiroAK = 3;                  
            }            

            else if(akDelayT2 <= 0 && tiroAK == 3)
            {
                GameObject bullet = (GameObject)Instantiate(Tiro, tiroPos.position, tiroPos.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * 20;
                
                antiSpan = 0.25f;
                tiroAK = 1;
            }
            else
            {
                akDelayT1 -= Time.deltaTime;
                akDelayT2 -= Time.deltaTime;
                antiSpan -= Time.deltaTime;
            }

        
        }
        else if (catalogoDeArmas.armaEscolhida == 5)
        {
            if (Input.GetMouseButtonDown(0) && antiSpan <= 0)
            {    

                // Disparos de escopeta
                ShootShotgun(shotgunTiro1, direction * 20);
                // Rotacione os tiros de escopeta conforme necessário
                ShootShotgun(shotgunTiro2, Quaternion.Euler(0, 0, -10) * direction * 20); 
                ShootShotgun(shotgunTiro3, Quaternion.Euler(0, 0, 10) * direction * 20);
                StartRecoil(direction);
                antiSpan = 0.9f; // Tempo de recarga da escopeta
                
		    }
            
            else
            {
                antiSpan -= Time.deltaTime;
            }
        }

        if (isRecoiling)
        {
            ApplyRecoil();
        }
    }

   

    // Função para disparar a shotgun
    void ShootShotgun(Transform tiroPos, Vector2 direction)
    {
        GameObject bullet = Instantiate(shotgunTiroPrefab, tiroPos.position, tiroPos.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = direction;
    }

    // Função para fazer o jogador recuar ao disparar
    void ApplyRecoil()
    {
      
         rig.AddForce(-recoilDirection * recuoForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    void StartRecoil(Vector2 recoilDirection)
    {
        this.recoilDirection = recoilDirection.normalized;
        isRecoiling = true;
        // Vai chamar o método apos x tempo (método, x tempo)
        Invoke("StopRecoil", recuoDuration);
    } 

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject == inimigo)
        {
            vidaMax -= 1;
        }
    }

    void StopRecoil()
    {
        isRecoiling = false;
    }

     IEnumerator PerformDash()
    {
        isDashing = true;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        Vector2 dashDirection = new Vector2(horizontalInput, verticalInput).normalized;
        

        if(dashDirection.magnitude > 0.1f)
        {
            // Salvar a posição atual do jogador.
            Vector2 startPos = transform.position;

            // Calcular a posição final do dash.
            Vector2 endPos = startPos + dashDirection * dashDistance;

            // Controlar o tempo de duração do dash.
            float elapsedTime = 0f;

            while (elapsedTime < dashDuration)
            {
                // Detectar colisões no caminho do dash.
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.4f); // Ajuste o raio conforme necessário.
                
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.layer == LayerMask.NameToLayer("salas"))
                    {
                        // Colidiu com um obstáculo, pare o dash.
                        isDashing = false;
                        yield break; // Sai da coroutine imediatamente.
                    }
                }

                // Mover o jogador de startPos para endPos.
                transform.position = Vector2.Lerp(startPos, endPos, elapsedTime / dashDuration);
                elapsedTime += Time.deltaTime;
                antiSpan = dashDelay;
                yield return null;
            }

            // Certificar-se de que o jogador está na posição final.
            transform.position = endPos;
        }
        DashAntiSpan = 0.9f;
        // Resetar a flag de dash.
        isDashing = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "passaFase")
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
