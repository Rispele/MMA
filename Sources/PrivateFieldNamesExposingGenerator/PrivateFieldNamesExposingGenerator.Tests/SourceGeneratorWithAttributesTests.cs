using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace PrivateFieldNamesExposingGenerator.Tests;

public class SourceGeneratorWithAttributesTests
{
    private const string VectorClassText = """

                                           using PrivateFieldNamesExposingGenerator.Attributes;

                                           namespace Rooms.Domain.Models.Room;

                                           [GenerateFieldNames]
                                           public class Room
                                           {
                                               private int? id = null;
                                               private readonly List<Equipment> equipments = null!;
                                           }
                                           """;

    private const string ExpectedGeneratedClassText = """
                                                      namespace Rooms.Domain.Models.Room;

                                                      public static class RoomFieldNames
                                                      {
                                                          public const string Id = "id";
                                                          public const string Equipments = "equipments";
                                                      }

                                                      """;

    [Fact]
    public void GenerateFieldNameClass_ShouldSuccessfullyGenerate()
    {
        var generator = new FieldNameGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);
        var compilation = CSharpCompilation.Create(nameof(FieldNameGenerator),
            syntaxTrees: [CSharpSyntaxTree.ParseText(VectorClassText)],
            references: [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]);

        var runResult = driver.RunGenerators(compilation).GetRunResult();

        var generatedFileSyntax = runResult.GeneratedTrees.Single(t => t.FilePath.EndsWith("Room.FieldNames.g.cs"));
        Assert.Equal(ExpectedGeneratedClassText, generatedFileSyntax.GetText().ToString(), ignoreLineEndingDifferences: true);
    }
}