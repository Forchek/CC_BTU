using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1_IA : MonoBehaviour
{
    private Transform Player;
    private Transform CameraPos;
    private bool JustInvoked = true;
    private int speed = 2;
    private bool FacingRight = true;
    private Vector2 Mov;
    private Vector2 PosObj;
    private Vector3 LocScale;

    //Ataque
    private bool Attacking = false;
    private int FreqAttack;
    private float TimeElapsed = 0f;
    private float aux = 0f;
    private int TypeAttack;
    private bool TypeAttackDecided = false;

    //Salto
    private bool jumping = false;
    private bool GoingUp = true;
    private float AlturaCaida = 0f;

    public Transform Pies;
    public CircleCollider2D RadAtck;
    public CircleCollider2D CollPies;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        CameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Debug.Log("InicioPrueba: " + Time.time);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetDirection();
        transform.Translate(Mov.normalized * speed * Time.deltaTime);
        Flip();
        if(JustInvoked == false)
        {
            CheckLimits();
        }
    }
    private void Update()
    {
        if(Mathf.Abs(transform.position.x - (-11f)) <= 0.05)
        {
            Debug.Log("FinPrueba: " + Time.time);
        }
    }
    private void CheckLimits()
    {
        //Comprobar que no pase los limites
        if (Pies.position.x > (CameraPos.position.x + 8.3))
        {
            transform.position = new Vector3(CameraPos.position.x + 8.3f, transform.position.y, transform.position.z);
        }
        else if (Pies.position.x <= (CameraPos.position.x - 8.3))
        {
            transform.position = new Vector3(CameraPos.position.x - 8.3f, transform.position.y, transform.position.z);
        }

        if (Pies.position.y >= (CameraPos.position.y + 0.5) && (jumping == false))
        {
            transform.position = new Vector3(transform.position.x, CameraPos.position.y + 1.5f, transform.position.z);
        }
        else if (Pies.position.y <= (CameraPos.position.y - 5))
        {
            transform.position = new Vector3(transform.position.x, CameraPos.position.y - 4f, transform.position.z);
        }
    }

    private void SetDirection()
    {
        //Nada mas aparecer se mete en pantalla y ataca con salto
        if (JustInvoked == true)
        {
            if(((CameraPos.position.x + 7f) - transform.position.x) < 0)
            {
                Mov = Vector2.left;
            }
            else if (((CameraPos.position.x - 7f) - transform.position.x) > 0)
            {
                Debug.Log("Entra en Pantalla");
                Mov = Vector2.right;
            }
            else
            {
                JustInvoked = false;
                TypeAttack = 1;
                Attack();
                Debug.Log("Ha entrado en pantalla");
                StartCoroutine("DecideAction");
                //Lanzar animacion de ataque salto
            }
        }
        
        else if (Attacking == true)
        {
            //Puñetazo
            if (TypeAttack == 0)
            {
                Debug.Log("Ataca de puño");
                //animacion puñetazo, lo del attacking false tiene que llamarse desde la animacion
                Attacking = false;
                RadAtck.enabled = false;
            }
            else
            {
                Debug.Log("Ataca de patada");
                //animacion patada
                jumping = true;
                CollPies.enabled = false;
                float LongS;
                if(Player.position.x > transform.position.x)
                {
                    LongS = 1.5f;
                }
                else
                {
                    LongS = -1.5f;
                }
                AttackJump(1f, LongS, AlturaCaida);
            }
        }
    }

    private void AttackJump(float AlturaSalto, float LongSalto, float PosCaida)
    {
        speed = 4;
        if (GoingUp == true)
        {
            Debug.Log("Subiendo");
            Mov = new Vector2(LongSalto, AlturaSalto);
            if (transform.position.y >= (PosCaida + AlturaSalto))
            {
                GoingUp = false;
            }
        }
        else
        {
            Debug.Log("Bajando");
            Mov = new Vector2(LongSalto, -AlturaSalto);
            if (transform.position.y <= PosCaida)
            {
                jumping = false;
                RadAtck.enabled = false;
                Attacking = false;
                CollPies.enabled = true;
                Debug.Log("En el suelo");
            }
        }
    }

    private void Flip()
    {
        if ((Mov.x > 0) && (FacingRight == false))
        {
            LocScale = transform.localScale;
            LocScale.x *= -1;
            transform.localScale = LocScale;
            FacingRight = true;
        }
        else if ((Mov.x < 0) && (FacingRight == true))
        {
            LocScale = transform.localScale;
            LocScale.x *= -1;
            transform.localScale = LocScale;
            FacingRight = false;
        }
    }

    private void Attack()
    {
        Debug.Log("Hora de Atacar");
        RadAtck.enabled = true;
        Attacking = true;
        GoingUp = true;
        AlturaCaida = transform.position.y;
    }

    private IEnumerator DecideAction()
    {
        while(true)
        {
            if(Attacking == false)
            {
                FreqAttack = Random.Range(5, 11);
                Debug.Log("Nuevo tiempo entre ataques: " + FreqAttack);
                for (TimeElapsed = 0; TimeElapsed < (FreqAttack - 1); TimeElapsed++)
                {
                    speed = Random.Range(2, 5);
                    if (Random.Range(0, 4) == 0)
                    {
                        Mov = new Vector2(0, 0);
                    }
                    else
                    {
                        Debug.Log("Mov Aleatorio");
                        Mov.x = Random.Range(-1f, 1f);
                        Mov.y = Random.Range(-1f, 1f);
                    }
                    yield return new WaitForSeconds(1);
                }

                TypeAttack = Random.Range(0, 2);
                PosObj.y = Player.position.y;
                Debug.Log("Tipo de ataque: " + TypeAttack);
                //Puñetazo
                if (TypeAttack == 0)
                {
                    //tengo el Player a la derecha o la izquierda
                    if ((Player.position.x - transform.position.x) < 0)
                    {
                        PosObj.x = Player.position.x + 0.75f;
                    }
                    else
                    {
                        PosObj.x = Player.position.x - 0.75f;
                    }
                }
                //Patada
                else
                {
                    //tengo el Player a la derecha o la izquierda
                    if ((Player.position.x - transform.position.x) < 0)
                    {
                        PosObj.x = Player.position.x + 3f;
                    }
                    else
                    {
                        PosObj.x = Player.position.x - 3;
                    }
                }

                Mov = new Vector2(PosObj.x - transform.position.x, PosObj.y - transform.position.y);
                Debug.Log("Dirigiendose para atacar: " + Mov.sqrMagnitude);

                TimeElapsed = 0;
                while ((((Vector2)transform.position - PosObj).sqrMagnitude > 0.1) && TimeElapsed <= 5f)
                {
                    TimeElapsed += 0.1f;
                    yield return new WaitForSeconds(0.1f);
                }
                Attack();
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
            
        }
    }

}
