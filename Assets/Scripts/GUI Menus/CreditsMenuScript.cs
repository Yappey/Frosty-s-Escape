using UnityEngine;
using System.Collections;

public class CreditsMenuScript : MonoBehaviour {

	public float speed = 10.0f;
	public GameObject scroll;
	private float yPosBegin;
	private float yPosEnd;
	private float progress = 0.0f;

	public float Progress
	{
		get {return progress;}
		set 
		{
				progress = value;

			if (progress > 1.0f)
				progress = 1.0f;
			if (progress < 0.0f)
				progress = 0.0f;

			transform.localPosition = Vector3.Lerp(new Vector3(transform.localPosition.x, yPosBegin), new Vector3(transform.localPosition.x, yPosEnd), progress);
		}
	}

	// Use this for initialization
	void Start () {
		yPosBegin = transform.localPosition.y;
		yPosEnd = yPosBegin + GetComponent<RectTransform>().rect.height * transform.localScale.y - transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
	}
	
	// Update is called once per frame
	void Update () {
		scroll.GetComponent<UnityEngine.UI.Scrollbar>().value = Progress + Time.deltaTime * speed / (yPosEnd - yPosBegin);
	}


}
