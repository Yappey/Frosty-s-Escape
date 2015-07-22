using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	public ArrayList inventory;//GameObject[] inventory;

	private int len = 0;
	private int Len
	{
		get { return len;}
		set 
		{
			len = value;
			Debug.Log("Inventory Length: " + len);
		}
	}


	// Use this for initialization
	void Start () {
		inventory = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
		if (Len != inventory.Count)
		{
			Len = inventory.Count;
		}
	}
}
