using System.Collections.Generic;
using System.Threading.Tasks;
using Films.MVVMLogic.Models;
using Films.MVVMLogic.Models.ImagesScroll;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.MVVMLogic_Test.Models_Test
{
    [TestClass]
    public class FilmBuilder_Test
    {
        [TestMethod]
        public async Task CreatingFilms_are_equal_count()
        {
            List<Film> films = new List<Film>();
            FilmParser filmBuilder = new FilmParser();

            filmBuilder.OnFilm += film => films.Add(film);
            await filmBuilder.CreatingFilms();

            Assert.AreEqual(5, films.Count);
        }
    }
}