using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ValidarFinDeEscenario : MonoBehaviour
{
    [SerializeField] private Animator animaciones;
    [SerializeField] private GameObject efectos;

    private void Awake() {
        animaciones = GetComponent<Animator>();

        foreach(Transform child in transform)
        {
            if (child.gameObject.name == "Efectos")
                efectos = child.gameObject;
        }
    }

    void Update()
    {
        efectos.SetActive(ControladorGlobal.Instance.NivelFinalizado);
        animaciones.speed = ControladorGlobal.Instance.NivelFinalizado ? 1.0f: 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && ControladorGlobal.Instance.NivelFinalizado)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
