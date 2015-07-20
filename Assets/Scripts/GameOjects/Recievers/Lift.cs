using UnityEngine;
using System.Collections;

public class Lift : BaseReceiver {

	public Transform top, bottom, platforms;
	public LiftToggler toggler;

	public float speed = 1.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (toggler.state == 0)
		{
			float height = top.position.y - bottom.position.y;


			platforms.position += Vector3.up * speed * Time.deltaTime * ((state == 0) ? 1 : -1);

			if (platforms.position.y > top.position.y)
			{
				platforms.position -= Vector3.up * height;
				for (int i = 0; i < platforms.childCount; i++)
				{
					platforms.GetChild(i).position += Vector3.up * height;
				}
			}
			else if (platforms.position.y < bottom.position.y)
			{
				platforms.position += Vector3.up * height;
				for (int i = 0; i < platforms.childCount; i++)
				{
					platforms.GetChild(i).position -= Vector3.up * height;
				}
			}

			for (int i = 0; i < platforms.childCount; i++)
			{
				if (platforms.GetChild(i).position.y > top.position.y)
					platforms.GetChild(i).position -= Vector3.up * height;
				else if (platforms.GetChild(i).position.y < bottom.position.y)
					platforms.GetChild(i).position += Vector3.up * height;
			}
		}
	}

	public override void Process()
	{
		if (state == 0)
			state = 1;
		else
			state = 0;
	}
}
