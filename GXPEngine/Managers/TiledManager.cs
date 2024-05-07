using GXPEngine.Utility;
using System;


namespace GXPEngine.Managers
{
    class TiledManager
    {
        string _tiledMapPath;
        string currentMap;

        public TiledManager()
        {
        }

        public static void LoadTiledMap( string currentMap)
        {
            TiledMap tiledMap = new TiledMap(currentMap);
        }
    }

}
