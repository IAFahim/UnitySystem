using EasyButtons;
using Game.Character.Health.Interface;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Character.Health
{
    public class Health : MonoBehaviour, IHealthBase, IHealthEvent
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;
        [SerializeField] private float overHealAmount;
        [SerializeField] private bool isImmune;
        [SerializeField] private UnityEvent<float, float> eventOnHealthChanged;
        [SerializeField] private UnityEvent<float, float> eventOnOverHeal;
        [SerializeField] private UnityEvent<float, float> eventOnMaxHealthChanged;
        [SerializeField] private UnityEvent<bool, float, float> eventOnImmuneChanged;
        [SerializeField] private UnityEvent<float, float, GameObject> eventOnDamageTaken;
        [SerializeField] private UnityEvent<float, float, GameObject> eventOnHeal;
        [SerializeField] private UnityEvent<float, float> eventOnDead;

        private void OnValidate()
        {
            SetHealth(health);
        }

        [Button]
        public float SetHealth(float value)
        {
            if (isImmune) return health;
            if (value <= 0) OnDead();
            if (value > maxHealth) OnOverHeal();
            health = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChange();
            return health;
        }
        
        
        public float Heal(float point, GameObject healer)
        {
            if (point >= 0)
            {
                eventOnHeal?.Invoke(SetHealth(health + point), health, healer);
            }

            return health;
        }

        
        public float TakeDamage(float point, GameObject attacker)
        {
            if (point >= 0)
            {
                eventOnDamageTaken?.Invoke(SetHealth(health - point), health, attacker);
            }

            return health;
        }

        [Button]
        public float SetHealthToMax() => SetHealth(maxHealth);

        
        public float SetMaxHealth(float value)
        {
            OnMaxHealthChange();
            return maxHealth = value;
        }

        [Button]
        public float SetImmune(bool state)
        {
            isImmune = state;
            OnImmuneChange();
            return health;
        }

        public float GetHealth() => health;

        public float GetDefault() => maxHealth;

        public float GetMaxHealth() => maxHealth;

        public bool IsImmune() => isImmune;

        public float TakeDamage(float point)
        {
            throw new System.NotImplementedException();
        }

        public void OnHealthChange() => eventOnHealthChanged?.Invoke(health, maxHealth);
        public void OnOverHeal() => eventOnOverHeal?.Invoke(health, maxHealth);
        public void OnImmuneChange() => eventOnImmuneChanged?.Invoke(isImmune, health, maxHealth);
        public void OnMaxHealthChange() => eventOnMaxHealthChanged?.Invoke(health, maxHealth);

        public void OnDamageTaken(GameObject attacker) =>
            eventOnDamageTaken?.Invoke(health, maxHealth, attacker);

        public void OnHeal(GameObject healer) =>
            eventOnHeal?.Invoke(health, maxHealth, healer);

        public void OnDead() => eventOnDead?.Invoke(health, maxHealth);

        public override string ToString()
        {
            return $"Health: {health}/{maxHealth}";
        }
    }
}