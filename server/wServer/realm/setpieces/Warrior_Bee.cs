using wServer.realm.worlds;

namespace wServer.realm.setpieces
{
    class Warrior_Bee : ISetPiece
    {
        public int Size { get { return 5; } }

        public void RenderSetPiece(World world, IntPoint pos)
        {
            Entity Bee = Entity.Resolve(world.Manager, "Warrior Bee");
            Bee.Move(pos.X + 2.5f, pos.Y + 2.5f);
            world.EnterWorld(Bee);
        }
    }
}