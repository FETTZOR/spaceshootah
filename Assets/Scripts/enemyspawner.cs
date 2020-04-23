using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startWave = 0;
    [SerializeField] bool looping = false;
     IEnumerator Start()
    {
        do
        {
            ////var currentWave = waveConfigs[startWave];
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }    
    }    
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberofEnemies(); enemyCount++)
        {
          var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), 
                waveConfig.GetWayPoints()[0].transform.position, 
                Quaternion.identity);
            newEnemy.GetComponent<enemypathh>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
