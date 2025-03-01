using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto tiene el tag "Player"
        {
            Debug.Log("Saliendo del juego...");
            Application.Quit(); // Cierra el juego

            // En el editor de Unity, esto solo mostrará un mensaje
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
