using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace DispatchEndpoints;

internal class SyntaxReceiver : ISyntaxReceiver
{
    public List<MethodDeclarationSyntax> CandidateMethods { get; } = new();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is MethodDeclarationSyntax methodDeclarationSyntax &&
            methodDeclarationSyntax.AttributeLists.Count > 0
        )
        {
            CandidateMethods.Add(methodDeclarationSyntax);
        }
    }
}