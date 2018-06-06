from PIL import Image
import numpy as np

from . import bitman, maskloader, generatorpaths

missing_masks = 0

def evaluate_conditions(i):
	res = []
	
	res.append(i % 2 == 1)
	res.append(bitman.isKthBitSet(i, 2))
	res.append(bitman.isKthBitSet(i, 3))
	res.append(bitman.isKthBitSet(i, 4))
	res.append(bitman.isKthBitSet(i, 5))
	res.append(bitman.isKthBitSet(i, 6))
	res.append(bitman.isKthBitSet(i, 7)) # Z

	return res

def get_z_coord(i):
	return evaluate_conditions(i)[6]

def get_coords_by_condition(cond_number, z):
	if z != 0 and z != 1:
		raise ValueError('Arg z must be 0 or 1')

	z0_coords = ['nzz','zzp','znp','pzz','pzp','pnp']
	z1_coords = ['znp','nzz','zzz','zpp','npz','zpz']

	if z == 0:
		return z0_coords[cond_number]
	return z1_coords[cond_number]


number_of_conditions = len(evaluate_conditions(0))
number_of_sprites = 2 ** number_of_conditions

sprite_width = 38
sprite_height = 130

border_mask_color = (255, 0, 255, 255)
top_mask_color = (0, 0, 255, 255)
side_dark_mask_color = (255, 0, 0, 255)
remove_mask_color = (255, 255, 255, 255)

def generate_empty_masks():
	ans = input("Warning, all previous masks will be overwritten.\nAre you sure? (Y/N)\n")
	if ans != 'Y':
		print("Aborting...")
		return

	empty_mask = Image.new('RGBA', (sprite_width, sprite_height), (0,0,0,0))

	for index in range(number_of_conditions-1):
		"""
		z = 0
		c = 0
		empty_mask.save(maskloader.get_mask_path(z, get_coords_by_condition(index, z), c, True))
		empty_mask.save(maskloader.get_mask_path(z, get_coords_by_condition(index, z), c, False))
		c = 1
		empty_mask.save(maskloader.get_mask_path(z, get_coords_by_condition(index, z), c, True))
		empty_mask.save(maskloader.get_mask_path(z, get_coords_by_condition(index, z), c, False))
		"""
		z = 1
		c = 0
		empty_mask.save(maskloader.get_mask_path(z, get_coords_by_condition(index, z), c, True))
		empty_mask.save(maskloader.get_mask_path(z, get_coords_by_condition(index, z), c, False))
		c = 1
		empty_mask.save(maskloader.get_mask_path(z, get_coords_by_condition(index, z), c, True))
		empty_mask.save(maskloader.get_mask_path(z, get_coords_by_condition(index, z), c, False))
			

def generate_images(blueprint_number):
	global missing_masks
	missing_masks = 0

	empty = Image.new('RGBA', (sprite_width, sprite_height), (0,0,0,0))

	# Should be asked as argument
	border = Image.open(generatorpaths.blueprint_border_path(blueprint_number))
	top = Image.open(generatorpaths.blueprint_top_path(blueprint_number))
	side_dark = Image.open(generatorpaths.blueprint_side_dark_path(blueprint_number))

	border_color = border.convert('RGBA').getpixel((1,1))
	top_color = top.convert('RGBA').getpixel((1,1))
	side_dark_color = side_dark.convert('RGBA').getpixel((1,1))

	res_images = []

	for i in range(number_of_sprites):

		z = get_z_coord(i)

		if z == 0:
			im = Image.open(generatorpaths.blueprint_z0_path(blueprint_number))
		else:
			im = Image.open(generatorpaths.blueprint_z1_path(blueprint_number))

		conds = evaluate_conditions(i)

		# Additive pass
		final_mask = Image.new('RGBA', (sprite_width, sprite_height), (0,0,0,0))
		for index in range(len(conds)-1):
			c = conds[index]

			try:
				mask = maskloader.get_mask(z, get_coords_by_condition(index, z), c, True)
				pixels = mask.load() # create the pixel map

				for i in range(mask.size[0]): # for every pixel:
					for j in range(mask.size[1]):
						if pixels[i,j] == border_mask_color:
							pixels[i,j] = border_color
						if pixels[i,j] == top_mask_color:
							pixels[i,j] = top_color
						if pixels[i,j] == side_dark_mask_color:
							pixels[i,j] = side_dark_color

				final_mask.paste(mask, (0,0), mask)
			except FileNotFoundError:
				handle_FileNotFoundError(z, get_coords_by_condition(index, z), c, True)

		pixels = final_mask.load()
		for i in range(mask.size[0]): # for every pixel:
					for j in range(mask.size[1]):
						if pixels[i,j] == remove_mask_color:
							pixels[i,j] = (0,0,0,0)
		im.paste(final_mask, (0,0), final_mask)

		# Substractive pass
		for index in range(len(conds)-1):
			c = conds[index]

			try:
				bordermask = maskloader.get_mask(z, get_coords_by_condition(index, z), c, False)
				im.paste(empty, (0,0), bordermask)
			except FileNotFoundError:
				handle_FileNotFoundError(z, get_coords_by_condition(index, z), c, False)

		res_images.append(im)

	if missing_masks != 0:
		print("Missing masks: {0}".format(str(missing_masks)))

	return res_images

def handle_FileNotFoundError(z, coords, c, mode):
	print("Missing mask: {0}".format(maskloader.get_mask_name(z, coords, c, mode)))
	global missing_masks
	missing_masks += 1