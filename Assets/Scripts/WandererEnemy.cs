using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WandererEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject waypointsParent;
    private GameObject[] waypoints;
    private int waypointsindex = 0;
    private int max;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        max = waypointsParent.transform.childCount;
        waypoints = new GameObject[max];
        for (int i = 0; i < max; i++)
        {
            waypoints[i] = waypointsParent.transform.GetChild(i).gameObject;
        }
        GoToWayPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.1)
        {
            waypointsindex = (waypointsindex + 1) % max;
            GoToWayPoint();
        }
    }
    private void GoToWayPoint()
    {
        agent.SetDestination(waypoints[waypointsindex].transform.position);
    }
}
