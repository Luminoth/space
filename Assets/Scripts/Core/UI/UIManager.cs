using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core.UI
{
    public sealed class UIManager : SingletonBehavior<UIManager>
    {
        [SerializeField]
        [ReadOnly]
        private GameObject _uiContainer;

        public GameObject UIContainer => _uiContainer;

#region Unity Lifecycle
        private void Awake()
        {
            _uiContainer = new GameObject("UI");
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
