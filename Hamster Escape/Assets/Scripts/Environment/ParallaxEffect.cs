using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float effectValue;

        private float _initialPositionX, _routeWidth;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            _initialPositionX = _transform.position.x;
            _routeWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            var distance = cameraTransform.position.x * (1 - effectValue);
            var offset = cameraTransform.position.x * effectValue;
            var newPosition = _transform.position;
            newPosition.x = _initialPositionX + offset;
            _transform.position = newPosition;
            if (distance > _initialPositionX + _routeWidth) _initialPositionX += _routeWidth;
        }
    }
}