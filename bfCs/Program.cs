using System.Text;

namespace bfCs;

class Program
{
    static byte[] _arr = new byte[65536];
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
                    while (string.IsNullOrEmpty(input))
                    {
                        input = Console.ReadLine();
                    }
                    _arr[_pointer] = Encoding.ASCII.GetBytes(input)[0];
                    break;
                case '[':
                    jumpBackIndices.Add(i);
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