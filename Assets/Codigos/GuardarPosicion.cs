using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarPosicion : MonoBehaviour
{
    private Animator animator;
    public bool Utilizado;

    private UnityEngine.Rendering.Universal.Light2D light2D;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        foreach(Transform child in transform)
        {
            if (child.gameObject.name == "Luz")
                light2D = child.gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if (!Utilizado)
            {
                audioSource.Play();
                other.GetComponent<MovimientoPersonaje>().posicionInicial = transform.position;
                Debug.Log(ControladorGlobal.ActivarPuntoDeGuardado(light2D.GetInstanceID()));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ControladorGlobal.cambiarCheckpoint == true)
        {
            Utilizado = ControladorGlobal.idDelCheckpointActual == light2D.GetInstanceID();
            light2D.color = Utilizado ? 
                Color.green : 
                Color.red;
        }
        
    }
}
