using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    class CycList<T> where T : IDisposable
    {
        private List<T> _items = new List<T>();
        private int _pointer = 0;
        private int _size;

        public CycList(int size)
        {

            this._size = size;
            for (int i = 0; i < this._size; i++)
            {
                this._items.Add(default(T));
            }
        }

        public void Add(T item)
        {
            Console.WriteLine($"Adding {item} at position {this._pointer}");
            this._items[this._pointer] = item;
            this._pointer++;
            if (this._pointer >= this._size)
            {
                this._pointer = 0;
            }
        }

        public T GetItem(int index)
        {
            Console.WriteLine($"Getting item from position {index}");
            if ((index > 0) && (index < this._size))
            {
                return this._items[index];
            }
            else return default(T);
        }

        public void View()
        {
            int itemsPrinted = 0;
            int tempPointer = this._pointer;
            while (itemsPrinted < this._size)
            {
                Console.WriteLine($"Pos{tempPointer}:{this._items[tempPointer]}");
                itemsPrinted++;
                tempPointer++;
                if (tempPointer >= this._size)
                {
                    tempPointer = 0;
                }

            }
            Console.WriteLine("--------------");
        }


    }
}
