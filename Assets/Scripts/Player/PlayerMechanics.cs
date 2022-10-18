using Controllers;
using UnityEngine;

namespace Player
{
    public class PlayerMechanics : MonoBehaviour
    {
        private ControllerManager _cm;
        private AudioSource _audioSource;
        private bool _canMeow;

        [Header("Mechanics")] public float meowCooldown = 2.0f;
        public float meowVolume = 0.5f;

        [Header("Keybindings")] public KeyCode meowKey = KeyCode.C;

        [Header("Audio")] public AudioClip[] meowSounds;

        private void Start()
        {
            _cm = FindObjectOfType<ControllerManager>();
            _audioSource = GetComponent<AudioSource>();

            if (_cm.GameController.debugMode) Debug.Log("Mechanics initialised.");

            _canMeow = true;
        }

        private void Update()
        {
            // Here we listen for the players keypresses to handle mechanics
            InputMechanics();
        }

        private void InputMechanics()
        {
            if (Input.GetKey(meowKey) && _canMeow)
            {
                if (_cm.GameController.debugMode) Debug.Log("Meow");

                _canMeow = false;
                Meow();

                Invoke(nameof(ResetMeow), meowCooldown);
            }
        }

        private void Meow()
        {
            // Play a random meow sound
            _audioSource.PlayOneShot(meowSounds[Random.Range(0, meowSounds.Length)], meowVolume);

            // Make it affect the AI somehow?
        }

        private void ResetMeow()
        {
            _canMeow = true;
        }
    }
}