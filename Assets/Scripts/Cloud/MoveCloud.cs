using UnityEngine;

namespace Cloud
{
	public class MoveCloud : MonoBehaviour
	{

		private RectTransform _rectTransform;
		
		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		private void FixedUpdate()
		{

			Vector2 speed = new Vector2(-0.5f, 0);
			
			_rectTransform.Translate(speed * Time.deltaTime);
		}
	}
}
