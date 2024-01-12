using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;
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
    public int experienceTotal;
    public int gold;
    public int level;

    // Player Stats Attributes
    public int health;
    public int healthBuff;
    public int healthMalus;
    public int healthFinal;
    public int currentHealth;
    public int healthRegeneration;
    public int healthRegenerationBuff;
    public int healthRegenerationMalus;
    public int healthRegenerationFinal;
    public int mana;
    public int manaBuff;
    public int manaMalus;
    public int manaFinal;
    public int manaRegeneration;
    public int manaRegenerationBuff;
    public int manaRegenerationMalus;
    public int mangaRegenerationFinal;
    public int currentMana;
    public int encombrement;

    // Player Attributes
    public int strenght;
    public int strenghtBuff;
    public int strenghtMalus;
    public int strenghtFinal;
    public int endurance;
    public int enduranceBuff;
    public int enduranceMalus;
    public int enduranceFinal;
    public int dexterity;
    public int dexterityBuff;
    public int dexterityMalus;
    public int dexterityFinal;
    public int intelect;
    public int intelectBuff;
    public int intelectMalus;
    public int intelectFinal;
    public int wisdom;
    public int wisdomBuff;
    public int wisdomMalus;
    public int wisdomFinal;

    // Player Skills
    public int attack;
    public int attackBuff;
    public int attackMalus;
    public int attackFinal;
    public int archery;
    public int archeryBuff;
    public int archeryMalus;
    public int archeryFinal;
    public int dodge;
    public int dodgeBuff;
    public int dodgeMalus;
    public int dodgeFinal;
    public float armorClass;
    public float armorClassBuff;
    public float armorClassMalus;
    public float armorClassFinal;

    // Canvas Health Bars
    public RectTransform healthBar;
    public Vector3 healthBarLocalSpace;

    // Run Speed
    [SerializeField] public float _speed = 8;
    public NavMeshAgent agent;

    // Moves & Attack
    public CharacterController characterController;
    public Animator animator;
    public GameObject hands;
    public Vector3 directionKeyboard;
    public Vector3 directionMouse;
    public bool moveToTarget;

    private float timeOnLeftClick;
    private float timeBetweenLeftClickDownAndUp = 0.30f;
    private bool leftClick;
    public bool isAttacking = false;
    public bool canAttack = true;
    public bool canMove = true;

    // Target Attack
    public Monster targetMonster;
    public GameObject targetSackGameObject;
    public GameObject hitParticles;
    public GameObject diedParticles;
    public GameObject lootParticles;
    private GameObject goHitParticles;
    private int damage;
    private float nextAttack = 0;
    private bool attackSuccess;

    // Items
    public Weapon weapon;
    public Item item;

    // Colliders & Transparency Objects Distance
    [SerializeField] private float _colliderDistance = 2;
    [SerializeField] private float _isBehingObjectDistance = 3;

    // XP
    public int experiencePerHit;

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

        // Get Components
        agent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        hands = GameObject.Find("Punch");

    }

    void Start()
    {
        // Init Cursos & Health Bar
        Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        healthBarLocalSpace = healthBar.localScale;

        // Repeat Player Health Regeneration every second
        InvokeRepeating("RegenerateHealthPlayer", 0, 1);

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
        // Refresh Attributes, buff & malus care
        currentAttributes();

        // Refresh Current Armor Class
        currentArmorClass();

        // Refresh Skills, buff & malus care
        currentSkills();

        // Try attacking a valid target
        if (targetMonster && targetMonster.currentHealth > 0 && isAttacking) ContinueAttack();

        // Try picking a valid item(targetItem)
        if (targetSackGameObject) ContinueToMoveToItem();

        // Update Health Player
        UpdateCanvasHealthPlayer();

        // Manager Left Click being Attack or Move
        leftClickManager();

        // Keyboard Moves
        keyboardMoveManager();

        // Force Camera Keep Initial Config
        forceCameraToBe3DIso();
    }

    // Custom Functions

    public void currentArmorClass()
    {
        armorClassFinal = armorClass + armorClassBuff - armorClassMalus;
    }

    public void currentAttributes()
    {
        strenghtFinal = strenght + strenghtBuff - strenghtMalus;
        enduranceFinal = endurance + enduranceBuff - enduranceMalus;
        dexterityFinal = dexterity + dexterityBuff - dexterityMalus;
        intelectFinal = intelect + intelectBuff - intelectMalus;
        wisdomFinal = wisdom + wisdomBuff - wisdomMalus;
    }

    public void currentSkills()
    {
        attackFinal = attack + attackBuff - attackMalus;
        archeryFinal = archery + archeryBuff - archeryMalus;
        dodgeFinal = dodge + dodgeBuff - dodgeMalus;
    }

    public void resetTarget()
    {
        agent.ResetPath();
        agent.isStopped = false;
        targetMonster = null;
        targetSackGameObject = null;
        isAttacking = false;
        animator.SetBool("meleeAttack", false);
        animator.SetBool("rangeAttack", false);
        animator.SetBool("handAttack", false);
        animator.SetBool("run", false);
        Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
    }

    private void keyboardMoveManager()
    {
        if (directionKeyboard == Vector3.zero && directionMouse == Vector3.zero && moveToTarget == false) animator.SetBool("run", false);
        if (directionKeyboard != Vector3.zero)
        {

            directionMouse = Vector3.zero;
            // Deal colliders with Raycast to make transform.Translate able to collids without rigidbody on player
            if (!Physics.Raycast(transform.position, transform.TransformDirection(directionKeyboard), _colliderDistance))
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

    private void forceCameraToBe3DIso()
    {
        transform.position = new Vector3(transform.position.x, 50, transform.position.z);
        var limitPlayerRotation = transform.localEulerAngles;
        limitPlayerRotation.x = 0;
        limitPlayerRotation.z = 0;
        transform.localEulerAngles = limitPlayerRotation;
        Camera.main.transform.rotation = Quaternion.Euler(30, 45, 0);
    }

    public void leftClickManager()
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
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);

            if ((Time.time - timeOnLeftClick) < timeBetweenLeftClickDownAndUp)
            {
                // This is a short Click Interaction
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // This is a attack monster short click
                if (Physics.Raycast(ray, out hit) && canAttack == true && hit.collider.gameObject.CompareTag("Monster")) FirstAttack(hit);

                // This is a unequipped item pick up click
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Sack")) FirstMoveToSack(hit);

            }

            // Reset to default cursor
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        }
    }

    public void FirstMoveToSack(RaycastHit hit)
    {
        resetTarget();
        // Thes conditions matches with ContinueMoveItem in Update()
        targetSackGameObject = hit.collider.gameObject;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetSackGameObject.transform.position - transform.position), 1);
    }

    public void ContinueToMoveToItem()
    {
        moveToTarget = true;
        animator.SetBool("run", true);
        agent.speed = _speed;
        agent.destination = targetSackGameObject.transform.position;
    }

    public void ContinueAttack()
    {
        // Monster isnt in range, player is moving until being in range
        if (Vector3.Distance(transform.position, targetMonster.transform.position) > weapon.range)
        {
            moveToTarget = true;
            animator.SetBool("run", true);
            agent.speed = _speed;
            agent.destination = targetMonster.transform.position;
        }

        // Attack confirms the object is a monster and in range
        if (Vector3.Distance(transform.position, targetMonster.transform.position) <= weapon.range && targetMonster.currentHealth > 0)
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
                        animator.SetBool("run", true);
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
        resetTarget();

        // These conditions matches with ContinueAttack() in Update()
        hit.collider.TryGetComponent(out Monster target);
        targetMonster = target;
        isAttacking = true;
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

            if (attackSuccess)
            {
                StartCoroutine("IAttackSuccess");
                StartCoroutine("IHitSuccessShowDamage");
                OnHitGiveExperience();
            }
            else
            {
                StartCoroutine("IAttackFail");
                StartCoroutine("IHitFailShowDodge");
            }

        }
    }

    private void OnHitGiveExperience()
    {
        // Experience per Hit
        if (damage <= targetMonster.currentHealth) experiencePerHit = damage * (targetMonster.experience / health);
        if (damage > targetMonster.currentHealth) experiencePerHit = currentHealth * (targetMonster.experience / health);
        experienceTotal += experiencePerHit;

    }

    private void UpdateCanvasHealthPlayer()
    {
        if (healthBar.localScale.x > 0.25f)
        {
            float currentHealthBar = currentHealth / (float)health;
            healthBar.localScale = new Vector3(currentHealthBar * healthBarLocalSpace.x, healthBar.localScale.y, healthBar.localScale.z);
        }
    }

    private void RegenerateHealthPlayer()
    {
        if (currentHealth < health)
        {
            currentHealth += health / 100 * healthRegeneration;
        }
        float currentHealthBar = currentHealth / (float)health;
        if (currentHealth > 0) healthBar.localScale = new Vector3(currentHealthBar * healthBarLocalSpace.x, healthBar.localScale.y, healthBar.localScale.z);
    }

    public void OnMoveMouse()
    {
        if (canMove)
        {
            // Reset Attack & Target
            resetTarget();
            isAttacking = false;

            // Get Direction
            directionMouse = Rotate.Instance.RotateCharacterAccordingMouseClick();

            // Keep Character on ground : preventing mouse press spam directions making him going sky
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (directionMouse == Vector3.zero && directionKeyboard == Vector3.zero && moveToTarget == false) animator.SetBool("run", false);
            if (directionMouse != Vector3.zero)
            {
                directionKeyboard = Vector3.zero;
                // Deal colliders with Raycast to make transform.Translate able to collids without rigidbody on player
                if (!Physics.Raycast(transform.position, transform.TransformDirection(directionMouse), _colliderDistance))
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

    public void OnMoveKeyboard(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            // Reset Attack & Target
            resetTarget();
            isAttacking = false;

            Vector2 readVector = context.ReadValue<Vector2>();
            Vector3 toConvert = new Vector3(readVector.x, 0, readVector.y);
            directionKeyboard = Rotate.Instance.RotateCharacterAccordingKeyboardDirectionals(toConvert);
        }
    }

    // Coroutines

    IEnumerator IAttackSuccess()
    {
        canAttack = false;
        canMove = false;
        if (weapon.isHand) animator.SetBool("handAttack", true);
        if (weapon.isMelee) animator.SetBool("meleeAttack", true);
        if (weapon.isRange) animator.SetBool("rangeAttack", true);
        damage = Random.Range(weapon.damageMin, weapon.damageMax) + weapon.damageFix;
        yield return new WaitForSeconds(weapon.frequency / 2); // Collision is usually on middle of anim, frequency shot by 2 should be match
        // Is target monster still available after delay ?
        if (targetMonster != null)
        {
            // Arrow Success Attack Move
            if (weapon.isRange)
            {
                GameObject.Find("Arrow").TryGetComponent(out Projectile projectile);
                projectile.OnBowShootingArrowSuccess(targetMonster);
            }
            targetMonster.currentHealth -= damage;
            if (targetMonster && targetMonster.isDead == false) goHitParticles = Instantiate(hitParticles, new Vector3(targetMonster.transform.position.x, targetMonster.transform.position.y + 1, targetMonster.transform.position.z), Quaternion.identity);
            if (targetMonster && targetMonster.isDead == false) Destroy(goHitParticles, 2);
        }
        canAttack = true;
        canMove = true;
    }

    IEnumerator IAttackFail()
    {
        canAttack = false;
        canMove = false;
        if (weapon.isMelee) animator.SetBool("meleeAttack", true);
        if (weapon.isRange) animator.SetBool("rangeAttack", true);
        yield return new WaitForSeconds(weapon.frequency / 2); // to do, weapon delay in ms to pass a parameter and use instead of 1
        // Is target monster still available after delay ?
        if (targetMonster != null)
        {
            // Arrow Fail Attack Move
            if (weapon.isRange)
            {
                GameObject.Find("Arrow").TryGetComponent(out Projectile projectile);
                projectile.OnBowShootingArrowFail(targetMonster);
            }
        }
        canAttack = true;
        canMove = true;
    }

    IEnumerator IHitSuccessShowDamage()
    {
        yield return new WaitForSeconds(weapon.frequency / 2);
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

    IEnumerator IHitFailShowDodge()
    {
        yield return new WaitForSeconds(weapon.frequency / 2);
        // Is target monster still available after delay ?
        if (targetMonster != null)
        {
            // Show UI Damage
            DynamicTextData data = targetMonster.damageTextData;
            Vector3 destination = targetMonster.transform.position;
            destination.x += (Random.value + 0.5f) / 3;
            destination.y += (Random.value + 5) / 3;
            destination.z += (Random.value - 0.5f) / 3;

            DynamicTextManager.CreateText(destination, "Dodged!", data);
        }
    }
}
