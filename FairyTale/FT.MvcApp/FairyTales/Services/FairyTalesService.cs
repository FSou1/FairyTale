using System;
using System.Collections.Generic;
using System.Linq;
using FT.Entities;
using FT.MvcApp.FairyTales.Models;

namespace FT.MvcApp.FairyTales.Services
{
    public class FairyTalesService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IndexViewModel BuildIndexViewModel()
        {
            var model = new IndexViewModel()
            {
                Title = "Главная страница",
                FairyTales = data
            };

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SingleViewModel BuildSingleViewModel(int id)
        {
            var model = new SingleViewModel
            {
                Title = "Просмотр",
                FairyTale = data.SingleOrDefault(t=>t.Id == id)
            };

            return model;
        }

        private static IList<FairyTale> data = new List<FairyTale>()
        {
            new FairyTale
            {
                Id = 1,
                Title = "First",
                CreatedAtUtc = DateTime.UtcNow
            },
            new FairyTale
            {
                Id = 2,
                Title = "Second",
                CreatedAtUtc = DateTime.UtcNow
            },
            new FairyTale
            {
                Id = 3,
                Title = "Third",
                CreatedAtUtc = DateTime.UtcNow
            }
        };
    }
}