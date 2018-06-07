import os
from PIL import Image

def crop_base_sprite(base_sprite):
	full_sprite_size = base_sprite.size

	upper_box = (0, 0, 38, 22)
	bottom_box = (0, 110, 38, 130)

	result = Image.new('RGBA', full_sprite_size)

	bottom_im = base_sprite.crop(bottom_box)
	result.paste(bottom_im, bottom_box[0:2], bottom_im)

	upper_im = base_sprite.crop(upper_box)
	result.paste(upper_im, (upper_box[0],bottom_box[1]-upper_box[3]), upper_im)

	return result

def crop_base_sprite_top_masked(sprite, mask):
	base_sprite = Image.new('RGBA', sprite.size, (0,0,0,0))
	base_sprite.paste(sprite, (0,0), sprite)

	upper_box = (0, 0, 38, 22)
	bottom_box = (0, 110, 38, 130)

	upper_im = base_sprite.crop(upper_box)
	base_sprite.paste(upper_im, (upper_box[0],bottom_box[1]-upper_box[3]), mask)

	empty_img = Image.new('RGBA', (38, bottom_box[1]-upper_box[3]), (0,0,0,0))
	square_mask = Image.new('RGBA', (38, bottom_box[1]-upper_box[3]), (255,255,255,255))
	base_sprite.paste(empty_img, (0,0), square_mask)

	return base_sprite

if __name__ == "__main__":
	for f in os.listdir():
		name = os.path.splitext(f)[0]
		extension = os.path.splitext(f)[1]
		if extension == '.png':
			crop_base_sprite(Image.open(f)).save(name + "_CROPPED" + extension)