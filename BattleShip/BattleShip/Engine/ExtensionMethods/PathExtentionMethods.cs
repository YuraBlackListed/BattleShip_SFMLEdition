using SFML.Graphics;
using System.IO;

namespace BattleShip.Engine.ExtensionMethods.PathExtentionMethods
{
    public static class PathExtentionMethods
    {
        private static string thisDirectoryPath = GetCurrentDirectory();
        public static string GetCurrentDirectory()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
            return path;
        }
    }
}
