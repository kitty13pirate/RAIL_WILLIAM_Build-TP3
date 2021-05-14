using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private int Wave = 1;
    public int enemyNumber;
    public Text waveCounter;
    public Text gameOverText;
    public Button gameOverButton;
    public GameObject skeleton;
    public GameObject bigSkeleton;
    public GameObject wolfRider;
    public GameObject rockGolem;
    private Vector3 spawnZone;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (singleton != null)
            return;
        singleton = this;
        spawnZone = new Vector3(-1f, 0f, -20f);
        gameOverButton.onClick.AddListener(gameOverButton_onClick);
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(newWave());
    }

    // Verifie le nombre d'enemie encore vivant
    public void deadEnemy()
    {
        enemyNumber -= 1;
        if (enemyNumber <= 0)
        {
            Wave += 1;
            StartCoroutine(newWave());
        }
    }

    // Commence une nouvelle vague d'enemies
    public IEnumerator newWave()
    {
        waveCounter.text = "Vague : " + Wave;
        for (int i = 1; i < Wave + 1; i++)
        {
            // 2 squelettes normaux par vagues
            for (int j = 0; j < 2; j++)
            {
                Instantiate(skeleton, spawnZone, Quaternion.identity);
                enemyNumber += 1;
            }

            // 1 Orc Monte par 2 vagues
            if (i % 2 == 0)
            {
                Instantiate(wolfRider, spawnZone, Quaternion.identity);
                enemyNumber += 1;
            }

            // 1 squelette geant par 3 vagues
            if (i % 3 == 0)
            {
                Instantiate(bigSkeleton, spawnZone, Quaternion.identity);
                // +4 puisque le gros squelette fait apparaitre a sa mort 3 petits de plus
                enemyNumber += 4;
            }

            // 1 golem de roche par 4 vagues
            if (i % 4 == 0)
            {
                Instantiate(rockGolem, spawnZone, Quaternion.identity);
                enemyNumber += 1;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public void GameOver()
    {
        audioManager.GameOverSound();
        // Empecher les mouvements du joueur
        PlayerCharacterBoi player = FindObjectOfType<PlayerCharacterBoi>();
        player.enabled = false;

        // Empecher les mouvements du NPC

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            Enemy scriptEnemy = enemy.GetComponent<Enemy>();
            scriptEnemy.win();
        }
        // Message de fin de jeu
        gameOverText.text = "Vous avez survecu jusqu'a la vague " + Wave;
        gameOverText.gameObject.SetActive(true);
        gameOverButton.gameObject.SetActive(true);
    }

    void gameOverButton_onClick()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
