using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace app.Services;

/// <summary>
/// Rewrites a student's parsed script so every loop body starts with a call to
/// <see cref="ExecutionGuard.CheckDeadline"/> — see that class for why this
/// source-level instrumentation, rather than anything external, is the only way
/// to stop a runaway loop in a single-threaded WASM host. Applied to the syntax
/// tree in <see cref="CSharpRunner"/> before compiling, so the student never
/// sees this code and it runs as if they'd written it themselves.
/// </summary>
public sealed class LoopGuardRewriter : CSharpSyntaxRewriter
{
    private static readonly StatementSyntax GuardStatement =
        SyntaxFactory.ParseStatement("global::app.Services.ExecutionGuard.CheckDeadline();");

    public override SyntaxNode? VisitForStatement(ForStatementSyntax node)
    {
        var visited = (ForStatementSyntax)base.VisitForStatement(node)!;
        return visited.WithStatement(GuardBody(visited.Statement));
    }

    public override SyntaxNode? VisitWhileStatement(WhileStatementSyntax node)
    {
        var visited = (WhileStatementSyntax)base.VisitWhileStatement(node)!;
        return visited.WithStatement(GuardBody(visited.Statement));
    }

    public override SyntaxNode? VisitDoStatement(DoStatementSyntax node)
    {
        var visited = (DoStatementSyntax)base.VisitDoStatement(node)!;
        return visited.WithStatement(GuardBody(visited.Statement));
    }

    public override SyntaxNode? VisitForEachStatement(ForEachStatementSyntax node)
    {
        var visited = (ForEachStatementSyntax)base.VisitForEachStatement(node)!;
        return visited.WithStatement(GuardBody(visited.Statement));
    }

    private static StatementSyntax GuardBody(StatementSyntax body)
    {
        // A single-statement body ("for (...) DoThing();") has to become a
        // block to fit two statements; a body that's already a block just gets
        // the guard call inserted first.
        if (body is BlockSyntax block)
        {
            return block.WithStatements(block.Statements.Insert(0, GuardStatement));
        }
        return SyntaxFactory.Block(GuardStatement, body);
    }
}
