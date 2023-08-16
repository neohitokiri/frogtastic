using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGlobal : MonoBehaviour
{
    [SerializeField] public static int idDelCheckpointActual;
    [SerializeField] public static bool cambiarCheckpoint;
    [SerializeField] public bool NivelFinalizado;
    [SerializeField] public int muertesAcumuladas;
    [SerializeField] private static int TotalColeccionables;

    // Inicia patrón singleton
    public static ControladorGlobal Instance;

    private void Awake()
    {
        if (ControladorGlobal.Instance == null)
        {
            ControladorGlobal.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("Instacia " + ControladorGlobal.Instance.ToString());
    }
    // Finaliza patrón singleton

    public void IncrementarMuertes()
    {
        muertesAcumuladas++;
    }

    public void ReiniciarValores()
    {
        Debug.Log("muertesAcumuladas: " + muertesAcumuladas);
        Debug.Log("idDelCheckpointActual: " + ControladorGlobal.idDelCheckpointActual);
        ControladorGlobal.idDelCheckpointActual = 0;
        ControladorGlobal.cambiarCheckpoint = false;
        muertesAcumuladas = 0;
        PausarJuego(false);
    }

    // Actualiza el punto de guardado, retorna el ID del anterior punto
    public static int ActivarPuntoDeGuardado(int nuevoId)
    {
        int paraRetornar = idDelCheckpointActual;        
        if (nuevoId != idDelCheckpointActual)
        {
            Debug.Log("Reemplazando " + idDelCheckpointActual + " por " + nuevoId);
            idDelCheckpointActual = nuevoId;
            cambiarCheckpoint = true;
        }
        return paraRetornar;
    }

    public static void Salir()
    {
        // https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
        #if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public static void ReiniciarEscena()
    {
        // Obtén el índice de la escena actual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Carga la misma escena por su índice
        SceneManager.LoadScene(currentSceneIndex);
        PausarJuego(false);
    }

    public static void PausarJuego(bool Estado)
    {
        Time.timeScale = Estado ? 0.0f : 1.0f; 
    }

    public void SetFinalizarEscena(bool Estado)
    {
        NivelFinalizado = Estado;
    }

    public bool GetFinalizarEscena()
    {
        return NivelFinalizado;
    }

    public static void SetTotalColeccionables(int total)
    {
        TotalColeccionables = total;
    }

    public static int GetTotalColeccionables()
    {
        return TotalColeccionables;
    }

}