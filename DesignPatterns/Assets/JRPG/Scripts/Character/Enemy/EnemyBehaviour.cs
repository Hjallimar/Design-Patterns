using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Slider healthSlider = null;
    [SerializeField] private string enemyName = "Boss";
    [SerializeField] private Text nameText = null;
    public bool Active { get; private set; }
    public void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        nameText.text = enemyName;
        Active = true;
        EnemyObserver.AssignEnemy(this);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0)
        {
            Active = false;
            gameObject.SetActive(false);
        }
    }

    public string GetName()
    {
        return enemyName;
    }
}
