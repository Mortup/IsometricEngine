import os

__author__ = 'Mortup'

def parent(path, n):
	if n < 0:
		raise ValueError('Arg n must be greater than 0.')
	if n == 1:
		return os.path.dirname(path)

	return parent(os.path.dirname(path), n-1)