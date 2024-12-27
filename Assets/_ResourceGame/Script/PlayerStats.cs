using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamage
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [SerializeField] private float damage;

    private Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDame(damage);
        }
    }

    public void TakeDame(float dame)
    {
        currentHealth -= dame;
        player.playerHurt();
        GetDame();

    }
    public void GetDame()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Die");
            player.playerDie();
        }
    }
}
