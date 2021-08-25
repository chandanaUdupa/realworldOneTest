namespace realworldOneTest.BusinessLogic
{
    public interface ICatBusinessLogic
    {
        string GetCatJsonData();
        byte[] GetRandomCatImageByFilter(string filter);
        byte[] GetRandomCatImageByType(string type);
        byte[] GetRandomCatImageDefault();
    }
}