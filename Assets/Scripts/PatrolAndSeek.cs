
using UnityEngine;
using UnityEngine.AI;

public class PatrolAndSeek : MonoBehaviour
{

    NavMeshAgent agent; // Obtenemos el NavMeshAgent 
    public GameObject target; // Objetivo a buscar y perseguir
    public float targetInRange = 6f;


    // Parametros para PATROL
    public float patrolRadio = 10f;
    Vector3 patrolPoint;
    bool hasPatrolPoint = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();// Obtenemos el NavMesh de la escena

    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    bool ObjetivoEnRango()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < targetInRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        // Si el objetivo no está en rango
        if (!ObjetivoEnRango())
        {
            // Patrullar
            Patrol();

        }
        // Si el objetivo está en rango entonces se busca y persigue
        else
        {
            // Perseguir objetivo
            Seek(target.transform.position);
        }
    }

    void Patrol()
    {
        if (!hasPatrolPoint)
        {
            //Obtenemos una dirección aleatoria dentro del radio
            //Con "insideUnitSphere" se crea una esfera imaginaria que se usa para multiplicar por el Radio de la 
            // patrulla preestablecido y así obtener la dirección aleatoria, sin tener que usar un Sphere Collider con isTrigger
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadio;
            randomDirection += transform.position;

            //
            NavMeshHit hit;
            // Si el punto aleatorio está sobre el NavMesh y es válido.
            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadio, NavMesh.AllAreas))
            {
                // Si el punto es válido, lo asignamos como punto de patrulla
                patrolPoint = hit.position;
                // Marcamos que ya tenemos un punto de patrulla
                hasPatrolPoint = true;

                // Indicamos al NavMeshAgent que se dirija a ese punto
                agent.SetDestination(patrolPoint);
            }
        }
        // Si hemos llegado al punto de patrulla o el agente está detenido
        if (!agent.pathPending && (agent.remainingDistance < 0.5f || agent.velocity.magnitude == 0f))
        {
            hasPatrolPoint = false; // Marcamos que necesitamos un nuevo punto
        }
    }



}
