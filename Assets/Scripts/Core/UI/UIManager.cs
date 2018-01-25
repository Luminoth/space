using EnergonSoftware.Core.Assets;
using EnergonSoftware.Core.Input;
using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core.UI
{
    public sealed class UIManager : SingletonBehavior<UIManager>
    {
// TODO: move the context menu junk out of here
        [SerializeField]
        private string _contextMenuPrefabPath = "Prefabs/UI/ContextMenu.prefab";

        public GameObject ContextMenuPrefab { get; private set; }

        [SerializeField]
        private GameObject _overlayUIContainerPrefab;

        public UnityEngine.Camera UICamera { get; set; }

        private GameObject _overlayUIContainer;

        public GameObject OverlayUIContainer => _overlayUIContainer;

        [SerializeField]
        private float _uiSpawnDistance = 10.0f;

        public float UISpawnDistance => _uiSpawnDistance;

#region Unity Lifecycle
        private void Awake()
        {
            _overlayUIContainer = Instantiate(_overlayUIContainerPrefab, PlayerManager.Instance.Player.transform);

            ContextMenuPrefab = AssetManager.Instance.LoadPrefab(_contextMenuPrefabPath);
        }

        protected override void OnDestroy()
        {
            Destroy(_overlayUIContainer);
            _overlayUIContainer = null;

            base.OnDestroy();
        }
#endregion

        public GameObject InstantiateUIChild(GameObject prefab, Transform parent, bool worldPositionStays)
        {
            Vector3 scale = prefab.transform.localScale;
            GameObject obj = Instantiate(prefab, parent, worldPositionStays);
            obj.transform.localScale = scale;
            return obj;
        }

        public Vector3 GetUIPointerSpawnPosition()
        {
            return InputManager.Instance.GetPointerSpawnPosition(UISpawnDistance);
        }
    }
}
