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

    private bool carrotsMissionComplete = false;
    private bool onionsMissionComplete = false;
    private HashSet<GameObject> deliveredCarrots = new HashSet<GameObject>(); // Zanahorias entregadas
    private HashSet<GameObject> deliveredOnions = new HashSet<GameObject>();  // Cebollas entregadas

    void Start()
    {
        UpdateMissionText();
    }

    void Update()
    {
        if (!carrotsMissionComplete)
        {
            CheckCarrotMissionStatus();
        }
        else if (!onionsMissionComplete)
        {
            CheckOnionMissionStatus();
        }
    }

    void CheckCarrotMissionStatus()
    {
        foreach (GameObject carrot in carrots)
        {
            float distance = Vector3.Distance(carrot.transform.position, deliveryZone.position);
            if (distance < 1.5f && !deliveredCarrots.Contains(carrot))
            {
                deliveredCarrots.Add(carrot); // Marca la zanahoria como entregada
            }
        }

        if (deliveredCarrots.Count >= carrots.Count)
        {
            carrotsMissionComplete = true;
            UpdateMissionText();
            Debug.Log("¡Misión de zanahorias completada!");
        }
        else
        {
            UpdateMissionText();
        }
    }

    void CheckOnionMissionStatus()
    {
        foreach (GameObject onion in onions)
        {
            float distance = Vector3.Distance(onion.transform.position, deliveryZone.position);
            if (distance < 1.5f && !deliveredOnions.Contains(onion))
            {
                deliveredOnions.Add(onion);
            }
        }

        if (deliveredOnions.Count >= onions.Count)
        {
            onionsMissionComplete = true;
            UpdateMissionText();
            Debug.Log("¡Misión de cebolla completada!");
            StartCoroutine(HideMissionTextAndPanelAfterDelay());
        }
        else
        {
            UpdateMissionText();
        }
    }

    void UpdateMissionText()
    {
        if (onionsMissionComplete)
        {
            missionText.text = "¡Misión completada!";
        }
        else if (carrotsMissionComplete)
        {
            missionText.text = $"Entrega 1 cebolla al caldero {deliveredOnions.Count}/1.";
        }
        else
        {
            missionText.text = $"Entrega 3 zanahorias al caldero {deliveredCarrots.Count}/3.";
        }
    }

    IEnumerator HideMissionTextAndPanelAfterDelay()
    {
        yield return new WaitForSeconds(completionDelay);
        missionText.text = "";
        missionPanel.SetActive(false);
    }
}
