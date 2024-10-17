using Raylib_cs;
using System.Numerics;

namespace Source;

public class Program
{

    private const int WINDOW_WIDTH = 1124;
    private const int WINDOW_HEIGHT = 740;

    public class Orc
    {
        public Vector2 Position;
        public Vector2 Velocity;
    }

    public static int Main()
    {
        int worldMargin = 16;
        Raylib.InitWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "Hello World");
        Texture2D orcTexture = Raylib.LoadTexture("resources/orc.png");
        
        List<Orc> allOrcs = new List<Orc>();

        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                var position = Raylib.GetMousePosition();
                allOrcs.Add(new Orc()
                {
                    Position = new Vector2(position.X, WINDOW_HEIGHT - position.Y),
                    Velocity = new Vector2(
                        Random.Shared.NextSingle() * 500f - 250f,
                        Random.Shared.NextSingle() * 500f - 250f
                    )
                });
            }
            if (Raylib.IsMouseButtonDown(MouseButton.Right))
            {
                var position = Raylib.GetMousePosition();
                var fixedPosition = new Vector2(position.X, WINDOW_HEIGHT - position.Y);
                foreach (var orc in allOrcs)
                {
                    var direction = Vector2.Normalize(fixedPosition - orc.Position);
                    orc.Velocity += direction * 500f * deltaTime;
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                allOrcs.Clear();
            }

            Vector2 boost = Vector2.Zero;
            int force = 500;
            if (Raylib.IsKeyPressed(KeyboardKey.Left))
            {
                boost = new Vector2(-force, 0f);
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.Right))
            {
                boost = new Vector2(force, 0f);
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.Up))
            {
                boost = new Vector2(0f, force);
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.Down))
            {
                boost = new Vector2(0f, -force);
            }

            foreach (var orc in allOrcs)
            {
                orc.Velocity += boost;
                if (orc.Velocity.Length() > 500)
                {
                    orc.Velocity = Vector2.Normalize(orc.Velocity) * 500;
                }

                orc.Position += orc.Velocity * deltaTime;
                
                if (orc.Position.Y < worldMargin)
                {
                    orc.Position.Y = WINDOW_HEIGHT - 300;
                }
                else if (orc.Position.Y > WINDOW_HEIGHT - 300)
                {
                    orc.Position.Y = worldMargin;
                }
                else if (orc.Position.X < worldMargin)
                {
                    orc.Position.X = WINDOW_WIDTH - worldMargin;
                }
                else if (orc.Position.X > WINDOW_WIDTH - worldMargin)
                {
                    orc.Position.X = worldMargin;
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            var xMin = worldMargin;
            var xMax = WINDOW_WIDTH - worldMargin;
            var yMin = WINDOW_HEIGHT - worldMargin;
            var yMax = 300;

            Raylib.DrawLine(xMin, yMin, xMin, yMax, Color.Black);
            Raylib.DrawLine(xMax, yMin, xMax, yMax, Color.Black);

            Raylib.DrawLine(xMin, yMin, xMax, yMin, Color.Black);
            Raylib.DrawLine(xMin, yMax, xMax, yMax, Color.Black);

            Raylib.DrawText($"Left Click to create orcs", 12, 25, 20, Color.Black);
            Raylib.DrawText($"Right Click to pull orcs", 312, 25, 20, Color.Black);
            Raylib.DrawText($"Space to clear all orcs", 612, 25, 20, Color.Black);
            Raylib.DrawText($"Orcs created: {allOrcs.Count}", 12, 45, 20, Color.Black);
            foreach (var orc in allOrcs)
            {
                Raylib.DrawTexture(orcTexture, (int)orc.Position.X, WINDOW_HEIGHT - (int)orc.Position.Y, Color.White);
            }
            Raylib.DrawFPS(12, WINDOW_HEIGHT - 25);
            Raylib.EndDrawing();
        }
        Raylib.UnloadTexture(orcTexture);

        Raylib.CloseWindow();
        return 0;
    }
}