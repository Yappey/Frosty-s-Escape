using UnityEngine;
using System.Collections;

public class BaseActivator : MonoBehaviour {

	//[SerializeField]
	public BaseReceiver[] receivers;
	//[SerializeField]
	public int state = 0;


	public virtual void Activate()
	{

	}
}
