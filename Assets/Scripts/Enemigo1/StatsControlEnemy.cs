using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsControlEnemy : MonoBehaviour
{
    private Mov_BTU Mov_Pj;
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

    // Update is called once per frame
    void Update()
    {
        Mov_Pj = GetComponent<Mov_BTU>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "AttackEnemy")
        {
            if (Mov_Pj.Inmunity == false)
            {
                Debug.Log("Golpeado");
                Vida -= collision.GetComponentInParent<StatsControl>().Daño;
            }
        }
    }
}
