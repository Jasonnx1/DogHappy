using ApiHelper.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiHelper
{
    public class DogApiProcessor
    {

        public static async Task<List<String>> LoadBreedList()
        {
            string url;

            url = $"https://dog.ceo/api/breeds/list/all";


            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DogBreedModel result = await response.Content.ReadAsAsync<DogBreedModel>();

                    var breeds = result.Breeds.Keys.ToList();

                    return breeds;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }



            
        }

        public static async Task<List<String>> GetImageUrl(string breed, int number = 1)
        {
            /// TODO : GetImageUrl()
            /// TODO : Compléter le modèle manquant
            /// 

            string url;

            url = $"https://dog.ceo/api/breed/{breed}/images/random/{number}";


            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DogModel result = await response.Content.ReadAsAsync<DogModel>();

                    return result.Dogs;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }



            
        }
    }
}
