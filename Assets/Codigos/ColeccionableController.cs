using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum TipoDeColeccionable
{
    Vela = 0,
    Bombillo = 1,
}

public class ColeccionableController : MonoBehaviour
{
    [SerializeField] private bool collected;
//    [SerializeField] public Puntaje puntaje;

    [SerializeField] public GameObject GuiPuntaje;

    [SerializeField] public Light2D luzCentral;
    [SerializeField] public Light2D luzProyeccion;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public TipoDeColeccionable Tipo;
    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private Texture2D ImagenAntesDeTomar;
    [SerializeField] private Texture2D ImagenDespuesDeTomar;

//    private void Awake() {
//    }

    private void Awake() {
        // arrastrar el objeto GUI del puntaje al campo "puntaje" 
        collected = false;
        sprite = GetComponent<SpriteRenderer>();
        foreach(Transform child in transform)
        {
            if (child.gameObject.name == "LuzCentral")
                luzCentral = child.gameObject.GetComponent<Light2D>();
            else if (child.gameObject.name == "LuzProyeccion")
                luzProyeccion = child.gameObject.GetComponent<Light2D>();
            else if (child.gameObject.name == "Sonido")
                audioSource = child.gameObject.GetComponent<AudioSource>();
        }

        GuiPuntaje = (GameObject)Resources.Load("prefabs/Elements/Bombillo", typeof(GameObject));

        string _antes = "";
        string _despues = "";

        switch(Tipo)
        {
            case TipoDeColeccionable.Vela:
                _antes = "Arte/Propio/bombillo-amarillo";
                _despues = "Arte/Propio/bombillo-blanco";
                break;
            case TipoDeColeccionable.Bombillo:
                _antes = "Arte/Propio/bombillo-amarillo";
                _despues = "Arte/Propio/bombillo-blanco";
                break;
        }

        // Configura textura
        ImagenAntesDeTomar = Resources.Load<Texture2D>(_antes);
        ImagenDespuesDeTomar = Resources.Load<Texture2D>(_despues);
        sprite.sprite = CrearSpriteDeTextura(ImagenAntesDeTomar);
    }

    private Sprite CrearSpriteDeTextura(Texture2D Textura)
    {
        return Sprite.Create(
            Textura, 
            new Rect(0, 0, Textura.width, Textura.height),
            new Vector2(0.5f, 0.5f)
        );
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if (!collected)
            {
//                puntaje.SumarPuntos(1.0f);
//                Destroy(gameObject);
//                other.GetComponent<MovimientoPersonaje>().Iluminar();
                luzCentral.intensity+=0.2f;
                luzProyeccion.intensity+=0.6f;
                luzProyeccion.color = Color.white;
                audioSource.Play();
                collected = true;
                sprite.sprite = CrearSpriteDeTextura(ImagenDespuesDeTomar);
                ControladorGlobal.Instance.SetTotalRecogidos();
            }
        }
    }
}
