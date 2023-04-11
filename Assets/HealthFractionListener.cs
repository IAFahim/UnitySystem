using _Game.Character.Health;
using _Game.Util;
using UnityEngine;

public class HealthFractionListener : MonoBehaviour
{
    [SerializeField] private Health health;
    [Range(0,1)]public float fraction = 0.2f;
    private PercentagePromise<Health> _promise;

    private void OnEnable()
    {
        _promise ??= new PercentagePromise<Health>(() => health.percentage <= fraction, Resolve, Reject);
        health.PercentagePromises.Add(_promise);
    }

    private void OnDisable()
    {
        health.PercentagePromises.Remove(_promise);
    }

    private void Resolve(Health health)
    {
        this.health = health;
        Debug.Log($"Health is at {fraction * 100}%");
    }

    private void Reject(Health health)
    {
        this.health = health;
        Debug.Log($"Health is not {fraction * 100}%");
    }
}