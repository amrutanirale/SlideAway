using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Player : MonoBehaviour
{
	public float speed;
	public GameObject menu;
	//public GameObject retry;
	public Animator pickUpAnim;
	public GameObject ps;
	public Text scoreText;
	public Text lastScoreText;
	public Text bestScoreText;
	public GameObject tapToPlayText, gamename;

	private bool isDead;
	private Vector3 dir;
	private int score = 0;
	private float playerYPos;

	//private const int counterReset = 3;
	//public static int counterForAds = counterReset;

	/*void Awake ()
	{
		if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize ("1", false);
		}
	}*/

	void Start ()
	{
		tapToPlayText.SetActive (true);
		gamename.SetActive (true);
		menu.SetActive (false);
		isDead = false;
		dir = Vector3.zero;
		playerYPos = transform.position.y;
		/*counterForAds--;

		if (counterForAds <= 0) {
			resetCounter ();
			Advertisement.Show (null, new ShowOptions {			
				pause = true,
				resultCallback = result => {

				}
			});
		}*/
	}

	/*public static void resetCounter ()
	{
		counterForAds = counterReset;
	}
*/
	void Update ()
	{
		if (Input.GetMouseButtonDown (0) && !isDead) {
			score++;
			scoreText.text = score.ToString ();
			gamename.SetActive (false);
			tapToPlayText.SetActive (false);
			if (dir == Vector3.forward) {
				dir = Vector3.left;
			} else {
				dir = Vector3.forward;
			}
		}
		float amountToMove = speed * Time.deltaTime;
		transform.Translate (dir * amountToMove);
	}

	void FixedUpdate ()
	{
		if (transform.position.y < (playerYPos - 0.2f) && !isDead) {
			transform.GetChild (0).transform.parent = null;
			isDead = true;
			GameEventManager.playerDieCounter++;
			print (GameEventManager.playerDieCounter + " " + PlayerPrefs.GetInt ("BestScore"));
			if (GameEventManager.playerDieCounter >= 3 && PlayerPrefs.GetInt ("BestScore") > 25) {
				UnityAdsExample.m_instance.ShowAd ();
				GameEventManager.playerDieCounter = 0;
			}
			GameOver ();
			StartCoroutine (WaitToRestart ());
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Pickup") {		   
			other.gameObject.SetActive (false);
			Instantiate (ps, transform.position, Quaternion.identity);
			score += 2;
			scoreText.text = score.ToString ();
			pickUpAnim.SetTrigger ("goup");
		}
	}

	IEnumerator WaitToRestart ()
	{
		yield return new WaitForSeconds (1);
		menu.SetActive (true);
		//retry.SetActive (true);
	}

	/*void OnTriggerExit (Collider other)
	{
		if (other.tag == "Tile") {
			RaycastHit hit;
			Ray downRay = new Ray (transform.position, -Vector3.up);

			if (!Physics.Raycast (downRay, out hit)) {
				//Kill Player
				isDead = true;
				resetBtn.SetActive (true);
				if (transform.childCount > 0) {
					transform.GetChild (0).transform.parent = null;
				}
			}
		}
	}*/

	private void GameOver ()
	{
		lastScoreText.text = score.ToString ();
		int bestScore = PlayerPrefs.GetInt ("BestScore", 0);

		if (score > bestScore) {
			PlayerPrefs.SetInt ("BestScore", score);
		}
		bestScoreText.text = PlayerPrefs.GetInt ("BestScore", 0).ToString ();
	}

}
