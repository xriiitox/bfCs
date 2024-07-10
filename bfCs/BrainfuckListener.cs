using System.Drawing;
using System.Net;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using static BrainfuckParser;
namespace bfCs;

public class BrainfuckListener : BrainfuckBaseListener
{
    public byte[] ByteArr = new byte[30000];
    public int Pointer = 0;

    public void EnterEveryRule(ParserRuleContext prc) { }

    public void ExitEveryRule(ParserRuleContext prc) { }
    
    public void VisitErrorNode(IErrorNode en) { }
    
    public void VisitTerminal(ITerminalNode tn) { }

    public void EnterFile_(File_Context fc)
    {
        
    }

    public void ExitFile_(File_Context fc)
    {
        
    }

    public void EnterStatement(StatementContext sc)
    {
        
    }

    public new void ExitStatement(StatementContext sc)
    {
        if (ByteArr[Pointer] != 0 && sc.GetText().Last() == ']')
        {
            EnterStatement(sc); // maybe this will work: re-enter statement
        }
    }

    public new void EnterOpcode(OpcodeContext oc)
    {
        switch (oc.GetText())
        {
            case ">":
                Pointer = Pointer == 29999 ? 0 : Pointer + 1;
                break;
            case "<":
                Pointer = Pointer == 0 ? 29999 : Pointer - 1;
                break;
            case "+":
                ByteArr[Pointer]++; // wrapping is implemented in byte already
                break;
            case "-":
                ByteArr[Pointer]--;
                break;
            case ".":
                Console.Write((char)ByteArr[Pointer]);
                break;
            case ",":
                ByteArr[Pointer] = Convert.ToByte(Console.ReadLine());
                break;
        }
    }
}