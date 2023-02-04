using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVidaController : MonoBehaviour
{
    private Slider slider;

    public StatsControl StatPj;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = StatPj.MaxVida;
        slider.value = StatPj.Vida;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slider.value = StatPj.Vida;
    }
}
