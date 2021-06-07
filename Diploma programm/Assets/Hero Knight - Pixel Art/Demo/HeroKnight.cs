using UnityEngine;
using System.Collections;
using System;

[System.Serializable]

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.5f;
    private float               m_delayToIdle = 0.2f;
   




    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    private int attackDamage = 50;

    public HealthBar healthBar;
    public float maxPlayerHealth = 100;
    public float currentPlayerHealth;

    public StaminaBar staminaBar;
    public float maxPlayerStamina = 100;
    public float currentPlayerStamina;

    public ManaBar manaBar;
    public float maxPlayerMana = 100;
    public float currentPlayerMana;
    public bool restoreStamina = true;
    public bool blockActive = false;
    public bool isJumpingBlock = false;
    private int extraJumps;
    public int extraJumpsValue = 2;

    public float positionPlayerX;
    public float positionPlayerY;


    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        currentPlayerHealth = maxPlayerHealth;
        healthBar.SetMaxHealth(maxPlayerHealth);
        currentPlayerStamina = maxPlayerStamina;
        staminaBar.SetMaxStamina(maxPlayerStamina);
        currentPlayerMana = maxPlayerMana;
        manaBar.SetMaxMana(maxPlayerMana);

    }

    void FixedUpdate()
    {
        RestoreStamina();
        positionPlayerX = transform.position.x;
        positionPlayerY = transform.position.y;
        
    }
    // Update is called once per frame
    void Update ()
    {
        if (m_grounded)
        {
            extraJumps = extraJumpsValue;
        }

        float inputX = Input.GetAxis("Horizontal");
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling)
            Move();

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide
        m_animator.SetBool("WallSlide", (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State()));

        //Death
        if (currentPlayerHealth == 0 && !m_rolling)
        {
            Death();
        }

        //Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
        {
            Hurt();
        }

        //Attack
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            Attack();
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            restoreStamina = false;
            blockActive = true;
            isJumpingBlock = true;
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);

        }

        else if (Input.GetMouseButtonUp(1))
        {
            isJumpingBlock = false;
            m_animator.SetBool("IdleBlock", false);
            restoreStamina = true;
            blockActive = false;
        }

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling)
        {
            Roll();
        }
        //Jump
        else if (Input.GetKeyDown("space") && !m_rolling && extraJumps > 0)
        {
            isJumpingBlock = true;
            Jump();
        }
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling && extraJumps == 0)
        {
            isJumpingBlock = true;
            Jump();
        }
        

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }

    // Animation Events
    // Called in end of roll animation.
    void AE_ResetRoll()
    {
        m_rolling = false;
    }

    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

    void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        if (blockActive == false)
        {
            
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }
        if(blockActive == true)  
        {
            m_body2d.velocity = new Vector2(inputX * 0, m_body2d.velocity.y);
        }
    }
    void Death()
    {
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
    }
    void Hurt()
    {
        m_animator.SetTrigger("Hurt");
    }
    void Attack()
    {
        m_currentAttack++;

        // Loop back to one after third attack
        if (m_currentAttack > 3)
            m_currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (m_timeSinceAttack > 1.0f)
            m_currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        m_animator.SetTrigger("Attack" + m_currentAttack);

        // Reset timer
        m_timeSinceAttack = 0.0f;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Roll()
    {
        if (currentPlayerStamina > 25 && m_grounded == true)
        {
            currentPlayerStamina -= 25;
            staminaBar.SetStamina(currentPlayerStamina);
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
    }

    void Jump()
    {
        if (currentPlayerStamina > 15)
        {
            extraJumps--;
            currentPlayerStamina -= 15;
            staminaBar.SetStamina(currentPlayerStamina);
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
    }

    void TakeDamageToPlayer(int damage)
    {
        currentPlayerHealth -= damage;
     
        healthBar.SetHealth(currentPlayerHealth);
        if (currentPlayerHealth < 0)
        {
            Death();
            for(int i = 1000; i>0; i-=7)
            Debug.Log("Lol U Died"+ i);
        }
        if(currentPlayerHealth >= 1)
        {
            Hurt();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
 
        Enemy enm = collision.gameObject.GetComponent<Enemy>();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit at enemy");
            TakeDamageToPlayer(10);         
        }
        if (collision.gameObject.CompareTag("Spikes"))
        {
            Debug.Log("Hit at spikes");
            TakeDamageToPlayer(25);
        }
    }
    void RestoreStamina()
    {
        if(restoreStamina)
        if (currentPlayerStamina < maxPlayerStamina)
        {
            currentPlayerStamina += Time.deltaTime * 5;
        }
        staminaBar.SetStamina(currentPlayerStamina);
    }
}
