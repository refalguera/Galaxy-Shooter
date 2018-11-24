using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour {

    [SerializeField]
    private float _speed;

    [SerializeField]
    private int powerUpID;

    [SerializeField]
    private AudioClip _clip;

	// Use this for initialization
	void Start () {
        _speed = 2;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
         if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Acessar o Player
        //Ativar o Triple Shot
        //Se destruir
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (player != null)
            {
                //Ativa TripleShot

                 if(powerUpID == 0)
                {
                    player.TripleShotPowerOn();
                }
               
                //Ativa TripleShot
                else if(powerUpID == 1)
                {
                    player.SpeedPowerUpOn();
                }
                 else if(powerUpID == 2)
                {
                    //Ativar Escudos
                    player.EnableShields();
                }
            }

            Destroy(this.gameObject);
        }   
         
    }
}
