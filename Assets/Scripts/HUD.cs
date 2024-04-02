using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    int intervalo = 1;
    float nexttime = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = "Monedas: " + GameManager.Instance.coins;
        if (Time.timeSinceLevelLoad >= nexttime)
        {
            nexttime += intervalo;
            GameManager.Instance.segundos -= 1;
        }
        if (GameManager.Instance.segundos < 0)
        {
            GameManager.Instance.segundos = 0;
        }
        if (GameManager.Instance.segundos == 0 && GameManager.Instance.minutos >= 1)
        {
            GameManager.Instance.minutos -= 1;
            GameManager.Instance.segundos = 59;
        }
        if (GameManager.Instance.minutos == 0 && GameManager.Instance.segundos == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (GameManager.Instance.segundos < 10)
        {
            GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = "Tiempo: " + GameManager.Instance.minutos + ":0" + GameManager.Instance.segundos;
        }
        else
        {
            GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = "Tiempo: " + GameManager.Instance.minutos + ":" + GameManager.Instance.segundos;
        }
    }
}
