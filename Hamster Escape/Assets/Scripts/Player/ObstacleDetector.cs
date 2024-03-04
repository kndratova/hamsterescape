using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class ObstacleDetector : MonoBehaviour
    {
        public UnityEvent onObstacleHit;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject.layer);
            if (other.gameObject.layer == 6)
            {
                onObstacleHit.Invoke();
                Debug.LogWarning("Obstacle");
            }
        }
    }
}
