using UnityEngine;
using System.Collections;

/// <summary>
/// This script handles the explosion animation on the explosion GameObject. 
/// It requires a SpriteRenderer, as the SpriteRenderer is used in the script to cycle through the sprites of the animation.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class Explosion : MonoBehaviour
{

    [Tooltip("The individual sprites of the animation")]
    public Sprite[] frames;
    [Tooltip("How fast does the animation play")]
    public float framesPerSecond;
    [Tooltip("An Audioclip with the sound that is played when the explosion happens")]
    public AudioClip explosionSound;
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// Start is called by Unity. This will play our explosion sound and start the sprite animation
    /// </summary>
    void Start()
    {
        if (explosionSound != null) {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnimation());
    }

    /// <summary>
    /// This is a coroutine that cycles through the sprites of our explosion animation. It needs to be started using StartCoroutine().
    /// </summary>
    IEnumerator PlayAnimation()
    {
        int currentFrameIndex = 0;
        while (currentFrameIndex < frames.Length) {
            spriteRenderer.sprite = frames [currentFrameIndex];
            yield return new WaitForSeconds(1f / framesPerSecond); // this halts the functions execution for x seconds. Can only be used in coroutines.
            currentFrameIndex++;
        }
        Destroy(gameObject);
    }
}
