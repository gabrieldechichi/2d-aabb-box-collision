using System;
using ArbitraryCollisionRectangle.App.Testing;

namespace ArbitraryCollisionRectangle
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TestPointRectangleCollision())
                game.Run();
        }
    }
}
