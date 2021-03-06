using System.Diagnostics;
using Newtonsoft.Json;
using RestSharp;

namespace learning_cSharp
{
    public class DeserializingWissemObject
    {
        string path = @"C:\Users\comp\Downloads\info.txt";
        string text = "";
        int nullDelegates = 0;
        List<Rootobject> rootobjects = new List<Rootobject>();

        //deserializes the file my friend sent :kms:
        public void DeserializeWissem()
        {
            Console.WriteLine("Reading data...");
            string[] ob = File.ReadAllLines(path);
            for (int i = 0; i < ob.Length; i++)
            {
                text += ob[i];
                if (ob[i].Contains("}"))
                {
                    desrialize();
                }
            }
            Console.WriteLine("Data Deserialized");
            GetAllData();
        }

        //deserializes the string and adds it to rootobject
        private void desrialize()
        {
            rootobjects.Add(JsonConvert.DeserializeObject<Rootobject>(text));
            text = "";
        }

        /*
         sends get requests and saves all the data in in a list of rootObject2 named task
         
         */
        private async void GetAllData()
        {
            Console.WriteLine("Getting Data from the web and saving it");
            RestClient restClient;
            string url;
            var request = new RestRequest();
            List<Rootobject2> task = new List<Rootobject2>();
            int counter = 0;

            for (int i = 0; i < rootobjects.Count; i++)
            {
                for (int j = 0; j < rootobjects[i].delegates.Length; j++)
                {
                    url = "https://www.meteo.tn/horaire_gouvernorat/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + rootobjects[i].mainCity + "/" + rootobjects[i].delegates[j];
                    restClient = new RestClient(url);
                    task.Add(await restClient.GetAsync<Rootobject2>(request));

                    //if the json requested is null it deletes it from my list and decrements the counter
                    if (task[counter].data == null)
                    {
                        nullDelegates++;
                        task.RemoveAt(counter);
                        counter--;
                        Console.WriteLine(nullDelegates + " delegates with no data");
                        Task.Delay(1000).Wait();
                    }
                    counter++;
                }
                Console.WriteLine((i + 1) + "/24");
            }
            Organize(task);
        }

        //it sorts the data from the previous list into a new list, states, then calls a function to serialize it 
        private void Organize(List<Rootobject2> ob)
        {
            Console.WriteLine("organizing the data");
            List<Rootobject3> states = new List<Rootobject3>();
            List<Delegation> delegation = new List<Delegation>();
            string currentState;
            int delegateCount = 0;
            for (int i = 0; i < 24; i++)
            {
                currentState = ob[delegateCount].data.gouvernorat.intituleAn;
                while (delegateCount < ob.Count && currentState == ob[delegateCount].data.gouvernorat.intituleAn)
                {
                    delegation.Add(ob[delegateCount].data.delegation);
                    delegateCount++;
                }
                states.Add(new Rootobject3()
                {
                    data = new Data2()
                    {
                        gouvernorat = new Gouvernorat2()
                        {
                            id = ob[delegateCount-1].data.gouvernorat.id,
                            intituleAn = currentState,
                            intituleAr = ob[delegateCount-1].data.gouvernorat.intituleAr
                        },
                        delegation = new List<Delegation>(delegation)
                    }
                });
                delegation.Clear();
            }
            serialize(states);
        }

        //serializes everything to a nice json file
        private async void serialize(List<Rootobject3> states)
        {
            Console.WriteLine("serializing data");
            string output = JsonConvert.SerializeObject(states);
            await File.AppendAllTextAsync(@"D:\Documents\output4.json", output);
            Process.Start("notepad", @"D:\Documents\output4.json");
        }

        private async void WriteToFile(Rootobject2 x)
        {
            string s = "states.Add(new state() {Id = " + x.data.delegation.id + ", ParentId = " + x.data.gouvernorat.id + ", NameEn = \"" + x.data.delegation.intituleAn + "\", NameAr = \"" + x.data.delegation.intituleAr + "\", ParentNameAr = \"" + x.data.gouvernorat.intituleAn + "\", ParentNameEn = \"" + x.data.gouvernorat.intituleAr + "\"});\n";
            await File.AppendAllTextAsync(@"D:\Documents\output.txt", s);

        }
    }
}