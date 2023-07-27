using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using espacioValores;

internal class Program
{
    private static void Main(string[] args)
    {
        var url = $"https://api.coindesk.com/v1/bpi/currentprice.json";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return;

                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        Valores val = JsonSerializer.Deserialize<Valores>(responseBody);

                        int aux;

                        Console.WriteLine("Seleccione una moneda:");
                        Console.WriteLine("1. USD");
                        Console.WriteLine("2. EUR");
                        Console.WriteLine("3. GBP");

                        int.TryParse(Console.ReadLine(), out aux);

                        switch (aux)
                        {
                            case 1:
                                Console.WriteLine($"Code: {val.bpi.USD.code}");
                                Console.WriteLine($"Symbol: {WebUtility.HtmlDecode(val.bpi.USD.symbol)}");
                                Console.WriteLine($"Rate: {val.bpi.USD.rate}");
                                Console.WriteLine($"Description: {val.bpi.USD.description}");
                                Console.WriteLine($"Rate_Float: {val.bpi.USD.rate_float}");
                                break;
                            case 2:
                                Console.WriteLine($"Code: {val.bpi.EUR.code}");
                                Console.WriteLine($"Symbol: {WebUtility.HtmlDecode(val.bpi.EUR.symbol)}");
                                Console.WriteLine($"Rate: {val.bpi.EUR.rate}");
                                Console.WriteLine($"Description: {val.bpi.EUR.description}");
                                Console.WriteLine($"Rate_Float: {val.bpi.EUR.rate_float}");
                                break;
                            case 3:
                                Console.WriteLine($"Code: {val.bpi.GBP.code}");
                                Console.WriteLine($"Symbol: {WebUtility.HtmlDecode(val.bpi.GBP.symbol)}");
                                Console.WriteLine($"Rate: {val.bpi.GBP.rate}");
                                Console.WriteLine($"Description: {val.bpi.GBP.description}");
                                Console.WriteLine($"Rate_Float: {val.bpi.GBP.rate_float}");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("No se pudo acceder a la API");
            throw;
        }
    }
}