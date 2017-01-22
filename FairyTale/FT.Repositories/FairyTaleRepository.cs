﻿using FT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Repositories {
    public class FairyTaleRepository {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IList<FairyTale> GetAll(string term, int skip, int take)
        {
            // TODO: extract to str.Contains(string, StringComparison) extension
            return data
                .Where(ft=>ft.Title.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public IList<FairyTale> GetAll<T>(Func<FairyTale, T> orderBy, int skip, int take) {
            // TODO: extract to str.Contains(string, StringComparison) extension
            return data
                .OrderBy(orderBy)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FairyTale Get(int id) {
            return data.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public int Count() {
            return data.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public int Count(string term)
        {
            // TODO: extract to str.Contains(string, StringComparison) extension
            return data.Count(ft => ft.Title.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }

        private IEnumerable<FairyTale> data = new List<FairyTale>()
        {
            new FairyTale
            {
                Id = 1,
                Title = "Даша и медведь",
                Text = "Жил себе старик со старухою. Было у них три сына: двое умные, а третий — дурачок. Умных они и жалеют, каждую неделю старуха им чистые рубашки дает, а дурачка все ругают, смеются над ним, — а он знай ...",
                Tags = new List<Tag>() {
                    new Tag() {
                        Id = 1,
                        Title = "Русские-народные"
                    }
                },
                CreatedAtUtc = DateTime.UtcNow
            },
            new FairyTale
            {
                Id = 2,
                Title = "Названый отец ",
                Text = "Oстались три брата сиротами — ни отца, ни матери. Ни кола ни двора. Вот и пошли они по селам, по хуторам в работники наниматься. Идут и думают: Эх, кабы наняться к доброму хозяину! Глядь, старичок ...",
                Tags = new List<Tag>() {
                    new Tag() {
                        Id = 2,
                        Title = "Бытовые"
                    },
                    new Tag() {
                        Id = 3,
                        Title = "Про животных"
                    }
                },
                CreatedAtUtc = DateTime.UtcNow
            },
            new FairyTale
            {
                Id = 3,
                Title = "Волшебная палка",
                Text = "Жили себе дед и баба. Всю жизнь бедовали и остались на старости с одной коровенкой, которую и кормить-то было нечем. А на пастбище водить ее трудно было старикам. Привел дед корову на ярмарку. ...",
                Tags = new List<Tag>(),
                CreatedAtUtc = DateTime.UtcNow
            },
                        new FairyTale
            {
                Id = 1,
                Title = "Даша и медведь",
                Text = "Жил себе старик со старухою. Было у них три сына: двое умные, а третий — дурачок. Умных они и жалеют, каждую неделю старуха им чистые рубашки дает, а дурачка все ругают, смеются над ним, — а он знай ...",
                Tags = new List<Tag>(),
                CreatedAtUtc = DateTime.UtcNow
            },
            new FairyTale
            {
                Id = 2,
                Title = "Названый отец ",
                Text = "Oстались три брата сиротами — ни отца, ни матери. Ни кола ни двора. Вот и пошли они по селам, по хуторам в работники наниматься. Идут и думают: Эх, кабы наняться к доброму хозяину! Глядь, старичок ...",
                Tags = new List<Tag>(),
                CreatedAtUtc = DateTime.UtcNow
            },
            new FairyTale
            {
                Id = 3,
                Title = "Волшебная палка",
                Text = "Жили себе дед и баба. Всю жизнь бедовали и остались на старости с одной коровенкой, которую и кормить-то было нечем. А на пастбище водить ее трудно было старикам. Привел дед корову на ярмарку. ...",
                Tags = new List<Tag>(),
                CreatedAtUtc = DateTime.UtcNow
            },
                        new FairyTale
            {
                Id = 1,
                Title = "Даша и медведь",
                Text = "Жил себе старик со старухою. Было у них три сына: двое умные, а третий — дурачок. Умных они и жалеют, каждую неделю старуха им чистые рубашки дает, а дурачка все ругают, смеются над ним, — а он знай ...",
                Tags = new List<Tag>(),
                CreatedAtUtc = DateTime.UtcNow
            },
            new FairyTale
            {
                Id = 2,
                Title = "Названый отец ",
                Text = "Oстались три брата сиротами — ни отца, ни матери. Ни кола ни двора. Вот и пошли они по селам, по хуторам в работники наниматься. Идут и думают: Эх, кабы наняться к доброму хозяину! Глядь, старичок ...",
                Tags = new List<Tag>(),
                CreatedAtUtc = DateTime.UtcNow
            },
            new FairyTale
            {
                Id = 3,
                Title = "Волшебная палка",
                Text = "Жили себе дед и баба. Всю жизнь бедовали и остались на старости с одной коровенкой, которую и кормить-то было нечем. А на пастбище водить ее трудно было старикам. Привел дед корову на ярмарку. ...",
                Tags = new List<Tag>(),
                CreatedAtUtc = DateTime.UtcNow
            },
                        new FairyTale
            {
                Id = 1,
                Title = "Даша и медведь",
                Text = "Жил себе старик со старухою. Было у них три сына: двое умные, а третий — дурачок. Умных они и жалеют, каждую неделю старуха им чистые рубашки дает, а дурачка все ругают, смеются над ним, — а он знай ...",
                Tags = new List<Tag>(),
                CreatedAtUtc = DateTime.UtcNow
            },
            new FairyTale
            {
                Id = 2,
                Title = "Названый отец ",
                Text = "Oстались три брата сиротами — ни отца, ни матери. Ни кола ни двора. Вот и пошли они по селам, по хуторам в работники наниматься. Идут и думают: Эх, кабы наняться к доброму хозяину! Глядь, старичок ...",
                Tags = new List<Tag>(),
                CreatedAtUtc = DateTime.UtcNow
            },
            new FairyTale
            {
                Id = 3,
                Title = "Волшебная палка",
                Text = "Жили себе дед и баба. Всю жизнь бедовали и остались на старости с одной коровенкой, которую и кормить-то было нечем. А на пастбище водить ее трудно было старикам. Привел дед корову на ярмарку. ...",
                Tags = new List<Tag>(),
                CreatedAtUtc = DateTime.UtcNow
            }
        };
    }
}