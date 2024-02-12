using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{

    // Monster State
    public int id;
    public bool isDead = false;
    private bool isWaitingForRespawn;
    public bool canAttack = true;
    public GameObject monsterPrefab;

    // Monster Particles
    public GameObject hitParticles;
    private GameObject goHitParticles;

    // Monster Stats
    public int health;
    public int currentHealth;
    private int meleeDamage;
    public int damageMin;
    public int damageMax;

    // Monster Elements
    public int powerLight;
    public int powerDark;
    public int powerFire;
    public int powerEarth;
    public int powerAir;
    public int powerWater;
    public int resistLight;
    public int resistDark;
    public int resistFire;
    public int resistEarth;
    public int resistAir;
    public int resistWater;

    // Monster Misc
    public int level;
    public int attack;
    public int dodge;
    public int armorClass;

    // Monster XP
    public float experience;

    // Monster Loot
    public int goldMin;
    public int goldMax;
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
    private Transform childHealthBarBorder;
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

    // Wandering
    [Range(1, 500)] public float walkRadius;

    // SFX
    public AudioClip[] sfx;
    private AudioSource audioSource;
    private float timePauseWander;
    private bool isMoving;
    private GameObject labelMonsterGo;
    private GameObject canvas;
    private bool isMouseOver;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        allChildObjetcs = GetComponentsInChildren<Transform>();
        childHealthBarContainer = allChildObjetcs.Where(k => k.gameObject.name == "Health Bar").FirstOrDefault();
        childHealthBarBorder = allChildObjetcs.Where(k => k.gameObject.name == "Health Bar Border").FirstOrDefault();
        childHealthBar = allChildObjetcs.Where(k => k.gameObject.name == "Health").FirstOrDefault();
        healthBarLocalSpace = childHealthBar.localScale;

        // Turn off canvas HP by default
        childHealthBarBorder.GetComponent<Image>().enabled = false;
        childHealthBar.GetComponent<Image>().enabled = false;
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
        UpdateCanvasMonsterNamePosition();

        if (currentHealth <= 0) isDead = true;
        if (isDead == false) OnMonsterAlive();
        if (isDead == true)
        {
            if (isWaitingForRespawn == false) OnMonsterDead();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }

    private Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            if (Vector3.Distance(initialPosition, hit.position) <= walkRadius)
            {
                RaycastHit hitObstacle;
                Physics.Raycast(transform.position, (hit.position - transform.position), out hitObstacle);
                finalPosition = randomPosition;
                if (agent.remainingDistance >= agent.stoppingDistance + 0.25f)
                {
                    animator.SetFloat("runSpeed", 1);
                    isMoving = true;
                    timePauseWander = Time.time;
                }
                else
                {
                    animator.SetFloat("runSpeed", 0);
                    isMoving = false;
                }
            }
        }
        return finalPosition;
    }

    private void OnMonsterAlive()
    {
        // Extra Rotation

        // Check Distance Between the Monster and the Player
        float distanceFromPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        float distanceFromInitialPosition = Vector3.Distance(transform.position, initialPosition);
        Vector3 wanderDestination = RandomNavMeshLocation();
        agent.stoppingDistance = stopDistance;

        // Out Distance Detection
        if (agent != null)
        {
            if (agent.velocity.sqrMagnitude > 0) animator.SetFloat("runSpeed", 1);
            if (distanceFromPlayer > meleeRange)
            {
                // Extra Rotate direction change during monster moves
                if (isMoving == true) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(agent.destination - transform.position), Time.deltaTime * 2.5f);

                // If the Distance is less than detect distance, the monster move to the player until being in melee range of 2
                if (distanceFromPlayer < detectDistance || (Player.Instance.targetMonster == this && Player.Instance.weapon.isRange))
                {
                    agent.destination = Player.Instance.transform.position;
                    //animator.SetFloat("runSpeed", 1);
                }
                // Pull back the monster if he got pulled to far away
                else if (distanceFromInitialPosition > walkRadius && distanceFromPlayer > detectDistance * 5)
                {
                    agent.destination = initialPosition;
                    //animator.SetFloat("runSpeed", 1);
                }
                // Wander System
                else
                {
                    if (wanderDestination != Vector3.zero)
                    {
                        if (timePauseWander + 5 <= Time.time)
                        {
                            timePauseWander = Time.time;
                            agent.SetDestination(wanderDestination);
                            //animator.SetFloat("runSpeed", 1);
                            isMoving = true;
                        }
                    }
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
                        if (Player.Instance.currentHealth > 0 && meleeDamage > 0)
                        {
                            Player.Instance.currentHealth -= meleeDamage;
                            Player.Instance.GetHitAudio();

                            // Show UI Damage
                            DynamicTextData data = Player.Instance.damageTextData;
                            Vector3 destination = Player.Instance.transform.position;
                            destination.x += (Random.value + 0.5f) / 3;
                            destination.y += (Random.value + 12) / 3;
                            destination.z += (Random.value - 0.5f) / 3;

                            DynamicTextManager.CreateText(destination, "-" + meleeDamage.ToString(), data);
                            StartCoroutine("AttackHitThePlayer");
                        }

                    }
                    else
                    {
                        // Show UI Damage
                        DynamicTextData data = Player.Instance.damageTextData;
                        Vector3 destination = Player.Instance.transform.position;
                        destination.x += (Random.value + 0.5f) / 3;
                        destination.y += (Random.value + 12) / 3;
                        destination.z += (Random.value - 0.5f) / 3;

                        DynamicTextManager.CreateText(destination, "Dodged", data);
                    }
                }
            }
        }
    }

    private void OnMonsterDead()
    {
        // Flag monster as dead
        isDead = true;

        // Get Gold
        Player.Instance.gold += Random.Range(goldMin, goldMax);

        // Get Back default attacks state
        if (Player.Instance.targetMonster == this) Player.Instance.targetMonster = null;
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
        transform.Find("Canvas").gameObject.SetActive(false);
        agent.isStopped = true;

        // Flag as dead, prepair respawn & reset Player's target & nav mash relative to dead monster
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        StartCoroutine(RespawnDeadMonster());

        // Get Back default cursor
        Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CustomCursor.Instance.cursorMode);

        // Check if the monster is part of quest kill monster
        foreach (var quest in Player.Instance.questInProgressList)
        {
            if (quest.category == Quest.ECategory.killMonster && quest.monsterToKill.GetComponent<Monster>().id == this.id && quest.status == Quest.EStatus.accepted)
            {
                quest.monsterKilledCount += 1;
            }
        }

        // Reset Target
        Player.Instance.ResetTarget();

    }

    private void UpdateCanvasHealthMonster()
    {
        if (Player.Instance.targetMonster == this)
        {
            childHealthBarBorder.GetComponent<Image>().enabled = true;
            childHealthBar.GetComponent<Image>().enabled = true;
        }
        if (Player.Instance.targetMonster != this && !isMouseOver)
        {
            childHealthBarBorder.GetComponent<Image>().enabled = false;
            childHealthBar.GetComponent<Image>().enabled = false;
        }
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
        transform.position = initialPosition;
        currentHealth = health;
        animator.SetTrigger("idle");
        GetComponent<Collider>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        isDead = false;
        isWaitingForRespawn = false;
        agent.isStopped = false;
        agent.ResetPath();
        agent.destination = initialPosition;
        transform.Find("Canvas").gameObject.SetActive(true);
        childHealthBar.localScale = healthBarLocalSpace;
    }

    private void ShowMonsterName()
    {
        GameObject ui = GameObject.Find("UI").gameObject;

        if (ui != null)
        {
            canvas = ui.transform.Find("Canvas").gameObject;
            if (canvas != null)
            {
                GameObject labelMonster = canvas.transform.Find("Label Description").gameObject;
                if (labelMonster != null)
                {
                    if (!labelMonsterGo)
                    {
                        labelMonsterGo = Instantiate(labelMonster, transform.position, Quaternion.identity);
                        labelMonsterGo.transform.SetParent(canvas.transform);
                        GameObject backgroundNameMonster = labelMonsterGo.transform.Find("Background").gameObject;
                        if (backgroundNameMonster != null)
                        {
                            GameObject nameMonster = backgroundNameMonster.transform.Find("Description").gameObject;
                            if (nameMonster != null)
                            {
                                TextMeshProUGUI labelText = nameMonster.GetComponent<TextMeshProUGUI>();
                                labelText.text = name;
                                float scaleFactor = Screen.width / 1920f;
                                labelText.fontSize = Mathf.RoundToInt(18f * scaleFactor);
                                Vector2 labelTextDimensions = labelText.GetPreferredValues();
                                backgroundNameMonster.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(labelTextDimensions.x + 16 * scaleFactor, 32 * scaleFactor);
                                labelMonsterGo.SetActive(true);
                            }
                            Destroy(labelMonsterGo, 5);
                        }
                    }
                }
            }
        }
    }

    private void UpdateCanvasMonsterNamePosition()
    {
        if (canvas && labelMonsterGo) labelMonsterGo.transform.position =
                worldToUISpace(canvas.transform.GetComponent<Canvas>(), new Vector3(transform.position.x, transform.position.y + 3.1f, transform.position.z));
    }

    public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        return parentCanvas.transform.TransformPoint(movePos);
    }

    private void OnMouseOver()
    {
        if (isDead == false)
        {
            isMouseOver = true;
            childHealthBarBorder.GetComponent<Image>().enabled = true;
            childHealthBar.GetComponent<Image>().enabled = true;
            if (Player.Instance.weapon.isHand) Cursor.SetCursor(CustomCursor.Instance.cursorMeleeAttack, Vector2.zero, CursorMode.Auto);
            if (Player.Instance.weapon.isMelee) Cursor.SetCursor(CustomCursor.Instance.cursorMeleeAttack, Vector2.zero, CursorMode.Auto);
            if (Player.Instance.weapon.isRange) Cursor.SetCursor(CustomCursor.Instance.cursorRangeAttack, Vector2.zero, CursorMode.Auto);
        }
        if (Input.GetMouseButtonDown(1)) ShowMonsterName();
    }
    private void OnMouseExit()
    {
        isMouseOver = false;
        childHealthBarBorder.GetComponent<Image>().enabled = false;
        childHealthBar.GetComponent<Image>().enabled = false;
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
        audioSource.PlayOneShot(sfx[1], 0.025f);
        audioSource.PlayOneShot(sfx[0], 0.025f);
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(1);
        canAttack = true;
    }
}
