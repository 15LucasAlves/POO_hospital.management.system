using Newtonsoft.Json;

namespace hospitalSpace
{
    public class ReadJson
    {
        public List<RandomPeople> ReadPersonsFromJson(string filePath)
        {
            // list of Random people (person copy.cs)

            return ReadFromFile(filePath).Result;

            // ReadFromFile method will read JSON from local file instead of a URL
        }

        private async Task<List<RandomPeople>> ReadFromFile(string filePath)
        {
            // async is used to allow asynchronous file operations

            try
            {
                string json = await File.ReadAllTextAsync(filePath);

                // Read the content of the local file as a string

                return JsonConvert.DeserializeObject<List<RandomPeople>>(json);

                // Deserializes the string into a list of random people based on the structure provided
            }
            catch (Exception)
            {
                // Error handling if the file could not be read
                return null;
            }
        }
    }

    public class ReadJsonStaff
    {
        public List<Staff> ReadStaffFromJson(string filePath)
        {
            // list of Random people (person copy.cs)

            return ReadFromFile(filePath).Result;

            // ReadFromFile method will read JSON from local file instead of a URL
        }

        private async Task<List<Staff>> ReadFromFile(string filePath)
        {
            // async is used to allow asynchronous file operations

            try
            {
                string json = await File.ReadAllTextAsync(filePath);

                // Read the content of the local file as a string

                return JsonConvert.DeserializeObject<List<Staff>>(json);

                // Deserializes the string into a list of random people based on the structure provided
            }
            catch (Exception)
            {
                // Error handling if the file could not be read
                return null;
            }
        }
    }
}