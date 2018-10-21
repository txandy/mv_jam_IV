using System;
using UnityEngine;

namespace Wall
{
    public class DeleteWall : MonoBehaviour
    {
       
        public static event Action WallDeleted = delegate { };
        public static event Action EnemyDeleted = delegate { };
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Wall"))
            {
                Destroy(other.gameObject);

                WallDeleted();
            }

            if (other.tag.Equals("Enemy"))
            {
                Destroy(other.gameObject);
                
                EnemyDeleted();
            }
        }
    }
}