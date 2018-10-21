using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpdateLifeBar : MonoBehaviour
    {
        public Image bar;

        private void Awake()
        {
            HorseController.HorseChangeLife += HorseControllerOnHorseChangeLife;
        }

        private void OnDestroy()
        {
            HorseController.HorseChangeLife -= HorseControllerOnHorseChangeLife;
        }

        private void HorseControllerOnHorseChangeLife(int value)
        {
            UpdateImageValue(value);
        }

        private void UpdateImageValue(int value)
        {
            bar.fillAmount = (float) value * 100 / HorseController.MaxLife / 100f;
        }
    }
}