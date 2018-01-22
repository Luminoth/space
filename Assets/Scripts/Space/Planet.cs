using EnergonSoftware.Core.UI;
using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Space
{
    [RequireComponent(typeof(ContextObject))]
    public class Planet : MonoBehavior
    {
        [SerializeField]
        private Collider _collider;

#region Unity Lifecycle
        private void Awake()
        {
            ContextObject context = GetComponent<ContextObject>();
            context.AddItem("Approach", () => {
                Debug.Log("Approach Planet!");
                GameManager.Instance.PlayerShip.Approach(transform);
            });
            context.AddSeparator();
            context.AddItem("Show Info", () => {
                Debug.Log("Show Planet Info!");
            });
        }
#endregion
    }
}
