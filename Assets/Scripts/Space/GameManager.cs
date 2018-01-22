using EnergonSoftware.Core.Camera;
using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Space
{
    public sealed class GameManager : SingletonBehavior<GameManager>
    {
        [SerializeField]
        private Camera _mainCamera;

        public Camera MainCamera => _mainCamera;

        [SerializeField]
        private Ship _playerShip;

        public Ship PlayerShip => _playerShip;

        public void SetPlayerShip(Ship playerShip)
        {
            _playerShip = playerShip;
            _mainCamera?.GetComponent<FollowCamera>()?.SetTarget(_playerShip?.gameObject);
        }
    }
}
