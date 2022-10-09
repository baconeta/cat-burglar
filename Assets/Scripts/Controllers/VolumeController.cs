using UnityEngine;

namespace Controllers
{
    public class VolumeController : MonoBehaviour
    {
        public void OnValueChanged(float sliderValue)
        {
            AudioListener.volume = sliderValue;
        }
    }
}