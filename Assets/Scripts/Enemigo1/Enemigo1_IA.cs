using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1_IA : MonoBehaviour
{
    private Transform Player;
    private Transform CameraPos;
    private bool JustInvoked = true;
    private int speed = 5;
    private bool FacingRight = true;
    private Vector2 Mov;
    private Vector2 PosObj;
    private Vector3 LocScale;
    private bool Attacking = false;
    private int FreqAttack;
    private float TimeElapsed = 0f;
    private float aux = 0f;
    private int TypeAttack;
    private bool TypeAttackDecided = false;
    private bool jumping = false;

    public Transform Pies;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        CameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        FreqAttack = Random.Range(5, 10);
        InvokeRepeating("Attack", 15, FreqAttack);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Moving();
        Flip();
        CheckLimits();
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

    private void Moving()
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
                Mov = Vector2.right;
            }
            else
            {
                JustInvoked = false;
                //Lanzar animacion de ataque salto
            }
            transform.Translate(Mov * speed * Time.deltaTime);
        }
        //Movimientos antes de pegar
        if((JustInvoked == false) && (Attacking == false))
        {
            //Primero se mueve un poco aleatorio
            if(TimeElapsed < FreqAttack - 2)
            {
                if(TimeElapsed >= (1.5f * aux))
                {
                    aux += 1.0f;
                    Mov.x = Random.Range(-1f, 1f);
                    Mov.y = Random.Range(-1f, 1f);
                }
                transform.Translate(Mov.normalized * speed * Time.deltaTime);
            }
            else
            {
                if(TypeAttackDecided == false)
                {
                    TypeAttackDecided = true;
                    TypeAttack = Random.Range(0, 2);
                    PosObj.y = Player.position.y;
                    Debug.Log(TypeAttack);
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
                }
                transform.position = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), PosObj, Time.deltaTime);
            }
            //Cuando se acerca el momento de atacar decice si acercarse para puñetazo o alejarse para patada
            TimeElapsed += Time.deltaTime;
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
        aux = 0f;
        TimeElapsed = 0f;
        TypeAttackDecided = false;
        //Decidir que ataque hacer
        if ((JustInvoked == false) && (Attacking == false))
        {

        }
    }
}
