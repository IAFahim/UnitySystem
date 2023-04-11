using UnityEngine;

namespace _Game.Character.Health.Interface
{
    public interface IHealthListener
    {
        void eventOnHealthChanged(float health, float maxHealth);
        void eventOnOverHeal(float health, float maxHealth);
        void eventOnImmuneChanged(bool isImmune, float health, float maxHealth);
        void eventOnMaxHealthChanged(float health, float maxHealth);
        void eventOnDamageTaken(float health, float maxHealth, GameObject attacker);
        void eventOnHeal(float health, float maxHealth, GameObject healer);
        void eventOnDead(float health, float maxHealth);
    }
}