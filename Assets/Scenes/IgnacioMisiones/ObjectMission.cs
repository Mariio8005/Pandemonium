using System.Collections;
using UnityEngine;
using TMPro;

public class ObjectMission : MonoBehaviour
{
    public GameObject objectToDeliver; // Objeto a transportar
    public Transform deliveryZone; // Zona de entrega
    public TextMeshProUGUI missionText; // Texto de la misión
    public GameObject missionPanel; // Panel que contiene el texto de la misión
    public float completionDelay = 2f; // Tiempo antes de desaparecer el mensaje de misión completada

    private bool missionComplete = false;

    void Start()
    {
        UpdateMissionText();
    }

    void Update()
    {
        if (!missionComplete)
        {
            CheckMissionStatus();
        }
    }

    void CheckMissionStatus()
    {
        float distanceToZone = Vector3.Distance(objectToDeliver.transform.position, deliveryZone.position);
        if (distanceToZone < 1.5f) // Si el objeto está en la zona
        {
            missionComplete = true;
            UpdateMissionText();
            Debug.Log("Misión completada: Objeto entregado en la zona.");
            StartCoroutine(HideMissionTextAndPanelAfterDelay());
        }
    }

    void UpdateMissionText()
    {
        if (missionComplete)
        {
            missionText.text = "Misión completada: Objeto entregado.";
        }
        else
        {
            missionText.text = "Misión: Lleva el objeto a la zona indicada.";
        }
    }

    IEnumerator HideMissionTextAndPanelAfterDelay()
    {
        yield return new WaitForSeconds(completionDelay); // Espera el tiempo especificado
        missionText.text = ""; // Elimina el texto
        missionPanel.SetActive(false); // Desactiva el panel
    }
}