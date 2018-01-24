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

        public UnityEngine.Camera UICamera { get; set; }

        private GameObject _uiContainer;

        public GameObject UIContainer => _uiContainer;

        [SerializeField]
        private float _uiSpawnDistance = 10.0f;

        public float UISpawnDistance => _uiSpawnDistance;

#region Unity Lifecycle
        private void Awake()
        {
            _uiContainer = new GameObject("UI");

            ContextMenuPrefab = AssetManager.Instance.LoadPrefab(_contextMenuPrefabPath);
        }

        protected override void OnDestroy()
        {
            Destroy(UIContainer);
            _uiContainer = null;

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

        public Vector3 GetUISpawnPosition()
        {
            return InputManager.Instance.GetPointerSpawnPosition(UISpawnDistance);
        }
    }
}
