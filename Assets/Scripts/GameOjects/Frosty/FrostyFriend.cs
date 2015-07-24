using UnityEngine;
using System.Collections;

public class FrostyFriend : MonoBehaviour {

	public float soundTrigger = 8;
	public int frostyFriendIndex;
	public Color notFoundColor = Color.white;
	public Color foundColor = Color.green;
	private bool hasBeenFound;
	private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		if (soundTrigger < 8.0f) 
		{
			soundTrigger = 8.0f;
		}

		hasBeenFound = (PlayerPrefs.GetInt("FrostyFriendIsFound" + frostyFriendIndex, 0) != 0);
		if (hasBeenFound)
		{
			GetComponent<SpriteRenderer>().color = foundColor;
		}
		else
		{
			GetComponent<SpriteRenderer>().color = notFoundColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= soundTrigger) 
		{
			GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
			sound.GetComponent<SoundEffectManager>().PlayHelpSnd(gameObject.transform.position);

			timer = 0.0f;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.tag == "Frosty") 
		{
			/*if (coll.GetComponent<Frostyehavior> ().headAttached && coll.GetComponent<Frostyehavior> ().torsoAttached &&
			    coll.GetComponent<Frostyehavior> ().baseAttached) 
			{*/
			soundTrigger = 123456789.0f;

			GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
			sound.GetComponent<SoundEffectManager>().StopHelpSnd();
			sound.GetComponent<SoundEffectManager>().PlayThanksSnd();

			PlayerPrefs.SetInt("FrostyFriendIsFound" + frostyFriendIndex, 1);
			hasBeenFound = true;
			GetComponent<SpriteRenderer>().color = foundColor;
			transform.position = new Vector3 (-9.0f, 10.0f);
			GetComponent<BoxCollider2D>().isTrigger = false;
			GetComponent<Rigidbody2D>().isKinematic = false;
			/*}*/
		}
	}
}
