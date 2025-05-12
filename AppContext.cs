using Models;

namespace animal_lombart;

public class AppContext
{
    private User currentUser;
    private static AppContext instance;
    
    private AppContext()
    {
        instance = 
    }
    
    public AppContext GetInstance()
    {
        if (instance == null)
        {
            instance = new AppContext();
        }
        return instance;
    }
}