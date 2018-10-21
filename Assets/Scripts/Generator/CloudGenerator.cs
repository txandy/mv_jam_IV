using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class CloudGenerator : MonoBehaviour
    {
        [Range(0, 20f)]
        public float spawnTime = 1.5f;
        public List<RectTransform> spawnPoints;
        public List<Image> clouds;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(SpawnCloud());
        }

        private IEnumerator SpawnCloud()
        {
            Image cloud = clouds[Random.Range(0, clouds.Count)];
            RectTransform spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];

            GameObject tmp = Instantiate(cloud.gameObject, spawn.transform);

            tmp.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            yield return new WaitForSeconds(Random.Range(spawnTime, spawnTime + 5));

            StartCoroutine(SpawnCloud());
        }

        private void OnDisable()
        {
            StopCoroutine(SpawnCloud());
        }
    }
}