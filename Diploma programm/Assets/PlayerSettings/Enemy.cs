using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    Bandit bandit;
    HeroKnight player;
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private bool m_isDead = false;
    public LayerMask playerLayer;
    public GameObject positionPlayer;


    public float positionEnemyX;
    public float positionEnemyY;
    // Start is called before the first frame update
    void Start()
    {
      
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        //m_isDead = true;
        Debug.Log("Enemy died");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        this.enabled = false;

    }
    void Update()
    {
        positionEnemyX = transform.position.x;
        positionEnemyY = transform.position.y;

    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colider work");
           
        }
        // Update is called once per frame
        
    }

    public void LoadData(SaveData.EnemySaveData save)
    {
        transform.position = new Vector2(save.positioneEnemy.x, save.positioneEnemy.y);
    }
}
