using Game.Character.Health.Interface;
using UnityEngine;

namespace Game.Character.Health
{
    public class HealthUI : MonoBehaviour, IHealthListener
    {
        public SpriteRenderer healthBar;

        public void Set(float health, float maxHealth)
        {
            Debug.Log("Setting health bar");
            float ratio = maxHealth == 0 ? 0 : health / maxHealth;
            Transform healthBarTransform = healthBar.transform;
            Vector3 position = healthBarTransform.position;
            Vector3 newPos = new Vector3(ratio - 1, position.y, position.z);
            healthBarTransform.position = newPos;
            healthBar.color = new Color(1 - ratio, ratio, 0, 1);
        }

        
        public void eventOnHealthChanged(float health, float maxHealth)
        {
            Set(health, maxHealth);
            Debug.Log("Health changed!");
        }

        
        public void eventOnOverHeal(float health, float maxHealth)
        {
            Set(health, maxHealth);
            Debug.Log("Over heal!");
        }
        
        
        public void eventOnMaxHealthChanged(float health, float maxHealth)
        {
            Set(health, maxHealth);
            Debug.Log("Max health changed!");
        }

        
        public void eventOnImmuneChanged(bool isImmune, float health, float maxHealth)
        {
            Debug.Log($"Immune: {isImmune}");
        }
        
        
        public void eventOnDamageTaken(float health, float maxHealth, GameObject attacker)
        {
            Set(health, maxHealth);
            Debug.Log("Damage taken by:" + attacker.name);
        }
        
        
        public void eventOnHeal(float health, float maxHealth, GameObject healer)
        {
            Set(health, maxHealth);
            Debug.Log("Healed by:" + healer.name);
        }
        
        
        public void eventOnDead(float health, float maxHealth)
        {
            Set(0, maxHealth);
            Debug.Log("You are dead!");
        }
    }
}