namespace ClearProgram
{
    public static class Answer
    {
        public const string No = "NO";
        public const string Yes = "YES";
    }
    public class InputReader
    {
        private readonly StreamReader _input;

        public InputReader(StreamReader input)
        {
            this._input = input;
        }
        public int ReadInt()
        {
            return int.Parse(_input.ReadLine());
        }

        public string ReadString()
        {
            return _input.ReadLine();
        }

        public (string, int)[] ReadProducts(int count)
        {
            var products = new (string, int)[count];
            for (int i = 0; i < count; i++)
            {
                string[] temp = _input.ReadLine().Split(' ');
                products[i] = (temp[0], int.Parse(temp[1]));
            }
            return products;
        }

        public List<(string, int)> ReadResultLine()
        {
            string line = ReadString();
            var result = new List<(string, int)>();
            var resultProd = line.Split(',');
            foreach (var item in resultProd)
            {
                var temp = item.Split(':');
                if (temp.Length != 2 || !int.TryParse(temp[1], out int value))
                {
                    throw new InvalidDataException("Invalid result format.");
                }
                if (temp[1].StartsWith('0') && value > 0 || temp[1].StartsWith("-0") && value < 0)
                {
                    throw new InvalidDataException("Invalid result format.");
                }
                
                result.Add((temp[0], value));
            }
            return result;
        }
    }
    
    public static class ProductValidator
    {
        public static string ValidateProducts((string, int)[] products, List<(string, int)> result)
        {
            var uniqPrice = new HashSet<int>(products.Select(x => x.Item2));

            if (result.Count != uniqPrice.Count)
            {
                return Answer.No;
            }

            for (int j = 0; j < result.Count; j++)
            {
                var t = result[j].Item2;
                if (products.FirstOrDefault(x => x == result[j]) == default)
                {
                    return Answer.No;
                }

                uniqPrice.Remove(t);
            }

            if (uniqPrice.Count > 0)
            {
                return Answer.No;
            }
            return Answer.Yes;
        }
    }

    // Класс для записи результатов в выходной поток
    public class OutputWriter
    {
        private readonly StreamWriter _output;

        public OutputWriter(StreamWriter output)
        {
            _output = output;
        }

        public void WriteResponse(string[] responses)
        {
            foreach (var response in responses)
            {
                _output.WriteLine(response);
            }
        }
    }

    // Основной класс программы
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using var input = new StreamReader(Console.OpenStandardInput());
            using var output = new StreamWriter(Console.OpenStandardOutput());

            var inputReader = new InputReader(input);
            var outputWriter = new OutputWriter(output);

            int col = inputReader.ReadInt();
            string[] responseArr = new string[col];

            for (int i = 0; i < col; i++)
            {
                try
                {
                    int countProducts = inputReader.ReadInt();
                    var products = inputReader.ReadProducts(countProducts);
                    var result = inputReader.ReadResultLine();

                    responseArr[i] = ProductValidator.ValidateProducts(products, result);
                }
                catch (Exception)
                {
                    responseArr[i] = Answer.No;
                }
            }
            outputWriter.WriteResponse(responseArr);
        }
    }
}