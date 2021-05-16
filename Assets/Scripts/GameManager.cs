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

    // L'UI
    public Text waveCounter;
    public Text gameOverText;
    public Button gameOverButton;

    // Les prefabs des enemies
    public GameObject skeleton;
    public GameObject bigSkeleton;
    public GameObject wolfRider;
    public GameObject rockGolem;

    // Les extremites de la table ou chaque enemie apparait aleatoirement
    private Vector3 spawnZone;
    private Vector3 spawnZone2;

    // L'audioManager
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Awake()
    {
        // Assignement des variables
        if (singleton != null)
            return;
        singleton = this;
        spawnZone = new Vector3(-45f, 0f, 0f);
        spawnZone2 = new Vector3(45f, 0f, 0f);
        gameOverButton.onClick.AddListener(gameOverButton_onClick);
        audioManager = FindObjectOfType<AudioManager>();

        // Debut de la premiere vague
        StartCoroutine(newWave());

#if !UNITY_EDITOR && UNITY_WEBGL
        UnityEngine.WebGLInput.captureAllKeyboardInput = false;
#endif
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
            // 3 squelettes normaux par vagues
            for (int j = 0; j < 3; j++)
            {
                if (Random.Range(0, 2) == 1)
                    Instantiate(skeleton, spawnZone, Quaternion.identity);
                else
                    Instantiate(skeleton, spawnZone2, Quaternion.identity);
                enemyNumber += 1;
            }

            // 1 Orc Monte par 2 vagues
            if (i % 2 == 0)
            {
                if (Random.Range(0, 2) == 1)
                    Instantiate(wolfRider, spawnZone, Quaternion.identity);
                else
                    Instantiate(wolfRider, spawnZone2, Quaternion.identity);
                enemyNumber += 1;
            }

            // 1 squelette geant par 3 vagues
            if (i % 3 == 0)
            {
                if (Random.Range(0, 2) == 1)
                    Instantiate(bigSkeleton, spawnZone, Quaternion.identity);
                else
                    Instantiate(bigSkeleton, spawnZone2, Quaternion.identity);
                // +4 puisque le gros squelette fait apparaitre a sa mort 3 petits de plus
                enemyNumber += 4;
            }

            // 1 golem de roche par 4 vagues
            if (i % 4 == 0)
            {
                if (Random.Range(0, 2) == 1)
                    Instantiate(rockGolem, spawnZone, Quaternion.identity);
                else
                    Instantiate(rockGolem, spawnZone2, Quaternion.identity);
                enemyNumber += 1;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    // La fonction lorsque le joueur meurt
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
        SceneManager.LoadScene("Accueil");
    }
}
