using wServer.realm.worlds;

namespace wServer.realm.setpieces
{
    class QueenRealm : ISetPiece
    {
        public int Size { get { return 50; } }

        public void RenderSetPiece(World world, IntPoint pos)
        {
            var QueenOfHearts = Entity.Resolve(world.Manager, "Queen of Hearts");
            QueenOfHearts.Move(pos.X + 2.5f, pos.Y + 2.5f);
            world.EnterWorld(QueenOfHearts);
        }
    }
}