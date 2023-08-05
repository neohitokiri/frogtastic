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

    private Rigidbody2D cuerpoRigido;
    private Animator animaciones;

    [SerializeField] public UnityEngine.Rendering.Universal.Light2D luzGlobal;

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
        enElSuelo = collision.gameObject.CompareTag("Suelo");

        if (collision.gameObject.CompareTag("Morir"))
            Reinicio();
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
        transform.position = posicionInicial;
        cuerpoRigido.velocity = Vector2.zero;
        cuerpoRigido.angularVelocity = 0;
        cuerpoRigido.bodyType = RigidbodyType2D.Static;
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
}

