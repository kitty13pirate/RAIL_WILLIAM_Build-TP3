using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private int Wave = 1;
    public int enemyNumber;
    public GameObject skeleton;
    public GameObject bigSkeleton;
    private Vector3 spawnZone;

    // Start is called before the first frame update
    void Awake()
    {
        if (singleton != null)
            return;
        singleton = this;
        spawnZone = new Vector3(-1f, 0f, -20f);
        newWave();
    }

    // Verifie le nombre d'enemie encore vivant
    public void deadEnemy()
    {
        enemyNumber -= 1;
        if (enemyNumber <= 0)
        {
            Wave += 1;
            newWave();
        }
    }

    // Commence une nouvelle vague d'enemies
    public void newWave()
    {
        for (int i = 0; i < Wave; i++)
        {
            // 3 squelettes normaux par vagues
            for (int j = 0; j< 3; j++)
            {
                Instantiate(skeleton, spawnZone, Quaternion.identity);
                enemyNumber += 1;
            }
            // 1 squelette geant a partir de la vague 3
            if (i >= 2)
            {
                Instantiate(bigSkeleton, spawnZone, Quaternion.identity);
                // 5 puisque le gros squelette fait apparaitre a sa mort 5 petits
                enemyNumber += 5;
            }
        }
    }

    public void GameOver()
    {
        // Empecher les mouvements du joueur
        PlayerCharacterBoi player = FindObjectOfType<PlayerCharacterBoi>();
        player.enabled = false;

        // Empecher les mouvements du NPC

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.enabled = false;
            //enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        }
        // Message de fin de jeu
        Debug.Log($"Fin du jeu a la vague {Wave}");
    }

}
