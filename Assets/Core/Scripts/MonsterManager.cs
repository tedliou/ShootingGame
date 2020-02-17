using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterManager : MonoBehaviour
{
    GameObject playerObject;
    NavMeshAgent navMeshAgent;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag(Parameter.Tag.Player);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.SetDestination(playerObject.transform.position);
    }
}
