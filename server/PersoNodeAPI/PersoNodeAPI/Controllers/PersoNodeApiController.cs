using Microsoft.AspNetCore.Mvc;
using PersoNodeApi.Models;
using PersoNodeApi.Impls;

namespace PersoNodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersoNodeApiController : ControllerBase
    {
        //GetRegisteredPeopleNames
        [HttpGet("getPeople/")]
        public string GetRegisteredPeopleNames()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            return files == Array.Empty<string>() ? "" : files.Aggregate(static (i, j) => i + "," + j);
        }

        //GetPerson
        [HttpGet("getPerson/")]
        public Person GetPerson(string name)
        {
            Person person = XmlFilerDeluxe.LoadPerson(name);

            return person;
        }

        //GetSpecificDataFromPerson
        [HttpGet("getSpecificDataFromPerson")]
        public string GetSpecificDataFromPerson(string name, string data)
        {
            Person person = XmlFilerDeluxe.LoadPerson(name + ".xml");

            return data.ToUpper() switch
            {
                "NAME" => person.Name,
                "METADATA" => person.Md,
                "TEXT" => person.Text,
                _ => ""
            };
        }

        //AddPerson
        [HttpPost("addPerson/")]
        public void AddPerson(Person p)
        {
            XmlFilerDeluxe.SavePerson(p);
        }

        //EditPerson
        [HttpPatch("editPerson/")]
        public void EditPerson(string name, string dataToChange, string newValue)
        {
            Person person = XmlFilerDeluxe.LoadPerson(name);

            switch (dataToChange.ToUpper())
            {
            case "NAME":
                person.Name = newValue;
                break;
            case "METADATA":
                person.Md = newValue;
                break;
            case "TEXT":
                person.Text = newValue;
                break;
            }

            XmlFilerDeluxe.SavePerson(person);
        }

        //DeletePerson
        [HttpDelete("deletePerson/")]
        public void DeletePerson(string name)
        {
            if (name == "*")
            {
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());

                foreach (string file in files) System.IO.File.Delete(file);
            }
            if (System.IO.File.Exists(name + ".xml")) System.IO.File.Delete(name + ".xml");
        }
    }
}
