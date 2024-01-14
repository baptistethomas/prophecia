using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{

    // Monster State
    public bool isDead = false;
    public bool canAttack = true;
    public GameObject monsterPrefab;

    // Monster Particles
    public GameObject hitParticles;
    private GameObject goHitParticles;

    // Monster Stats
    public int health;
    private bool isWaitingForRespawn;
    public int currentHealth;
    public int meleeDamage;
    public int damageMin;
    public int damageMax;
    public int experience;

    // Monster Skills
    public int attack;
    public int dodge;

    // Monster Loot
    public int gold;
    public GameObject lootSack;
    public GameObject[] loot;
    public int[] lootRate;
    public List<GameObject> lootList;

    // Monster Infos
    private GameObject meleeWeapon;
    private int playerDamage;

    // Canvas
    public DynamicTextData damageTextData;
    public RectTransform healthBar;
    private Transform childHealthBar;
    public Vector3 healthBarLocalSpace;
    private Transform[] allChildObjetcs;
    private Transform childHealthBarContainer;

    // AI
    Vector3 initialPosition;
    Quaternion initialRotation;
    public NavMeshAgent agent;
    public Animator animator;
    [Range(2, 100)]
    public float detectDistance = 20;
    public float stopDistance;
    public float meleeRange;

    // SFX
    public AudioClip[] sfx;
    private AudioSource audioSource;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        allChildObjetcs = GetComponentsInChildren<Transform>();
        childHealthBarContainer = allChildObjetcs.Where(k => k.gameObject.name == "Health Bar").FirstOrDefault();
        childHealthBar = allChildObjetcs.Where(k => k.gameObject.name == "Health").FirstOrDefault();
        healthBarLocalSpace = childHealthBar.localScale;
    }


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        // Update Health Monster
        UpdateCanvasHealthMonster();

        if (currentHealth <= 0) isDead = true;
        if (isDead == false) OnMonsterAlive();
        if (isDead == true)
        {
            if (isWaitingForRespawn == false)
            {
                isWaitingForRespawn = true;
                OnMonsterDead();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }

    private void OnMonsterAlive()
    {
        // Check Distance Between the Monster and the Player
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);
        agent.stoppingDistance = stopDistance;

        if (distance > meleeRange)
        {
            // If the Distance is less than detect distance, the monster move to the player until being in melee range of 2
            if (distance <= detectDistance)
            {
                agent.destination = Player.Instance.transform.position;
                animator.SetFloat("runSpeed", 1);
            }
            // If the player is out of 1.5 detect range after aggro, monster cames back to initial position
            else if (distance > detectDistance * 1.5f && Vector3.Distance(transform.position, initialPosition) > 3)
            {
                agent.destination = initialPosition;
                animator.SetFloat("runSpeed", 1);
            }
            // If the monster is back to his initial positon
            else if (distance > detectDistance && Vector3.Distance(transform.position, initialPosition) < 3)
            {
                animator.SetFloat("runSpeed", 0);
            }
        }
        else
        {

            // If the Monster is in meleeRange, he attacks
            if (canAttack)
            {
                canAttack = false;
                animator.SetTrigger("meleeAttack");
                StartCoroutine("AttackPlayer");

                if (Random.Range(1, attack) >= Random.Range(1, Player.Instance.dodgeFinal))
                {
                    meleeDamage = Random.Range(damageMin, damageMax) - (int)(Player.Instance.armorClassFinal);
                    if (Player.Instance.currentHealth > 0)
                        Player.Instance.currentHealth -= meleeDamage;
                    StartCoroutine("AttackHitThePlayer");
                }
            }
        }
    }

    private void OnMonsterDead()
    {
        // Get Gold
        Player.Instance.gold += gold;

        // Get Back default attacks state
        Player.Instance.isAttacking = false;
        Player.Instance.canAttack = true;

        // Loot
        for (int i = 0; i < loot.Length; i++)
        {
            if (Random.Range(1, 100) <= lootRate[i])
            {
                lootList.Add(loot[i]);
            }
        };
        if (lootList.Count > 0)
        {
            GameObject droppedSack = Instantiate(lootSack, transform.position, Quaternion.identity);
            droppedSack.GetComponent<Sack>().items = lootList;
        }

        // Die animation and particle
        animator.SetTrigger("died");
        agent.isStopped = true;

        // Flag as dead, prepair respawn & reset Player's target & nav mash relative to dead monster
        isDead = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        StartCoroutine(RespawnDeadMonster());

        // Get Back default cursor
        Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CustomCursor.Instance.cursorMode);

        // Reset Target
        Player.Instance.resetTarget();

    }

    private void UpdateCanvasHealthMonster()
    {
        if (childHealthBarContainer)
            childHealthBarContainer.transform.rotation = Quaternion.Euler(0, 45, 0);
        if (childHealthBar.localScale.x > 0.1)
        {
            float currentHealthBar = currentHealth / (float)health;
            childHealthBar.localScale = new Vector3(currentHealthBar * healthBarLocalSpace.x, childHealthBar.localScale.y, childHealthBar.localScale.z);
        }
        if (currentHealth <= 0)
            childHealthBar.localScale = new Vector3(0, childHealthBar.localScale.y, childHealthBar.localScale.z);
    }

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }


    IEnumerator RespawnDeadMonster()
    {
        isWaitingForRespawn = true;
        yield return StartCoroutine(Wait(30));
        currentHealth = health;
        animator.SetTrigger("idle");
        GetComponent<Collider>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        isDead = false;
        transform.position = initialPosition;
        isWaitingForRespawn = false;
        agent.isStopped = false;
        agent.ResetPath();
        childHealthBar.localScale = healthBarLocalSpace;
    }

    private void OnMouseOver()
    {
        if (isDead == false)
        {
            if (Player.Instance.weapon.isHand) Cursor.SetCursor(CustomCursor.Instance.cursorMeleeAttack, Vector2.zero, CursorMode.Auto);
            if (Player.Instance.weapon.isMelee) Cursor.SetCursor(CustomCursor.Instance.cursorMeleeAttack, Vector2.zero, CursorMode.Auto);
            if (Player.Instance.weapon.isRange) Cursor.SetCursor(CustomCursor.Instance.cursorRangeAttack, Vector2.zero, CursorMode.Auto);
        }
        // Show monster name on right click
        if (Input.GetMouseButtonDown(1))
        {
            DynamicTextManager.CreateText(new Vector3(transform.position.x, transform.position.y + 2.2f, transform.position.z), name.ToString(), damageTextData);
        }
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If trigger is a projectile we destroy it
        if (Player.Instance.weapon.isRange && other.gameObject.tag == "Projectile")
            Destroy(GameObject.Find(other.name));

        // If the monster is hitted by outranged, aggro go on
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if (distance > meleeRange)
        {
            agent.destination = Player.Instance.transform.position;
            animator.SetFloat("runSpeed", 1);
        }
    }

    IEnumerator AttackHitThePlayer()
    {
        yield return new WaitForSeconds(1); // On patiente 1 sec
        goHitParticles = Instantiate(hitParticles, new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 2, Player.Instance.transform.position.z), Quaternion.identity);
        Destroy(goHitParticles, 2);
    }

    public void GetHitAudio()
    {
        audioSource.PlayOneShot(sfx[1], 0.05f);
        audioSource.PlayOneShot(sfx[0], 0.05f);
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(1);
        canAttack = true;
    }
}
