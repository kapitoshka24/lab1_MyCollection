using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;

namespace Collection_lab1
{
    public class NodeStack<T> : IEnumerable<T>, ICollection, INotifyCollectionChanged
    {
        Node<T> head;
        int count;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private bool IsEmpty
        {
            get
            {
                return count == 0;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public object SyncRoot => this;
        public bool IsSynchronized => false;

        public void Push(T item)
        {
            Node<T> node = new Node<T>(item);
            node.Next = head;
            head = node;
            count++;

            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add, node.Data)
                );
        }

        public T Pop()
        {
            ReturnEmpty();
            Node<T> temp = head;
            head = head.Next;
            count--;

            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove, temp.Data)
                );

            return temp.Data;
        }

        public T Peek()
        {
            ReturnEmpty();
            return head.Data;
        }

        public void ReturnEmpty()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("\nStack is empty!");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (Node<T> current = head; current != null; current = current.Next)
            {
                yield return current.Data;
            }
        }
        void ICollection.CopyTo(Array array, int index)
        {
            int item = 0;

            for (Node<T> node = head; node != null; node = node.Next, item++)
            {
                array.SetValue(node.Data, item);
            }
        }
    }
}
