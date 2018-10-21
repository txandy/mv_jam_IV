using UnityEngine;
using Wall;
namespace Generator
{
    public class WallGenerator : MonoBehaviour
    {
        public GameObject wallPrefab;
        public Transform spawnPosition;

        private int n_walls = 18;

        private Vector2 lastPosition;

        private void Awake()
        {
            DeleteWall.WallDeleted += DeleteWallOnWallDeleted;
        }

        private void DeleteWallOnWallDeleted()
        {
            SpawnNew(lastPosition + new Vector2(0.8f,0));
        }

        private void OnDestroy()
        {
            DeleteWall.WallDeleted -= DeleteWallOnWallDeleted;
        }

        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < n_walls; i++)
            {
                Vector2 newSpawnPosition =
                    new Vector2(i * 0.8f, 0);

                SpawnNew(newSpawnPosition);
            }
        }

        private void SpawnNew(Vector2 newSpawnPosition)
        {
            GameObject tmp = Instantiate(wallPrefab, spawnPosition);
            tmp.transform.localPosition = newSpawnPosition;

            lastPosition = newSpawnPosition;
        }

        private void FixedUpdate()
        {
            spawnPosition.transform.Translate(Vector2.left * Time.deltaTime);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}