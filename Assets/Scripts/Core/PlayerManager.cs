﻿using EnergonSoftware.Core.Util;

namespace EnergonSoftware.Core
{
    public sealed class PlayerManager : SingletonBehavior<PlayerManager>
    {
        public Player Player { get; set; }

#region Unity Lifecycle
        private void Start()
        {
            Player.EnableFollowCam(true);
        }
#endregion
    }
}
