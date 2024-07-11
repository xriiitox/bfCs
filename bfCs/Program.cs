using System.Text;

namespace bfCs;

class Program
{
    static byte[] _arr = new byte[30000];
    static int _pointer = 0;
    static string _program = "";
    public static void Main(string[] args)
    {
        _program = File.ReadAllText(args[0]).ReplaceLineEndings(String.Empty).Trim();

        RunCmd();
    }

    private static void RunCmd()
    {
        var jumpBackIndices = new List<int>();


        for (int i = 0; i < _program.Length; i++)
        {
            switch (_program[i])
            {
                case '>':
                    _pointer = _pointer == 29999 ? 0 : _pointer + 1;
                    break;
                case '<':
                    _pointer = _pointer == 0 ? 29999 : _pointer - 1;
                    break;
                case '+':
                    if (_arr[_pointer] == byte.MaxValue)
                    { 
                        _arr[_pointer] = 0;
                    }
                    _arr[_pointer]++;
                    break;
                case '-':
                    if (_arr[_pointer] == byte.MinValue)
                    {
                        _arr[_pointer] = 255;
                    }
                    _arr[_pointer]--;
                    break;
                case '.':
                    //char yeah = Encoding.ASCII.GetChars([_byteArr[_pointer]])[0];
                    Console.Write(char.ConvertFromUtf32(_arr[_pointer]));
                    break;
                case ',':
                    var input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        _arr[_pointer] = 0;  // Handle EOF by setting cell to 0
                    }
                    else
                    {
                        _arr[_pointer] = Encoding.ASCII.GetBytes(input)[0];
                    }
                    break;
                case '[':
                    // If the current cell is 0, skip to the matching ]
                    if (_arr[_pointer] == 0)
                    {
                        int openBrackets = 1;
                        while (openBrackets > 0)
                        {
                            i++;
                            if (_program[i] == '[') openBrackets++;
                            if (_program[i] == ']') openBrackets--;
                        }
                    }
                    else
                    {
                        jumpBackIndices.Add(i);
                    }
                    break;
                case ']':
                    if (_arr[_pointer] != 0)
                    {
                        i = jumpBackIndices.Last();
                        break;
                    }
                    jumpBackIndices.RemoveAt(jumpBackIndices.Count - 1);

                    break;
                default:
                    continue;
            }
        }
    }
}