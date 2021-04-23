using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterBoi : MonoBehaviour
{
    // La vie du personnage
    public int health = 1000;

    // La vitesse du personnage
    public float movementSpeed;

    // La direction du mouvement
    private Vector3 moveDirection;

    //Le rigidbody
    private Rigidbody rb;

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
    private bool pushCooldown = false;
    private bool healCooldown = false;
    private bool earthCooldown = false;
    private bool fireCooldown = false;
    private bool waterCooldown = false;
    private bool windCooldown = false;

    // Le point de depart des sortileges
    public Transform castLocation;

    // Le layermask a ignorer
    public LayerMask mousePointMask;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCharacterBoiAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       

        // Vérifier les inputs du joueur
        

        // Les touches pour les sorts
        if (Input.GetMouseButtonDown(0))
        {
            playerCharacterBoiAnimator.SetTrigger("Basic Spell");
        }
        if (Input.GetMouseButtonDown(1) && !pushCooldown)
        {
            playerCharacterBoiAnimator.SetTrigger("Push Spell");
            castSpectralHand();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && !fireCooldown)
        {
            playerCharacterBoiAnimator.SetTrigger("Fire Spell");
            castFireBall();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !earthCooldown)
        {
            playerCharacterBoiAnimator.SetTrigger("Earth Spell");
            castEarthSpike();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !waterCooldown)
        {
            playerCharacterBoiAnimator.SetTrigger("Water Spell");
            castWaterJet();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !windCooldown)
        {
            playerCharacterBoiAnimator.SetTrigger("Wind Spell");
            castWindBurst();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && !healCooldown)
        {
            playerCharacterBoiAnimator.SetTrigger("Heal Spell");
            castHealLight();
        }
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Déplacer le personnage selon le vecteur de direction
        rb.MovePosition(rb.position + moveDirection.normalized * movementSpeed * Time.fixedDeltaTime);

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
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, mousePointMask))
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

    public void takeDamage(int damageNumber)
    {
        health -= damageNumber;
        Debug.Log(health);
        if (health <= 0)
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
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

    void castSpectralHand()
    {
        object castedSpectralHand = Instantiate(spectralHand, castLocation.position, transform.rotation);
    }

    void castFireBall()
    {
        object castedFireBall = Instantiate(fireBall, castLocation.position, Quaternion.identity);
    }

    void castEarthSpike()
    {
        object castedEarthSpike = Instantiate(earthSpike);
    }

    void castWaterJet()
    {
        object castedWaterJet = Instantiate(waterJet, castLocation.position, transform.rotation, transform);
    }

    void castWindBurst()
    {
        object castedWindBurst = Instantiate(windBurst, transform.position, Quaternion.identity);
    }

    void castHealLight()
    {
        object castedHealLight = Instantiate(healLight, transform.position, Quaternion.identity);
    }
}
