using System;

namespace AssemblyCSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class Heap<K, V> : IPriorityQueue<K, V> where V : IComparable where K : System.IEquatable<K>
      {
        private List<KeyValuePair<K, V>> items;

        public Heap() {
            items = new List<KeyValuePair<K, V>>();
        }

        public KeyValuePair<K, V> Next()
        {
            if (items.Count() > 0) {
                KeyValuePair<K, V> min = items.First();
                foreach (KeyValuePair<K, V> item in items)
                {
                    if (item.Value.CompareTo(min.Value) == -1)
                    {
                        min = item;
                    }
                }

                items.Remove(min);
                return min;
            }
            else {
                return default(KeyValuePair<K, V>);
            }


        }

        public void Add(K n, V dist)
        {
            items.Add(new KeyValuePair<K, V>(n, dist));
        }

        public bool ContainsKey(K n)
        {
			foreach (KeyValuePair<K, V> item in items)
			{
                if (item.Key.Equals(n))
				{
                    return true;
				}
			}

            return false;
        }

        public int Count
          {
            get { return items.Count(); }
          }

        public V this[K index_key] {
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
    }

}





