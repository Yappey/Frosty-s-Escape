using UnityEngine;
using System.Collections;

public class RadialCooldown : MonoBehaviour {

    public GameObject cooldown;
    public GameObject frosty;
    bool set = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (set)
        {
            frosty = GameObject.FindGameObjectWithTag("Frosty");
            if (cooldown.activeSelf)
            {
                cooldown.transform.localScale = new Vector3(1 - frosty.GetComponent<Frostyehavior>().timer / frosty.GetComponent<Frostyehavior>().SnowballCooldown, 1 - frosty.GetComponent<Frostyehavior>().timer / frosty.GetComponent<Frostyehavior>().SnowballCooldown, 1 - frosty.GetComponent<Frostyehavior>().timer / frosty.GetComponent<Frostyehavior>().SnowballCooldown);
            }
            if (cooldown.transform.localScale.x <= 0.0f)
            {
                cooldown.SetActive(false);
                cooldown.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                set = false;
            } 
        }
	}

    public void Cooldown()
    {
        cooldown.SetActive(true);
        set = true;
    }
}
