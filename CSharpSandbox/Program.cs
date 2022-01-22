using System.Threading.Tasks;
using System;
using Octokit;
using System.Net;
using Octokit.Internal;
using SteamKit2;
using System.Collections.Generic;
using learning_cSharp;
using RestSharp;
using System.Diagnostics;

class Solution
{

    static private string _key = "C33850DA003779D31492ADDBC4F3CDFB";
    static private ulong _id = 76561198032665161;

    static public async Task Main(string[] args)
    {
        //Rootobject a = await GetPrayerTime();
        //Console.WriteLine(a.data.dhohr.ToString());
        DateTime dateTime = DateTime.Now;
        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
        Console.WriteLine("Program started");
        DeserializingWissemObject class1 = new DeserializingWissemObject();
        class1.DeserializeWissem();
        Console.ReadLine();
}

    //get data for prayer time 
    static async Task<Rootobject> GetPrayerTime()
    {
        string url = "https://www.meteo.tn/horaire_gouvernorat/"+ DateTime.Now.ToString("yyyy-MM-dd")+"/361/634";
        var client = new RestClient(url);
        var request = new RestRequest();
        var result = client.GetAsync<Rootobject>(request);
        return await result;
    }
    

    //gets prayer time
    static public void GetPrayer()
    {
        FastWebScraper scraper = new FastWebScraper();
        string url = "https://www.meteo.tn/ar/heures-prieres";
        var backgrounds = scraper.GetPrayers(url);
        foreach (var background in backgrounds)
        {
            Console.WriteLine(background.PrayerTime);
        }
    }

    //gets data from github
    static public async Task<SearchRepositoryResult> Git()
    {
        var request = new SearchRepositoriesRequest()
        {
            //user name
            User = "cabiste69"
        };

        // set your proxy details here
        var proxy = new WebProxy();

        // this is the core connection
        var connection = new Connection(new ProductHeaderValue("cabiste69"),
            new HttpClientAdapter(() => HttpMessageHandlerFactory.CreateDefault(proxy)));

        // and pass this connection to your client
        var client = new GitHubClient(connection);

        SearchRepositoryResult repos = await client.Search.SearchRepo(request);

        return repos;
    }

    static public async void GetSteamDataAsync()
    {
        List<string> ts = new List<string>();

        //var webInterfaceFactory = new SteamWebInterfaceFactory(key);
        //var steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUser>(new HttpClient());
        //var friendsListResponse = await steamInterface.GetFriendsListAsync(myId);
        //var friendsList = friendsListResponse.Data;

        using (dynamic steamUser = WebAPI.GetInterface("ISteamUser", _key))
        {

            KeyValue keyValue = new KeyValue();
            // additionally, if the API you are using requires you to POST,
            // you may specify with the "method" reserved parameter
            keyValue = await steamUser.GetPlayerSummaries2(steamids: _id);


            //foreach (KeyValue user in keyValue["players"]["0"].Children)
            //{
            //    Console.WriteLine("{0}", user.AsString());
            //}

            //name
            Console.WriteLine(keyValue["players"]["0"].Children[3]);
            ts.Add(keyValue["players"]["0"].Children[20].AsString());
            //avatar
            Console.WriteLine(keyValue["players"]["0"].Children[9]); //6-7-8
            //real Name
            Console.WriteLine(keyValue["players"]["0"].Children[12]);
            //primary Clan
            Console.WriteLine(keyValue["players"]["0"].Children[13]);
            //country
            Console.WriteLine(keyValue["players"]["0"].Children[16]);
        }

        Console.WriteLine("written from the list: \n" + ts[0]);


        //using (dynamic steamNews = WebAPI.GetInterface("ISteamNews"))
        //{
        //    // note the usage of c#'s dynamic feature, which can be used
        //    // to make the api a breeze to use

        //    // the ISteamNews WebAPI has only 1 function: GetNewsForApp,
        //    // so we'll be using that

        //    // when making use of dynamic, we call the interface function directly
        //    // and pass any parameters as named arguments
        //    KeyValue kvNews = steamNews.GetNewsForApp(appid: 440); // get news for tf2

        //    // the return of every WebAPI call is a KeyValue class that contains the result data

        //    // for this example we'll iterate the results and display the title
        //    foreach (KeyValue news in kvNews["newsitems"]["newsitem"].Children)
        //    {
        //        Console.WriteLine("News: {0}", news["url"].AsString());
        //    }

        //    Console.WriteLine(kvNews["newsitems"].Children.Count);
        //}

    }

    }
