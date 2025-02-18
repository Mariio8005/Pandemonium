using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ActivarPuerta : MonoBehaviour
{
    public GameObject door; // Referencia a la puerta

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra es el "Dog"
        if (other.CompareTag("Dog"))
        {
            // Activar la puerta
            door.SetActive(true);

        }
    }
}
