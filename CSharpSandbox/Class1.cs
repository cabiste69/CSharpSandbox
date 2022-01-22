namespace learning_cSharp
{
    public class Rootobject
    {
        public string[] delegates { get; set; }
        public string mainCity { get; set; }
    }

    //this is for the get reqeust
    public class Rootobject2
    {
        public Data data { get; set; }
    }
    public class Rootobject3
    {
        public Data2 data { get; set; }
    }
    public class Data
    {
        public int id { get; set; }
        public Gouvernorat gouvernorat { get; set; }
        public Delegation delegation { get; set; }
    }

    public class Data2
    {
        public Gouvernorat2 gouvernorat { get; set; }
        public List<Delegation> delegation { get; set; }
    }
    public class Gouvernorat
    {
        public int id { get; set; }
        public string intituleAr { get; set; }
        public string intituleAn { get; set; }
    }
    public class Gouvernorat2
    {
        public int id { get; set; }
        public string intituleAr { get; set; }
        public string intituleAn { get; set; }
    }

    public class Delegation
    {
        public int id { get; set; }
        public string intituleAr { get; set; }
        public string intituleAn { get; set; }
    }

    public class State
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string ParentNameEn { get; set; }
        public string ParentNameAr { get; set; }
    }


}