import os

from PIL import Image

from . import generatorpaths

def get_mask(requesting_z, coords, isOccupied, additive):
	return Image.open(get_mask_path(requesting_z, coords, isOccupied, additive)).convert('RGBA')

def get_mask_path(requesting_z, coords, isOccupied, additive):
	if (additive):
		mode = 'ADDITIVE'
	else:
		mode = 'SUBSTRACTIVE'

	path = os.path.join(generatorpaths.MASKS_PATH, 'MASK_{1}{3}_{0}_{2}.png'.format(
		mode,
		str(int(requesting_z)),
		str(int(isOccupied)),
		coords))

	return path

def get_mask_name(requesting_z, coords, isOccupied, additive):
	head, tail = os.path.split(get_mask_path(requesting_z,coords,isOccupied, additive))
	return tail