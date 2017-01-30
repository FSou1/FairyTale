using System;
using System.Collections.Generic;
using System.Linq;
using FT.Entities;
using FT.MvcApp.FairyTales.Models;
using FT.Repositories;

namespace FT.MvcApp.FairyTales.Services
{
    public class FairyTalesService
    {
        private readonly FairyTaleRepository repository = new FairyTaleRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SingleViewModel BuildSingleViewModel(int id) {
            var fairyTale = repository.Get(id);

            var model = new SingleViewModel
            {
                Title = "Просмотр",
                Description = fairyTale.Description,
                FairyTale = fairyTale
            };

            return model;
        }        
    }
}