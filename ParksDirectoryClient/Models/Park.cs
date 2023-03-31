using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace ParksDirectoryClient.Models
{
  public class Park
  {
    public int ParkId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Review { get; set; }
    public int Rating { get; set; }

    public static Park[] GetParks()
    {
      Task<string> apiCallTask = ApiHelper.GetAll();
      string result =  apiCallTask.Result;
      
      JArray jsonResponse = JArray.Parse(result);
      List<Park> destinationList = JsonConvert.DeserializeObject<List<Park>>(jsonResponse.ToString());

      return destinationList.ToArray();
    }


    public static Park GetDetails(int id)
    {
      var apiCallTask = ApiHelper.Get(id);
      var result = apiCallTask.Result;

      JObject jsonResponse = JObject.Parse(result);
      Park park = JsonConvert.DeserializeObject<Park>(jsonResponse.ToString());

      return park;
    }

    public static void Post(Park park)
    {
      string jsonPark = JsonConvert.SerializeObject(park);
      ApiHelper.Post(jsonPark);
    }

    public static void Put(Park park)
    {
      string jsonPark = JsonConvert.SerializeObject(park);
      ApiHelper.Put(park.ParkId, jsonPark);
    }

    public static void Delete(int id)
    {
      ApiHelper.Delete(id);
    }

    public static Park RandomPark(int id)
    {
      var apiCallTask = ApiHelper.Get(id);
      var result = apiCallTask.Result;

      JObject jsonResponse = JObject.Parse(result);
      Park park = JsonConvert.DeserializeObject<Park>(jsonResponse.ToString());

      return park;
    }
  }
}