using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    private GameObject[] powerUps;

    private GameManager _gameManager;

	// Use this for initialization
	void Start () {

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUPsRoutine());
    }
	
    IEnumerator EnemySpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

    }

    IEnumerator PowerUPsRoutine()
    {
        while (!_gameManager.gameOver)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerUp], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

    }
}
