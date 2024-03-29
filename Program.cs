﻿using AngryMonkey.Cloud;
using AngryMonkey.Cloud.Geography;
using System.Text.Json;
using data;
using System.Text;
using System.Text.Encodings.Web;
using Newtonsoft.Json;

CloudGeography cloudGeography = new CloudGeography();


//Remove Comment for Serializing------------

string fileName ="";
List<Country> countries = await cloudGeography.Countries.GetAllAsync();
List<CountryModel> models = new();
CountryModel model = new();

List<SubdivisionModel> subdivisionModel = new();

//SUBDIVISION CODE--------------------------------------
//foreach (Country c in countries)
//{
//    List<Subdivision> subdivisions = await cloudGeography.Subdivisions.GetByCountryAsync(c.ID);
//    subdivisions.ForEach(result =>
//    {
//        fileName = "../../../Jsonfiles/Subdivisions/"+ c.TwoLettersCode +"-subdivisions.json";
//        SubdivisionModel sub = new()
//        {
//            Code = result.Code,
//            Name = result.Name,
//            LocalName = result.LocalName,
//            Type = result.Type
//        };
//        subdivisionModel.Add(sub);
//    });
//    string jsonString = JsonConvert.SerializeObject(subdivisionModel);
//    if(!jsonString.Equals("[]"))
//        File.WriteAllText(fileName, jsonString);
//    else
//        File.WriteAllText(fileName, jsonString+".EMPTY");
//    subdivisionModel.Clear();
//}

//LANGUAGES CODE--------------------------------------
//List<Language> languagesCloud = await cloudGeography.Languages.GetAllAsync();
//List<CountryLanguage> languages = new();
//List<LanguageModel> languagesList = new();

//languagesCloud.ForEach(result =>
//    {
//        LanguageModel language = new()
//        {
//            Code = result.TwoLettersCode,
//            ThreeLettersCode=result.ThreeLettersCode,
//            Direction=result.Direction.ToString(),
//            Name=result.Name,
//            NativeName=result.NativeName
//        };
//        languagesList.Add(language);

//    });


//CURRENCIES CODE--------------------------------------
//List<Currency> CurrenciesCloud = await cloudGeography.Currencies.GetAllAsync();
//List<CountryCurrency> currency = new();
//List<CurrencieModel> CurrenciesList = new();

//CurrenciesCloud.ForEach(result =>
//    {
//        CurrencieModel currency = new()
//        {
//            Code = result.ThreeLettersCode,
//            Name = result.Name,
//            NativeName = result.NativeName,
//            ShortName = result.ShortName,
//            Symbol = result.Symbol,
//            NativeSymbol = result.NativeSymbol,
//            PluralShortName = result.PluralShortName
//        };
//        CurrenciesList.Add(currency);

//    });


//COUNTRIES CODE--------------------------------------
foreach (Country c in countries)
{
    List<CountryLanguage> languages = new();
    List<CountryCurrency> currencies = new();


    List<LanguageModel> languagesList = new();
    List<CurrencieModel> currencieList = new();
    try
    {
        languages = await cloudGeography.Languages.GetByCountryAsync(c.TwoLettersCode);
        currencies = await cloudGeography.Currencies.GetByCountryAsync(c.TwoLettersCode);
    }
    catch (Exception e)
    {
    }
    languages.ForEach(result =>
    {
        LanguageModel language = new()
        {
            Code = result.Language.TwoLettersCode,
            IsOffical=result.IsOfficial
        };
        languagesList.Add(language);
    });
    currencies.ForEach(result =>
    {
        CurrencieModel currencie = new()
        {
            Code = result.Currency.ThreeLettersCode,
            IsOffical = result.IsOfficial
        };
        currencieList.Add(currencie);
    });
    model =new()
    {
        Code = c.TwoLettersCode,
        Area= c.Area,
        CallingCode= c.CallingCode,
        Capital=c.Capital,
        ThreeLettersCode=c.ThreeLettersCode,
        Demonym=c.Demonym,
        Latitude = c.Latitude,
        LocalName = c.LocalName,
        Longitude = c.Longitude,
        Name = c.Name,
        ShortName = c.ShortName,
        Languages=languagesList,
        Currencies=currencieList

    };
    models.Add(model);

}

//country lebanon = await cloudGeography.Countries.GetAsync("lb");

//Serializing into a json file
//string jsonString = JsonSerializer.Serialize(models);


fileName = "../../../Jsonfiles/Countries.json";

//--------------------------------
string jsonString = JsonConvert.SerializeObject(models);
File.WriteAllText(fileName, jsonString);
//--------------------------------

//Deserializing json file
//string jsonString = File.ReadAllText(fileName);
//List <CountryModel> country = JsonSerializer.Deserialize<List<CountryModel>>(jsonString);

//CountryModel? geographyModel =
//                JsonSerializer.Deserialize<CountryModel>(fileName);

//Console.WriteLine(country[0].Area);

