namespace SerTom_Test;

public class Tests
{
    class SomeClass
    {
        public string Title { get; set; }
    }
    
    
    private string GetTomlFile()
    {
        return @"
            # To jest przykładowy dokument TOML
            title = ""TOML Example""

            [owner]
            name = ""Tom Preston-Werner""
            dob = 1979-05-27T07:32:00-08:00 # Data

            [database]
            server = ""192.168.1.1""
            ports = [ 8001, 8001, 8002 ]
            connection_max = 5000
            enabled = true

            [servers]

            # Wcięcia (tab lub spacje) są dozwolone ale nie wymagane
            [servers.alpha]
            ip = ""10.0.0.1""
            dc = ""eqdc10""

            [servers.beta]
            ip = ""10.0.0.2""
            dc = ""eqdc10""

            [clients]
            data = [ [""gamma"", ""delta""], [1, 2] ]

                # Wewnątrz tablic można łamać linie
            hosts = [
                ""alpha"",
                ""omega""
            ]
        
        ";
    }
    
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}