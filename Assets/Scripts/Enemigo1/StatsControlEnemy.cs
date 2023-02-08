using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsControlEnemy : MonoBehaviour
{
    private Enemigo1_IA EnemIA;
    private GameManager gameManager;
    // Start is called before the first frame update
    public int MaxVida;
    public int Vida;
    public int Daño;
    void Awake()
    {
        MaxVida = 200;
        Vida = MaxVida;
        Daño = 30;
    }
    private void Start()
    {
        EnemIA = GetComponent<Enemigo1_IA>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "AttackPlayer")
        {
            if (EnemIA.Inmunity == false)
            {
                Debug.Log("GolpeadoPorPlayer");
                Vida -= collision.GetComponentInParent<StatsControl>().Daño;
                gameManager.EnemigoEliminado();
                Destroy(gameObject);
            }
        }
    }
}
