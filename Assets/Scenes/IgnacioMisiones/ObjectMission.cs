using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectMission : MonoBehaviour
{
    public List<GameObject> carrots; // Lista de zanahorias a entregar
    public List<GameObject> onions;  // Lista de cebollas a entregar
    public Transform deliveryZone;   // Zona de entrega
    public TextMeshProUGUI missionText; // Texto de la misión
    public GameObject missionPanel;  // Panel de la misión
    public float completionDelay = 2f; // Tiempo antes de ocultar la misión

    private bool missionComplete = false;
    private HashSet<GameObject> deliveredCarrots = new HashSet<GameObject>(); // Zanahorias entregadas
    private HashSet<GameObject> deliveredOnions = new HashSet<GameObject>();  // Cebollas entregadas

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
        foreach (GameObject carrot in carrots)
        {
            float distance = Vector3.Distance(carrot.transform.position, deliveryZone.position);
            if (distance < 1.5f && !deliveredCarrots.Contains(carrot))
            {
                deliveredCarrots.Add(carrot); // Marca la zanahoria como entregada
            }
        }

        foreach (GameObject onion in onions)
        {
            float distance = Vector3.Distance(onion.transform.position, deliveryZone.position);
            if (distance < 1.5f && !deliveredOnions.Contains(onion))
            {
                deliveredOnions.Add(onion); // Marca la cebolla como entregada
            }
        }

        // Si todas las zanahorias y cebollas han sido entregadas, completar misión
        if (deliveredCarrots.Count >= carrots.Count && deliveredOnions.Count >= onions.Count)
        {
            missionComplete = true;
            UpdateMissionText();
            Debug.Log("¡Misión completada!");
            StartCoroutine(HideMissionTextAndPanelAfterDelay());
        }
        else
        {
            UpdateMissionText(); // Actualiza el texto en tiempo real
        }
    }

    void UpdateMissionText()
    {
        if (missionComplete)
        {
            missionText.text = "¡Misión completada!";
        }
        else
        {
            missionText.text = $"Entrega 3 zanahorias al caldero {deliveredCarrots.Count}/3.\n" +
                               $"Entrega 1 cebolla al caldero {deliveredOnions.Count}/1.";
        }
    }

    IEnumerator HideMissionTextAndPanelAfterDelay()
    {
        yield return new WaitForSeconds(completionDelay);
        missionText.text = "";
        missionPanel.SetActive(false);
    }
}