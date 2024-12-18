using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Models;

namespace True.Extensions
{
    public static class PropertyExceptionHandler
    {
        // Get Image URL
        /// <summary>
        /// Get the Image URL Or return # Grantee no exception thrown
        /// </summary>
        /// <param name="Element">The Element that has the Image</param>
        /// <param name="Property">The Image Property name</param>
        /// <returns>return The Image URL as string</returns>


        public static string GetImageURL(this IPublishedElement Element, string Property, string defaultvalue = "")

        {
            try
            {
                if (Element.HasProperty(Property) && Element.HasValue(Property))
                {
                    return Element.Value<MediaWithCrops>(Property).Url();
                }
            }
            catch (Exception) { }

            return defaultvalue;

        }

        public static Link GetLink(this IPublishedElement Element, string Property, string ItemLinkUrl = "#", string Target = "_self")
        {
            var ItemLink = new Link
            {
                Url = ItemLinkUrl,
                Target = Target
            };

            try
            {
                if (Element.HasProperty(Property) && Element.HasValue(Property))
                {
                    return Element.Value<Link>(Property) ?? ItemLink;
                }

            }
            catch (Exception) { }
            return ItemLink;
        }
        // Get Multi URL 
        /// <summary>
        /// Get the Multi URL Or return # Grantee no exception thrown
        /// </summary>
        /// <param name="Element">The Element that has the Links Array</param>
        /// <param name="Property">The Multi URL Property name</param>
        /// <returns>return The Multi URL as Array of Links</returns>

        public static Link[] GetLinkArray(this IPublishedElement Element, string Property, string ItemLinkUrl = "#", string Target = "_self")
        {
            Link[] Links = Enumerable.Empty<Link>().ToArray();
            try
            {
                var itemLink = new Link
                {
                    Name = string.Empty,
                    Target = Target,
                    Url = ItemLinkUrl
                };
                //itemLink.Url = ItemLinkUrl;
                //itemLink.Target = Target;


                if (Element.HasProperty(Property) && Element.HasValue(Property) && Links != null)
                {
                    Links = Element.Value<IEnumerable<Link>>(Property).ToArray();
                    for (int i = 0; i < Links.Length; i++)
                    {
                        if (Links[i] == null)
                        {
                            Links[i] = itemLink;
                        }
                        else
                        {
                            Links[i].Url ??= ItemLinkUrl;
                            Links[i].Target ??= Target;
                            //if (Links[i].Url == null)
                            //{
                            //    Links[i].Url = ItemLinkUrl;
                            //}
                            //if (Links[i].Target == null)
                            //{
                            //    Links[i].Target = Target;
                            //}

                        }

                    }


                }
            }

            catch (Exception) { }
            return Links;


        }
        // Get Text Value
        /// <summary>
        /// Get the Value of the string Or return empty string Grantee no exception thrown
        /// </summary>
        /// <param name="Element">The Element that has the Text</param>
        /// <param name="Property">The Text Property name</param>
        /// <returns>return The Text or Empty string</returns>

        public static string GetText(this IPublishedElement Element, string Property, int startIndex = 0, int Length = 0)
        {
            string Value = "";
            try
            {
                if (Element.HasProperty(Property) && Element.HasValue(Property))
                {
                    if (startIndex == 0 && Length == 0)
                        Value = Element?.Value<string>(Property) ?? string.Empty;

                    else if (Element?.Value<string>(Property)?.Length >= startIndex + Length)
                    { Value = Element?.Value<string>(Property)?.Substring(startIndex, Length) ?? string.Empty; }
                }
                else
                {
                    Value = Element?.Value<string>(Property) ?? string.Empty;
                }
            }

            catch (Exception) { }
            return Value;
        }
        // Get Date Value
        /// <summary>
        /// Get the Value of the string Or return empty string Grantee no exception thrown
        /// </summary>
        /// <param name="Element">The Element that has the Text</param>
        /// <param name="Property">The Text Property name</param>
        /// <returns>return The Text or Empty string</returns>

        public static DateTime GetDate(this IPublishedElement Element, string Property)
        {
            try
            {
                if (Element.HasProperty(Property) && Element.HasValue(Property))
                {
                    return Element.Value<DateTime>(Property);
                }

            }
            catch (Exception) { }
            return new DateTime(day: 30, month: 4, year: 1990);


        }

        // Get Elements Array
        /// <summary>
        /// Get Array of the IPublishedElement Or return Empty one Grantee no exception thrown
        /// </summary>
        /// <param name="Element">The Element or The Page that has the Block List Property</param>
        /// <param name="Property">The Block List Property name</param>
        /// <returns>return The Array of IPublishedElement</returns>

        public static IPublishedElement[] GetElementsArray(this IPublishedElement Element, string Property)
        {
            try
            {

                if (Element.HasProperty(Property) && Element.HasValue(Property))
                {
                    return Element
                              .Value<IEnumerable<BlockListItem>>(Property)
                              .Select(x => x.Content)
                              .Where(x => x.IsVisible())
                              .ToArray();
                }
            }

            catch (Exception) { }
            return Enumerable.Empty<IPublishedElement>().ToArray();
        }
        // Get Published Content Array
        /// <summary>
        /// Get Array of the Published Content Or return empty one Grantee no exception thrown
        /// </summary>
        /// <param name="Element">The Element that has the Array</param>
        /// <param name="Property">The List/Array Property name</param>
        /// <returns>return The Array of Published Content</returns>
        public static IPublishedContent[] GetPublishedContentArray(this IPublishedContent PublishedContent, string Property)
        {
            try
            {
                if (PublishedContent.HasProperty(Property) && PublishedContent.HasValue(Property))
                {
                    return PublishedContent.Value<IEnumerable<IPublishedContent>>(Property).ToArray();
                }
            }
            catch (Exception) { }
            return Enumerable.Empty<IPublishedContent>().ToArray();
        }
        // Get Block List
        /// <summary>
        /// Get Array of the Block List Items Or return empty one Grantee no exception thrown
        /// </summary>
        /// <param name="Element">The Element that has the BlockList</param>
        /// <param name="Property">The Block List Property name</param>
        /// <returns>return The Array of Block List Items</returns>
        public static BlockListItem[] GetBlockList(this IPublishedElement Element, string Property)
        {
            BlockListItem[] Elements = { };
            try
            {
                if (Element.HasValue(Property))
                {
                    Elements = Element.Value<IEnumerable<BlockListItem>>(Property).ToArray();
                }
            }
            catch (Exception) { }
            return Elements;
        }

        // Get Array of Strings 
        /// <summary>
        /// Get Array of the Strings Or return empty one Grantee no exception thrown
        /// </summary>
        /// <param name="Element">The Element that has the Array</param>
        /// <param name="Property">The List/Array Property name</param>
        /// <returns>return The Array of String</returns>
        public static string[] GetStringArray(this IPublishedElement Element, string Property)
        {
            try
            {
                if (Element.HasProperty(Property) && Element.HasValue(Property))
                {
                    return Element.Value<IEnumerable<string>>(Property).ToArray();
                }
            }
            catch (Exception) { }
            return Enumerable.Empty<string>().ToArray();
        }

        // Get Text Value
        /// <summary>
        /// Get the Value of the Numeric Value Or return 0 Grantee no exception thrown
        /// </summary>
        /// <param name="Element">The Element that has the Numeric Value</param>
        /// <param name="Property">The Numeric Property name</param>
        /// <returns>return The Numeric Value or 0 </returns>
        public static int GetNumericValue(this IPublishedElement Element, string Property)
        {
            try
            {
                if (Element.HasProperty(Property) && Element.HasValue(Property))
                {
                    return Element?.Value<int>(Property) ?? 0;
                }
            }
            catch (Exception) { }
            return 0;
        }

        public static bool GetBoolValue(this IPublishedElement Element, string Property)
        {
            try
            {
                if (Element.HasProperty(Property) && Element.HasValue(Property))
                {
                    return Element.Value<bool>(Property);
                }
            }
            catch (Exception) { }
            return false;
        }

    }
}
