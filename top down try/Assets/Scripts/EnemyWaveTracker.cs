using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTracker : MonoBehaviour
{
    // put this on your enemy prefabs. You could just copy the on destroy onto a pre-existing script if you want.
    void OnDestroy()
    {
        if (GameObject.FindWithTag("WaveSpawner") != null)
        {
            GameObject.FindWithTag("WaveSpawner").GetComponent<Spawner>().spawnedEnemies.Remove(gameObject);
        }

    }
}