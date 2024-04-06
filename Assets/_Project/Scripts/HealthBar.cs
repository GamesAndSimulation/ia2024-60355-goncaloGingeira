using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class HealthBar : MonoBehaviour {
        
        [SerializeField] Slider slider;
        [SerializeField] Gradient gradient;
        [SerializeField] Image fill;
        
        public void SetHealth(int health) {
            slider.value = health;
            
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
        
        public void SetMaxHealth(int health) {
            slider.maxValue = health;
            slider.value = health;
            
            fill.color = gradient.Evaluate(1f);
        }
        
        public void TakeDamage(int damage) {
            slider.value -= damage;
        }
    }
}
