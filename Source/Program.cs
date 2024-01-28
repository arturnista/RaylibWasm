using Raylib_cs;
using System.Numerics;

namespace Source;

public class Program
{

    public class Orc
    {
        public Vector2 Position;
        public Vector2 Velocity;
    }

    public static int Main()
    {
        Raylib.InitWindow(800, 480, "Hello World");
        Texture2D orcTexture = Raylib.LoadTexture("resources/orc.png");
        
        List<Orc> allOrcs = new List<Orc>();

        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();
            if (Raylib.IsMouseButtonPressed(0))
            {
                var position = Raylib.GetMousePosition();
                allOrcs.Add(new Orc()
                {
                    Position = new Vector2(position.X, 480 - position.Y),
                    Velocity = new Vector2(
                        Random.Shared.NextSingle() * 500f - 250f,
                        Random.Shared.NextSingle() * 500f - 250f
                    )
                });
            }

            foreach (var orc in allOrcs)
            {
                orc.Position += orc.Velocity * deltaTime;
                orc.Velocity += new Vector2(0f, -50f) * deltaTime;
                if (orc.Position.Y < 0f) orc.Velocity = Vector2.Reflect(orc.Velocity, Vector2.UnitY) * (Random.Shared.NextSingle() * 1f + 0.5f);
                else if (orc.Position.Y > 480f) orc.Velocity = Vector2.Reflect(orc.Velocity, -Vector2.UnitY) * (Random.Shared.NextSingle() * 1f + 0.5f);
                else if (orc.Position.X < 0f) orc.Velocity = Vector2.Reflect(orc.Velocity, Vector2.UnitX) * (Random.Shared.NextSingle() * 1f + 0.5f);
                else if (orc.Position.X > 800f) orc.Velocity = Vector2.Reflect(orc.Velocity, -Vector2.UnitX) * (Random.Shared.NextSingle() * 1f + 0.5f);
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.DrawText($"Press to create orcs", 12, 25, 20, Color.Black);
            Raylib.DrawText($"Orcs created: {allOrcs.Count}", 12, 45, 20, Color.Black);
            foreach (var orc in allOrcs)
            {
                Raylib.DrawTexture(orcTexture, (int)orc.Position.X, 480 - (int)orc.Position.Y, Color.White);
            }
            Raylib.DrawFPS(12, 450);
            Raylib.EndDrawing();
        }
        Raylib.UnloadTexture(orcTexture);

        Raylib.CloseWindow();
        return 0;
    }
}