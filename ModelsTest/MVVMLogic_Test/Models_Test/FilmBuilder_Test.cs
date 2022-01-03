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
        private List<Film> _films = new List<Film>();

        [TestMethod]
        private async Task CreatingFilms_Test()
        {
            FilmParser filmBuilder = new FilmParser();

            filmBuilder.OnFilm += film => _films.Add(film);
            await filmBuilder.CreatingFilms();

            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            myStopwatch.Start(); 
            
            int currentCount = 0;
            while (_films.Count != 5)
            {
                
                if (currentCount < _films.Count)
                {
                    currentCount = _films.Count;
                    myStopwatch.Restart();
                }
                else if (myStopwatch.ElapsedMilliseconds > 5000)
                    break;
            }
            myStopwatch.Stop();
        }

        [TestMethod]
        public async Task CreatingFilms_are_equal_count()
        {
            await CreatingFilms_Test();

            Assert.AreEqual(5, _films.Count);
        }

        [TestMethod]
        public async Task CreatingFilms_all_items_notnull()
        {
            await CreatingFilms_Test();

            CollectionAssert.AllItemsAreNotNull(_films);
        }
    }
}