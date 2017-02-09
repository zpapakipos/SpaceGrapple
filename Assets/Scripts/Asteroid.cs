using UnityEngine;
using System.Collections;

/// <summary>
/// This script needs to be attached to the games asteroid objects. A asteroid also needs the asteroid sprite, any 2D collider (set to trigger) and a Rigidbody2D (set to kinematic) attached to its GameObject.
/// </summary>
public class Asteroid : MonoBehaviour {

    [Tooltip("How fast does the asteroid move downwards? in units per second")]
	public float speed = 5;
    [Tooltip("How long does the asteroid life before it is automatically destroyed, in seconds")]
    public float lifeTime = 5;
    [Tooltip("How fast does the asteroid rotate, in degrees")]
    public float rotationSpeed = 60;
    [Tooltip("A prefab that is instantiated when the asteroid is destroyed")]
    public GameObject explosionPrefab;
    [Tooltip("A prefab that is instantiated when the asteroid is destroyed")]
    public int score = 1;

    /// <summary>
    /// Start this instance. Get Called by Unity when this GameObject enters the scene
    /// </summary>
    void Start(){
        // Start the KillAfterSeconds coroutine immediately, so the asteroid is destroyed after lifetime seconds pass.
        StartCoroutine(KillAfterSeconds(lifeTime));
    }

    /// <summary>
    /// Moves the asteroid downwards using speed and rotates it using rotationSpeed. Update is called each frame by Unity
    /// </summary>
	void Update () {
	    transform.position += Vector3.left * speed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward);
	}

    /// <summary>
    /// Destroys the asteroid after seconds. This is a coroutine that needs be started using StartCoroutine().
    /// </summary>
    IEnumerator KillAfterSeconds(float seconds){
        yield return new WaitForSeconds(seconds); // this halts the functions execution for x seconds. Can only be used in coroutines.
        Destroy(gameObject);
    }

    /// <summary>
    /// Is called by another instance when it collides with the asteroid
    /// </summary>
    public void OnHit(){
        GameManager.AddPoints(score);   
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
