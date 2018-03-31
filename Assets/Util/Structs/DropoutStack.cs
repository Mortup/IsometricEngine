using System;
using System.Collections;
using System.Collections.Generic;

namespace com.gStudios.utils.structs {

	public class DropoutStack<T> {

		private readonly T[] _items;

		private int _top; // The position in the array that the next item will be placed.

		private int _count; // The amount of items in the array.

		public DropoutStack(int capacity) {
			_items = new T[capacity];
		}

		/// <summary>
		/// Pushes the specified item onto the stack.
		/// </summary>
		/// <param name="item">The item.</param>
		public void Push(T item)
		{
			_count += 1;
			_count = _count > _items.Length ? _items.Length : _count;

			_items[_top] = item;
			_top = (_top + 1) % _items.Length; // After filling the array the next item will be placed at the beginning of the array!
		}

		/// <summary>
		/// Pops last item from the stack.
		/// </summary>
		/// <returns>T. The last item. Null is empty.</returns>
		public T Pop()
		{
			_count -= 1;
			_count = _count < 0 ? 0 : _count;

			_top = (_items.Length + _top - 1) % _items.Length;
			return _items[_top];
		}

		/// <summary>
		/// Peeks at last item on the stack.
		/// </summary>
		/// <returns>T.</returns>
		public T Peek()
		{
			return _items[(_items.Length + _top - 1) % _items.Length]; //Same as pop but without changing the value of top.
		}

		/// <summary>
		/// Returns the amount of elements on the stack.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public int Count
		{
			get {
				return _count;
			}
		}

		/// <summary>
		/// Clears the stack.
		/// </summary>
		public void Clear()
		{
			_count = 0;
		}
	}

}