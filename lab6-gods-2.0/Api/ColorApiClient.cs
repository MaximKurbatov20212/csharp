using System;
using System.Net.Http;
using System.Threading.Tasks;
using lab1;

public class ColorApiClient
{
    private readonly HttpClient _httpClient;

    public ColorApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CardColor?> GetColor(int port, string endpoint)
    {
        var response = await _httpClient.GetAsync($"http://localhost:{port}/{endpoint}/getcolor");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Error: " + response);
            return null;
        }
        
        var colorString = await response.Content.ReadAsStringAsync();
        return Enum.Parse<CardColor>(colorString);
    }
}
