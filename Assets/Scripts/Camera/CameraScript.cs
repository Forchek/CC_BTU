using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private float CameraTopeDerecha = 3f;
    private float CameraTopeIzquierda = -2f;
    private float CameraTopeArriba = 0f;
    private float CameraTopeAbajo = -0.7f;
    private bool KeepMovingCamera = false;

    public Transform PjPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovingCamera();
    }

    private void MovingCamera()
    {
        if ((PjPos.position.x >= (transform.position.x + 5)) && (transform.position.x < CameraTopeDerecha))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(PjPos.position.x, transform.position.y, transform.position.z), Time.deltaTime);
        }
        else if ((PjPos.position.x <= (transform.position.x - 5)) && (transform.position.x > CameraTopeIzquierda))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(PjPos.position.x, transform.position.y, transform.position.z), Time.deltaTime);
        }
        else if (PjPos.position.y >= (transform.position.y ) && (transform.position.y < CameraTopeArriba))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, PjPos.position.y, transform.position.z), Time.deltaTime*10);
        }
        else if (PjPos.position.y <= (transform.position.y - 0.5) && (transform.position.y > CameraTopeAbajo))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, PjPos.position.y, transform.position.z), Time.deltaTime);
        }
    }

    public void ChangeCameraTops(int TopRight, int TopLeft)
    {
        if(TopRight > 0)
        {
            CameraTopeDerecha = TopRight;
        }
        if(TopLeft > 0)
        {
            CameraTopeIzquierda = TopLeft;
        }
    }
}
