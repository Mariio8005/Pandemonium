using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenas : MonoBehaviour
{
    [Tooltip("Nombre de la escena a la que cambiar cuando el jugador toque este objeto")]
    public string sceneToLoad; // Define la escena desde el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador entra en contacto
        {
            if (!string.IsNullOrEmpty(sceneToLoad)) // Verifica que haya una escena asignada
            {
                SceneManager.LoadScene(sceneToLoad); // Cambia de escena
            }
            else
            {
                Debug.LogWarning("No se ha asignado una escena en " + gameObject.name);
            }
        }
    }
}
