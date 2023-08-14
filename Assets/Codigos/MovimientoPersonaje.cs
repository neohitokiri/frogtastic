using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [SerializeField] public float velocidadMovimiento = 5.0f;
    [SerializeField] public Vector2 posicionInicial;
    [SerializeField] public float fuerzaSalto = 7.0f;
    [SerializeField] public bool enElSuelo = false;
    [SerializeField] public bool enElAgua = false;

    [SerializeField] private int muertesAcumuladas = 0;
    [SerializeField] public int puntosAcumulados = 0;
    [SerializeField] public int puntosDesplegados = 0;

    [SerializeField] public float intensidadMaximaDeLaLuz;
    [SerializeField] public float intensidadMinimaDeLaLuz;

    [SerializeField] private ParticleSystem particulas;
    [SerializeField] private ParticleSystem particulasMuerte;

    private Rigidbody2D cuerpoRigido;
    private Animator animaciones;

    private AudioSource sonidoSalto;
    private AudioSource sonidoMuerte;

    [SerializeField] public UnityEngine.Rendering.Universal.Light2D luzGlobal;

    private void Start() {
        foreach(Transform child in transform)
        {
            if (child.gameObject.name == "SonidoSalto")
                sonidoSalto = child.gameObject.GetComponent<AudioSource>();
            if (child.gameObject.name == "SonidoMuerte")
                sonidoMuerte = child.gameObject.GetComponent<AudioSource>();
            else if (child.gameObject.name == "Particulas")
                particulas = child.gameObject.GetComponent<ParticleSystem>();
            else if (child.gameObject.name == "ParticulasMuerte")
                particulasMuerte = child.gameObject.GetComponent<ParticleSystem>();
        }
    }

    void Awake()
    {
        intensidadMaximaDeLaLuz = 2.0f;
        intensidadMinimaDeLaLuz = 0.2f;
        cuerpoRigido = GetComponent<Rigidbody2D>();   
        animaciones = GetComponent<Animator>();
        posicionInicial = transform.position;
        puntosDesplegados = GameObject.FindGameObjectsWithTag("Coleccionable").Length;
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        cuerpoRigido.velocity = new Vector2(movimientoHorizontal * velocidadMovimiento, cuerpoRigido.velocity.y);

        if (Input.GetButtonDown("Jump") && (enElSuelo || enElAgua))
        {
            cuerpoRigido.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            enElSuelo = false;
            sonidoSalto.Play();
            particulas.Play();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (movimientoHorizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (movimientoHorizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        animaciones.SetInteger("Salto", (int) cuerpoRigido.velocity.y);
        animaciones.SetBool("Piso", enElSuelo);

        if(enElSuelo)
            animaciones.SetFloat("MovimientoHorizontal", Mathf.Abs(movimientoHorizontal));
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
            enElSuelo = true;

        if (collision.gameObject.CompareTag("PlataformaMovil"))
        {
            enElSuelo = true;
            transform.parent = collision.transform;
        }

        if (collision.gameObject.CompareTag("Morir"))
            Reinicio();
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("PlataformaMovil"))
        {
            transform.parent = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        enElAgua = other.gameObject.CompareTag("Agua");
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Agua"))
        {
            enElAgua = false;
        };
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Agua"))
        {
            enElAgua = true;
        };
    }

    void Reinicio()
    {
        sonidoMuerte.Play();
        transform.position = posicionInicial;
        cuerpoRigido.velocity = Vector2.zero;
        cuerpoRigido.angularVelocity = 0;
        cuerpoRigido.bodyType = RigidbodyType2D.Static;
        // Partículas para indicar muerte
        particulasMuerte.transform.position = transform.position;
        particulasMuerte.Play();

        cuerpoRigido.bodyType = RigidbodyType2D.Dynamic;
        muertesAcumuladas++;
        Debug.Log(muertesAcumuladas);
    }

    public void Iluminar()
    {
        puntosAcumulados++;
        float intensidadPorLuz = (intensidadMaximaDeLaLuz - intensidadMinimaDeLaLuz) / puntosDesplegados;
        luzGlobal.intensity = 0.2f + (intensidadPorLuz * puntosAcumulados);

//        luzGlobal.intensity = 1 + (luz * porcentajeLuz / 100);
    }

        void Quit()
        {
            #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be
                // set to false to end the game.
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
}

