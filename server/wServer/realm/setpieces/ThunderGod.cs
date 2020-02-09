using wServer.realm.worlds;

namespace wServer.realm.setpieces
{
    class ThunderGod : ISetPiece
    {
        public int Size { get { return 5; } }

        public void RenderSetPiece(World world, IntPoint pos)
        {
            Entity TGod = Entity.Resolve(world.Manager, "Thunder God");
            TGod.Move(pos.X + 2.5f, pos.Y + 2.5f);
            world.EnterWorld(TGod);
        }
    }
}
