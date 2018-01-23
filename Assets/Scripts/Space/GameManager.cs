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
        private Camera _mainCamera;

        public Camera MainCamera => _mainCamera;

        [SerializeField]
        private FollowCamera _followCamera;

        [SerializeField]
        [ReadOnly]
        private Ship _playerShip;

        public Ship PlayerShip => _playerShip;

        public void SetPlayerShip(Ship playerShip)
        {
            _playerShip = playerShip;
            _followCamera.SetTarget(PlayerShip.gameObject);
        }

#region Unity Lifecycle
        private void Awake()
        {
            InputManager.Instance.PointerDownEvent += PointerDownEventHandler;
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
            if(PointerEventData.InputButton.Right != args.Button) {
                return;
            }

            if(!Physics.Raycast(MainCamera.ScreenPointToRay(args.PointerPosition))) {
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

                        contextMenu.MoveTo(args.PointerPosition);
                    }
                );
            }
        }
#endregion
    }
}
