using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace bfCs;

class Program
{
    public static void Main(string[] args)
    {
        ICharStream inputStream = CharStreams.fromPath(args[0]);
        BrainfuckLexer brainfuckLexer = new BrainfuckLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(brainfuckLexer);
        BrainfuckParser brainfuckParser = new BrainfuckParser(commonTokenStream);

        BrainfuckParser.File_Context fileContext = brainfuckParser.file_();
        BrainfuckListener listener = new BrainfuckListener();
        ParseTreeWalker.Default.Walk(listener, fileContext);
    }
}