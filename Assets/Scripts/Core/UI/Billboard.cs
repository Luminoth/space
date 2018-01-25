using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core.UI
{
    [RequireComponent(typeof(IWindow))]
    public sealed class Billboard : MonoBehavior
    {
#region Unity Lifecycle
        private void LateUpdate()
        {
// TODO: billboard should "stay" in space but appear to be in teh same UI place...
            transform.forward = transform.position - UIManager.Instance.UICamera.transform.position;
        }
#endregion
    }
}
