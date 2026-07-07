using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Application.ExternalInterfaces;
using MeepleHub.Application.ExternalModels.Bgg;
using System.Globalization;
using System.Xml.Linq;

namespace MeepleHub.Infrastructure.ExternalRepositories
{
    public class BggGameParser : IBggGameParser
    {
    
        public BggGameImportData? ParseThingXml(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return null;
            }

            var document = XDocument.Parse(xml);
            var item = document.Root?.Element("item");

            if (item is null)
            {
                return null;
            }

            var name = item.Elements("name").FirstOrDefault(element => element.Attribute("type")?.Value == "primary")?.Attribute("value")?.Value;

            if(string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            return new BggGameImportData
            {
                Name = name,
                Description = item.Element("description")?.Value,
                ImageUrl = item.Element("image")?.Value,
                PublishedYear = ParseNullableInt(item, "yearpublished"),
                PublisherName = item.Elements("link").FirstOrDefault(element => element.Attribute("type")?.Value == "boardgamepublisher")?.Attribute("value")?.Value,
                MinPlayers = ParseInt(item, "minplayers"),
                MaxPlayers = ParseInt(item, "maxplayers"),
                MinPlayTime = ParseInt(item, "minplaytime"),
                MaxPlayTime = ParseInt(item, "maxplaytime"),
                MinAge = ParseInt(item, "minage"),
                Complexity = ParseComplexity(item),
                Aliases = item.Elements("name").Where(element => element.Attribute("type")?.Value == "alternate").Select(element => element.Attribute("value")?.Value).Where(value => !string.IsNullOrWhiteSpace(value)).Select(value => value!).Distinct().ToList()
            };
        }


        private static int ParseInt(XElement item, string elementName)
        {
            var value = item.Element(elementName)?.Attribute("value")?.Value;

            return int.TryParse(value, out var result) ? result : 0;
        }

        private static int? ParseNullableInt(XElement item, string elementName)
        {
            var value = item.Element(elementName)?.Attribute("value")?.Value;

            return int.TryParse(value, out var result) ? result : null;
        }

        private static decimal ParseComplexity(XElement item)
        {
            var value = item.Element("statistics")?.Element("ratings")?.Element("averageweight")?.Attribute("value")?.Value;

            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var result) ? result : 0;
        }
    }
}
