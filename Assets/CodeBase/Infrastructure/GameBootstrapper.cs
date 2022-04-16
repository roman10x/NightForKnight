using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game m_game;

        private void Awake()
        {
            m_game = new Game();

            DontDestroyOnLoad(this);
        }
    }
}