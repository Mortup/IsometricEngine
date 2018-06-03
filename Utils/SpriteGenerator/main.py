import os

from PIL import Image

img_width = 38
img_height = 130

folderName = "Placeholders"

def getImagePath(index):
	return os.path.join(folderName, "/img_" + str(i) + ".png")




# MAIN
if not os.path.exists(folderName):
	os.makedirs(folderName)

for i in range(64):
	im = Image.new("RGBA", (img_width, img_height))
	im.save(getImagePath(i))
	print(getImagePath(i))

