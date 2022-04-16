using System;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField]
        private CharacterController m_characterController;
        [SerializeField]
        private float m_movementSpeed = 4.0f;
        
        private IInputService m_inputService;
        private Camera m_camera;

        private void Awake()
        {
            m_inputService = Game.InputService;
        }

        private void Start()
        {
            m_camera = Camera.main;
            CameraFollow();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (m_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = m_camera.transform.TransformDirection(m_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            m_characterController.Move(movementVector * (m_movementSpeed * Time.deltaTime));
        }

        private void CameraFollow() => m_camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}