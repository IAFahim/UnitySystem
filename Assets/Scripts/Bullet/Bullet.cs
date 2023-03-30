using System;
using UnityEngine;

namespace Bullet
{
    public class Bullet : MonoBehaviour
    {
        public float fireRate = .5f;

        private void Update()
        {
            transform.Translate(Vector3.up * (Time.deltaTime * SlowDownSpeed.GetSpeed() * 10));
        }
    }
}
