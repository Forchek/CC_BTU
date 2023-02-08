using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int EnemigosOleada;
    private bool NextOleada = false;
    private int OleadaNumero = 0;
    private int PosNextOleada;

    public GameObject Enemigo1;
    public CameraScript Camera;
    // Start is called before the first frame update
    void Start()
    {
        PrimeraOleada();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((NextOleada == true) && (Mathf.Abs(Camera.transform.position.x - PosNextOleada) <= 0.5))
        {
            NextOleada = false;
            switch (OleadaNumero)
            {
                case 1:
                    SegundaOleada();
                    break;
                default:
                    break;
            }
        }
    }

    private void PrimeraOleada()
    {
        Instantiate(Enemigo1, new Vector3(-12f, -1f, 0f), Quaternion.identity);
        Instantiate(Enemigo1, new Vector3(-12f, -3.5f, 0f), Quaternion.identity);
        Instantiate(Enemigo1, new Vector3(12f, -1f, 0f), Quaternion.identity);
        Instantiate(Enemigo1, new Vector3(12f, -3.5f, 0f), Quaternion.identity);

        EnemigosOleada = 4;
        OleadaNumero = 1;
    }

    private void SegundaOleada()
    {
        Instantiate(Enemigo1, new Vector3(20f, -1f, 0f), Quaternion.identity);
        Instantiate(Enemigo1, new Vector3(20f, -3.5f, 0f), Quaternion.identity);
        Instantiate(Enemigo1, new Vector3(40f, -1f, 0f), Quaternion.identity);
        Instantiate(Enemigo1, new Vector3(40f, -3.5f, 0f), Quaternion.identity);

        EnemigosOleada = 4;
        OleadaNumero = 2;
    }

    public void EnemigoEliminado()
    {
        EnemigosOleada--;
        if (EnemigosOleada == 0)
        {
            Debug.Log("Oleada Eliminada");
            Camera.ChangeCameraTops(32, 28);
            NextOleada = true;
            PosNextOleada = 30;
        }
    }
}
