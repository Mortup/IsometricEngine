

Pixel Perfect Sprite
====================

Hey there, thanks for buying my code! :)


Getting Started
---------------

To get an idea for what Perfect Pixel Sprite can do, play the scene
"Demo - Perfect Pixel Sprite" or see https://youtu.be/CX3pQHe7SYU

To set up your own sprites:

 1. Create a new GameObject. This is the object you will move around.
 2. Create a child of that game object called Sprite Container.
 3. Add PerfectPixelSprite to Sprite Container
 4. Set the new component's "Texture Pixels Per World Unit" property
    in the inspector to the PPU value you're using on your sprites. This
    is almost always 8, 16 or 32.
 5. Add sprites as children of the Sprite Container
 6. You're done!

Important things to keep in mind:

 * Move only the top-level GameObject and let PerfectPixelSprite have control
   of the Sprite Container's local position.
 * Sprites under the Sprite Container should have local x and y coordinates
   that are multiples of 1/TexturePixelsPerUnit so that they are grid-aligned.
 * For textures, use point filtering, no mipmaps, turn off compression,
   use power-of-2 sized textures only, and anchor sprites at corners.
 * The camera's orthographicSize needs to be adjusted based on the resolution,
   and should also be aligned to the pixel grid. For an easy fix, try
   out my Perfect Pixel Camera: http://u3d.as/WVc
 * This is built to work using the default 2D orientation, so keep the camera
   facing parallel to the Z axis and make sure it's orthographic.




Support
-------

Check out the article on my website at:

    http://ggez.org/posts/perfect-pixel-sprite/

Feel free to send me an email if you need help!

    - Karl

    support@mail.ggez.org
