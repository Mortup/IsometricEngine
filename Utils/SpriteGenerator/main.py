from PIL import Image

im = Image.open("TestFolder/Wall_01_0.png")

r, g, b, a = im.split()
im = Image.merge("RGBA", (b, g, r, a))
im.show()