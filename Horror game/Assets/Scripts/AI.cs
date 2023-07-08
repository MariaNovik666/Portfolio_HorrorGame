using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    Transform player;


    public GameObject AICaracter;
    private Animator AIAnimator;

    void Start()
    {
        AIAnimator = AICaracter.GetComponent<Animator>();
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }
}
