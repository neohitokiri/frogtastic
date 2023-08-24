//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ReiniciarEscenario : MonoBehaviour
{
    public void Reiniciar()
    {
        ControladorGlobal.Instance.ReiniciarEscena();
    }

    public void Pausar(bool Estado)
    {
        ControladorGlobal.Instance.PausarJuego(Estado);
    }

    public void Ocultar(bool Estado)
    {
        // Btn > Panel > MenuPausa
        Transform parentTransform = transform.parent.transform.parent;
        if (parentTransform != null)
        {
            parentTransform.gameObject.SetActive(Estado);
        }
    }

    public void AlMenuPrincipal()
    {
        ControladorGlobal.Instance.CambiarNivel(0);
    }
}
