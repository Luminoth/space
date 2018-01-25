using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.UI;

namespace EnergonSoftware.Core.VR
{
    public sealed class GraphicRaycasterSelector : MonoBehavior
    {
        [SerializeField]
        private GraphicRaycaster _graphicRaycaster;

        [SerializeField]
        private GvrPointerGraphicRaycaster _gvrGraphicRaycaster;

#region Unity Lifecycle
        private void Awake()
        {
            _graphicRaycaster.enabled = !PlayerManager.Instance.Player.EnableVR;
            _gvrGraphicRaycaster.enabled = PlayerManager.Instance.Player.EnableVR;
        }
#endregion
    }
}
