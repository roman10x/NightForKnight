using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets m_assets;

        public GameFactory(IAssets assets)
        {
            m_assets = assets;
        }
        
        public GameObject CreateHero(GameObject at)
        {
            var spawnPoint = at.transform.position;
            var hero = m_assets.Instantiate(AssetPath.HeroPrefab, spawnPoint: spawnPoint);
            return hero;
        }
        
        public void CreateHud()
        {
            m_assets.Instantiate(AssetPath.HudPrefab);
        }
    }
}
