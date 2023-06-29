using SFML.Graphics;

namespace BattleShip.Engine.ExtensionMethods.AnimationExtensionMethods
{
    public static class AnimationExtensionMethods
    {
        public static Sprite SwapFrame(this Sprite sprite, Animation animation)
        {
            sprite.Texture = animation.frames[animation.curretFrame];
            return sprite;
        }
    }
}
