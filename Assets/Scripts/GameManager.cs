using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This is a managing instance that handles basic game control, the score and some basic UI.
/// GameManager is a Singleton to give static access to some functionality that is actually attached to a GameObject.
/// Place the GameManager script on an empty GameObject anywhere in the scene.
/// </summary>
public class GameManager : MonoBehaviour {

    /// <summary>
    /// A simple enumeration of our possible game states.
    /// </summary>
    public enum GameState{
        Playing, Ended
    }

    /// <summary>
    /// The Singletons instance, this references the actual script on the GameObject.
    /// </summary>
    private static GameManager _instance;

    [Tooltip("How many points to we need (how many asteroids to shoot) for winning")]
    public int pointsToWin;
    [Tooltip("A UI Text for the score to display")]
    public Text pointsText;
    [Tooltip("Some UI graphics to display when we win")]
    public GameObject winGameGraphics;
    [Tooltip("Some UI graphics to display when we lose")]
    public GameObject loseGameGraphics;

    /// <summary>
    /// The games current state, mainly used by other instances to check if the game is paused.
    /// </summary>
    public static GameState State = GameState.Playing;

    private int points = 0;

    /// <summary>
    /// Awake is called by Unity. This is used here to initialize the Singleton
    /// </summary>
	void Awake(){
		if(_instance != null){
			Destroy(_instance);
		}
		_instance = this;
	}

    /// <summary>
    /// Start is called by Unity. This disables the win and lose graphics, so they dont interfere with the game until we actually win or lose.
    /// </summary>
    void Start(){
        
		if(winGameGraphics != null){
			winGameGraphics.SetActive(false);
		}
		if(loseGameGraphics != null){
			loseGameGraphics.SetActive(false);
		}
    }

    /// <summary>
    /// Adds points to the games global score. Since this is static it can be called from anywhere (Singletons for the win).
    /// </summary>
    /// <param name="points">Points.</param>
	public static void AddPoints(int points){
        _instance.points += points;
		if(_instance.pointsText != null){
			_instance.pointsText.text = _instance.points.ToString();	
		}
        if(_instance.points >= _instance.pointsToWin){
            WinTheGame();
        }
    }

    /// <summary>
    /// Call this when the game is won and displays the win graphics.
    /// </summary>
    public static void WinTheGame(){
        State = GameState.Ended;
        if(_instance.winGameGraphics != null){
			_instance.winGameGraphics.SetActive(true);
		}
    }

    /// <summary>
    /// Call this when the game is lost and displays the lose graphics.
    /// </summary>
    public static void LoseTheGame(){
        State = GameState.Ended;
        if(_instance.loseGameGraphics != null){
			_instance.loseGameGraphics.SetActive(true);
		}
    }
}
