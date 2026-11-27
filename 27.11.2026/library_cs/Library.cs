using System;
using System.Collections.Generic;
using LibraryApp.Exceptions;
using LibraryApp.Models;

namespace LibraryApp
{
    public class Library
    {
        private List<Publication> _items = new List<Publication>();

        private Publication FindByNumber(int invNumber)
        {
            foreach (Publication item in _items)
            {
                if (item.InvNumber == invNumber)
                {
                    return item;
                }
            }
            throw new PublicationNotFoundException($"инв. номер {invNumber}");
        }

        public void Add(Publication publication)
        {
            foreach (Publication item in _items)
            {
                if (item.InvNumber == publication.InvNumber)
                {
                    throw new DuplicateInventoryException(publication.InvNumber);
                }
            }
            _items.Add(publication);
        }

        public List<Publication> GetAll()
        {
            return new List<Publication>(_items);
        }

        public List<Publication> Search(string title)
        {
            List<Publication> found = new List<Publication>();
            foreach (Publication item in _items)
            {
                if (item.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    found.Add(item);
                }
            }
            if (found.Count == 0)
            {
                throw new PublicationNotFoundException($"название «{title}»");
            }
            return found;
        }

        public void Issue(int invNumber)
        {
            Publication item = FindByNumber(invNumber);
            if (!item.Available)
            {
                throw new AlreadyIssuedException(invNumber);
            }
            item.Issue();
        }

        public void GiveBack(int invNumber)
        {
            Publication item = FindByNumber(invNumber);
            if (item.Available)
            {
                throw new NotIssuedException(invNumber);
            }
            item.GiveBack();
        }

        public void Remove(int invNumber)
        {
            Publication item = FindByNumber(invNumber);
            _items.Remove(item);
        }

        public void GetStats(out int total, out int available, out int issued)
        {
            total = _items.Count;
            available = 0;
            foreach (Publication item in _items)
            {
                if (item.Available)
                {
                    available++;
                }
            }
            issued = total - available;
        }
    }
}
