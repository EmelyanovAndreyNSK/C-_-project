using System;
using System.Collections;
using System.Collections.Generic;


namespace Generics.BinaryTrees
{
    static class BinaryTree
    {
        public static BinaryTree<T> Create<T>(params T[] nodeValues) where T : IComparable
        {
            var binaryTree = new BinaryTree<T>();
            foreach (var newValue in nodeValues)
            {
                binaryTree.Add(newValue);
            }
            return binaryTree;
        }
    }

    class BinaryTree<T> : IEnumerable<T> where T : IComparable
    { 
        public BinaryTree<T> Left { get; private set; }
        public BinaryTree<T> Right { get; private set; }
        public T Value { get; private set; }
        private bool IsEmpty { get; set; }

        public BinaryTree()
        {
            Left = null;
            Right = null;
            Value = default;
            IsEmpty = false;
        }

        public BinaryTree(T value)
        {
            Value = value;
            Left = null;
            Right = null;
            IsEmpty = true;
        }

        public void Add(T newValue)
        {
            if (!IsEmpty)
            {
                Value = newValue;
                IsEmpty = true;
                return;
            }

            IsEmpty = true;

            if (Value.CompareTo(newValue) >= 0)
            {
                if (Left != null)
                {
                    Left.Add(newValue);
                }
                else
                {
                    Left = new BinaryTree<T>(newValue);
                }
            }
            else
            {
                if (Right != null)
                {
                    Right.Add(newValue);
                }
                else
                {
                    Right = new BinaryTree<T>(newValue);
                }
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        public IEnumerable<T> GetEnumerable()
        {
            if (!IsEmpty)
            {
                yield break;
            }

            if (Left != null)
            {
                foreach (var nodeValue in Left.GetEnumerable())
                {
                    yield return nodeValue;
                }
            }

            yield return Value;

            if (Right != null)
            {
                foreach (var nodeValue in Right.GetEnumerable())
                {
                    yield return nodeValue;
                }
            }
        }
    }
}
