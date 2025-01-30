using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    public float pickupRange = 5f;          // Distancia máxima para recoger objetos
    public float holdDistance = 2f;        // Distancia a la que se sostendrá el objeto
    public float smoothness = 10f;         // Suavidad del movimiento del objeto al ser sostenido
    public float throwForce = 500f;        // Fuerza con la que se lanzará el objeto

    private Camera playerCamera;           // La cámara del jugador
    private Rigidbody heldObject;          // El objeto que estamos sosteniendo
    private Transform holdPoint;           // Punto donde sostenemos el objeto

    void Start()
    {
        playerCamera = Camera.main;

        // Crear un punto vacío para sostener los objetos
        holdPoint = new GameObject("HoldPoint").transform;
        holdPoint.parent = playerCamera.transform;
        holdPoint.localPosition = new Vector3(0, 0, holdDistance);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo para recoger o soltar
        {
            if (heldObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Tecla Q para lanzar el objeto
        {
            if (heldObject != null)
            {
                ThrowObject();
            }
        }

        if (heldObject != null)
        {
            MoveHeldObject();
        }
    }

    void TryPickUpObject()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Raycast desde el centro de la pantalla
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                heldObject = rb;
                heldObject.useGravity = false;
                heldObject.drag = 10f; // Añadir algo de resistencia para estabilidad
            }
        }
    }

    void DropObject()
    {
        heldObject.useGravity = true;
        heldObject.drag = 0f;
        heldObject = null;
    }

    void ThrowObject()
    {
        heldObject.useGravity = true;
        heldObject.drag = 0f;

        // Aplicar una fuerza hacia adelante desde la cámara
        heldObject.AddForce(playerCamera.transform.forward * throwForce);

        heldObject = null;
    }

    void MoveHeldObject()
    {
        Vector3 targetPosition = holdPoint.position;
        Vector3 moveDirection = targetPosition - heldObject.position;

        // Mover el objeto suavemente hacia el punto de sostén
        heldObject.velocity = moveDirection * smoothness;
    }
}