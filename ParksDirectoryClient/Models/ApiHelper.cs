using System.Threading.Tasks;
using RestSharp;

namespace ParksDirectoryClient.Models
{
  public class ApiHelper
  {
    public static async Task<string> GetAll()
    {
      RestClient client = new RestClient("http://localhost:5000/");
      RestRequest request = new RestRequest($"api/Parks", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }

    public static async Task<string> Get(int id)
    {
      RestClient client = new RestClient("https://localhost:5001/");
      RestRequest request = new RestRequest($"api/Parks/{id}", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }

    public static async void Post(string newPark)
    {
      RestClient client = new RestClient("https://localhost:5001/");
      RestRequest request = new RestRequest($"api/Parks", Method.Post);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newPark);
      await client.PostAsync(request);
    }

    public static async void Put(int id, string newPark)
    {
      RestClient client = new RestClient("https://localhost:5001/");
      RestRequest request = new RestRequest($"api/Parks/{id}", Method.Put);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newPark);
      await client.PutAsync(request);
    }
    
    public static async void Delete(int id)
    {
      RestClient client = new RestClient("https://localhost:5001/");
      RestRequest request = new RestRequest($"api/Parks/{id}", Method.Delete);
      request.AddHeader("Content-Type", "application/json");
      await client.DeleteAsync(request);
    }

    public static async Task<string> RandomPark()
    {
      RestClient client = new RestClient("https://localhost:5001/");
      RestRequest request = new RestRequest($"api/Parks/random", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }
  }
}