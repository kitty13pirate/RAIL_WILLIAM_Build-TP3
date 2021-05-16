using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterBoi : MonoBehaviour
{
    // La vie du personnage
    public int health = 1000;
    public Slider healthBar;

    // La vitesse du personnage
    public float movementSpeed;

    // La direction du mouvement
    private Vector3 moveDirection;

    //Le rigidbody
    private Rigidbody rb;

    //Si le personnage est mort
    public bool isDead;

    //Le particle system pour la teleportation
    public ParticleSystem psTeleport;

    // L'animator
    private Animator playerCharacterBoiAnimator;

    // Pour verifier si le personnage utilise un sort
    private bool isCasting;

    // Les prefabs pour les sorts divers
    public Object spectralHand;
    public Object fireBall;
    public Object earthSpike;
    public Object waterJet;
    public Object windBurst;
    public Object healLight;

    // Pour verifier le cooldown des sortileges
    private bool teleportCooldown;
    private bool pushCooldown;
    private bool healCooldown;
    private bool earthCooldown;
    private bool fireCooldown;
    private bool waterCooldown;
    private bool windCooldown;

    // Le point de depart des sortileges
    public Transform castLocation;

    // Le layermask a ignorer
    public LayerMask mousePointMask;
    public LayerMask TeleportMask;
    public LayerMask NoTeleportMask;

    // L'audioSource et les audioClips
    private AudioSource audioSource;

    // La variable pour le menu
    private bool isMenu = false;

    // Les canvas pour le menu et l'UI
    public Canvas UI;
    public Canvas SoundMenu;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCharacterBoiAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifier les inputs du joueur
        if (!isCasting && !isMenu)
        {
            // Les touches pour les sorts
            if (Input.GetMouseButtonDown(0) && !teleportCooldown)
            {
                audioSource.PlayOneShot(audioSource.clip);
                playerCharacterBoiAnimator.SetTrigger("Basic Spell");
                isCasting = true;
            }
            if (Input.GetMouseButtonDown(1) && !pushCooldown)
            {
                playerCharacterBoiAnimator.SetTrigger("Push Spell");
                isCasting = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) && !fireCooldown)
            {
                playerCharacterBoiAnimator.SetTrigger("Fire Spell");
                isCasting = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !earthCooldown)
            {
                playerCharacterBoiAnimator.SetTrigger("Earth Spell");
                isCasting = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && !waterCooldown)
            {
                playerCharacterBoiAnimator.SetTrigger("Water Spell");
                isCasting = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && !windCooldown)
            {
                playerCharacterBoiAnimator.SetTrigger("Wind Spell");
                isCasting = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && !healCooldown)
            {
                playerCharacterBoiAnimator.SetTrigger("Heal Spell");
                isCasting = true;
            }
        }
        // Pour ouvrir le menu de son
        if (Input.GetKeyDown(KeyCode.P))
        {
            isMenu = !isMenu;
            SoundMenu.gameObject.SetActive(!SoundMenu.gameObject.activeSelf);
            UI.gameObject.SetActive(!UI.gameObject.activeSelf);
        }

        
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Déplacer le personnage selon le vecteur de direction
        if (!isCasting)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * movementSpeed * Time.fixedDeltaTime);
        }

        // Positionner le personnage vers la souris
        Turning();

        // Corriger les animations pour la direction du personnage
        Animating(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the mousePointMask layer...
        // Le 10 est le numero du layermask mousePoint, utilise pour detecter la position de la souris
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, mousePointMask) && !isDead)
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            rb.MoveRotation(newRotation);
        }
    }

    // Le jouer perd de la vie
    public void takeDamage(int damageNumber)
    {
        health -= damageNumber;
        if (health > 1000)
        {
            health = 1000;
        }
        healthBar.value = health;
        if (health <= 0)
        {
            die();
        }
    }

    // Le joueur meurt
    void die()
    {
        isDead = true;
        playerCharacterBoiAnimator.SetTrigger("Death");
        GameManager.singleton.GameOver();
    }

    void Animating(float h, float v)
    {
        moveDirection = new Vector3(h, 0, v);

        if (moveDirection.magnitude > 1.0f)
        {
            moveDirection = moveDirection.normalized;
        }

        moveDirection = transform.InverseTransformDirection(moveDirection);

        playerCharacterBoiAnimator.SetFloat("Horizontal", moveDirection.x, 0.05f, Time.deltaTime);
        playerCharacterBoiAnimator.SetFloat("Vertical", moveDirection.z, 0.05f, Time.deltaTime);
    }
    void casting()
    {
        isCasting = false;
    }

    void castTeleport()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the Teleport layer but not the NoTeleport layer...
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, NoTeleportMask))
        {
            return;
        }

        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, TeleportMask))
        {
            psTeleport.Emit(200);
            transform.position = new Vector3(floorHit.point.x, floorHit.point.y - 0.5f, floorHit.point.z);
            psTeleport.Emit(200);
            teleportCooldown = true;

            StartCoroutine(coolDown(0, 3f));
        }
        
    }

    void castSpectralHand()
    {
        object castedSpectralHand = Instantiate(spectralHand, castLocation.position, transform.rotation);
        pushCooldown = true;
        StartCoroutine(coolDown(1, 3f));
    }

    void castFireBall()
    {
        object castedFireBall = Instantiate(fireBall, castLocation.position, Quaternion.identity);
        fireCooldown = true;
        StartCoroutine(coolDown(2, 5f));
    }

    void castEarthSpike()
    {
        object castedEarthSpike = Instantiate(earthSpike);
        earthCooldown = true;
        StartCoroutine(coolDown(3, 7f));
    }

    void castWaterJet()
    {
        object castedWaterJet = Instantiate(waterJet, castLocation.position, transform.rotation, transform);
        waterCooldown = true;
        StartCoroutine(coolDown(4, 7f));
    }

    void castWindBurst()
    {
        object castedWindBurst = Instantiate(windBurst, transform.position, Quaternion.identity);
        windCooldown = true;
        StartCoroutine(coolDown(5, 6f));
    }

    void castHealLight()
    {
        object castedHealLight = Instantiate(healLight, transform.position, Quaternion.identity);
        healCooldown = true;
        StartCoroutine(coolDown(6, 15f));
    }

    // La foncton pour le cooldown des abilites et pour l'afficher sur l'UI
    private IEnumerator coolDown(int spellNumber, float time)
    {
        GameObject spellIcon = GameObject.Find("");
        if (spellNumber == 0)
            spellIcon = GameObject.Find("TeleportIcon");
        
        else if (spellNumber == 1)
            spellIcon = GameObject.Find("SpectralHandIcon");
        
        else if (spellNumber == 2)
            spellIcon = GameObject.Find("FireBallIcon");
        
        else if (spellNumber == 3)
            spellIcon = GameObject.Find("EarthSpikeIcon");
        
        else if (spellNumber == 4)
            spellIcon = GameObject.Find("WaterJetIcon");

        else if (spellNumber == 5)
           spellIcon = GameObject.Find("WindBurstIcon");
        
        else if (spellNumber == 6)
            spellIcon = GameObject.Find("HealLightIcon");

        SpellCooldownUI cooldownScript = spellIcon.GetComponent<SpellCooldownUI>();
        cooldownScript.UseSpell();

        yield return new WaitForSeconds(time);
        if (spellNumber == 0)
            teleportCooldown = false;
        else if (spellNumber == 1)
            pushCooldown = false;
        else if (spellNumber == 2)
            fireCooldown = false;
        else if (spellNumber == 3)
            earthCooldown = false;
        else if (spellNumber == 4)
            waterCooldown = false;
        else if (spellNumber == 5)
            windCooldown = false;
        else if (spellNumber == 6)
            healCooldown = false;
        
    }
}
