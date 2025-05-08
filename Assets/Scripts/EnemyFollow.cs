using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;

    private float bulletTime;
    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float enemySpeed;
    public float sightRange;
    public ParticleSystem muzzleFlash;

    public LayerMask whatIsPlayer;

    [SerializeField] private float time = 5;
    [SerializeField] private float minimumDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.isOnNavMesh && PlayerInSightRange())
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Only move towards the player if the distance is greater than the minimum distance
            if (distanceToPlayer > minimumDistance)
            {
                enemy.SetDestination(player.position);
            }
            else
            {
                enemy.SetDestination(transform.position);  // Stop moving
            }

            AimAtPlayer();
            ShootingAtPlayer();
        }
    }

    private bool PlayerInSightRange()
    {
        return Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    }

    private void AimAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Keep the enemy upright when rotating
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void ShootingAtPlayer()
    {

        bulletTime -= Time.deltaTime;

        if (bulletTime > 0)
        {
            return;
        }

        bulletTime = time;

        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Tính toán hướng bắn từ spawnPoint đến player
        Vector3 direction = (player.position - spawnPoint.position).normalized;

        // Tạo đạn và cài đặt hướng bắn
        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.position, Quaternion.LookRotation(direction));
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();

        // Thêm vận tốc để đạn di chuyển về phía người chơi
        bulletRig.velocity = direction * enemySpeed;
        Destroy(bulletObj, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
