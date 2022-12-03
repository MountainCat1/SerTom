using SerTom;

namespace SerTom_Test;


public class SimpleClassTest
{
    private class SimpleClass
    {
        public string Title { get; set; }
    }

    private string GetTextFile()
    {
        return @"
            title = ""TOML Example""
            desc = ""TOML Description""
        ";
    }
    
    [Test]
    public void Test1()
    {
        var fileText = GetTextFile();

        var simpleClass = SerTomSerializer.ParseToTomlObject(fileText);

        var firstProp = simpleClass.Properties.First();
        
        Console.WriteLine($"{firstProp.Key} {firstProp.Value}");
        
        if (    firstProp.Key                   == "title"
            &&  firstProp.Value.GetString()     == "TOML Example")
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
}