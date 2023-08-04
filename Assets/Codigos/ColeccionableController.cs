using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColeccionableController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
//            Debug.Log("Coge");
            Destroy(gameObject);
//            int algo = other.GetComponent<MovimientoPersonaje>().puntosAcumulados++;
//            other.GetComponent<MovimientoPersonaje>().luzGlobal.intensity++;
            other.GetComponent<MovimientoPersonaje>().Iluminar();

//            Debug.Log(algo);
//            animator.SetTrigger("Activar");
        }
    }
}
