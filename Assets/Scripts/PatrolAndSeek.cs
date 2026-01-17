using System.Numerics;
using UnityEngine;

public class PatrolAndSeek : MonoBehaviour
{

    NavMeshAgent agent; // Obtenemos el NavMeshAgent 
    public GameObject target; // Objetivos a buscar y perseguir

    // Parametros para PATROL
    public float patrolRadio = 10f;
    public float patrolWaitTime = 2f; // Tiempo de espera en cada punto de la patrulla
    private float waitTime = 0f; // Tiempo de espera

    private Vector3 patrolDestination;

    // Parametros de SEEK
    public float detecionRadio = 5f; // Distancia minima para detectar al agente
    public float stopDistance = 2f; // Distancia minima para detenerse ante el objetivo

    private bool isSeeking = false; // Booleana para controlar el estado

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
