import os

from . import pathutils

__author__ = 'Mortup'

DEFAULT_OUTPUT_PATH = os.path.join(pathutils.parent(os.path.realpath(__file__), 4), 'Assets', 'Resources', 'Sprites', 'Walls')
MASKS_PATH = os.path.join(pathutils.parent(os.path.realpath(__file__), 2), 'masks')
BLUEPRINTS_PATH = os.path.join(pathutils.parent(os.path.realpath(__file__), 2), 'blueprints')

def blueprint_z0_path(index):
	return os.path.join(BLUEPRINTS_PATH, str(index), 'z0.png')

def blueprint_z1_path(index):
	return os.path.join(BLUEPRINTS_PATH, str(index), 'z1.png')

def blueprint_border_path(index):
	return os.path.join(BLUEPRINTS_PATH, str(index), 'borders.png')

def blueprint_top_path(index):
	return os.path.join(BLUEPRINTS_PATH, str(index), 'top.png')

def blueprint_side_dark_path(index):
	return os.path.join(BLUEPRINTS_PATH, str(index), 'side_dark.png')