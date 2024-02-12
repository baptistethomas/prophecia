using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, Controls.IPlayerActions
{

    // Player Instance
    private static Player _instance;
    public static Player instance;
    public static Player Instance
    {
        get { return _instance; }
    }

    // Player Progress
    public string pseudo;
    public string title;
    public float experience;
    public int gold;
    public int level;

    // Player Stats Attributes
    public int health;
    [HideInInspector] public int healthBuff;
    [HideInInspector] public int healthMalus;
    public int healthFinal;
    public int currentHealth;
    public int healthRegeneration;
    [HideInInspector] public int healthRegenerationBuff;
    [HideInInspector] public int healthRegenerationMalus;
    public int healthRegenerationFinal;
    public int mana;
    [HideInInspector] public int manaBuff;
    [HideInInspector] public int manaMalus;
    public int manaFinal;
    public int manaRegeneration;
    [HideInInspector] public int manaRegenerationBuff;
    [HideInInspector] public int manaRegenerationMalus;
    [HideInInspector] public int mangaRegenerationFinal;
    public int currentMana;

    // Encumbrance
    public int encumbrance;
    public int currentEncumbrance;
    public int leftEncumbrance;

    // Player Attributes
    public int strenght;
    [HideInInspector] public int strenghtBuff;
    [HideInInspector] public int strenghtMalus;
    [HideInInspector] public int strenghtTemp;
    public int strenghtFinal;
    public int endurance;
    [HideInInspector] public int enduranceBuff;
    [HideInInspector] public int enduranceMalus;
    [HideInInspector] public int enduranceTemp;
    public int enduranceFinal;
    public int dexterity;
    [HideInInspector] public int dexterityBuff;
    [HideInInspector] public int dexterityMalus;
    [HideInInspector] public int dexterityTemp;
    public int dexterityFinal;
    public int intelect;
    [HideInInspector] public int intelectBuff;
    [HideInInspector] public int intelectMalus;
    [HideInInspector] public int intelectTemp;
    public int intelectFinal;
    public int wisdom;
    [HideInInspector] public int wisdomBuff;
    [HideInInspector] public int wisdomMalus;
    [HideInInspector] public int wisdomTemp;
    public int wisdomFinal;

    // Player Skills
    public int attack;
    [HideInInspector] public int attackBuff;
    [HideInInspector] public int attackMalus;
    [HideInInspector] public int attackTemp;
    public int attackFinal;
    public int archery;
    [HideInInspector] public int archeryBuff;
    [HideInInspector] public int archeryMalus;
    [HideInInspector] public int archeryTemp;
    public int archeryFinal;
    public int dodge;
    [HideInInspector] public int dodgeBuff;
    [HideInInspector] public int dodgeMalus;
    [HideInInspector] public int dodgeTemp;
    public int dodgeFinal;
    public float armorClass;
    [HideInInspector] public float armorClassBuff;
    [HideInInspector] public float armorClassMalus;
    [HideInInspector] public int armorClassTemp;
    public float armorClassFinal;

    // Remaining Points
    public int remainingAttributesPoints;
    public int usedAttributesPointsTemp;
    public int remainingSkillsPoints;
    public int usedSkillsPointsTemp;

    // Power
    public int powerLight;
    [HideInInspector] public int powerLightBuff;
    [HideInInspector] public int powerLightMalus;
    public int powerLightFinal;
    public int powerDark;
    [HideInInspector] public int powerDarkBuff;
    [HideInInspector] public int powerDarkMalus;
    public int powerDarkFinal;
    public int powerFire;
    [HideInInspector] public int powerFireBuff;
    [HideInInspector] public int powerFireMalus;
    public int powerFireFinal;
    public int powerEarth;
    [HideInInspector] public int powerEarthBuff;
    [HideInInspector] public int powerEarthMalus;
    public int powerEarthFinal;
    public int powerAir;
    [HideInInspector] public int powerAirBuff;
    [HideInInspector] public int powerAirMalus;
    public int powerAirFinal;
    public int powerWater;
    [HideInInspector] public int powerWaterBuff;
    [HideInInspector] public int powerWaterMalus;
    public int powerWaterFinal;

    // Resist
    public int resistLight;
    [HideInInspector] public int resistLightBuff;
    [HideInInspector] public int resistLightMalus;
    public int resistLightFinal;
    public int resistDark;
    [HideInInspector] public int resistDarkBuff;
    [HideInInspector] public int resistDarkMalus;
    public int resistDarkFinal;
    public int resistFire;
    [HideInInspector] public int resistFireBuff;
    [HideInInspector] public int resistFireMalus;
    public int resistFireFinal;
    public int resistEarth;
    [HideInInspector] public int resistEarthBuff;
    [HideInInspector] public int resistEarthMalus;
    public int resistEarthFinal;
    public int resistAir;
    [HideInInspector] public int resistAirBuff;
    [HideInInspector] public int resistAirMalus;
    public int resistAirFinal;
    public int resistWater;
    [HideInInspector] public int resistWaterBuff;
    [HideInInspector] public int resistWaterMalus;
    public int resistWaterFinal;

    // Canvas Bars
    public RectTransform experienceBar;
    public Vector3 experienceBarLocalSpace;
    public RectTransform healthBar;
    public Vector3 healthBarLocalSpace;
    public RectTransform heroHealthBar;
    public Vector3 heroHealthBarLocalSpace;
    public RectTransform manaBar;
    public Vector3 manaBarLocalSpace;
    public RectTransform heroManaBar;
    public Vector3 heroManaBarLocalSpace;
    // Canvas
    public DynamicTextData damageTextData;

    // Run Speed
    [SerializeField] public float _speed = 8;
    public NavMeshAgent agent;

    // Moves & Attack
    public CharacterController characterController;
    public Animator animator;
    [HideInInspector] public GameObject hands;
    [HideInInspector] public Vector3 directionKeyboard;
    [HideInInspector] public Vector3 directionMouse;
    [HideInInspector] public bool moveToTarget;

    private float timeOnLeftClick;
    private float timeBetweenLeftClickDownAndUp = 0.20f;
    private bool leftClick;
    public bool isAttacking = false;
    private float timeClickOnMonster;
    public bool canAttack = true;
    public bool canMove = true;

    // Target
    [HideInInspector] public Monster targetMonster;
    [HideInInspector] public GameObject targetSackGameObject;
    [HideInInspector] public GameObject targetNpcGameObject;
    [HideInInspector] public GameObject targetStorageGameObject;
    public GameObject hitParticles;
    public GameObject teleportParticles;
    public GameObject lootParticles;
    private GameObject goHitParticles;
    private int damage;
    private int naturalDamageFromAttributes;
    private float nextAttack = 0;
    private bool attackSuccess;

    // Items
    public Weapon weapon;
    [HideInInspector] public Item item;

    // Colliders
    [SerializeField] private float _colliderDistance = 2;

    // XP
    private float experiencePerHit;
    public Level leveling;

    // SFX
    public AudioClip[] sfx;
    private AudioSource audioSource;

    // Player Chat
    [HideInInspector] public bool isFocusedChat;
    [HideInInspector] public string lastChat;

    // Player Skin
    [HideInInspector] public GameObject body;
    [HideInInspector] public GameObject accessories;
    [HideInInspector] public bool isWaitingNpcAnswer;

    // Player Quest
    public List<Quest> questInProgressList;

    // Unity Functions

    void Awake()
    {
        // Instance Singleton, Player is used in Monster Class
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Get Body Player
        GetPlayerBodyGo();
        GetPlayerAccessoriesGo();

        // Get Components
        agent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        hands = GameObject.Find("Punch");
        audioSource = GetComponent<AudioSource>();
        leveling = GetComponent<Level>();
    }

    void Start()
    {
        // Init Cursor & Bars
        Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        healthBarLocalSpace = healthBar.localScale;
        heroHealthBarLocalSpace = heroHealthBar.localScale;
        manaBarLocalSpace = manaBar.localScale;
        heroManaBarLocalSpace = heroManaBar.localScale;
        experienceBarLocalSpace = experienceBar.localScale;

        // Repeat Player Regeneration every second
        InvokeRepeating("RegenerateHealthPlayer", 0, 1);
        InvokeRepeating("RegenerateManaPlayer", 0, 1);

        // Weapon Equipped
        var playerItems = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < playerItems.Length; i++)
        {
            if (playerItems[i].GetComponent<Weapon>() != null) { }
            {
                if (playerItems[i].activeSelf == true)
                {
                    weapon = playerItems[i].GetComponent<Weapon>();

                    // This item is a weapon
                    if (weapon != null)
                    {
                        // If the weapon is active in one of the hands, we define the item as equipped
                        if (weapon.transform.parent != null)
                        {
                            if (weapon.transform.parent.name == "RIGHT_HAND_COMBAT" || weapon.transform.parent.name == "LEFT_HAND_COMBAT" && weapon.gameObject.activeSelf == true)
                            {
                                item = playerItems[i].GetComponent<Item>();
                                item.equipped = true;

                                // Since we have a real weapon equip, we disable punch hands fake one
                                hands.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
        // No real weapon is equiped, player gonna punch with hands
        if (weapon == null)
        {
            hands.SetActive(true);
            weapon = hands.GetComponent<Weapon>();
        }
    }

    void Update()
    {
        // Try attacking a valid target
        if (targetMonster && targetMonster.currentHealth > 0 && isAttacking) ContinueAttack();

        // Try picking a valid Sack target
        if (targetSackGameObject) ContinueToMoveToSack();

        // Try picking a valid Sack target
        if (targetNpcGameObject) ContinueToMoveToNpc();

        // Try picking a valid Storage target
        if (targetStorageGameObject) ContinueToMoveToStorage();

        // Update Frame Hero
        UpdateHeroFrame();

        // Refresh Attributes, buff & malus care
        CurrentAttributes();

        // Refresh Current Armor Class
        CurrentArmorClass();

        // Refresh Skills, buff & malus care
        CurrentSkills();

        // Refresh Power & Resist
        CurrentPowersAndResists();

        // Refresh Current Encombrement
        CurrentEncumbrance();

        // Update Health Player
        UpdateHealthPlayer();

        // Update Mana Player
        UpdateManaPlayer();

        // Update XP Bar
        UpdateCanvasExperiencePlayer();

        // Manager Left Click being Attack or Move
        LeftClickManager();

        // Keyboard Moves
        KeyboardMoveManager();

        // Force Camera Keep Initial Config
        ForceCameraToBe3DIso();
    }

    // Custom Functions

    public void CurrentEncumbrance()
    {
        encumbrance = (strenghtFinal * 500) / (strenghtFinal + 100);
        leftEncumbrance = encumbrance - currentEncumbrance;
    }

    public void CurrentArmorClass()
    {
        armorClassFinal = armorClass + armorClassBuff - armorClassMalus;
    }

    public void CurrentAttributes()
    {
        strenghtFinal = strenght + strenghtBuff - strenghtMalus;
        enduranceFinal = endurance + enduranceBuff - enduranceMalus;
        dexterityFinal = dexterity + dexterityBuff - dexterityMalus;
        intelectFinal = intelect + intelectBuff - intelectMalus;
        wisdomFinal = wisdom + wisdomBuff - wisdomMalus;
        healthFinal = health + healthBuff - healthMalus;
        manaFinal = mana + manaBuff - manaMalus;
    }

    public void CurrentPowersAndResists()
    {
        powerFireFinal = powerFire + powerFireBuff - powerFireMalus;
        powerEarthFinal = powerEarth + powerEarthBuff - powerEarthMalus;
        powerAirFinal = powerAir + powerAirBuff - powerAirMalus;
        powerWaterFinal = powerWater + powerWaterBuff - powerWaterMalus;
        powerLightFinal = powerLight + powerLightBuff - powerLightMalus;
        powerDarkFinal = powerDark + powerDarkBuff - powerDarkMalus;
        resistFireFinal = resistFire + resistFireBuff - resistFireMalus;
        resistEarthFinal = resistEarth + resistEarthBuff - resistEarthMalus;
        resistAirFinal = resistAir + resistAirBuff - resistAirMalus;
        resistWaterFinal = resistWater + resistWaterBuff - resistWaterMalus;
        resistLightFinal = resistLight + resistLightBuff - resistLightMalus;
        resistDarkFinal = resistDark + resistDarkBuff - resistDarkMalus;
    }

    public void CurrentSkills()
    {
        attackFinal = attack + attackBuff - attackMalus;
        archeryFinal = archery + archeryBuff - archeryMalus;
        dodgeFinal = dodge + dodgeBuff - dodgeMalus;
    }

    public void ResetTarget()
    {
        targetStorageGameObject = null;
        targetMonster = null;
        targetSackGameObject = null;
        targetNpcGameObject = null;
        agent.ResetPath();
        agent.isStopped = false;
        isAttacking = false;
        canMove = true;
        canAttack = true;
        moveToTarget = false;
        animator.SetBool("meleeAttack", false);
        animator.SetBool("rangeAttack", false);
        animator.SetBool("handAttack", false);
        animator.SetBool("run", false);
    }

    private void KeyboardMoveManager()
    {
        if (isFocusedChat == false)
        {
            if (directionKeyboard == Vector3.zero && directionMouse == Vector3.zero && moveToTarget == false) animator.SetBool("run", false);
            if (directionKeyboard != Vector3.zero)
            {

                directionMouse = Vector3.zero;
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.TransformDirection(directionKeyboard), out hit, _colliderDistance);

                Ray rayWater = new Ray(transform.position, Vector3.down);
                if (!Physics.Raycast(rayWater, 50, 4))
                {
                    // There is no collid or this is a available portal or teleport zone
                    if (hit.collider == false || hit.collider.tag == "Teleport")
                    {
                        // Run Animation On
                        animator.SetBool("handAttack", false);
                        animator.SetBool("meleeAttack", false);
                        animator.SetBool("rangeAttack", false);
                        animator.SetBool("run", true);
                        // Move to none collider position
                        transform.Translate(directionKeyboard * (_speed * Time.deltaTime));
                    }
                }
            }
        }
    }

    private void ForceCameraToBe3DIso()
    {
        var limitPlayerRotation = transform.localEulerAngles;
        limitPlayerRotation.x = 0;
        limitPlayerRotation.z = 0;
        transform.localEulerAngles = limitPlayerRotation;
    }

    public void LeftClickManager()
    {
        // Time and Set Left Click True
        if (Input.GetMouseButtonDown(0))
        {
            timeOnLeftClick = Time.time;
            leftClick = true;
        }

        // This is a long click, meaning the player want to mouse move
        if (leftClick && (Time.time - timeOnLeftClick) > timeBetweenLeftClickDownAndUp) OnMoveMouse();

        //  This is a short click, meaning player is attacking or picking item
        if (Input.GetMouseButtonUp(0))
        {
            leftClick = false;

            // Run Animation On
            animator.SetBool("run", false);
            //Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);

            if ((Time.time - timeOnLeftClick) < timeBetweenLeftClickDownAndUp)
            {
                // This is a short Click Interaction
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // This is a attack monster short click
                if (Physics.Raycast(ray, out hit) && canAttack == true && hit.collider.gameObject.CompareTag("Monster")) FirstAttack(hit);

                // This is a unequipped item pick up click
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Sack")) FirstMoveToSack(hit);

                // This is a click to go to a Npc
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Npc")) FirstMoveToNpc(hit);

                // This is a click to go to a Storage
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Storage")) FirstMoveToStorage(hit);

            }

            // Reset to default cursor
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        }
    }

    public void FirstMoveToSack(RaycastHit hit)
    {
        ResetTarget();
        targetSackGameObject = hit.collider.gameObject;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetSackGameObject.transform.position - transform.position), 1);
    }

    public void FirstMoveToNpc(RaycastHit hit)
    {
        ResetTarget();
        targetNpcGameObject = hit.collider.gameObject;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetNpcGameObject.transform.position - transform.position), 1);
    }

    public void FirstMoveToStorage(RaycastHit hit)
    {
        ResetTarget();
        targetStorageGameObject = hit.collider.gameObject;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetStorageGameObject.transform.position - transform.position), 1);
    }

    public void ContinueToMoveToSack()
    {
        moveToTarget = true;
        if (agent.velocity.sqrMagnitude > 0) animator.SetBool("run", true);
        agent.speed = _speed;
        agent.destination = targetSackGameObject.transform.position;
    }

    public void ContinueToMoveToNpc()
    {
        moveToTarget = true;
        if (agent.velocity.sqrMagnitude > 0) animator.SetBool("run", true);
        agent.speed = _speed;
        agent.destination = targetNpcGameObject.transform.position;
    }

    public void ContinueToMoveToStorage()
    {
        moveToTarget = true;
        if (agent.velocity.sqrMagnitude > 0) animator.SetBool("run", true);
        agent.speed = _speed;
        agent.destination = targetStorageGameObject.transform.position;
    }

    // Entered NPC collid and said nothing
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Npc")
        {
            lastChat = null;
            isWaitingNpcAnswer = true;
            other.gameObject.GetComponent<Npc>().isWaitingPlayerAnswer = false;
        }
    }

    // In NPC collid and said something
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Npc" && lastChat != null)
        {
            isWaitingNpcAnswer = true;
            other.gameObject.GetComponent<Npc>().isWaitingPlayerAnswer = false;
        }
    }

    // Leaving & reset all the flags
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Npc")
        {
            isWaitingNpcAnswer = false;
            other.gameObject.GetComponent<Npc>().isWaitingPlayerAnswer = false;
        }
    }

    public void ContinueAttack()
    {
        // Monster isnt in range, player is moving until being in
        float distanceToMonster = Vector3.Distance(transform.position, targetMonster.transform.position);
        if (distanceToMonster > weapon.range)
        {
            //agent.speed = _speed;
            agent.SetDestination(targetMonster.transform.position);
            directionKeyboard = Vector3.zero;
            directionMouse = Vector3.zero;
            moveToTarget = true;
            if (agent.velocity.sqrMagnitude > 0) animator.SetBool("run", true);
        }

        // Attack confirms the object is a monster and in range
        if (distanceToMonster <= weapon.range && targetMonster.currentHealth > 0)
        {
            // Transform rotation to prepair player to shot forward being tuned in monster direction
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetMonster.transform.position - transform.position), 1);

            // Stop Agent cuz we are close enought
            agent.isStopped = true;
            animator.SetBool("run", false);

            // Check if ray simulating attack is colliding something
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (targetMonster.transform.position - transform.position), out hit, weapon.range))
            {
                // If we collid something that is not the monster, attack is cancel, IA turn's on to move until being in a angle letting player shot
                if (!hit.collider.gameObject.CompareTag("Monster"))
                {
                    // Move to monster only if monster his behind another collid
                    if (Vector3.Distance(transform.position, targetMonster.transform.position) > Vector3.Distance(transform.position, hit.collider.gameObject.transform.position))
                    {
                        if (agent.velocity.sqrMagnitude > 0) animator.SetBool("run", true);
                        agent.destination = targetMonster.transform.position;
                        agent.isStopped = false;
                    }
                }
                else
                {
                    animator.SetBool("run", false);
                    agent.isStopped = true;
                }
            }

            // Check if the frame refresh is not quicker than the weapon frequency should be
            if (Time.time > nextAttack && agent.isStopped == true)
            {
                ApplyAttack();
                nextAttack = Time.time + weapon.frequency;
            }
        }

    }

    public void FirstAttack(RaycastHit hit)
    {
        ResetTarget();

        // These conditions matches with ContinueAttack() in Update()
        hit.collider.TryGetComponent(out Monster target);
        targetMonster = target;
        isAttacking = true;
        timeClickOnMonster = Time.time;
    }

    public void ApplyAttack()
    {
        moveToTarget = false;

        // Target Monster is dead, reset target
        if (targetMonster.currentHealth <= 0 || targetMonster.isDead == true)
        {
            animator.SetBool("handAttack", false);
            animator.SetBool("meleeAttack", false);
            animator.SetBool("rangeAttack", false);
            targetMonster.isDead = true;
            targetMonster = null;
        }
        else
        {
            // Rotate in direction of Monster
            var targetRotation = Quaternion.LookRotation(targetMonster.transform.position - transform.position);
            var cameraRotation = Camera.main.transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1);
            Camera.main.transform.rotation = cameraRotation;

            if (weapon.isHand) attackSuccess = Random.Range(1, attackFinal) >= Random.Range(1, targetMonster.dodge);
            if (weapon.isMelee) attackSuccess = Random.Range(1, attackFinal) >= Random.Range(1, targetMonster.dodge);
            if (weapon.isRange) attackSuccess = Random.Range(1, archeryFinal) >= Random.Range(1, targetMonster.dodge);

            StartCoroutine("IAttack");
        }
    }

    private void OnHitGiveExperience()
    {
        // Experience per Hit
        if (damage <= targetMonster.currentHealth)
        {
            experiencePerHit = (float)(damage * Math.Round((targetMonster.experience / targetMonster.health), 2));
        }
        if (damage > targetMonster.currentHealth)
        {
            experiencePerHit = (float)(targetMonster.currentHealth * Math.Round((targetMonster.experience / targetMonster.health), 2));
        }
        experience += experiencePerHit;
    }

    private void UpdateCanvasExperiencePlayer()
    {
        float currentExperienceBar = (experience - leveling.currentLevelExperience) / leveling.nextLevelExperience;
        if (currentExperienceBar > 0)
        {
            experienceBar.localScale = new Vector3(currentExperienceBar, experienceBar.localScale.y, experienceBar.localScale.z);
            GameObject.Find("XP Percent").GetComponent<TMPro.TextMeshProUGUI>().text = Math.Round((currentExperienceBar * 100), 2).ToString() + "%";
        }
        else
        {
            experienceBar.localScale = new Vector3(0, experienceBar.localScale.y, experienceBar.localScale.z);
            GameObject.Find("XP Percent").GetComponent<TMPro.TextMeshProUGUI>().text = "0%";
        }
    }

    private void UpdateHealthPlayer()
    {
        if (currentHealth > 0)
        {
            float currentHealthBar = currentHealth / (float)health;
            healthBar.localScale = new Vector3(currentHealthBar * healthBarLocalSpace.x, healthBar.localScale.y, healthBar.localScale.z);
            heroHealthBar.localScale = new Vector3(currentHealthBar * heroHealthBarLocalSpace.x, heroHealthBar.localScale.y, heroHealthBar.localScale.z);
        }
        if (currentHealth <= 0)
        {
            GetPlayerDeath();
        }
    }
    private void UpdateHeroFrame()
    {
        GameObject heroFrame = GameObject.Find("Hero Frame");
        if (heroFrame != null)
        {
            GameObject playerName = heroFrame.transform.Find("Player Name").gameObject;
            if (playerName != null) playerName.GetComponent<TextMeshProUGUI>().text = pseudo;
            GameObject levelFrame = heroFrame.transform.Find("Level Frame").gameObject;
            if (levelFrame != null)
            {
                levelFrame.transform.Find("Player Level").GetComponent<TextMeshProUGUI>().text = level.ToString();
            }
        }
    }
    private void UpdateManaPlayer()
    {
        if (currentMana > 0)
        {
            float currentManaBar = currentMana / (float)mana;
            manaBar.localScale = new Vector3(currentManaBar * manaBarLocalSpace.x, manaBar.localScale.y, manaBar.localScale.z);
            heroManaBar.localScale = new Vector3(currentManaBar * heroManaBarLocalSpace.x, heroManaBar.localScale.y, heroManaBar.localScale.z);
        }
    }

    private void GetPlayerDeath()
    {
        ResetTarget();
        agent.enabled = false;
        audioSource.PlayOneShot(sfx[6], 0.1f);
        //GameObject goTeleportParticles = Instantiate(teleportParticles, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity); ;
        //Destroy(goTeleportParticles, 2);
        transform.position = new Vector3(588, 50, 530);
        agent.enabled = true;
        GetLostExperienceAndGold();
        currentHealth = 1;
        return;
    }

    private void GetLostExperienceAndGold()
    {
        float experienceFromThisLevel = experience - leveling.currentLevelExperience;
        experience -= (experienceFromThisLevel / 100) * 20;
        gold -= gold / 10;
    }

    private void RegenerateHealthPlayer()
    {
        if (currentHealth < health)
        {
            // We back to Zero is we get lowed than that by big hit
            if (currentHealth < 0) currentHealth = 0;

            // Calcul the health to add with regeneration
            int regenerateHealth = health / 100 * healthRegeneration;

            // Add Health
            if (regenerateHealth > 0) currentHealth += health / 100 * healthRegeneration;
            if (regenerateHealth == 0) currentHealth += 1;
        }
    }

    private void RegenerateManaPlayer()
    {
        if (currentMana < mana)
        {
            // We back to Zero is we get lowed than that by big hit
            if (currentMana < 0) currentMana = 0;

            // Calcul the mana to add with regeneration
            int regenerateMana = mana / 100 * manaRegeneration;

            // Add Mana
            if (regenerateMana > 0) currentMana += mana / 100 * manaRegeneration;
            if (regenerateMana == 0) currentMana += 1;
        }
    }

    public void OnMoveMouse()
    {
        if (canMove && GameManager.Instance.inventoryEnabled == false)
        {
            // Reset Attack & Target
            ResetTarget();
            isAttacking = false;

            // Get Direction
            directionMouse = Rotate.Instance.RotateCharacterAccordingMouseClick();

            // Keep Character on ground : preventing mouse press spam directions making him going sky
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (directionMouse == Vector3.zero && directionKeyboard == Vector3.zero && moveToTarget == false) animator.SetBool("run", false);
            if (directionMouse != Vector3.zero)
            {
                directionKeyboard = Vector3.zero;
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.TransformDirection(directionMouse), out hit, _colliderDistance);
                // There is no collid or this is a available portal or teleport zone
                Ray rayWater = new Ray(transform.position, Vector3.down);
                if (!Physics.Raycast(rayWater, 50, 4))
                {
                    if (hit.collider == false || hit.collider.tag == "Teleport")
                    {
                        // Run Animation On
                        animator.SetBool("meleeAttack", false);
                        animator.SetBool("rangeAttack", false);
                        animator.SetBool("handAttack", false);
                        animator.SetBool("run", true);
                        // Move to none collider position
                        transform.Translate(directionMouse * (_speed * Time.deltaTime));
                    }
                }
            }

        }
    }

    public void OnMoveKeyboard(InputAction.CallbackContext context)
    {
        if (canMove && isFocusedChat == false)
        {
            // Reset Attack & Target
            ResetTarget();
            isAttacking = false;

            Vector2 readVector = context.ReadValue<Vector2>();
            Vector3 toConvert = new Vector3(readVector.x, 0, readVector.y);
            directionKeyboard = Rotate.Instance.RotateCharacterAccordingKeyboardDirectionals(toConvert);
        }
    }

    // Coroutines

    IEnumerator IAttack()
    {
        canAttack = false;
        canMove = false;
        if (weapon.isHand) animator.SetBool("handAttack", true);
        if (weapon.isMelee) animator.SetBool("meleeAttack", true);
        if (weapon.isRange) animator.SetBool("rangeAttack", true);
        GetNaturalDamageFromAttributes();
        damage = Random.Range(weapon.damageMin, weapon.damageMax) + weapon.damageFix + naturalDamageFromAttributes - targetMonster.armorClass;
        yield return new WaitForSeconds(weapon.frequency);
    }

    IEnumerator IFollowTarget()
    {
        yield return new WaitForSeconds(0.2f);
    }

    public void HitAttack()
    {
        if (attackSuccess)
        {
            // Attack is success
            if (targetMonster != null)
            {
                // Arrow Success Attack Move
                if (weapon.isRange)
                {
                    GameObject.Find("Arrow").TryGetComponent(out Projectile projectile);
                    projectile.OnBowShootingArrowSuccess(targetMonster);
                }

                OnHitSuccessShowDamage();
                OnHitGiveExperience();

                // Damage apply
                targetMonster.currentHealth -= damage;
                targetMonster.GetComponent<Monster>().GetHitAudio();
                if (targetMonster && targetMonster.isDead == false) goHitParticles = Instantiate(hitParticles, new Vector3(targetMonster.transform.position.x, targetMonster.transform.position.y + 1, targetMonster.transform.position.z), Quaternion.identity);
                if (targetMonster && targetMonster.isDead == false) Destroy(goHitParticles, 2);
            }
        }
        else
        {
            // Attack is fail
            if (targetMonster != null)
            {
                // Arrow Fail Attack Move
                if (weapon.isRange)
                {
                    GameObject.Find("Arrow").TryGetComponent(out Projectile projectile);
                    projectile.OnBowShootingArrowFail(targetMonster);
                }
                OnHitFailShowDodge();
            }
        }

        canAttack = true;
        canMove = true;
    }

    private void OnHitSuccessShowDamage()
    {
        // Is target monster still available after delay ?
        if (targetMonster != null)
        {
            // Show UI Damage
            DynamicTextData data = targetMonster.damageTextData;
            Vector3 destination = targetMonster.transform.position;
            destination.x += (Random.value + 0.5f) / 3;
            destination.y += (Random.value + 5) / 3;
            destination.z += (Random.value - 0.5f) / 3;

            DynamicTextManager.CreateText(destination, "-" + damage.ToString(), data);
        }
    }

    private void OnHitFailShowDodge()
    {
        // Is target monster still available after delay ?
        if (targetMonster != null)
        {
            // Show UI Damage
            DynamicTextData data = targetMonster.damageTextData;
            Vector3 destination = targetMonster.transform.position;
            destination.x += (Random.value + 0.5f) / 3;
            destination.y += (Random.value + 5) / 3;
            destination.z += (Random.value - 0.5f) / 3;

            DynamicTextManager.CreateText(destination, "Missed", data);
        }
    }

    private void GetNaturalDamageFromAttributes()
    {
        if (weapon.isMelee || weapon.isHand) naturalDamageFromAttributes = (strenghtFinal - 20) / 5;
        if (weapon.isRange) naturalDamageFromAttributes = ((strenghtFinal - 20) / 20) + ((dexterityFinal - 20) / 10);
        if (naturalDamageFromAttributes < 0) naturalDamageFromAttributes = 0;
    }

    // Audio

    public void RunAudio()
    {
        audioSource.PlayOneShot(sfx[0], 0.25f);
    }

    public void AttackMeleeAudio()
    {
        audioSource.PlayOneShot(sfx[1], 0.05f);
    }

    public void AttackRangeAudio()
    {
        audioSource.PlayOneShot(sfx[2], 0.05f);
    }

    public void AttackRangeArrowAudio()
    {
        audioSource.PlayOneShot(sfx[3], 0.05f);
    }

    public void AttackPunchAudio()
    {
        audioSource.PlayOneShot(sfx[5], 0.1f);
    }

    public void GetHitAudio()
    {
        audioSource.PlayOneShot(sfx[4], 0.2f);
    }

    public void GetPlayerBodyGo()
    {
        GameObject meshes = transform.Find("Meshes").gameObject;
        if (meshes != null)
        {
            body = meshes.transform.Find("Body").gameObject;

        }
    }

    public void GetPlayerAccessoriesGo()
    {
        GameObject meshes = transform.Find("Meshes").gameObject;
        if (meshes != null)
        {
            accessories = meshes.transform.Find("Accessories").gameObject;

        }
    }
}
