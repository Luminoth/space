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
            if(null != GameManager.Instance.PlayerShip) {
                _velocityText.text = $"Velocity: {(int)GameManager.Instance.PlayerShip.Velocity} m/s";
            }
        }
#endregion
    }
}
