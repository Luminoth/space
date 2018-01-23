using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core.Camera
{
    public class TestCam : MonoBehavior
    {
        public float followDistance = 25.0f;

        public GameObject target;

        private void LateUpdate()
        {
            transform.position = target.transform.position - (target.transform.forward * followDistance);
        }
    }
}
