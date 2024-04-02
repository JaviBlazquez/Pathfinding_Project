using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
public class Nav_TerrainWater : MonoBehaviour
{
    private NavMeshModifier _meshSurface;
    // Start is called before the first frame update
    void Start()
    {
        _meshSurface = GetComponent<NavMeshModifier>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            //Compruebo que lo que ha entrado es un agente
            //Compruebo que el agente se vea afectado por este tipo de terreno
            if (_meshSurface.AffectsAgentType(agent.agentTypeID) && GameManager.Instance.WaterWalkers == false)
            {
                agent.speed /= NavMesh.GetAreaCost(_meshSurface.area);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent) && GameManager.Instance.WaterWalkers == false)
        {
            //Compruebo que lo que ha salido es un agente
            //Compruebo que el agente se vea afectado por este tipo de terreno
            if (_meshSurface.AffectsAgentType(agent.agentTypeID))
            {
                agent.speed *= NavMesh.GetAreaCost(_meshSurface.area);
            }
        }
    }
}
