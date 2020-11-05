using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Slider healthSlider = null;
    [SerializeField] private string enemyName = "Boss";
    [SerializeField] private Text nameText = null;
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
    }

    public void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        nameText.text = enemyName;
    }

    public string GetName()
    {
        return enemyName;
    }
}
