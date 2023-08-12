using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColeccionableController : MonoBehaviour
{
    [SerializeField] private bool collected;
    [SerializeField] public Puntaje puntaje;

    [SerializeField] public Light2D luzCentral;
    [SerializeField] public Light2D luzProyeccion;
    [SerializeField] public AudioSource audioSource;


    private void Start() {
        collected = false;
//        light2D = GetComponent<Light2D>();
        foreach(Transform child in transform)
        {
            if (child.gameObject.name == "LuzCentral")
                luzCentral = child.gameObject.GetComponent<Light2D>();
            else if (child.gameObject.name == "LuzProyeccion")
                luzProyeccion = child.gameObject.GetComponent<Light2D>();
            else if (child.gameObject.name == "Sonido")
                audioSource = child.gameObject.GetComponent<AudioSource>();
        }

    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if (!collected)
            {
                puntaje.SumarPuntos(1.0f);
//                Destroy(gameObject);
//                other.GetComponent<MovimientoPersonaje>().Iluminar();
                luzCentral.intensity++;
                luzProyeccion.intensity++;
                audioSource.Play();
                collected = true;
            }
        }
    }
}
