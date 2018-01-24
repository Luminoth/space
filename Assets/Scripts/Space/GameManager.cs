using EnergonSoftware.Core;
using EnergonSoftware.Core.Camera;
using EnergonSoftware.Core.Input;
using EnergonSoftware.Core.UI;
using EnergonSoftware.Core.Util;

using UnityEngine;
using UnityEngine.EventSystems;

namespace EnergonSoftware.Space
{
    public sealed class GameManager : SingletonBehavior<GameManager>
    {
        [SerializeField]
        private Player _playerPrefab;

        private Player _player;

        public Camera MainCamera => _player.Camera;

        private FollowCamera _followCamera;

        public Ship PlayerShip { get; private set; }

        public void SetPlayerShip(Ship playerShip)
        {
            PlayerShip = playerShip;
            _followCamera.SetTarget(PlayerShip.gameObject);
        }

#region Unity Lifecycle
        private void Awake()
        {
            InputManager.Instance.PointerDownEvent += PointerDownEventHandler;

            _player = Instantiate(_playerPrefab);
            _followCamera = _player.GetComponent<FollowCamera>();
            UIManager.Instance.UICamera = _player.Camera;
        }

        protected override void OnDestroy()
        {
            if(InputManager.HasInstance) {
                InputManager.Instance.PointerDownEvent -= PointerDownEventHandler;
            }
        }
#endregion

#region Event Handlers
        private void PointerDownEventHandler(object sender, InputManager.PointerEventArgs args)
        {
            if(!Config.UseVR && PointerEventData.InputButton.Right != args.Button) {
                return;
            }

            if(!InputManager.Instance.IsPointerOverGameObject()) {
                Core.UI.ContextMenu.Create(UIManager.Instance.ContextMenuPrefab,
                    contextMenu =>
                    {
                        contextMenu.AddItem("Free Look", () => {
                            Debug.Log("UI: Free look enabled");
                            _followCamera.SetTarget(null);
                        });
                        contextMenu.AddItem("Follow My Ship", () => {
                            Debug.Log("UI: Following ship");
                            _followCamera.SetTarget(PlayerShip.gameObject);
                        });

                        contextMenu.MoveTo(UIManager.Instance.GetUISpawnPosition());
                    }
                );
            }
        }
#endregion
    }
}
