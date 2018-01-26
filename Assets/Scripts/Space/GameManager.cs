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
        private ShipHUD _shipHUDPrefab;

        public Camera MainCamera => PlayerManager.Instance.Player.Camera;

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
            InputManager.Instance.PointerUpEvent += PointerUpEventHandler;

// TODO: just expose a method in the player manager rather than getting the component
            _followCamera = PlayerManager.Instance.Player.GetComponent<FollowCamera>();
            UIManager.Instance.UICamera = MainCamera;
        }
        private void Start() 
        { 
            ShipHUD.CreateOverlay(_shipHUDPrefab.gameObject,
                shipHUD => 
                { 
                    shipHUD.transform.position = new Vector3(shipHUD.transform.position.x, shipHUD.transform.position.y, UIManager.Instance.UISpawnDistance); 
                }); 
        } 

        protected override void OnDestroy()
        {
            if(InputManager.HasInstance) {
                InputManager.Instance.PointerUpEvent -= PointerUpEventHandler;
            }
        }
#endregion

#region Event Handlers
        private void PointerUpEventHandler(object sender, InputManager.PointerEventArgs args)
        {
            if(!PlayerManager.Instance.Player.EnableVR && PointerEventData.InputButton.Right != args.Button) {
                return;
            }

            if(!InputManager.Instance.IsPointerOverGameObject()) {
                Core.UI.ContextMenu.CreateOverlay(UIManager.Instance.ContextMenuPrefab,
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

                        contextMenu.MoveTo(UIManager.Instance.GetUIPointerSpawnPosition());
                    }
                );
            }
        }
#endregion
    }
}
