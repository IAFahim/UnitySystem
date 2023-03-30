using UnityEngine;

namespace Bullet
{
    public class BulletSpawner : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float baseFireRate = .5f;
        public float lastFireTime = 0f;
        public float timeUntilNextFire = 0f;

        private void OnEnable()
        {
            SlowDownSpeed.onSlowMo += OnSlowDown;
            SlowDownSpeed.onNormalSpeed += OnNormalSpeed;
        }

        private void OnDisable()
        {
            SlowDownSpeed.onSlowMo -= OnSlowDown;
            SlowDownSpeed.onNormalSpeed -= OnNormalSpeed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SlowDownSpeed.Toggle();
            }

            float currentTime = Time.time;
            float timeSinceLastFire = currentTime - lastFireTime;
            float fireRate = baseFireRate / SlowDownSpeed.GetSpeed();
            timeUntilNextFire = fireRate - timeSinceLastFire;

            if (timeUntilNextFire <= 0f)
            {
                lastFireTime = currentTime;
                Fire();
            }
        }

        private void Fire()
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }

        private void OnSlowDown()
        {
            // Do nothing
        }

        private void OnNormalSpeed()
        {
            // Reset the last fire time to avoid firing immediately after slowing down
            lastFireTime = Time.time;
        }
    }
}