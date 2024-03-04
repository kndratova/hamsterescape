using UnityEngine;

namespace Player
{
    public class DistanceCounter : MonoBehaviour
    {
        public static float Distance { get; private set; }

        [SerializeField] private Transform playerTransform;

        private float _initialPlayerPositionX;

        private void Start()
        {
            if (playerTransform is null)
            {
                Debug.LogError("Player Transform is not assigned");
                return;
            }
            _initialPlayerPositionX = playerTransform.position.x;
        }

        private void LateUpdate()
        {
            Distance = CountDistance();
        }

        private float CountDistance()
        {
            return playerTransform is null ? 0 : playerTransform.position.x - _initialPlayerPositionX;
        }
    }
}
