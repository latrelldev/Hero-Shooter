using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemyHealth health;
    public ShootPool shootPool;
    public EnemyMelee melee;
    public Transform[] waypoints;
    public LayerMask visionMask = ~0;

    public float visionRange = 20f;
    public float visionAngle = 60f;
    public float attackRange = 10f;
    public float losePlayerDistance = 30f;
    public float alertRadius = 10f;
    public float fleeHealth = 20f;

    private Transform player;
    private PlayerHealth playerHealth;

    public enum EnemyState
    {
        Procura, Persegue, Ataca, Foge
    }
    public EnemyState currentState = EnemyState.Procura;

    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        player = p.transform;
        playerHealth = p.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (health.currentHealth <= fleeHealth && currentState != EnemyState.Foge)
        {
            ChangeState(EnemyState.Foge);
        }

        FiniteStateMachine();

        if (shootPool != null)
        {
            shootPool.canShoot = currentState == EnemyState.Ataca;
        }
    }

    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    void FiniteStateMachine()
    {
        switch (currentState)
        {
            case EnemyState.Procura:
                Procura();
                break;
            case EnemyState.Persegue:
                Persegue();
                break;
            case EnemyState.Ataca:
                Ataca();
                break;
            case EnemyState.Foge:
                Foge();
                break;
        }
    }

    void Procura()
    {
        agent.stoppingDistance = 0f;
        if (waypoints.Length != 0 && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            int randomIndex = Random.Range(0, waypoints.Length);
            agent.SetDestination(waypoints[randomIndex].position);
        }

        if (CanSeePlayer())
        {
            ChangeState(EnemyState.Persegue);
            ChamarReforcos();
        }
    }

    void Persegue()
    {
        agent.stoppingDistance = attackRange * 0.8f;
        agent.SetDestination(player.position);

        float distancia = Vector3.Distance(transform.position, player.position);
        if (distancia <= attackRange && CanSeePlayer())
        {
            ChangeState(EnemyState.Ataca);
        }
        else if (distancia > losePlayerDistance)
        {
            ChangeState(EnemyState.Procura);
        }
    }

    void Ataca()
    {
        agent.ResetPath();

        float distancia = Vector3.Distance(transform.position, player.position);
        if (distancia > attackRange || !CanSeePlayer())
        {
            ChangeState(EnemyState.Persegue);
            return;
        }

        Vector3 olhar = player.position - transform.position;
        olhar.y = 0f;
        transform.rotation = Quaternion.LookRotation(olhar);

        if (melee != null)
        {
            melee.Atacar(playerHealth);
        }
    }

    void Foge()
    {
        agent.stoppingDistance = 0f;

        HealthPickup cura = CuraMaisProxima();
        if (cura != null)
        {
            agent.SetDestination(cura.transform.position);
        }
        else
        {
            Vector3 longe = transform.position + (transform.position - player.position).normalized * 5f;
            agent.SetDestination(longe);
        }

        if (health.currentHealth > fleeHealth)
        {
            ChangeState(EnemyState.Persegue);
        }
    }

    HealthPickup CuraMaisProxima()
    {
        HealthPickup maisProxima = null;
        float menorDistancia = Mathf.Infinity;

        foreach (HealthPickup cura in HealthPickup.todas)
        {
            if (!cura.disponivel) continue;

            float distancia = Vector3.Distance(transform.position, cura.transform.position);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                maisProxima = cura;
            }
        }
        return maisProxima;
    }

    bool CanSeePlayer()
    {
        Vector3 olhos = transform.position + Vector3.up * 0.5f;
        Vector3 direcao = player.position - olhos;

        if (direcao.magnitude > visionRange) return false;
        if (Vector3.Angle(transform.forward, direcao) > visionAngle) return false;

        if (Physics.Raycast(olhos, direcao.normalized, out RaycastHit hit, visionRange, visionMask, QueryTriggerInteraction.Ignore))
        {
            return hit.collider.GetComponentInParent<PlayerHealth>() != null;
        }
        return false;
    }

    public void Alertar()
    {
        if (currentState == EnemyState.Procura)
        {
            ChangeState(EnemyState.Persegue);
        }
    }

    public void ChamarReforcos()
    {
        EnemyNavMesh[] todosInimigos = FindObjectsByType<EnemyNavMesh>();
        foreach (EnemyNavMesh aliado in todosInimigos)
        {
            float distancia = Vector3.Distance(transform.position, aliado.transform.position);
            if (distancia < alertRadius)
            {
                aliado.Alertar();
            }
        }
    }
}