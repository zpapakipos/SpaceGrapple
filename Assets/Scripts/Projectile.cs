using UnityEngine;
using System.Collections;

/// <summary>
/// Projectile handles the movement of the fired lasers. The script needs any 2D Collider (set to trigger) and a Rigidbody2D (set to kinematic) on the same GameObject.
/// The script moves the GameObject constantly upwards using speed. After the lifeTime the projectile is
/// </summary>
public class Projectile : MonoBehaviour {

    [Tooltip("How fast is the projectile moving upwards")]
    public float speed;
    [Tooltip("After how many seconds is the projectile destroyed")]
    public float lifeTime;

	bool stuck = false;
	public GameObject spaceShip;
	public LineRenderer ropeLineRendererPrefab;
	public LineRenderer ropeLineRenderer;
	public Vector3 dir;

    /// <summary>
    /// This is called by Unity. It starts the coroutine that destroyes the projectile after the lifetime.
    /// </summary>
    void Start(){
		dir = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position);
		dir.z = 0;
		dir.Normalize ();
        StartCoroutine(KillAfterSeconds(lifeTime));
		ropeLineRenderer = Instantiate(ropeLineRendererPrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Update is called by Unity each frame. This moves the GameObject upwards at speed.
    /// </summary>
    void Update () {
        transform.position += dir * speed * Time.deltaTime;
		checkWrap();

        if (Input.GetButtonUp("Fire"))
        {
            Destroy(gameObject);
        }

		updateRope();
    }

	void updateRope(){
		ropeLineRenderer.SetPosition (0, spaceShip.transform.position);
		ropeLineRenderer.SetPosition (1, transform.position);
	}

	void checkWrap(){
		if (Camera.main.WorldToScreenPoint(transform.position).y > Screen.height) {
			var curPos = transform.position;
			curPos.y = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).y;
			transform.position = curPos;
		} else if (Camera.main.WorldToScreenPoint(transform.position).y < 0) {
			var curPos = transform.position;
			curPos.y = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,0)).y;
			transform.position = curPos;
		}
		if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width) {
			var curPos = transform.position;
			curPos.x = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x;
			transform.position = curPos;
		} else if (Camera.main.WorldToScreenPoint(transform.position).x < 0) {
			var curPos = transform.position;
			curPos.x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x;
			transform.position = curPos;
		}
	}

    /// <summary>
    /// This is called by Unity when the GameObject where this script is on collides with another GameObject. 
    /// Both objects need to have 2D colliders attached, at least one of them needs to have the collider set to trigger and at least one of them needs to have a Rigidbody2D attached!!
    /// On collision the projectile is destroyed and if it hits an object with the tag "Asteroid" the asteroid will be notified it got hit.
    /// </summary>
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Hook"))
        { // This checks if we hit an asteroid. The asteroid needs the "Asteroid" tag for this to work!!
          //Asteroid asteroid = other.GetComponent<Asteroid>(); // Grab the asteroid script from the hit GameObject
          //asteroid.OnHit(); // notify the asteroid it got hit
          //Destroy(gameObject); // Destory this projectile
            stuck = true;
            dir = new Vector3(0, 0, 0);
        }
   //     if(other.CompareTag("Asteroid"))
   //     { // This checks if we hit an asteroid. The asteroid needs the "Asteroid" tag for this to work!!
   //         Asteroid asteroid = other.GetComponent<Asteroid>(); // Grab the asteroid script from the hit GameObject
   //         asteroid.OnHit(); // notify the asteroid it got hit
   //         Destroy(gameObject); // Destory this projectile
			//Destroy(ropeLineRenderer);
   //     }
    }

    /// <summary>
    /// Destroys the projectile after seconds. This is a coroutine that needs be started using StartCoroutine().
    /// </summary>
    IEnumerator KillAfterSeconds(float seconds){
        yield return new WaitForSeconds(seconds);
		if (!stuck) {
			Destroy (gameObject);
		}
        Destroy(gameObject);
		Destroy(ropeLineRenderer);
    }
}
