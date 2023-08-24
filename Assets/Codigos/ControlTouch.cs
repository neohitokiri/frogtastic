using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTouch : MonoBehaviour
{
    [SerializeField] public GameObject menuPausa;

    public void BtnPauseDown()
    {
        Time.timeScale = 0;
        if (menuPausa != null)
            menuPausa.SetActive(true);
        Debug.Log("Presiona Pausa");
    }

    public void BtnJumpDown(){
        ControladorGlobal.Instance.BotonSaltoTactilEstaPresionado = true;
        Debug.Log("Presiona Salto");
    }
    public void BtnJumpUp(){
        ControladorGlobal.Instance.BotonSaltoTactilEstaPresionado = false;
        Debug.Log("Suelta Salto");
    }

    public void BtnLeftDown(){
        ControladorGlobal.Instance.BotonIzquierdoTactilEstaPresionado = true;
        Debug.Log("Presiona Izquierda");
    }
    public void BtnLeftUp(){
        ControladorGlobal.Instance.BotonIzquierdoTactilEstaPresionado = false;
        Debug.Log("Suelta Izquierda");
    }

    public void BtnRightDown(){
        ControladorGlobal.Instance.BotonDerechoTactilEstaPresionado = true;
        Debug.Log("Presiona Derecha");
    }
    public void BtnRightUp(){
        ControladorGlobal.Instance.BotonDerechoTactilEstaPresionado = false;
        Debug.Log("Suelta Derecha");
    }

}
