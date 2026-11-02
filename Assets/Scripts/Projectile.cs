using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    public float lifeTime = 3f;
    public LayerMask hitMask = ~0;
    private float damage;
    private bool isEnemy;
    private bool ativo;
    private Vector3 direction;
    private ShootPool Shootpool;

    void OnEnable()
    {
        Invoke("SpawnTime", lifeTime);
    }

    public void StartProjectile(Vector3 direction, ShootPool shooter, float damage, bool isEnemy)
    {
        this.direction = direction;
        this.Shootpool = shooter;
        this.damage = damage;
        this.isEnemy = isEnemy;
        ativo = true;
    }

    void Update()
    {
        float passo = speed * Time.deltaTime;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, passo, hitMask, QueryTriggerInteraction.Ignore))
        {
            if (Acertar(hit.collider)) return;
        }
        transform.position += direction * passo;
    }

    bool Acertar(Collider col)
    {
        PlayerHealth player = col.GetComponentInParent<PlayerHealth>();
        EnemyHealth enemy = col.GetComponentInParent<EnemyHealth>();

        if (isEnemy && enemy != null) return false;
        if (!isEnemy && player != null) return false;

        if (enemy != null)
        {
            float dano = damage;
            if (col.CompareTag("Head"))
            {
                dano = damage * 2f;
            }
            enemy.TakeDamage(dano);
        }
        if (player != null)
        {
            player.TakeDamage(damage);
        }

        Voltar();
        return true;
    }

    void SpawnTime()
    {
        Voltar();
    }

    void Voltar()
    {
        if (!ativo) return;
        ativo = false;
        CancelInvoke("SpawnTime");
        Shootpool.ReturnProjectile(gameObject);
    }
}