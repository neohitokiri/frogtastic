using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ValidarFinDeEscenario : MonoBehaviour
{
    [SerializeField] private Animator animaciones;
    [SerializeField] private GameObject efectos;
    [SerializeField] private GameObject controlesTouch;

    private void Start() {
        animaciones = GetComponent<Animator>();

        foreach(Transform child in transform)
        {
            if (child.gameObject.name == "Efectos")
                efectos = child.gameObject;
        }

        controlesTouch = GameObject.Find("ControlesTouch");
        if (controlesTouch != null)
        {
            Debug.Log("Existen controles touch");
            #if UNITY_ANDROID || UNITY_IOS
            controlesTouch.SetActive(true);
            #else
            controlesTouch.SetActive(false);
            #endif
        }
        else
        {
            Debug.Log("No hay controles touch");
        }


    }

    void Update()
    {
        efectos.SetActive(ControladorGlobal.Instance.GetFinalizarEscena());
        animaciones.speed = ControladorGlobal.Instance.GetFinalizarEscena() ? 1.0f: 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && ControladorGlobal.Instance.GetFinalizarEscena())
        {
            ControladorGlobal.Instance.CambiarNivel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
