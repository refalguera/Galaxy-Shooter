using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed;

	// Use this for initialization
	void Start () {
        _speed = 4;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Destroi o laser quando sair fora da tela do jogo
         if(transform.position.y >= 6)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
	}
}
