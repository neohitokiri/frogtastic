using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControladorGlobal
{
    
    [SerializeField] public static int idDelCheckpointActual;
    [SerializeField] public static bool cambiarCheckpoint;

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
}
