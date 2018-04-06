using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.utils {

	public static class CoordUtil {

		/// <summary>
		/// Returns a Vector2Int with the min x and y coordinates.
		/// </summary>
		/// <returns>The coords.</returns>
		/// <param name="a">The a vector.</param>
		/// <param name="b">The b vector.</param>
		public static Vector2Int MinCoords(Vector2Int a, Vector2Int b) {
			return new Vector2Int (Mathf.Min (a.x, b.x), Mathf.Min(a.y, b.y));
		}

		/// <summary>
		/// Returns a Vector2Int with the max x and y coordinates.
		/// </summary>
		/// <returns>The coords.</returns>
		/// <param name="a">The a vector.</param>
		/// <param name="b">The b vector.</param>
		public static Vector2Int MaxCoords(Vector2Int a, Vector2Int b) {
			return new Vector2Int (Mathf.Max (a.x, b.x), Mathf.Max(a.y, b.y));
		}
		
	}

}