using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9f;
	public int enemyCount;
    public int waveNumber =1;
    public GameObject powerupPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		SpawnEnemyWave(waveNumber);
        SpawnPowerup();
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
		if (enemyCount ==0){
			waveNumber++;
			SpawnEnemyWave(waveNumber);
            SpawnPowerup();
		}
    }

    Vector3 GenerateSpawnPoint()
    {

        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
	
	void SpawnEnemyWave(int enemiesToSpawn){
		
		for (int spawnedEnemies=0; spawnedEnemies < enemiesToSpawn; spawnedEnemies++){
			Instantiate(enemyPrefab, GenerateSpawnPoint(), enemyPrefab.transform.rotation);
		}
	}
	
    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPoint(), powerupPrefab.transform.rotation);
    }
}
