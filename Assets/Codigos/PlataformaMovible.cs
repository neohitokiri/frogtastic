using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovible : MonoBehaviour
{
    [SerializeField] public GameObject Plataforma;
    [SerializeField] public Transform PuntoInicial;
    [SerializeField] public Transform PuntoFinal;
    [SerializeField] public float Velocidad;

    private Vector3 MoverA;

    // Start is called before the first frame update
    void Start()
    {
        MoverA = PuntoFinal.position;
    }

    // Update is called once per frame
    void Update()
    {
        Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, MoverA, Velocidad * Time.deltaTime);

        if (Plataforma.transform.position == PuntoFinal.position)
        {
            MoverA = PuntoInicial.position;
        }
        else if (Plataforma.transform.position == PuntoInicial.position)
        {
            MoverA = PuntoFinal.position;
        }

    }
}
