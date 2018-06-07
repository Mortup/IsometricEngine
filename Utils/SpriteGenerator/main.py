import os
import random
import argparse

from PIL import Image, ImageFont, ImageDraw

from generator import generatorpaths, spritetransformer

__author__ = 'Mortup'

output_path = generatorpaths.DEFAULT_OUTPUT_PATH

# MAIN
parser = argparse.ArgumentParser(description='Script to generate wall sprite variations.\nAuthor: Mortup')
parser.add_argument('-n', '--number', help='Index of the wall to create.', required=True)
parser.add_argument('-o', '--output', help='Index to save the sprites.', required=False)
args = parser.parse_args()

# Should be asked as
print ("Running...")

images = spritetransformer.generate_images(args.number)

output_folder = str(args.number)
if args.output:
	output_folder = str(args.output)
if not os.path.exists(os.path.join(output_path, output_folder)):
	os.makedirs(os.path.join(output_path, output_folder))

for i, im in enumerate(images):
	im.save(os.path.join(output_path, output_folder, 'wall_'+str(i).zfill(5)+'.png'))

print("Done!")

