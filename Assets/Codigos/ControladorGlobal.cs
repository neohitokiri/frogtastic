using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGlobal : MonoBehaviour
{
    [SerializeField] public int idDelCheckpointActual;
    [SerializeField] public bool cambiarCheckpoint;
    [SerializeField] public bool NivelFinalizado;
    [SerializeField] public int muertesAcumuladas;
    [SerializeField] private int TotalColeccionables;
    [SerializeField] private int TotalRecogidos;
    [SerializeField] [Range(0.0f, 1.0f)]public float VolumenGlobalDeLaMusica;
    [SerializeField] [Range(0.0f, 1.0f)]public float VolumenGlobalDeLosSFX;

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

        NivelFinalizado = false;
    }
    // Finaliza patrón singleton

    private void OnValidate() {
        NormalizarVolumen();
    }

    private void NormalizarVolumen()
    {
        // Obtén la cantidad de escenas en la build
        int sceneCount = SceneManager.sceneCount;

        // Itera a través de todas las escenas
        for (int i = 0; i < sceneCount; i++)
        {
            // Obtiene la escena en la posición "i"
            Scene scene = SceneManager.GetSceneAt(i);

            // Encuentra todos los objetos AudioSource en la escena actual
            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject rootObject in rootObjects)
            {
                AudioSource[] audioSources = rootObject.GetComponentsInChildren<AudioSource>(true);

                // Itera a través de los AudioSources en el objeto actual
                foreach (AudioSource audioSource in audioSources)
                {
                    // Verifica si el objeto tiene la etiqueta "Musica"
                    audioSource.volume = audioSource.gameObject.CompareTag("Musica") ? VolumenGlobalDeLaMusica : VolumenGlobalDeLosSFX;
                    Debug.Log("Encontré un AudioSource de música en el objeto: " + audioSource.gameObject.name + " y le asigné el volumen " + audioSource.volume.ToString());
                }
            }
        }
    }

    public void IncrementarMuertes()
    {
        muertesAcumuladas++;
    }

    private void Update() {
        NivelFinalizado = (TotalColeccionables - TotalRecogidos < 1);
//        if (!NivelFinalizado && (TotalColeccionables - TotalRecogidos < 1))
//        {
//            NivelFinalizado = true;
//        }
    }

    public void ReiniciarValores()
    {
        idDelCheckpointActual = 0;
        cambiarCheckpoint = false;
        muertesAcumuladas = 0;
        TotalRecogidos = 0;
        NivelFinalizado = false;
        PausarJuego(false);
        NormalizarVolumen();
    }

    public void ReiniciarMundo()
    {
        ReiniciarValores();
    }


    // Actualiza el punto de guardado, retorna el ID del anterior punto
    public int ActivarPuntoDeGuardado(int nuevoId)
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

    public void Salir()
    {
        // https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
        #if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ReiniciarEscena()
    {
        // Obtén el índice de la escena actual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        NivelFinalizado = false;

        // Carga la misma escena por su índice
        SceneManager.LoadScene(currentSceneIndex);
        PausarJuego(false);
        NormalizarVolumen();
    }

    public void PausarJuego(bool Estado)
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

    public void SetTotalColeccionables(int total)
    {
        TotalColeccionables = total;
    }

    public int GetTotalColeccionables()
    {
        return TotalColeccionables;
    }

    public void SetTotalRecogidos(int total = -99)
    {
        TotalRecogidos = total == -99 ? TotalRecogidos + 1 : total;
    }

    public int GetTotalRecogidos()
    {
        return TotalRecogidos;
    }

    public void CambiarNivel(int IdEscena)
    {
        StartCoroutine(CambiarNivelAsincronamente(IdEscena));
    }

    public IEnumerator CambiarNivelAsincronamente(int IdEscena)
    {
        // Carga la escena asíncronamente
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(IdEscena);
        Debug.Log("CARGANDO ESCENA " + IdEscena.ToString());
        // Espera hasta que la carga esté completa
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Ahora todos los recursos se han cargado, ejecuta la función
        ReiniciarValores();
//        SceneManager.LoadScene(IdEscena);
    }

    public void SetVolumenMusica(float Volumen)
    {
        VolumenGlobalDeLaMusica = Volumen;
    }

    public float GetVolumenMusica()
    {
        return VolumenGlobalDeLaMusica;
    }

    public void SetVolumenSFX(float Volumen)
    {
        VolumenGlobalDeLosSFX = Volumen;
    }

    public float GetVolumenSFX()
    {
        return VolumenGlobalDeLosSFX;
    }

}