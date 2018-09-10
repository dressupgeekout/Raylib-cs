

using Raylib;

using static Raylib.Raylib;



public partial class Examples

{

    /*******************************************************************************************
    *
    *   raylib [audio] example - Using audio module as standalone module
    *
    *   NOTE: This example does not require any graphic device, it can run directly on console.
    *
    *   DEPENDENCIES:
    *       mini_al.h    - Audio device management lib (http://kcat.strangesoft.net/openal.html)
    *       stb_vorbis.c - Ogg audio files loading (http://www.nothings.org/stb_vorbis/)
    *       jar_xm.h     - XM module file loading
    *       jar_mod.h    - MOD audio file loading
    *       dr_flac.h    - FLAC audio file loading
    *
    *   COMPILATION:
    *       gcc -c ..\..\src\external\mini_al.c -Wall -I.
    *       gcc -o audio_standalone.exe audio_standalone.c ..\..\src\audio.c ..\..\src\external\stb_vorbis.c mini_al.o  /
    *           -I..\..\src -I..\..\src\external -L. -Wall -std=c99  / 
    *           -DAUDIO_STANDALONE -DSUPPORT_FILEFORMAT_WAV -DSUPPORT_FILEFORMAT_OGG
    *
    *   LICENSE: zlib/libpng
    *
    *   This example is licensed under an unmodified zlib/libpng license, which is an OSI-certified,
    *   BSD-like license that allows static linking with closed source software:
    *
    *   Copyright (c) 2014-2018 Ramon Santamaria (@raysan5)
    *
    *   This software is provided "as-is", without any express or implied warranty. In no event
    *   will the authors be held liable for any damages arising from the use of this software.
    *
    *   Permission is granted to anyone to use this software for any purpose, including commercial
    *   applications, and to alter it and redistribute it freely, subject to the following restrictions:
    *
    *     1. The origin of this software must not be misrepresented; you must not claim that you
    *     wrote the original software. If you use this software in a product, an acknowledgment
    *     in the product documentation would be appreciated but is not required.
    *
    *     2. Altered source versions must be plainly marked as such, and must not be misrepresented
    *     as being the original software.
    *
    *     3. This notice may not be removed or altered from any source distribution.
    *
    ********************************************************************************************/
    
    #include "audio.h"              // Audio library
    
    #include <stdio.h>              // Required for: printf()
    
    #if defined(_WIN32)
        #include <conio.h>          // Windows only, no stardard library
    #else
        
    // Provide kbhit() function in non-Windows platforms
    #include <stdio.h>
    #include <termios.h>
    #include <unistd.h>
    #include <fcntl.h>
    
    // Check if a key has been pressed
    static int kbhit(void)
    {
    	struct termios oldt, newt;
    	int ch;
    	int oldf;
    
    	tcgetattr(STDIN_FILENO, &oldt);
    	newt = oldt;
    	newt.c_lflag &= ~(ICANON | ECHO);
    	tcsetattr(STDIN_FILENO, TCSANOW, &newt);
    	oldf = fcntl(STDIN_FILENO, F_GETFL, 0);
    	fcntl(STDIN_FILENO, F_SETFL, oldf | O_NONBLOCK);
    
    	ch = getchar();
    
    	tcsetattr(STDIN_FILENO, TCSANOW, &oldt);
    	fcntl(STDIN_FILENO, F_SETFL, oldf);
    
    	if (ch != EOF)
    	{
    		ungetc(ch, stdin);
    		return 1;
    	}
    
    	return 0;
    }
    
    // Get pressed character
    static char getch() { return getchar(); }
    
    #endif
    
    private const int KEY_ESCAPE = 27;
    
    public static int audio_standalone()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        static unsigned char key;
    
        InitAudioDevice();
    
        Sound fxWav = LoadSound("resources/audio/weird.wav");         // Load WAV audio file
        Sound fxOgg = LoadSound("resources/audio/tanatana.ogg");      // Load OGG audio file
    
        Music music = LoadMusicStream("resources/audio/guitar_noodling.ogg");
        PlayMusicStream(music);
    
        printf("\nPress s or d to play sounds...\n");
        //--------------------------------------------------------------------------------------
    
        // Main loop
        while (key != KEY_ESCAPE)
        {
            if (kbhit()) key = getch();
    
            if (key == 's')
            {
                PlaySound(fxWav);
                key = 0;
            }
    
            if (key == 'd')
            {
                PlaySound(fxOgg);
                key = 0;
            }
    
            UpdateMusicStream(music);
        }
    
        // De-Initialization
        //--------------------------------------------------------------------------------------
        UnloadSound(fxWav);         // Unload sound data
        UnloadSound(fxOgg);         // Unload sound data
    
        UnloadMusicStream(music);   // Unload music stream data
    
        CloseAudioDevice();
        //--------------------------------------------------------------------------------------
    
        return 0;
    }
    

}