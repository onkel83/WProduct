namespace WPAZV;

public interface IController
{
    abstract void Action(string action = "view", string data = "0"); 
    abstract void _Add();
    abstract void _Delete(int id);
    abstract void _Edit(int id);
    abstract void _View();
}
