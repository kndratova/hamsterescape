using UnityEngine;

namespace Player
{
    [ExecuteInEditMode]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;

            _transform.position = targetTransform.position;
        }

        private void LateUpdate()
        {
            Follow(targetTransform.position);
        }
        
        private void Follow(Vector3 point)
        {
            var newPosition = _transform.position;
            newPosition.x = point.x;
            
            _transform.position = newPosition;
        }
    }
}
