using UnityEngine;
using System.Collections;

/// <summary>
/// This script handles the spaceships functionality, including movement and shooting. The spaceship needs any Collider2D attached that has the trigger flag checked, so it can be hit
/// by the asteroids and die.
/// </summary>
public class SpaceShip : MonoBehaviour {

	[Tooltip("How fast is the spaceship moving left to right")]
    public float speed;
	[Tooltip("How fast is the spaceship shooting")]
    public float rateOfFire;
    [Tooltip("Prefab to be instantiated when shooting (Projectile)")]
    public GameObject projectilePrefab;
    [Tooltip("Prefab to be instantiated when dying (explosioin)")]
    public GameObject explosionPrefab;
    [Tooltip("An Audioclip that is played when the ship shoots")]
    public AudioClip laserSound;

    private float lastTimeFired = 0;

    /// <summary>
    /// This is called by Unity every frame. It handles the ships movement and checks if it should fire
    /// </summary>
    void Update() {
        if(GameManager.State != GameManager.GameState.Playing){ // if we are not playing, do nothing
            return;
        }

        // move the ship left and right, depending on the horizontal input
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // if the fire button is pressed and we waited long enough since the last shot was fired, FIRE!
        if (Input.GetButton("Fire") && (lastTimeFired + 1 / rateOfFire) < Time.time) {
            lastTimeFired = Time.time;
            FireTheLasers();
        } 
    }

    /// <summary>
    /// This is called by Unity when the object collides with another object. It is only called, when both objects have any 2D collider attached, at least one of them is a trigger and at least of of
    /// the two colliding GameObjects has a Rigidbody2D attached.
    /// </summary>
    void OnTriggerEnter2D(Collider2D other){

        // if the other object has the asteroid tag, the destroy the ship and lose the game
        if(other.CompareTag("Asteroid")){
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager.LoseTheGame();
        }
    }

    void FireTheLasers(){
        AudioSource.PlayClipAtPoint(laserSound, transform.position);
        Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
    }
}
