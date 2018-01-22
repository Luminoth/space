using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Space
{
    public sealed class GameManager : SingletonBehavior<GameManager>
    {
        [SerializeField]
        private Ship _playerShip;

        public Ship PlayerShip { get { return _playerShip; } set { _playerShip = value; } }
    }
}
