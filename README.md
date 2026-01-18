## (NO ESTOY SEGURO DE SI ESTE EJERCICIO ESTA CORRECTO)
# IAAutonomouslyMovingAgents

En este ejercicio creé un metoo llamado **PatronandSeek** el cual combina el **PATROL** parecido al del ejercicio de FSM y el **SEEK**
basicamente en la escena ***los ladrones patrullan aleatoriamente por la escena y si el policia entra dentro de su radio de detección 
se mueven hacia el***.

 - Creo y declaro las variables que voy a usar:
    ```csharp
   
    NavMeshAgent agent; // Obtenemos el NavMeshAgent 
    public GameObject target; // Objetivo a buscar y perseguir
    public float targetInRange = 6f; // Rango para detectar el objetivo


    // Parametros para **PATROL**
    public float patrolRadio = 10f; // Radio de patrulla
    Vector3 patrolPoint; // Punto de patrulla
    bool hasPatrolPoint = false; // Indica si ya tenemos un punto de patrulla
    ```

  - En el método **START** obtengo el **NavMesh** de la escena:
    ```csharp
        void Start()
    {
        // Inicializamos el NavMeshAgent
        agent = GetComponent<NavMeshAgent>();// Obtenemos el NavMesh de la escena

    }
    ```
  - Creo el **metodo Seek** para buscar e ir hacia el objetivo:
    ```csharp
     void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }
    ```
  - Mediante el metod **ObjetivoEnRango** compruebo si el Agent esta en el rango preestablecido:
    ```csharp
    bool ObjetivoEnRango()
    {
        //Si la distancia entre el ladron y el objetivo es menor que el rango establecido
        if (Vector3.Distance(transform.position, target.transform.position) < targetInRange)
        {
            // El objetivo está en el rango
            return true;
        }
        else
        {
            // El objetivo no está en el rango
            return false;
        }
    }
    ```
- Mediante el metodo **Update** alterno entre **PATROL** y **SEEK**:
  ```csharp
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
  ```

  - El método **PATROL** es distino en este caso, en ***FSM se usaban WayPoints aqui lo que hago es que Patrullen
    por el NAVMESH de forma aleatoria***, para ello primero obtengo una dirección aleatorio dentro del Radio de acción.
    ```csharp
        void Patrol()
    {
        if (!hasPatrolPoint)
        {
            //Obtenemos una dirección aleatoria dentro del radio
            //Con "insideUnitSphere" se crea una esfera imaginaria que se usa para multiplicar por el Radio de la 
            // patrulla preestablecido y así obtener la dirección aleatoria, sin tener que usar un Sphere Collider con isTrigger
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadio;
            // Sumamos la posición actual para obtener una posición relativa al agente
            randomDirection += transform.position;
    ```
    - Creo la variable para almacenar el punto donde deberan patrullar en el NavMesh y si dicho punto el cual es aleatorio
      es valido y esta sobre el **NavMesh** entones la **Patrulla** se ejecuta haciendo que los ladrones se **"muevan"** de manera aleatorio or el mapa
    ```csharp
       // Variable para almacenar el punto de patrulla en el NavMesh
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
    ```
    - Por ultimo comprueba si los ladrones ya han llegado al punto de patrulla que se genero para ellos y si se detienen en el
      entonces se genera uno nuevo, haciendo de esta manera que no se queden quietos en la escena y esten patrullando siempre.
    ```csharp
      // Si los ladrones han llegado al punto de patrulla y necesitan un nuevo punto
        if (!agent.pathPending && (agent.remainingDistance < 0.5f || agent.velocity.magnitude == 0f))
        {
            hasPatrolPoint = false; // Marcamos que necesitamos un nuevo punto
        }
    ```

