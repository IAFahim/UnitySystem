using UnityEngine;

namespace _Game.Character.Health.Interface
{
    public interface IHealthBase
    {
        bool isImmune { get; set; }
        float SetImmune(bool state);
        bool IsImmune();
        float GetHealth();
        float SetHealth(float value);
        float SetMaxHealth(float value);
        float GetMaxHealth();
        public float SetPercentage(float normalizedPercentage);
        public float GetPercentage();
        float SetHealthToMax();
        float Heal(float point, GameObject healer);
        float TakeDamage(float amount);
        float TakeDamage(float amount, GameObject attacker);
    }
}