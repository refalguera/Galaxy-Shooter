using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _enemyExplsion;

    private UIManager _UIManager;
    [SerializeField]
    private AudioClip _audioclip;

    // Use this for initialization
    void Start () {

        _speed = 4;
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        SpawEnemy();
    }
	
	// Update is called once per frame
	void Update () {
 
        Movemment();

         if(transform.position.y < -7)
        {
            SpawEnemy();
        }
	}

    private void SpawEnemy()
    {
        transform.position = new Vector3(Random.Range(-7f, 7f), 7, 0);
    }

    private void Movemment()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            if (collision.transform.parent != null)
            {
                Destroy(collision.transform.parent.gameObject);
            }

            Destroy(collision.gameObject);
            Instantiate(_enemyExplsion, transform.position, Quaternion.identity);
            _UIManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_audioclip, Camera.main.transform.position,1f);
            Destroy(this.gameObject);
        }
        else if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
             if(player != null)
            {
                player.Damage();
            }

            Instantiate(_enemyExplsion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioclip, Camera.main.transform.position,1f);
            Destroy(this.gameObject);
        }
    }
}
