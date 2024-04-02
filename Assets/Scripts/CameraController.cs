using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class CameraController : MonoBehaviour
{
    public Transform objetivoAseguir;
    private float distanciaObjetivoX = 2f;
    private float velocidadcamara = 15f;
    public bool suavizadoactivado = false;
    private Vector3 nuevaPosicion;
    

    // Update is called once per frame
    void Update()
    {
        nuevaPosicion = this.transform.position;
        nuevaPosicion.x = objetivoAseguir.transform.position.x + distanciaObjetivoX;
        nuevaPosicion.z = objetivoAseguir.transform.position.z;
        if (suavizadoactivado)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, nuevaPosicion, velocidadcamara * Time.deltaTime);
        }
        else
        {
            this.transform.position = nuevaPosicion;
        }
    }
}

