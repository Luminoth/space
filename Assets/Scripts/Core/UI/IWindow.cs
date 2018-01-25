using UnityEngine;

namespace EnergonSoftware.Core.UI
{
    public interface IWindow
    {
        void MoveTo(Vector3 position);

        void Close();
    }
}
