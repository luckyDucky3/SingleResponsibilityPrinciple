﻿namespace test;

class Program
{
    static void Main(string[] args)
    {
        using var input = new StreamReader(Console.OpenStandardInput());
        using var output = new StreamWriter(Console.OpenStandardOutput());
        int col = int.Parse(input.ReadLine());
        string[] responseArr = new string[col];
        for (int i = 0; i < col; i++)
        {
            bool response = false;
            int countProducts = int.Parse(input.ReadLine());
            (string, int)[] products = new (string, int)[countProducts];
            for (int j = 0; j < countProducts; j++)
            {
                string[] temp = new string[2];
                temp = input.ReadLine().Split(' ');
                products[j] = (temp[0], int.Parse(temp[1]));
            }
            string line = input.ReadLine();
            List<(string, int)> result = new List<(string, int)>();
            {
                var resultProd = line.Split(',');
                for (int j = 0; j < resultProd.Length; j++)
                {
                    var temp = resultProd[j].Split(':');
                    if (temp.Length != 2)
                    {
                        responseArr[i] = "NO";
                        response = true;
                        break;
                    }
                    if (int.TryParse(temp[1], out int tem))
                    {
                        if (temp[1].StartsWith('0') && tem > 0 || temp[1].StartsWith("-0") && tem < 0)
                        {
                            responseArr[i] = "NO";
                            response = true;
                            break;
                        }
                        if (!response)
                        {
                            result.Add((temp[0], tem));
                        }
                    }
                    else
                    {
                        responseArr[i] = "NO";
                        response = true;
                        break;
                    }
                }
            }
            HashSet<int> uniqProducts = new HashSet<int>();
            if (!response)
            {
                for (int j = 0; j < products.Length; j++)
                {
                    uniqProducts.Add(products[j].Item2);
                }

                if (uniqProducts.Count != result.Count)
                {
                    responseArr[i] = "NO";
                    response = true;
                }
                if (!response)
                {
                    for (int j = 0; j < result.Count; j++)
                    {
                        var t = result[j].Item2;
                        if (products.FirstOrDefault(x => x == result[j]) == default)
                        {
                            responseArr[i] = "NO";
                            response = true;
                            break;
                        }
                        uniqProducts.Remove(t);
                    }
                }
            }
            if (!response && uniqProducts.Count > 0)
            {
                responseArr[i] = "NO";
                continue;
            }
            if (!response)
                responseArr[i] = "YES";
        }
        foreach (var t in responseArr)
        {
            output.WriteLine(t);
        }
    }
}