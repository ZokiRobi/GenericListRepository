using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    public class ListGeneric<T>
    {
        private class GenericElement<T>
        {
            public int index;
            public T _elementData;
            public GenericElement<T> _leftElement;
            public GenericElement<T> _rightElement;

            public GenericElement(T data, GenericElement<T> leftE, GenericElement<T> rightE, int index)
            {
                this._elementData = data;
                this._leftElement = leftE;
                this._rightElement = rightE;
                this.index = index;
            }

        }

        public int Lenght = 0;
        private GenericElement<T> _first;
        private GenericElement<T> _last;

        public ListGeneric()
        {
            _first = _last = null;
        }

        public ListGeneric(T[] genericArray)
        {
            _first = _last = null;
            for (int i = 0; i < genericArray.Length; i++)
            {
                this.Add(genericArray[i]);
            }
        }

        public ListGeneric(T[] genericArray, bool direction)
        {
            if (direction == true)
            {
                _first = _last = null;
                for (int i = 0; i < genericArray.Length; i++)
                {
                    this.PreAdd(genericArray[i]);
                }
            }
            else
            {
                _first = _last = null;
                for (int i = 0; i < genericArray.Length; i++)
                {
                    this.Add(genericArray[i]);
                }
            }

        }

        public void Clear()
        {
            _first = _last = null;
            Lenght = 0;
        }

        public void Add(T data)
        {
            GenericElement<T> element = new GenericElement<T>(data, _last, null, Lenght);
            if (IsEmpty())
            {
                _first = element;
                _last = element;
            }
            else
            {
                _last._rightElement = element;
                _last = element;
            }
            Lenght++;
        }

        public void PreAdd(T data)
        {
            GenericElement<T> element = new GenericElement<T>(data, null, _first, 0);

            if (IsEmpty())
            {
                _first = element;
                _last = element;
            }
            else
            {
                _first._leftElement = element;
                _first = element;
            }
            Lenght++;
            this.OrganizeIndexes();
        }

        public void Concat(T[] colection)
        {
            foreach (T item in colection)
            {
                Add(item);
            }
        }

        public void PreConcat(T[] colection)
        {
            foreach (T item in colection)
            {
                PreAdd(item);
            }
        }

        public bool Exists(T data)
        {

            if (FindElementByData(data) == null)
            {
                return false;
            }
            else
                return true;

        }

        public T TakeFirst()
        {
            return _first._elementData;
        }

        public T TakeLast()
        {
            return _last._elementData;
        }

        public void AddBefore(T elementFromListData, T newElementData)
        {
            GenericElement<T> elementFromList = FindElement(_first, elementFromListData);

            if (elementFromList != null)
            {
                if (elementFromList._leftElement != null)
                {
                    GenericElement<T> NewElement = new GenericElement<T>(newElementData, elementFromList._leftElement, elementFromList, Lenght);

                    elementFromList._leftElement._rightElement = NewElement;
                    elementFromList._leftElement = NewElement;
                    Lenght++;

                }
                else
                {
                    GenericElement<T> NewElement = new GenericElement<T>(newElementData, null, elementFromList, Lenght);

                    elementFromList._leftElement = NewElement;
                    _first = NewElement;
                    Lenght++;
                }
            }

            this.OrganizeIndexes();
        }

        public void AddBeforeIndex(int elementIndex, T newElementData)
        {
            GenericElement<T> elementFromList = FindElementByIndex(elementIndex);

            if (elementFromList != null)
            {
                if (elementFromList._leftElement != null)
                {
                    GenericElement<T> NewElement = new GenericElement<T>(newElementData, elementFromList._leftElement, elementFromList, Lenght);

                    elementFromList._leftElement._rightElement = NewElement;
                    elementFromList._leftElement = NewElement;
                    Lenght++;
                }
                else
                {
                    GenericElement<T> NewElement = new GenericElement<T>(newElementData, null, elementFromList, Lenght);

                    elementFromList._leftElement = NewElement;
                    _first = NewElement;
                    Lenght++;
                }

            }
            this.OrganizeIndexes();
        }

        public T GetElementByIndex(int i)
        {
            return FindElementByIndex(i)._elementData;
        }

        public ListGeneric<T> Distinct()
        {

            var distinctList = new ListGeneric<T>();

            for (int i = 0; i < this.Lenght; i++)
            {
                var curentElement = this.FindElementByIndex(i);
                if (!distinctList.Exists(curentElement._elementData))
                {
                    distinctList.Add(curentElement._elementData);
                }

            }
            return distinctList;
        }

        public void UpdateOnIndex(int index, T data)
        {
            if (this.ExistsByIndex(index))
            {
                this.FindElementByIndex(index)._elementData = data;
            }

        }

        public void UpdateBeforeIndex(int index, T data)
        {
            if (this.ExistsByIndex(index))
            {
                GenericElement<T> FoundElement = this.FindElementByIndex(index);

                if (FoundElement._leftElement != null)
                {
                    this.FindElementByIndex(index)._leftElement._elementData = data;
                }
            }
        }

        public void UpdateAfterIndex(int index, T data)
        {
            if (this.ExistsByIndex(index))
            {
                GenericElement<T> FoundElement = this.FindElementByIndex(index);

                if (FoundElement != null)
                {
                    if (FoundElement._rightElement != null)
                    {
                        this.FindElementByIndex(index)._rightElement._elementData = data;
                    }
                }

            }
        }

        public void UpdateFirst(T data)
        {
            if (_first != null)
                _first._elementData = data;
        }
        public void UpdateLast(T data)
        {
            if (_last != null)
                _last._elementData = data;
        }

        public void DeleteFirst()
        {
            if (Lenght == 1)
            {
                _first = _last = null;
                OrganizeIndexes();
                --Lenght;
            }
            else if (Lenght > 1)
            {
                _first = _first._rightElement;
                _first._leftElement = null;
                --Lenght;
                OrganizeIndexes();
            }
        }
        public void DeleteLast()
        {
            if (Lenght == 1)
            {
                _first = _last = null;
                --Lenght;
            }
            else if (Lenght > 1)
            {
                _last = _last._leftElement;
                _last._rightElement = null;
                --Lenght;
                OrganizeIndexes();
            }
        }
        ///<summary>
        ///This method requires int parameter for index of list, if list has element with index parameter element that match will be deleted from the list.
        ///</summary>
        public void DeleteOnIndex(int index)
        {
            if (ExistsByIndex(index))
            {
                GenericElement<T> elementFromList = FindElementByIndex(index);

                if (elementFromList != null)
                {
                    if (elementFromList._leftElement == null)
                    {
                        elementFromList._rightElement._leftElement = null;
                        _first = elementFromList._rightElement;
                    }
                    else if (elementFromList._rightElement == null)
                    {
                        elementFromList._leftElement._rightElement = null;
                        _last = elementFromList._leftElement;
                    }
                    else
                    {
                        elementFromList._leftElement._rightElement = elementFromList._rightElement;
                        elementFromList._rightElement._leftElement = elementFromList._leftElement;
                    }
                    Lenght--;
                    OrganizeIndexes();
                }

            }
        }

        public void DeleteBefore(T data)
        {
            GenericElement<T> elementFound = FindElementByData(data);

            if (elementFound != null)
            {
                DeleteOnIndex(elementFound._leftElement.index);
            }
        }

        public void DeleteAfter(T data)
        {
            GenericElement<T> elementFound = FindElementByData(data);
            if (elementFound != null)
            {
                DeleteOnIndex(elementFound._rightElement.index);
            }
        }

        public void DeleteBeforeIndex(int indexOfElement)
        {
            GenericElement<T> elementFound = FindElementByIndex(indexOfElement - 1);
            if (elementFound != null)
            {
                DeleteOnIndex(elementFound._rightElement.index);
            }
        }

        public void DeleteAfterIndex(int indexOfElement)
        {
            GenericElement<T> elementFound = FindElementByIndex(indexOfElement + 1);
            if (elementFound != null)
            {
                DeleteOnIndex(elementFound.index);
            }
        }

        public T GetElementAtIndex(int index)
        {
            return FindElementByIndex(index)._elementData;
        }

        public T[] ToArray()
        {
            T[] array = new T[Lenght];

            for (int i = 0; i < Lenght; i++)
            {
                array[i] = GetElementByIndex(i);
            }

            return array;
        }

        public List<T> ToList()
        {
            List<T> elementsInList = new List<T>();

            for (int i = 0; i < Lenght; i++)
            {
                elementsInList.Add(GetElementByIndex(i));
            }

            return elementsInList;
        }

        // private methods
        private bool IsEmpty()
        {
            return _first == null;
        }

        private GenericElement<T> FindElement(GenericElement<T> Element, T elementFromList)
        {
            if (Element._elementData.Equals(elementFromList))
                return Element;

            return FindElement(Element._rightElement, elementFromList);
        }

        private GenericElement<T> FindElementByData(T data)
        {

            GenericElement<T> GenericElement = _first;

            GenericElement<T> FoundElement = null;

            while (GenericElement != null)
            {

                if (GenericElement._elementData.Equals(data))
                {
                    FoundElement = GenericElement;
                    break;
                }
                else
                    GenericElement = GenericElement._rightElement;
            }

            return FoundElement;
        }

        private GenericElement<T> FindElementByIndex(int i)
        {
            var GE = _first;

            GenericElement<T> FoundElement = null;

            while (GE != null)
            {

                if (GE.index.Equals(i))
                {
                    FoundElement = GE;
                    break;
                }
                else
                    GE = GE._rightElement;
            }

            return FoundElement;
        }

        private void InsertElement(GenericElement<T> element)
        {

            if (IsEmpty())
            {
                _first = element;
                _last = element;
            }
            else
            {
                _last._rightElement = element;
                _last = element;
            }
            Lenght++;
        }

        private bool ExistsByIndex(int index)
        {
            if (index < Lenght)
                return true;
            else
                return false;
        }

        private void OrganizeIndexes()
        {
            var GE = _first;
            int incrementIndex = 0;

            while (GE != null)
            {
                GE.index = incrementIndex;
                GE = GE._rightElement;
                if (GE != null)
                    incrementIndex++;
                if (GE == _last)
                {
                    GE.index = incrementIndex;
                }

            }

        }
    }
}
