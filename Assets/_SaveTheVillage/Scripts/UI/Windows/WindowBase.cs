using UnityEngine;
using UnityEngine.UI;

namespace _SaveTheVillage.Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button CloseButton;
        
        private void Awake() =>
            OnAwake();

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy() =>
            Cleanup();

        protected virtual void OnAwake() => 
            CloseButton.onClick.AddListener(Close);

        private void Close()
        {
            OnClose();
            Destroy(gameObject);
        }
        
        protected virtual void Initialize() { }
        protected virtual void OnClose() { }
        protected virtual void Cleanup() { }
    }
}