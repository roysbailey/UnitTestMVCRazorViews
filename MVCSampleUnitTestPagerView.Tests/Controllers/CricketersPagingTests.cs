using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCSampleUnitTestPagerView.Models;
using System.Collections.Generic;
using ASP;
using System.Linq;
using PagedList;
using RazorGenerator.Testing;
using HtmlAgilityPack;

namespace MVCSampleUnitTestPagerView.Tests
{
    [TestClass]
    public class CricketersPagingTests
    {
        private readonly int pageSize = 3;

        [TestMethod]
        public void Single_page_of_results_will_not_have_next_or_prev_page_links()
        {
            // Arrange
            var indexView = new _Views_Cricketers_Index_cshtml();
            var cricketers = getCricketersForTest(1);
            var model = getPagedList(cricketers);

            // Act
            var htmlDom =  indexView.RenderAsHtml(model);

            // Assert
            var navList = getPaginationList(htmlDom);
            Assert.IsFalse(HasNextPageLink(navList));
            Assert.IsFalse(HasPrevPageLink(navList));
        }

        [TestMethod]
        public void Two_pages_of_results_will_have_next_page_and_page2_links()
        {

            // Arrange
            var indexView = new _Views_Cricketers_Index_cshtml();
            var cricketers = getCricketersForTest(4);
            var model = getPagedList(cricketers);

            // Act
            var htmlDom = indexView.RenderAsHtml(model);

            // Assert
            var navList = getPaginationList(htmlDom);
            Assert.IsTrue(HasNextPageLink(navList));
            Assert.IsTrue(HasPageLink(navList, 2));
        }


        [TestMethod]
        public void Two_pages_of_results_on_page2_will_have_prev_page_and_page1_links()
        {

            // Arrange
            var indexView = new _Views_Cricketers_Index_cshtml();
            var cricketers = getCricketersForTest(4);
            var model = getPagedList(cricketers, 2);

            // Act
            var htmlDom = indexView.RenderAsHtml(model);

            // Assert
            var navList = getPaginationList(htmlDom);
            Assert.IsTrue(HasPrevPageLink(navList));
            Assert.IsTrue(HasPageLink(navList, 1));
        }



        private List<CricketerProfile> getCricketersForTest(int count = 1)
        {
            var profiles = new List<CricketerProfile>();

            for (int i = 0; i < count; i++)
                profiles.Add(new CricketerProfile { Id = i, Name = string.Format("Mr {0}", i), TestRuns = i * 1000 });

            return profiles;
        }


        private StaticPagedList<CricketerProfile> getPagedList(List<CricketerProfile> profiles, int page = 1)
        {
            var pageOfCricketers = profiles.Skip((page - 1) * pageSize).Take(pageSize);
            var pagedList = new StaticPagedList<CricketerProfile>(pageOfCricketers, Convert.ToInt32(page), Convert.ToInt32(pageSize), profiles.Count);
            return pagedList;
        }

        private bool HasPageLink(HtmlNode navList, int pageNum)
        {
            var pageAsString = pageNum.ToString();
            var hasLink = navList.Descendants("li").Count(n => n.InnerText == pageAsString) == 1;
            return hasLink;

        }

        private bool HasNextPageLink(HtmlNode navList)
        {
            var hasLink = navList.Descendants("li").Count(n => n.InnerText == "»") == 1;
            return hasLink;
        }

        private bool HasPrevPageLink(HtmlNode navList)
        {
            var hasLink = navList.Descendants("li").Count(n => n.InnerText == "«") == 1;
            return hasLink;
        }
        
        private HtmlNode getPaginationList(HtmlDocument htmlDom)
        {
            var navList = htmlDom.DocumentNode.Descendants("ul")
                .Where(x => x.Attributes["class"].Value == "pagination")
                .FirstOrDefault();

            return navList;
        }
    }
}
