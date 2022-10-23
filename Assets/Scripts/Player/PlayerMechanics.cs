using AI;
using Controllers;
using UnityEngine;

namespace Player
{
    public class PlayerMechanics : MonoBehaviour
    {
        private ControllerManager _cm;
        private AudioSource _audioSource;
        private bool _canMeow;

        [Header("Mechanics")]
        public float meowCooldown = 2.0f;
        public float meowVolume = 0.5f;
        public float meowHearingDistance = 100f;
        public LayerMask enemyLayerMask;

        [Header("Keybindings")]
        public KeyCode meowKey = KeyCode.C;

        [Header("Audio")]
        public AudioClip[] meowSounds;

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
            _cm.Achievements.Meow();
            _cm.TaskController.CheckTasksOfType(TaskController.TaskType.Meow);

            // Cast a sphere to inform any nearby AI of the meow
            var hits = Physics.SphereCastAll(transform.position, meowHearingDistance,
                Vector3.forward, 500f, layerMask: enemyLayerMask);

            var numGuardsHit = 0;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    numGuardsHit++;
                    hit.collider.gameObject.GetComponent<AIMovement>().HearMeow(transform.position);
                    _cm.Achievements.HearMeMeow();
                }
            }

            _cm.Achievements.CheckGuardsAtOnce(numGuardsHit);
            _cm.TaskController.CheckCompletion();
        }

        private void ResetMeow()
        {
            _canMeow = true;
        }
    }
}