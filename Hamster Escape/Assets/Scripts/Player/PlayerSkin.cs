using UnityEngine;

namespace Player
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private MovementController movementController;

        private void Update()
        {
            transform.rotation = GetRotationFromSurfaceNormal(movementController.SurfaceNormal);
        }

        private Quaternion GetRotationFromSurfaceNormal(Vector3 surfaceNormal)
        {
            return Quaternion.FromToRotation(Vector3.up, surfaceNormal);
        }
    }
}
