using EnergonSoftware.Core.Assets;
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
        private UnityEngine.Camera _uiCamera;

        public UnityEngine.Camera UICamera => _uiCamera;

        [SerializeField]
        [ReadOnly]
        private GameObject _uiContainer;

        public GameObject UIContainer => _uiContainer;

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
    }
}
