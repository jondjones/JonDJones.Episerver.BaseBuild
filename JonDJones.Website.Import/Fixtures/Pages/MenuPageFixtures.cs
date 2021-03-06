﻿namespace JonDJones.Fixtures.Fixtures.Pages
{
    using System.Linq;

    using EPiServer.Core;

    using JonDJones.Fixtures.Fixtures.Base;
    using JonDJones.Fixtures.Resources;
    using JonDJones.Website.Core.Dependencies.RepositoryDependencies.Interfaces;
    using JonDJones.Website.Core.Pages.MetaPages.Menu;
    using JonDJones.Website.Interfaces;
    using JonDJones.Website.Shared.Helpers;

    public class MenuPageFixtures : FixturePageBase
    {
        public MenuPageFixtures(
            IWebsiteDependencies websiteDependencies,
            IPageTypeServices pageServices,
            IContent homepage)
            : base(websiteDependencies, pageServices, homepage)
        {
        }

        public MenuPage CreatePage(
            string pageName,
            ContentReference parentPageReference,
            PageData contentPage,
            int defaultSortOrder)
        {
            Guard.ValidateObject(parentPageReference);

            var menuPages = PageTypeServices.MenuService.GetChildren(parentPageReference);

            if (menuPages.Any(x => x.Name == pageName))
            {
                return menuPages.FirstOrDefault(x => x.Name == pageName);
            }

            var newPage = PageTypeServices.MenuService.CreateNewEmptyPage(parentPageReference);
            newPage.Name = pageName;
            newPage.MenuContentPage = contentPage.LinkURL;
            newPage.MainMenuTitle = pageName;

            PageTypeServices.MenuService.Save(newPage);

            return newPage;
        }

        public void CreateDummyMenuPages(
            MenuContainerPage menuContainerPage,
            PageData dummyPage)
        {
            CreatePage(
                FixtureConstants.PageNames.DummyPageName,
                menuContainerPage.ContentLink,
                dummyPage,
                10);
        }
    }
}