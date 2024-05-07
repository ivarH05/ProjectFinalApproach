using GXPEngine.Utility;
using System;
using TiledMapParser;

namespace GXPEngine.Managers
{
    public class TiledManager
    {
        string _tiledMapPath = "assets/tiledmaps/";
        string currentMap;
        TiledLoader _tiledLoader;

        public TiledManager( GameObject TiledRootObject )
        {
            tiledRootObject = TiledRootObject;
        }

        public GameObject tiledRootObject { get; set; }

        public void LoadTiledMap( string mapToLoad )
        {
            currentMap = mapToLoad;
            _tiledLoader = new TiledLoader( _tiledMapPath+mapToLoad );
            _tiledLoader.rootObject = tiledRootObject;
            _tiledLoader.addColliders = false;
            _tiledLoader.autoInstance = true;
            _tiledLoader.LoadImageLayers(0);
        }
    }
}
