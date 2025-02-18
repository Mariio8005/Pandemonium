using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public Rigidbody rb; // Referencia al Rigidbody del objeto
    public float jumpForce = 500f; // Fuerza de salto
    public LayerMask groundLayer; // Capa del suelo
    public Transform groundCheck; // Punto para detectar el suelo
    public float checkRadius = 0.1f; // Radio de detección del suelo

    private bool isGrounded;

    void Update()
    {
        // Verificar si está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        // Saltar si está en el suelo y se presiona espacio
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnDrawGizmos()
    {
        // Mostrar la esfera de detección del suelo en el Editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

}
