using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bullet
{
    public class BulletSpawner : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float baseFireRate = .5f;
        public float lastFireTime = 0f;
        public float timeUntilNextFire = 0f;
        public float fireRate = .5f;
        public float diff = 0;
        private Coroutine fireCoroutine;

        private void Awake()
        {
            fireRate = baseFireRate;
        }

        private void Start()
        {
            fireCoroutine = StartCoroutine(FireCoroutine());
        }

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
        }

        private void Fire()
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }

        

        private IEnumerator FireCoroutine()
        {
            while (true)
            {
                float currentTime = Time.time;
                float timeSinceLastFire = currentTime - lastFireTime;
                timeUntilNextFire = fireRate - timeSinceLastFire;
                if (timeUntilNextFire <= 0f)
                {
                    lastFireTime = currentTime;
                    Fire();
                    if (fixRequiredForSlowMo) SlowMoFix();
                    else if(fixRequiredForNormalSpeed) onNormalFix();
                }
                yield return new WaitForSeconds(timeUntilNextFire);
            }
        }
        
        public bool fixRequiredForSlowMo = false;

        private void OnSlowDown()
        {
            diff = (fireRate - (Time.time - lastFireTime)) / fireRate;
            fireRate = baseFireRate / SlowDownSpeed.GetSpeed() * diff;
            fixRequiredForSlowMo = true;
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
            }
            fireCoroutine = StartCoroutine(FireCoroutine());
        }

        private void SlowMoFix()
        {
            fireRate = baseFireRate / SlowDownSpeed.GetSpeed();
            fixRequiredForSlowMo = false;
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
            }
            fireCoroutine = StartCoroutine(FireCoroutine());
        }

        public bool fixRequiredForNormalSpeed = false;
        private void OnNormalSpeed()
        {
            fireRate = baseFireRate / SlowDownSpeed.GetSpeed();
            fixRequiredForNormalSpeed = true;
            lastFireTime = Time.time;
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
            }
            fireCoroutine = StartCoroutine(FireCoroutine());
        }
        
        private void onNormalFix()
        {
            diff = (fireRate - (Time.time - lastFireTime)) / fireRate;
            Debug.Log($"fireRate: {fireRate} diff: {diff} baseFireRate: {baseFireRate}");
            fireRate = baseFireRate * diff;
            fixRequiredForNormalSpeed = false;
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
            }
            fireCoroutine = StartCoroutine(FireCoroutine());
        }
    }
}
