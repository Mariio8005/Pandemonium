using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class misionesCocina : MonoBehaviour
{
    // Referencias a los objetos necesarios
    public GameObject fridge;          // Nevera donde está la sandía
    public GameObject watermelon;      // Sandía que debe sacarse
    public GameObject knife;           // Cuchillo que debe cogerse
    public Transform kitchenLocation;  // Ubicación de la cocina
    public TextMeshProUGUI missionText; // Texto de la misión
    public GameObject missionPanel;    // Panel de la misión
    public float completionDelay = 2f; // Tiempo antes de ocultar la misión

    private bool goToKitchenComplete = false;   // Misión para ir a la cocina
    private bool takeWatermelonComplete = false; // Misión para sacar la sandía
    private bool takeKnifeComplete = false;     // Misión para coger el cuchillo
    private bool suicideComplete = false;       // Misión final (suicidio)

    void Start()
    {
        UpdateMissionText();
    }

    void Update()
    {
        if (!goToKitchenComplete)
        {
            // No necesitas nada aquí porque usaremos OnTriggerEnter
        }
        else if (!takeWatermelonComplete)
        {
            CheckTakeWatermelonStatus();
        }
        else if (!takeKnifeComplete)
        {
            CheckTakeKnifeStatus();
        }
        else if (!suicideComplete)
        {
            CheckSuicideStatus();
        }
    }

    // Detecta cuando el jugador entra en el área de la cocina
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject && kitchenLocation != null)
        {
            goToKitchenComplete = true;
            missionText.text = "¡Has llegado a la cocina! Ahora saca la sandía de la nevera.";
            UpdateMissionText();
        }
    }

    // Detecta cuando el jugador sale del área de la cocina (opcional)
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == gameObject && !goToKitchenComplete)
        {
            // Puedes agregar lógica adicional si es necesario
        }
    }

    void CheckTakeWatermelonStatus()
    {
        if (watermelon != null && Vector3.Distance(watermelon.transform.position, fridge.transform.position) > 2.0f)
        {
            takeWatermelonComplete = true;
            missionText.text = "¡Has sacado la sandía de la nevera! Ahora coge el cuchillo.";
            UpdateMissionText();
        }
    }

    void CheckTakeKnifeStatus()
    {
        if (knife != null && Vector3.Distance(knife.transform.position, transform.position) < 1.0f)
        {
            takeKnifeComplete = true;
            missionText.text = "Última misión: Suicídate presionando 'K'.";
            UpdateMissionText();
        }
    }

    void CheckSuicideStatus()
    {
        if (Input.GetKeyDown(KeyCode.K)) // Pulsar 'K' para completar el acto
        {
            suicideComplete = true;
            missionText.text = "La misión ha terminado. Descansa en paz.";
            StartCoroutine(HideMissionTextAndPanelAfterDelay());
        }
    }

    void UpdateMissionText()
    {
        if (suicideComplete)
        {
            missionText.text = "La misión ha terminado. Descansa en paz.";
        }
        else if (takeKnifeComplete)
        {
            missionText.text = "Última misión: Suicídate presionando 'K'.";
        }
        else if (takeWatermelonComplete)
        {
            missionText.text = "Ahora coge el cuchillo.";
        }
        else if (goToKitchenComplete)
        {
            missionText.text = "Saca la sandía de la nevera.";
        }
        else
        {
            missionText.text = "Ve a la cocina.";
        }
    }

    IEnumerator HideMissionTextAndPanelAfterDelay()
    {
        yield return new WaitForSeconds(completionDelay);
        missionText.text = "";
        missionPanel.SetActive(false);
    }
}