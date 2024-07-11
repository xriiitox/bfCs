namespace bfCs;

class Program
{
    static byte[] _byteArr = new byte[30000];
    static int _pointer = 0;
    static readonly string _program = File.ReadAllText("./program.bf");
    public static void Main()
    {
         // store bracket indices
        
        RunCmd();


    }

    private static void RunCmd()
    {
        var bracketPairLooping = 0; // integer to allow for nested loops
        var jumpBackIndices = new List<int>();


        for (int i = 0; i < _program.Length; i++)
        {
            if (bracketPairLooping == 0)
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
                        _byteArr[_pointer]++; // wrapping is implemented in byte already
                        break;
                    case '-':
                        _byteArr[_pointer]--;
                        break;
                    case '.':
                        Console.Write(char.ConvertFromUtf32(_byteArr[_pointer]));
                        break;
                    case ',':
                        _byteArr[_pointer] = Convert.ToByte(Console.ReadLine());
                        break;
                    case '[':
                        bracketPairLooping++;
                        jumpBackIndices.Add(i);
                        break;
                    case ']':
                        if (_byteArr[_pointer] != 0)
                        {
                            i = jumpBackIndices.Last();
                        }

                        break;
                    default:
                        continue;
                }
            }
            else
            {
                if (_program[i] == '[')
                {
                    bracketPairLooping++;
                    jumpBackIndices.Add(i);
                    continue;
                }

                if (_program[i] == ']')
                {
                    bracketPairLooping--;
                    i = jumpBackIndices.Last() + 1;
                }
            }
        }
    }
}