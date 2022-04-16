using System;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private float m_rotationAngleX;
        [SerializeField]
        private int m_distance;
        [SerializeField]
        private float m_offsetY;
        [SerializeField]
        private Transform m_following;

        private void LateUpdate()
        {
            if(m_following == null)
                return;
            Quaternion rotation = Quaternion.Euler(m_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -m_distance) + FollowingPointPosition();
            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following)
        {
            m_following = following.transform;
        }
        
        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = m_following.position;
            followingPosition.y += m_offsetY;
            return followingPosition;
        }
    }
}