using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class misionesCocina : MonoBehaviour
{
    // Referencias a los objetos necesarios
    public GameObject fridge;          // Nevera donde est� la sand�a
    public GameObject watermelon;      // Sand�a que debe sacarse
    public GameObject knife;           // Cuchillo que debe cogerse
    public Transform kitchenLocation;  // Ubicaci�n de la cocina
    public TextMeshProUGUI missionText; // Texto de la misi�n
    public GameObject missionPanel;    // Panel de la misi�n
    public float completionDelay = 2f; // Tiempo antes de ocultar la misi�n

    private bool goToKitchenComplete = false;   // Misi�n para ir a la cocina
    private bool takeWatermelonComplete = false; // Misi�n para sacar la sand�a
    private bool takeKnifeComplete = false;     // Misi�n para coger el cuchillo
    private bool suicideComplete = false;       // Misi�n final (suicidio)

    void Start()
    {
        UpdateMissionText();
    }

    void Update()
    {
        if (!goToKitchenComplete)
        {
            // No necesitas nada aqu� porque usaremos OnTriggerEnter
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

    // Detecta cuando el jugador entra en el �rea de la cocina
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject && kitchenLocation != null)
        {
            goToKitchenComplete = true;
            missionText.text = "�Has llegado a la cocina! Ahora saca la sand�a de la nevera.";
            UpdateMissionText();
        }
    }

    // Detecta cuando el jugador sale del �rea de la cocina (opcional)
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == gameObject && !goToKitchenComplete)
        {
            // Puedes agregar l�gica adicional si es necesario
        }
    }

    void CheckTakeWatermelonStatus()
    {
        if (watermelon != null && Vector3.Distance(watermelon.transform.position, fridge.transform.position) > 2.0f)
        {
            takeWatermelonComplete = true;
            missionText.text = "�Has sacado la sand�a de la nevera! Ahora coge el cuchillo.";
            UpdateMissionText();
        }
    }

    void CheckTakeKnifeStatus()
    {
        if (knife != null && Vector3.Distance(knife.transform.position, transform.position) < 1.0f)
        {
            takeKnifeComplete = true;
            missionText.text = "�ltima misi�n: Suic�date presionando 'K'.";
            UpdateMissionText();
        }
    }

    void CheckSuicideStatus()
    {
        if (Input.GetKeyDown(KeyCode.K)) // Pulsar 'K' para completar el acto
        {
            suicideComplete = true;
            missionText.text = "La misi�n ha terminado. Descansa en paz.";
            StartCoroutine(HideMissionTextAndPanelAfterDelay());
        }
    }

    void UpdateMissionText()
    {
        if (suicideComplete)
        {
            missionText.text = "La misi�n ha terminado. Descansa en paz.";
        }
        else if (takeKnifeComplete)
        {
            missionText.text = "�ltima misi�n: Suic�date presionando 'K'.";
        }
        else if (takeWatermelonComplete)
        {
            missionText.text = "Ahora coge el cuchillo.";
        }
        else if (goToKitchenComplete)
        {
            missionText.text = "Saca la sand�a de la nevera.";
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