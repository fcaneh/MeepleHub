using MeepleHub.Infrastructure.ExternalRepositories;

namespace MeepleHub.UnitTests.ExternalRepositories;

public class BggGameParserTests
{
    [Fact]
    public void ParseThingXml_WithValidBggXml_ReturnsGameImportData()
    {
        // Arrange: XML BGG minimal mais complet pour représenter un jeu importable.
        const string xml = """
            <?xml version="1.0" encoding="utf-8"?>
            <items>
              <item type="boardgame" id="174430">
                <image>https://example.com/gloomhaven.jpg</image>
                <name type="primary" value="Gloomhaven" />
                <name type="alternate" value="Gloomhaven: Aventures a Havrenuit" />
                <description>A tactical combat campaign game.</description>
                <yearpublished value="2017" />
                <minplayers value="1" />
                <maxplayers value="4" />
                <minplaytime value="60" />
                <maxplaytime value="120" />
                <minage value="14" />
                <link type="boardgamepublisher" id="27425" value="Cephalofair Games" />
                <statistics>
                  <ratings>
                    <averageweight value="3.919" />
                  </ratings>
                </statistics>
              </item>
            </items>
            """;

        var parser = new BggGameParser();

        // Act: le parser transforme le XML en objet d'import exploitable.
        var result = parser.ParseThingXml(xml);

        // Assert: les informations principales du XML sont correctement extraites.
        Assert.NotNull(result);
        Assert.Equal("Gloomhaven", result.Name);
        Assert.Equal("Cephalofair Games", result.PublisherName);
        Assert.Equal(2017, result.PublishedYear);
        Assert.Equal(1, result.MinPlayers);
        Assert.Equal(4, result.MaxPlayers);
        Assert.Equal(60, result.MinPlayTime);
        Assert.Equal(120, result.MaxPlayTime);
        Assert.Equal(14, result.MinAge);
        Assert.Equal(3.919m, result.Complexity);
        Assert.Single(result.Aliases);
        Assert.Equal("Gloomhaven: Aventures a Havrenuit", result.Aliases[0]);
    }

    [Fact]
    public void ParseThingXml_WithoutPrimaryName_ReturnsNull()
    {
        // Arrange: XML invalide pour notre import, car aucun nom principal n'est fourni.
        const string xml = """
            <items>
              <item type="boardgame" id="174430">
                <name type="alternate" value="Only an alias" />
              </item>
            </items>
            """;

        var parser = new BggGameParser();

        // Act: le parser tente de lire un jeu sans nom primaire.
        var result = parser.ParseThingXml(xml);

        // Assert: sans nom principal, aucun jeu importable ne doit être produit.
        Assert.Null(result);
    }
}
