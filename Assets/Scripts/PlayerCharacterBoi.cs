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
        // Fair regarder au personnage la position de la souris

        // Vérifier les inputs du joueur
        // Vertical (W, S et Joystick avant/arrière)
        inputVertical = Input.GetAxis("Vertical");
        // Horizontal (A, D et Joystick gauche/droite)
        inputHorizontal = Input.GetAxis("Horizontal");

        //Les variables pour les animations
        playerCharacterBoiAnimator.SetFloat("Horizontal", inputHorizontal);
        playerCharacterBoiAnimator.SetFloat("Vertical", inputVertical);

        // Vecteur de mouvements (Avant/arrière + Gauche/Droite)
        moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;

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
        // Déplacer le personnage selon le vecteur de direction
        rb.MovePosition(rb.position + moveDirection.normalized * movementSpeed * Time.fixedDeltaTime);
    }
}
