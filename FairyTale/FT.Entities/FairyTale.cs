using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FT.Entities {
    public class FairyTale {
        public int Id { get; set; }        

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Превью
        /// </summary>
        public string Teaser => FormatTeaser(Text, 1);

        /// <summary>
        /// HTML содержимое
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Список тегов
        /// </summary>
        public IList<Tag> Tags { get; set; }

        /// <summary>
        /// Дата добавления
        /// </summary>
        public DateTime CreatedAtUtc { get; set; }

        public virtual string FormatTeaser(string text, int lines) {
            var parts = Text
                .Split(new[] {"<p>", "</p>"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(part => $"<p>{part}</p>");

            return string.Join(string.Empty, parts.Take(lines));
        }
    }
}
