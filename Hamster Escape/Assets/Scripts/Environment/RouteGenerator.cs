using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Environment
{
    public class RouteGenerator : MonoBehaviour
    {
        public GameObject[] routePrefabs; 
        public Transform player;
        public float despawnDistance = 30f;
        public int loadRoutesCount = 3;

        private float _routeWidth;
        private Queue<GameObject> _spawnedRoutes;
        private int _currentPrefabIndex;
        private float _nextSpawnPosition;

        private void Start()
        {
            _currentPrefabIndex = Random.Range(0, routePrefabs.Length);
            _spawnedRoutes = new Queue<GameObject>(2);
            _nextSpawnPosition = transform.position.x;
            for (var i = 0; i < loadRoutesCount; i++) SpawnRoute();
        }

        private void Update()
        {
            if (_spawnedRoutes.First() is not null && player.position.x >= _spawnedRoutes.First().transform.position.x + despawnDistance)
            {
                SpawnRoute();
                Destroy(_spawnedRoutes.First().gameObject);
                _spawnedRoutes.Dequeue();
            }
        }

        private void SpawnRoute()
        {
            _routeWidth = GetRouteWidth(routePrefabs[_currentPrefabIndex]);
            _currentPrefabIndex = (_currentPrefabIndex + 1) % routePrefabs.Length;
            var newRoute = Instantiate(
                routePrefabs[_currentPrefabIndex],
                new Vector3(
                    _nextSpawnPosition,
                    transform.position.y,
                    0),
                Quaternion.identity
            );

            _spawnedRoutes.Enqueue(newRoute);

            _nextSpawnPosition += GetRouteWidth(newRoute);
        }

        private static float GetRouteWidth(GameObject route)
        {
            var routeCollider = route.GetComponent<SpriteRenderer>();
        
            if (routeCollider is null)
            {
                Debug.LogWarning("BoxCollider2D component not found on the new route prefab.");
                return 0;
            }

            return routeCollider.size.x;
        }
    }
}
