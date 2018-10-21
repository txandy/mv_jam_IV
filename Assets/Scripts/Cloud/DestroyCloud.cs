using UnityEngine;

namespace Cloud
{
	public class DestroyCloud : MonoBehaviour {
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.tag.Equals("Cloud"))
			{
				Destroy(other.gameObject);
			}
		}
	}
}
