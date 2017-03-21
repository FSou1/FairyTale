using System.Threading.Tasks;
using System.Web.Mvc;
using FT.MvcApp.AlphaIndex.Models;

namespace FT.MvcApp.AlphaIndex.Controllers
{
    public class AlphaIndexController : Controller
    {
        private static readonly int PerPage = AppPropertyKeys.TalesPerPage;

        public AlphaIndexController()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task Index(IndexParams param)
        {

        }
    }
}