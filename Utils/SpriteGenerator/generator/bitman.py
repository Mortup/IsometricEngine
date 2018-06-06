def isKthBitSet(n, k):
	if n & (1 << (k - 1)):
		return True
	return False