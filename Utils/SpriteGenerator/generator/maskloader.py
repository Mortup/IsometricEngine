import os

from PIL import Image

from . import generatorpaths

def get_mask(requesting_z, coords, isOccupied, additive, isCropped):
	return Image.open(get_mask_path(requesting_z, coords, isOccupied, additive, isCropped)).convert('RGBA')

def get_mask_path(requesting_z, coords, isOccupied, additive, isCropped):
	if (additive):
		mode = 'ADDITIVE'
	else:
		mode = 'SUBSTRACTIVE'

	if isCropped:
		crop = '_CROPPED'
	else:
		crop = ''

	path = os.path.join(generatorpaths.MASKS_PATH, 'MASK_{1}{3}_{0}_{2}{4}.png'.format(
		mode,
		str(int(requesting_z)),
		str(int(isOccupied)),
		coords,
		crop))

	return path

def get_mask_name(requesting_z, coords, isOccupied, additive, isCropped):
	head, tail = os.path.split(get_mask_path(requesting_z,coords,isOccupied, additive, isCropped))
	return tail