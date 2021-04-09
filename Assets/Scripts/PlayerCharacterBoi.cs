using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterBoi : MonoBehaviour
{
    // La vitesse du personnage
    public float movementSpeed;

    // Pour verifier les inputs
    private float inputVertical;
    private float inputHorizontal;

    // La direction du mouvement
    private Vector3 moveDirection;

    //Le rigidbody
    private Rigidbody rb;

    // L'animator
    private Animator playerCharacterBoiAnimator;

    // Pour verifier si le personnage utilise un sort
    private bool isCasting;

    // Pour verifier le cooldown des habilites
    private bool pushCooldown = false;
    private bool healCooldown = false;
    private bool earthCooldown = false;
    private bool fireCooldown = false;
    private bool waterCooldown = false;
    private bool windCooldown = false;

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
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerCharacterBoiAnimator.SetTrigger("Fire Spell");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerCharacterBoiAnimator.SetTrigger("Earth Spell");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerCharacterBoiAnimator.SetTrigger("Water Spell");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerCharacterBoiAnimator.SetTrigger("Wind Spell");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerCharacterBoiAnimator.SetTrigger("Heal Spell");
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

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit))
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
}
