using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core.Camera
{
    public class TestCube : MonoBehavior
    {
        private void FixedUpdate()
        {
            transform.position += transform.forward * (5 * Time.fixedDeltaTime);
        }
    }
}
