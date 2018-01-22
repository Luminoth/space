using EnergonSoftware.Core.UI;

using UnityEngine;
using UnityEngine.UI;

namespace EnergonSoftware.Space
{
    public sealed class ShipHUD : Window<ShipHUD>
    {
        [SerializeField]
        private Text _velocityText;

#region Unity Lifecycle
        private void Update()
        {
            _velocityText.text = $"Velocity: {GameManager.Instance.PlayerShip.Velocity}";
        }
#endregion
    }
}
