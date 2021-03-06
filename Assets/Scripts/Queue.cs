﻿using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Queue<K, V> : IPriorityQueue<K, V> where V : IComparable where K : System.IEquatable<K>
	{
		private const int DEFAULT_CAPACITY = 100;
		private int count;
		private List<KeyValuePair<K, V>> items;

		public Queue()
		{
			this.items = new List<KeyValuePair<K, V>>();

		}

		public void Add(K n, V dist)
		{
			this.items.Add(new KeyValuePair<K, V>(n, dist));
			this.count++;
		}

		public V this[K index_key]
		{
			set
			{
				foreach (KeyValuePair<K, V> kvp in items)
				{
					if (kvp.Key.Equals(index_key))
					{
						int index = items.IndexOf(kvp);
						items[index] = new KeyValuePair<K, V>(kvp.Key, value);
					}
				}
			}
			get
			{
				foreach (KeyValuePair<K, V> kvp in items)
				{
					if (kvp.Key.Equals(index_key))
					{
						return kvp.Value;
					}
				}
				return default(V);
			}
		}

		public bool ContainsKey(K n)
		{
			foreach (KeyValuePair<K, V> kvp in this.items)
			{
				if (kvp.Key.Equals(n))
				{
					return true;
				}
			}
			return false;
		}

		public KeyValuePair<K, V> Next()
		{
			if (this.count > 0)
			{
				KeyValuePair<K, V> topOfStack = this.items[0];
                this.items.RemoveAt(0);
				count--;
				return topOfStack;
			}
			else
			{
				return default(KeyValuePair<K, V>);
			}
		}
	}
}

