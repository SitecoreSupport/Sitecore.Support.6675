namespace Sitecore.Support.XA.Foundation.Theming.Pipelines.ResolveTokens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data.Items;
    using Sitecore.XA.Foundation.IoC;
    using Sitecore.XA.Foundation.Multisite;

    public class ResolveThemingTokens : Sitecore.XA.Foundation.Theming.Pipelines.ResolveTokens.ResolveThemingTokens
    {
        protected override string ReplaceWithCompatibleThemes(string location, string token, Item contextItem)
        {
            if (location.IndexOf(token, StringComparison.OrdinalIgnoreCase) <= 0)
            {
                return location;
            }
            Item[] availableThemeItems = this.GetAvailableThemeItems(ServiceLocator.Current.Resolve<IMultisiteContext>().GetSiteItem(contextItem));

            #region Added code
            List<string> themePaths = new List<string>();
            foreach (Item theme in availableThemeItems)
            {
                if (theme.Name.Contains('-'))
                {
                    themePaths.Add(theme.Paths.Path.Replace(theme.Name, "#" + theme.Name + "#"));
                }
                else
                {
                    themePaths.Add(theme.Paths.Path);
                }
            } 
            #endregion

            if (availableThemeItems.Length == 0)
            {
                return "query:/sitecore/media library/Themes//*[@@templatename='Theme']";
            }
            #region Changed code
            return ("query:" + string.Join("|", themePaths)); 
            #endregion
        }
    }
}