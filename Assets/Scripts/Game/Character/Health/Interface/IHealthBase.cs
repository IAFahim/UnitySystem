using UnityEngine;

namespace Game.Character.Health.Interface
{
    public interface IHealthBase
    {
        float SetMaxHealth(float value);
        float SetHealth(float value);
        float SetHealthToMax();
        float Heal(float point, GameObject healer);
        float TakeDamage(float point, GameObject attacker);
        float SetImmune(bool state);
        float GetHealth();
        float GetMaxHealth();
        bool IsImmune();
        float GetDefault();
    }
}