using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarPosicion : MonoBehaviour
{
    private Animator animator;
    public bool Utilizado;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Debug.Log("En checkpoint");
            other.GetComponent<MovimientoPersonaje>().posicionInicial = transform.position;
            Utilizado = true;
//            animator.SetTrigger("Activar");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
