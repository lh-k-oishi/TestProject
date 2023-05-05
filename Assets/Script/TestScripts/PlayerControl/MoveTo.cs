using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private void Update()
    {
        MoveToTarget();
    }
    public void FindTarget()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        MoveToTarget();
    }
    void MoveToTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent.destination = player.transform.position;
    }
}
