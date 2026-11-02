using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.InputSystem;

public class ShootPool : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public int poolSize = 40;
    public int maxAmmo = 20;
    public int currentAmmo;
    private int activeProjectiles = 0;
    private ObjectPool<GameObject> pool;

    public bool isEnemy = false;
    [HideInInspector] public bool canShoot = false;
    public float fireRate = 0.5f;
    public float damage = 20f;
    private float nextFireTime = 0f;
    public LayerMask aimMask;
    public TextMeshProUGUI ammoText;
    private Transform player;

    void Awake()
    {
        currentAmmo = maxAmmo;
        pool = new ObjectPool<GameObject>(
            () => Instantiate(projectilePrefab),
            projectile => projectile.SetActive(true),
            projectile => projectile.SetActive(false),
            projectile => Destroy(projectile),
            false,
            poolSize,
            poolSize
        );
    }

    void Start()
    {
        if (isEnemy)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (isEnemy)
        {
            EnemyUpdate();
        }
        else
        {
            PlayerUpdate();
        }

        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + maxAmmo;
        }
    }

    void EnemyUpdate()
    {
        if (canShoot && Time.time >= nextFireTime)
        {
            Shoot();
        }
        if (currentAmmo <= 0 && !IsInvoking("Reload"))
        {
            Invoke("Reload", 2f);
        }
    }

    void PlayerUpdate()
    {
        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            Shoot();
        }
        if (Keyboard.current[Key.R].wasPressedThisFrame && !IsInvoking("Reload"))
        {
            Invoke("Reload", 2f);
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0 || IsInvoking("Reload")) return;

        Vector3 direcao;
        if (isEnemy)
        {
            direcao = (player.position - firePoint.position).normalized;
        }
        else
        {
            direcao = AimDirection();
        }

        if (Disparar(firePoint.position, direcao))
        {
            currentAmmo--;
            nextFireTime = Time.time + fireRate;
        }
    }

    public bool Disparar(Vector3 origem, Vector3 direcao)
    {
        if (activeProjectiles >= poolSize) return false;

        GameObject projectile = pool.Get();
        activeProjectiles++;
        projectile.transform.SetPositionAndRotation(origem, Quaternion.LookRotation(direcao));
        projectile.GetComponent<Projectile>().StartProjectile(direcao, this, damage, isEnemy);
        return true;
    }

    Vector3 AimDirection()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 alvo = ray.GetPoint(100f);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, aimMask, QueryTriggerInteraction.Ignore))
        {
            alvo = hit.point;
        }
        return (alvo - firePoint.position).normalized;
    }

    public void ReturnProjectile(GameObject projectile)
    {
        activeProjectiles--;
        pool.Release(projectile);
    }

    private void Reload()
    {
        currentAmmo = maxAmmo;
    }
}