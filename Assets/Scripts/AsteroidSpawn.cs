using UnityEngine;
using System.Collections;

/// <summary>
/// Asteroid spawn is used to spawn the asteroids from the top of the screen. Place this script on an empty GameObject on top of the screen.
/// </summary>
public class AsteroidSpawn : MonoBehaviour {

    [Tooltip("A prefab that is instantiated when the asteroid is destroyed")]
    public float spawnWidth;
    [Tooltip("How many asteroids spawn per second?")]
    public float spawnRate;
    [Tooltip("The prefab that is to be instantiated as asteroids")]
    public GameObject AsteroidPrefab;

    private float lastSpawnTime = 0;

    /// <summary>
    /// Update is called by Unity. This will spawn asteroids while the game is in play mode.
    /// </summary>
    void Update() {
        if(GameManager.State != GameManager.GameState.Playing){
            return;
        }

        // this is a simple timer structure that executes every 1/spawnRate seconds. This means it spawns spawnRate asteroids every second.
        if (lastSpawnTime + 1 / spawnRate < Time.time) {
            lastSpawnTime = Time.time;
            Vector3 spawnPosition = transform.position;
			spawnPosition += new Vector3(0, Random.Range(-spawnWidth, spawnWidth), 0);
            Instantiate(AsteroidPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
