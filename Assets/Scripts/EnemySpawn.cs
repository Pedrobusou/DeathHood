using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawn : NetworkBehaviour {
    [SerializeField] private GameObject objectToSpawn;

    [Tooltip("Time (s) between enemy spawns")]
    [SerializeField] private float timeBetweenSpawns = 30f;
    [SerializeField] private float timeUntilNextSpawn;

    private Transform spawnPoint;
    [SerializeField] private List<Transform> enemySpawns;

    private void Start() {
        getEnemySpawns();
        timeUntilNextSpawn = timeBetweenSpawns;
    }

    // Update is called once per frame
    private void Update() {
        if (!isLocalPlayer) { return; }
        spawnEnemy();
    }

    /// <summary>
    /// Spawns an enemy each given time
    /// </summary>
    private void getEnemySpawns() {
        foreach (GameObject enemySpawn in GameObject.FindGameObjectsWithTag("EnemySpawn")) {
            enemySpawns.Add(enemySpawn.transform);
        }
    }

    /// <summary>
    /// Spawns an enemy each given time
    /// </summary>
    private void spawnEnemy() {
        timeUntilNextSpawn -= 1 * Time.deltaTime;

        if (timeUntilNextSpawn <= 0) {
            timeUntilNextSpawn = timeBetweenSpawns;
            print("Spawn points lenght: " + enemySpawns.Count);

            //Get Random spawnpoint;
            spawnPoint = enemySpawns[Random.Range(0, enemySpawns.Count)];

            CmdSpawn();
        }
    }

    [Command]
    private void CmdSpawn() {
        GameObject go = Instantiate(objectToSpawn, spawnPoint);
        NetworkServer.Spawn(go);
    }
}