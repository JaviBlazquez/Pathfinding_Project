using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetCoins : MonoBehaviour
{
    public Image Healthbar;
    private void Start()
    {
        Healthbar.fillAmount = 1f;
    }
    private HUD funcion;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Coin")
        {
            Destroy(other.gameObject);
            GameManager.Instance.coins = GameManager.Instance.coins+1;
        }
        if (other.transform.tag == "Lava Walker")
        {
            Destroy(other.gameObject);
            GameManager.Instance.LavaWalkers = true;
        }
        if (other.transform.tag == "Water Walker")
        {
            Destroy(other.gameObject);
            GameManager.Instance.WaterWalkers = true;
        }
        if(other.transform.tag == "Enemy")
        {
            GameManager.Instance.health = GameManager.Instance.health - 1;
            Healthbar.fillAmount -=  Healthbar.fillAmount/(GameManager.Instance.health+1);
        }
        if(other.transform.tag == "FinalJuego")
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
