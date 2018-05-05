using System;
using System.Collections;
using System.Collections.Generic;

namespace CippSharp.ClassTemplates
{
    public sealed class Text : IEnumerable<string>, IEquatable<Text>
    {
        public static readonly Text Empty = new Text();

        private readonly List<string> phrases;

        public int Count
        {
            get { return phrases.Count; }
        }

        public string this[int index]
        {
            get { return phrases[index]; }
            set { phrases[index] = value; }
        }

        private Text()
        {
            this.phrases = new List<string>();
        }

        public Text(IEnumerable<string> newPhrases)
        {
            this.phrases = new List<string>();
            if (newPhrases != null)
            {
                this.phrases.AddRange(newPhrases);
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return phrases.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) phrases.GetEnumerator();
        }

        public bool Equals(Text other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }
            
            return phrases.Equals(other.phrases);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            
            return obj is Text && Equals((Text) obj);
        }

        public override int GetHashCode()
        {
            return phrases.GetHashCode();
        }

        public static bool operator ==(Text left, Text right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Text left, Text right)
        {
            return !Equals(left, right);
        }

        public static Text operator +(Text left, Text right)
        {
            left.AddRange(right);
            return left;
        }

        public static Text operator +(Text left, string right)
        {
            left.Add(right);
            return left;
        }
        
        public static Text operator -(Text left, string right)
        {
            left.Add(right);
            return left;
        }

        /// <summary>
        /// Adds a phrase;
        /// </summary>
        /// <param name="value"></param>
        public void Add(string value)
        {
            this.phrases.Add(value);
        }

        /// <summary>
        /// Adds many phrases;
        /// </summary>
        /// <param name="value"></param>
        public void AddRange(IEnumerable<string> value)
        {
            this.phrases.AddRange(value);
        }

        /// <summary>
        /// A modified version of "Contains" that spit out the index of the found item.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Contains(string value, out int index)
        {
            index = IndexOf(value);
            return index > 0;
        }

        /// <summary>
        /// Remove target phrase from this text.
        /// </summary>
        /// <param name="value"></param>
        public void Remove(string value)
        {
            int index = IndexOf(value);
            if (index > 0)
            {
                RemoveAt(index);
            }
        }

        /// <summary>
        /// Remove all phrases as the passed one from this text.
        /// </summary>
        /// <param name="value"></param>
        public void RemoveAll(string value)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < phrases.Count; i++)
            {
                if (phrases[i] == value)
                {
                    indices.Add(i);
                }
            }

            foreach (int index in indices)
            {
                phrases.RemoveAt(index);
            }
        }

        /// <summary>
        /// Remove element at index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            phrases.RemoveAt(index);
        }

        /// <summary>
        /// Returns -1 if that phrase isn't in this text.
        /// Otherwise returns the right index of that phrase.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(string value)
        {
            for (int i = 0; i < phrases.Count; i++)
            {
                if (phrases[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// Advanced index of.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int IndexOf/*his mom*/(Predicate<string> predicate)
        {
            for (int i = 0; i < phrases.Count; i++)
            {
                if (predicate(phrases[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Like Substring but for a Text.
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public Text Subtext(int startIndex, int length)
        {
            if (phrases == null || phrases.Count < 1)
            {
                return Empty;
            }

            List<string> subPhrases = new List<string>();
            
            for (int i = startIndex; i < length; i++)
            {
                subPhrases.Add(phrases[i]);
            }
            
            return new Text(subPhrases);
        }

        public List<string> ToList()
        {
            List<string> tmpPhrases = new List<string>();
            tmpPhrases.AddRange(phrases);
            return tmpPhrases;
        }

    }
}
