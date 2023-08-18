using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    private float puntos;
    [SerializeField] private float recogidos;
    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = puntos.ToString("0");
    }

    public void SumarPuntos(float puntosEntrada)
    {
        recogidos += puntosEntrada;
        puntos = ControladorGlobal.GetTotalColeccionables() - recogidos;
        ControladorGlobal.Instance.SetFinalizarEscena(puntos < 1.0f);
    }
}
