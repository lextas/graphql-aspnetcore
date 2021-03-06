﻿using GraphQlDemo.Data.Repositories;
using GraphQlDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlDemo.Data.InMemory.Repositories
{
    public class BookRepository : IBookRepository
    {
        private static List<Book> _books;

        /// <summary>
        /// Setup sample data
        /// </summary>
        public static void Initialize()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            using (var reader = new StreamReader($"{path}/books.json"))
            {
                var json = reader.ReadToEnd();
                _books = JsonConvert.DeserializeObject<List<Book>>(json);
            }
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            _books.Add(book);
            return await Task.FromResult(book);
        }

        public async Task<Book> GetBookByIsbnAsync(string isbn)
        {
            // async because normally you would call a database or external system
            return await Task.FromResult(_books.FirstOrDefault(m => m.Isbn == isbn));
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId)
        {
            // async because normally you would call a database or external system
            return await Task.FromResult(_books.Where(m => m.Author.Id == authorId));
        }

        public async Task<IEnumerable<Book>> GetBooksByPublisherIdAsync(int publisherId)
        {
            // async because normally you would call a database or external system
            return await Task.FromResult(_books.Where(m => m.Publisher.Id == publisherId));
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            // async because normally you would call a database or external system
            return await Task.FromResult(_books.AsEnumerable());
        }
    }
}
