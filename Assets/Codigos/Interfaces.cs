using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Interfaces : MonoBehaviour
{
    [SerializeField] public bool pasarNivel;
    [SerializeField] public int indiceNivel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pasarNivel)
        {
            CambiarNivel(indiceNivel);
        }
    }

    public void CambiarNivel(int indice)
    {
        SceneManager.LoadScene(indice);
    }

    public void Salir()
    {
        ControladorGlobal.Salir();
    }
}
