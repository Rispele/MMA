using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PrivateFieldNamesExposingGenerator;

[Generator]
public sealed class FieldNameGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Добавляем исходник с атрибутом
        context.RegisterPostInitializationOutput(static ctx =>
        {
            ctx.AddSource("GenerateFieldNamesAttribute.g.cs", """
                using System;
                namespace PrivateFieldNamesExposingGenerator.Attributes
                {
                    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
                    public sealed class GenerateFieldNamesAttribute : Attribute { }
                }
                """);
        });

        // Отбираем классы, помеченные [GenerateFieldNames]
        var classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => node is ClassDeclarationSyntax cds && HasAttribute(cds),
                static (ctx, _) => (ClassDeclarationSyntax)ctx.Node
            )
            .Where(static c => c is not null);

        // Комбинируем с Compilation
        var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndClasses, static (spc, source) =>
        {
            var (compilation, classDecls) = source;

            foreach (var classDecl in classDecls.Distinct())
            {
                var model = compilation.GetSemanticModel(classDecl.SyntaxTree);
                if (model.GetDeclaredSymbol(classDecl) is not INamedTypeSymbol classSymbol)
                    continue;

                var hasAttribute = classSymbol
                    .GetAttributes()
                    .Any(a => a.AttributeClass?.ToDisplayString() == "PrivateFieldNamesExposingGenerator.Attributes.GenerateFieldNamesAttribute");
                if (!hasAttribute)
                    continue;

                var privateFields = classSymbol.GetMembers()
                    .OfType<IFieldSymbol>()
                    .Where(f => f.DeclaredAccessibility == Accessibility.Private)
                    .Where(f => !f.IsImplicitlyDeclared)
                    .ToImmutableArray();

                if (privateFields.IsDefaultOrEmpty)
                    continue;

                var ns = classSymbol.ContainingNamespace.IsGlobalNamespace
                    ? null
                    : classSymbol.ContainingNamespace.ToDisplayString();

                var sb = new StringBuilder();
                if (!string.IsNullOrEmpty(ns))
                    sb.AppendLine($"namespace {ns};");
                sb.AppendLine();
                sb.AppendLine($"public static class {classSymbol.Name}FieldNames");
                sb.AppendLine("{");

                foreach (var field in privateFields)
                {
                    var name = Normalize(field.Name);
                    sb.AppendLine($"    public const string {name} = \"{field.Name}\";");
                }

                sb.AppendLine("}");

                spc.AddSource($"{classSymbol.Name}.FieldNames.g.cs", sb.ToString());
            }
        });
    }

    private static bool HasAttribute(ClassDeclarationSyntax cds)
    {
        return cds.AttributeLists.Count > 0;
    }

    private static string Normalize(string fieldName)
    {
        var n = fieldName.TrimStart('_');
        return char.ToUpper(n[0]) + n.Substring(1);
    }
}
