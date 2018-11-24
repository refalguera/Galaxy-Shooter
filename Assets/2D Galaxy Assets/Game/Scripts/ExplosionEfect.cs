using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEfect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject,4f);
	}

}
