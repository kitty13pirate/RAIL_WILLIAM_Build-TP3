using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldownUI : MonoBehaviour
{
    // L'image et le text de l'icone
    public Image imageCooldown;
    public Text textCooldown;

    // Les variables pour le cooldown
    private bool isCooldown = false;
    public float cooldownTime;
    private float cooldownTimer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown)
        {
            applyCooldown();
        }
    }

    private void applyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public bool UseSpell()
    {
        // en cas d'erreur
        if (isCooldown)
        {
            return false;
        }
        // Le cooldown est demarrer
        else
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            cooldownTimer = cooldownTime;
            return true;
        }
    }
}
