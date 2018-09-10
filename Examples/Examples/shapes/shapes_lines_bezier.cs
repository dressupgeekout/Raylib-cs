

using Raylib;

using static Raylib.Raylib;



public partial class Examples

{

    /*******************************************************************************************
    *
    *   raylib [shapes] example - Cubic-bezier lines
    *
    *   This example has been created using raylib 1.7 (www.raylib.com)
    *   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
    *
    *   Copyright (c) 2017 Ramon Santamaria (@raysan5)
    *
    ********************************************************************************************/
    
    
    
    public static int shapes_lines_bezier()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        int screenWidth = 800;
        int screenHeight = 450;
    
        SetConfigFlags(FLAG_MSAA_4X_HINT);
        InitWindow(screenWidth, screenHeight, "raylib [shapes] example - cubic-bezier lines");
        
        Vector2 start = { 0, 0 };
        Vector2 end = { screenWidth, screenHeight };
        
        SetTargetFPS(60);
        //--------------------------------------------------------------------------------------
    
        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            if (IsMouseButtonDown(MOUSE_LEFT_BUTTON)) start = GetMousePosition();
            else if (IsMouseButtonDown(MOUSE_RIGHT_BUTTON)) end = GetMousePosition();
            //----------------------------------------------------------------------------------
    
            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();
    
                ClearBackground(RAYWHITE);
                
                DrawText("USE MOUSE LEFT-RIGHT CLICK to DEFINE LINE START and END POINTS", 15, 20, 20, GRAY);
    
                DrawLineBezier(start, end, 2.0f, RED);
                
            EndDrawing();
            //----------------------------------------------------------------------------------
        }
    
        // De-Initialization
        //--------------------------------------------------------------------------------------   
        CloseWindow();        // Close window and OpenGL context
        //--------------------------------------------------------------------------------------
    
        return 0;
    }
    

}