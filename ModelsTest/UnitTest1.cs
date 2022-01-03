using Films.Classes.BingSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Films.MVVMLogic.Models.ImagesScroll;

namespace ModelsTest
{
    [TestClass]
    public class ImagesTest
    {
        [TestMethod]
        public void Images_Download()
        {
            FilmBuilder images = new FilmBuilder();

            images.CreatingFilms();
        }
    }
}
