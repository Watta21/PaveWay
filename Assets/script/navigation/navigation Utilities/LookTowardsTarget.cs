using UnityEngine;

namespace Immersal.Samples.Navigation
{
    public class LookTowardsTarget : MonoBehaviour
    {
        [SerializeField]
        private Transform m_TransformToRotate = null;

        public void LookAt(Vector3 target, Vector3 up)
        {
            if (m_TransformToRotate != null)
            {
                Vector3 pos = transform.position;
                Vector3 direction = (target - pos).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction, up);

                m_TransformToRotate.rotation = rotation;
            }
        }
    }
}