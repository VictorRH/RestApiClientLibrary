using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestApiClientLibrary.Core.Dto;
using RestSharp;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static RestApiClientLibrary.Core.EntitiesCutoms.RandomUser;

namespace RestApiClientLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestAPIClientController : ControllerBase
    {

        [HttpGet("restsharp")]
        public async Task<ActionResult> GetRandomUserRestSharp()
        {

            try
            {
                var client = new RestClient("https://randomuser.me/api/?nat=us")
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.GET);
                var response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var contentData = response.Content;
                    var result = JsonConvert.DeserializeObject<RandomUserRoot>(contentData);
                    var responseDto = new RandomUserDto
                    {
                        Firstname = result?.Results[0]?.Name?.First,
                        Lastname = result?.Results[0]?.Name?.Last,
                        Street = result?.Results[0]?.Location?.Street?.Name + result?.Results[0]?.Location?.Street?.Number,
                        City = result?.Results[0]?.Location?.City,
                        State = result?.Results[0]?.Location?.State,
                        Country = result?.Results[0]?.Location?.Country,
                        Email = result?.Results[0]?.Email
                    };

                    return Ok(responseDto);
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("httpclient")]
        public async Task<ActionResult> GetRandomUserHttpClient()
        {

            try
            {

                var httpclient = new HttpClient();
                var response = await httpclient.GetAsync("https://randomuser.me/api/?nat=us");


                if (response.IsSuccessStatusCode)
                {
                    var contentData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<RandomUserRoot>(contentData);

                    var responseDto = new RandomUserDto
                    {
                        Firstname = result?.Results[0]?.Name?.First,
                        Lastname = result?.Results[0]?.Name?.Last,
                        Street = result?.Results[0]?.Location?.Street?.Name + result?.Results[0]?.Location?.Street?.Number,
                        City = result?.Results[0]?.Location?.City,
                        State = result?.Results[0]?.Location?.State,
                        Country = result?.Results[0]?.Location?.Country,
                        Email = result?.Results[0]?.Email
                    };

                    return Ok(responseDto);
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
