using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mision : MonoBehaviour
{
    public TextMeshProUGUI missionText; // Asigna el objeto de texto TMP en el Inspector
    public GameObject missionPanel; // Asigna el panel que contiene el texto en el Inspector
    private bool isCollected = false;

    void Start()
    {
        UpdateMissionText();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            UpdateMissionText();
            StartCoroutine(HideMissionTextAndPanel()); // Inicia la corutina para ocultar el texto y el panel
        }
    }

    void UpdateMissionText()
    {
        if (isCollected)
        {
            missionText.text = "Misión 1/3: Recoger la esfera roja: 1/1";
        }
        else
        {
            missionText.text = "Misión 0/3: Recoger la esfera roja: 0/1";
        }
    }

    IEnumerator HideMissionTextAndPanel()
    {
        yield return new WaitForSeconds(1f); // Espera 1 segundo antes de ocultar el texto
        missionText.gameObject.SetActive(false); // Oculta el texto
        missionPanel.SetActive(false); // Desactiva el panel que contiene el texto
        gameObject.SetActive(false); // Desactiva la esfera (el objeto recolectable)
    }
}