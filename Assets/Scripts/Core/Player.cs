using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core
{
    public sealed class Player : MonoBehavior
    {
        [SerializeField]
        private UnityEngine.Camera _camera;

        public UnityEngine.Camera Camera => _camera;

        [SerializeField]
        private GameObject _gvrController;

#region Unity Lifecycle
        private void Awake()
        {
            _gvrController.SetActive(Config.UseVR);
        }
#endregion
    }
}
