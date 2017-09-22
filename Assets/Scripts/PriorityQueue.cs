using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPriorityQueue<T, V> {

	/// <summary>
	/// Gets the next node from PriorityQueue and removes it 
	/// </summary>
	KeyValuePair<T, V> Next();

	/// <summary>
	/// Adds a node to the PriorityQueue.
	/// </summary>
	/// <param name="n">Node to add</param>
	void Add (T n, V dist);

	/// <summary>
	/// Return true if the key exists within the structure
	/// </summary>
	/// <returns><c>true</c>, if key was contained, <c>false</c> otherwise.</returns>
	/// <param name="n">N.</param>
	bool ContainsKey(T n);
}
