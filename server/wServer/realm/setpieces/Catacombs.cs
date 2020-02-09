using wServer.realm.worlds;

namespace wServer.realm.setpieces
{
    class Catacombs : ISetPiece
    {
        public int Size { get { return 20; } }

        public void RenderSetPiece(World world, IntPoint pos)
        {
            var proto = world.Manager.Resources.Worlds["Catacombs"];
            SetPieces.RenderFromProto(world, pos, proto);
        }
    }
}
