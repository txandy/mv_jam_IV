using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Generator
{
    public class EnemyGenerator : MonoBehaviour
    {
        public List<GameObject> enemyPrefabs;


        private void Awake()
        {
            EnemyRun.EnemyResearchRespawn += EnemyRunOnEnemyResearchRespawn;
        }

        private void OnDestroy()
        {
            EnemyRun.EnemyResearchRespawn -= EnemyRunOnEnemyResearchRespawn;
        }

        private void EnemyRunOnEnemyResearchRespawn()
        {
            StartCoroutine(DelaySpawn());
        }

        void Start()
        {
            Spawn();
        }

        IEnumerator DelaySpawn()
        {
            float secons = Random.Range(0f, 1f);
            yield return new WaitForSeconds(secons);

            Spawn();
        }

        public void Spawn()
        {
            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            GameObject tmp = Instantiate(randomEnemy, transform);
        }
    }
}