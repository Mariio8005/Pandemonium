using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectMission : MonoBehaviour
{
    public List<GameObject> carrots; // Lista de zanahorias a entregar
    public List<GameObject> onions;  // Lista de cebollas a entregar
    public GameObject dog;           // Objeto del perro
    public GameObject child;         // Objeto del niño
    public Transform deliveryZone;   // Zona de entrega
    public TextMeshProUGUI missionText; // Texto de la misión
    public GameObject missionPanel;  // Panel de la misión
    public float completionDelay = 2f; // Tiempo antes de ocultar la misión

    private bool carrotsMissionComplete = false;
    private bool onionsMissionComplete = false;
    private bool dogMissionComplete = false;
    private bool childMissionComplete = false;

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
        else if (!dogMissionComplete)
        {
            CheckDogMissionStatus();
        }
        else if (!childMissionComplete)
        {
            CheckChildMissionStatus();
        }
    }

    void CheckCarrotMissionStatus()
    {
        foreach (GameObject carrot in carrots)
        {
            float distance = Vector3.Distance(carrot.transform.position, deliveryZone.position);
            if (distance < 2.0f && !deliveredCarrots.Contains(carrot))
            {
                deliveredCarrots.Add(carrot); // Marca la zanahoria como entregada
            }
        }
        if (deliveredCarrots.Count >= carrots.Count)
        {
            carrotsMissionComplete = true;
            missionText.text = "¡Misión de zanahorias completada!";
            UpdateMissionText();
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
            if (distance < 2.0f && !deliveredOnions.Contains(onion))
            {
                deliveredOnions.Add(onion);
            }
        }
        if (deliveredOnions.Count >= onions.Count)
        {
            onionsMissionComplete = true;
            missionText.text = "¡Misión de cebolla completada!";
            UpdateMissionText();
        }
        else
        {
            UpdateMissionText();
        }
    }

    void CheckDogMissionStatus()
    {
        if (dog != null)
        {
            float distance = Vector3.Distance(dog.transform.position, deliveryZone.position);
            if (distance < 2.0f)
            {
                dogMissionComplete = true;
                missionText.text = "¡Misión del perro completada!";
                UpdateMissionText();
            }
        }
    }

    void CheckChildMissionStatus()
    {
        if (child != null)
        {
            float distance = Vector3.Distance(child.transform.position, deliveryZone.position);
            if (distance < 2.0f)
            {
                childMissionComplete = true;
                missionText.text = "¡Misión del niño completada!";
                UpdateMissionText();
                StartCoroutine(HideMissionTextAndPanelAfterDelay());
            }
        }
    }

    void UpdateMissionText()
    {
        if (childMissionComplete)
        {
            missionText.text = "¡Todas las misiones completadas!";
        }
        else if (dogMissionComplete)
        {
            missionText.text = "Lleva al niño al caldero para que cuente su historia.";
        }
        else if (onionsMissionComplete)
        {
            missionText.text = "Lleva el perro al caldero.";
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