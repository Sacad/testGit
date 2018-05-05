using AGLTest.Configuration;
using AGLTest.Models;
using AGLTest.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace AGLTest.Services
{
    public class PetService : IPetService
    {
        public readonly IConfiguration _configuration;

        public PetService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IList<Cat> GetCatsByOwnerGender()
        {
            var serializer = new JavaScriptSerializer();
            var client = new System.Net.WebClient();
            var response = string.Empty;

            try
            {
                response = client.DownloadString(_configuration.PersonServiceUrl);
            }
            catch (System.Net.WebException wex)
            {
                if (((System.Net.HttpWebResponse)wex.Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new HttpException(404, "Not found");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            var person = serializer.Deserialize<List<Owner>>(response);

            var catsByOwnerGender = mapResponseToCatList(person);

            return catsByOwnerGender;
        }

        
        public IList<Cat> mapResponseToCatList(List<Owner> Owner)
        {
            List<Cat> cat = new List<Cat>();

            foreach (Owner Catowner in Owner)
            {
               if (Catowner.Pets != null)
                {
                   var ownerGender =  Catowner.Gender;

                   foreach(Pet pet in Catowner.Pets.Where(s=>s.Type == Pets.Cat.ToString()))
                   {                      
                        cat.Add(new Cat
                        {
                            Name = pet.Name,
                            OwnerGender = ownerGender

                        });                                            
                       
                   }                   
                   
                }            
              
            }

            var OrderedCatsList = cat.OrderBy(s => s.Name).ToList();

            return OrderedCatsList;
        }

        /*public IList<Cat> mockCatObject()
        {
            var cat = new List<Cat>()
            {
                new Cat()
                {
                    Name = "Bob",
                    OwnerGender = "Male"
                },
                new Cat()
                {
                    Name = "smith",
                    OwnerGender = "Male"
                },

            };

            return cat;
        }*/

    }
}