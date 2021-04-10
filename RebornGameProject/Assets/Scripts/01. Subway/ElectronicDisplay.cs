using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 권준호
/// Player 가 전광판 앞을 지나갈 때 불이 켜지도록 하는 스크립트. 
/// </summary>
public class ElectronicDisplay : MonoBehaviour
{
    private bool isTurnedOn = false;

    // ReadOnly vars
    private int playerLayerMask;
    private Material displayMaterial;
    private Vector3 rayStartPoint;
    private Color emissionOff;
    private Color emissionOn;
    private Light backLight;

    private void Start()
    {
        playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        displayMaterial = GetComponent<Renderer>().material;
        displayMaterial.EnableKeyword("_EMISSION");
        rayStartPoint = transform.position - new Vector3(0, 15, 0);
        backLight = transform.Find("BackLight").GetComponent<Light>();
        emissionOff = displayMaterial.GetColor("_EmissionColor");
        emissionOn = new Color(0.8f, 0.8f, 0.8f);
    }

    private void Update()
    {
        if (isTurnedOn) return;
        if (Physics.Raycast(rayStartPoint, new Vector3(1, 0, 0), 100f, playerLayerMask))
        {
            isTurnedOn = true;
            StartCoroutine(TurnOnCoroutine());
        }
    }

    private void BackLight(bool turnOn)
    {
        if (turnOn)
        {
            backLight.enabled = true;
            displayMaterial.SetColor("_EmissionColor", emissionOn);
        }
        else
        {
            backLight.enabled = false;
            displayMaterial.SetColor("_EmissionColor", emissionOff);
        }
    }

    private IEnumerator TurnOnCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 1; i < Random.Range(2.1f, 3.1f); i++)
        {
            BackLight(true);
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            BackLight(false);
            yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));
        }
        BackLight(true);
    }
}
