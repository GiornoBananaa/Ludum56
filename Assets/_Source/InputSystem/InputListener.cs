using UnityEngine;
using VContainer;

namespace InputSystem
{
    public class InputListener : MonoBehaviour
    {
        private GameInputActions _gameInput;

        [Inject]
        public void Construct()
        {
        
        }
    
        private void Awake()
        {
            _gameInput = new GameInputActions();
            _gameInput.Enable();
            EnableInput();
        }

        private void Update()
        {
        
        }
    
    
        private void OnDestroy()
        {
            DisableInput();
        }

        public void EnableInput()
        {
        
            _gameInput.Enable();
        }
    
        public void DisableInput()
        {
        
            _gameInput.Disable();
        }
    }
}