    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public bool canTripleShot = false;
    public bool canSpeedPowerUp = false;
    public bool shieldAtive = false;

    [SerializeField]
    private float _speed;
   
   public int _life;

    [SerializeField]
    private GameObject _laserprefab;

    [SerializeField]
    private GameObject _tripleshotprefab;

    [SerializeField]
    private GameObject _playerExplosion;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private float _FireRate = 0.25f;
    private float CanFire = 0.0f;

    private UIManager _UIManager;
    private GameManager _gameManager;
    private Spawn_Manager _spawnManager;
    private AudioSource _audiosource;

    [SerializeField]
    private GameObject[] _engines;

    private int hitCount = 0;


    // Use this for initialization
    void Start () {
        _speed = 11;
        _life = 3;
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
       
        if (_UIManager != null)
        {
            _UIManager.UpdateLives(_life);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();

         if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }
        _audiosource = GetComponent<AudioSource>();
        hitCount = 0;
    }
	
	// Update is called once per frame
	void Update () {

        Movemment();

        // Se apertar o espaço ablita o Laser da nave

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
	}

    private void Shoot()
    {
        // Se o tripleshot tiver ativo
        //lança 3 lasers
        //se não
        //lança 1

        if (Time.time > CanFire)
        {
            _audiosource.Play();
            if (canTripleShot)
            {
                TripleShot();
            }
            else
            {
                Instantiate(_laserprefab, transform.position + new Vector3(0, 0.56f, 0), Quaternion.identity);
            }
            CanFire = Time.time + _FireRate;
        }
    }

    private void TripleShot()
    {
        Instantiate(_tripleshotprefab, transform.position + new Vector3(0, 0.56f, 0), Quaternion.identity);
    }

    private void Movemment()
    {
        //  Controla Moviemto do Player usando as teclas

        float horizontalIput = Input.GetAxis("Horizontal");
        float verticalIput = Input.GetAxis("Vertical");

        if (canSpeedPowerUp)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * Time.deltaTime * horizontalIput);
            transform.Translate(Vector3.up * _speed * 1.5f * Time.deltaTime * verticalIput);

        }
        else
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime * horizontalIput);
            transform.Translate(Vector3.up * _speed * Time.deltaTime * verticalIput);
        }

        //Limita a Movimentação do Player

        if (transform.position.x > 8)
        {
            transform.position = new Vector3(8, transform.position.y, 0);
        }
        else if (transform.position.x < -8)
        {
            transform.position = new Vector3(-8, transform.position.y, 0);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);
        }
    }

    public void Damage()
    {
        // Se o escudo está ativo, não tira vida
        // Se não
        //Tira 1 vida e caso a vida for < 1 , destroi o player

        if (shieldAtive)
        {
            shieldAtive = false;
            _shield.SetActive(false);
            return;
        }

        hitCount++;
        if (hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            _engines[1].SetActive(true);
        }

            _life--;
            _UIManager.UpdateLives(_life);

            if(_life < 1)
            {
                Instantiate(_playerExplosion, transform.position, Quaternion.identity);
                _gameManager.gameOver = true;
                _UIManager.ShowTitleScreen();
                Destroy(this.gameObject);
            }
    }

    public void EnableShields()
    {
        shieldAtive = true;
        _shield.SetActive(true);
    }

    public void SpeedPowerUpOn()
    {
        canSpeedPowerUp = true;
        StartCoroutine(SpeedPowerUpRoutine());
    }

    public IEnumerator SpeedPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedPowerUp = false;
    }

    public void TripleShotPowerOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShot_PowerDownRoutine());
    }   

    public IEnumerator TripleShot_PowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;  
    }
}
