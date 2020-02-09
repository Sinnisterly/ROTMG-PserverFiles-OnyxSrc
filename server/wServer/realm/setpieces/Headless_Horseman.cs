using wServer.realm.worlds;

namespace wServer.realm.setpieces
{
    class Headless_Horseman : ISetPiece
    {
        public int Size { get { return 5; } }

        public void RenderSetPiece(World world, IntPoint pos)
        {
            Entity HMN = Entity.Resolve(world.Manager, "Arena Headless Horseman");
            HMN.Move(pos.X + 2.5f, pos.Y + 2.5f);
            world.EnterWorld(HMN);
        }
    }
}