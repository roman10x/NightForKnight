using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMove : MonoBehaviour
    {
        [SerializeField]
        private float m_movementSpeed = 4.0f;
        private CharacterController m_characterController;
        private HeroAnimator m_heroAnimator;

        private IInputService m_inputService;
       
        private void Awake()
        {
            m_inputService = AllServices.Container.Single<IInputService>();
            m_characterController = GetComponent<CharacterController>();
            m_heroAnimator = GetComponent<HeroAnimator>();
        }
        
        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (m_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(m_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            m_characterController.Move(movementVector * (m_movementSpeed * Time.deltaTime));
        }
    }
}