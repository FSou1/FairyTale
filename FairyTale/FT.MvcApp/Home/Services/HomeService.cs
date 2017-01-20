﻿using FT.MvcApp.Home.Models;
using FT.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FT.MvcApp.Home.Services {
    public class HomeService {
        private readonly FairyTaleRepository _repository = new FairyTaleRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IndexViewModel BuildIndexViewModel() {
            var model = new IndexViewModel() {
                Title = "Главная страница",
                RandomFairyTales = _repository.GetAll()
            };

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AboutViewModel BuildAboutViewModel() {
            var model = new AboutViewModel() {
                Title = "О сайте"
            };

            return model;
        }
    }
}