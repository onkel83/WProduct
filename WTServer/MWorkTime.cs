using WPBasic;
using WPBasic.Basissystem;

namespace WTServer;

[Serializable()]
public class MWorkTime : BasisModel
{
    private int _UserID;
    private DateTime _Start;
    private DateTime _Stop;
    private double _Pause;

    public string UserID{
        get => _UserID + "";
        set => _UserID = Convert.ToInt32(value);
    }
    public string Start{
        get => DateTimeConverter.ConvertDateTimeToString(_Start);
        set => _Start = DateTimeConverter.ConvertStringToDateTime(value);
    }
    public string Stop{
        get => DateTimeConverter.ConvertDateTimeToString(_Stop);
        set => _Stop = DateTimeConverter.ConvertStringToDateTime(value);
    }
    public string Pause{
        get => _Pause + "";
        set => _Pause = Convert.ToDouble(value);
    }
    public string Arbeitszeit{
        get => _Stop.Subtract(_Start).TotalHours - ((_Pause != 0)?_Pause:0) + "";
    }

    public MWorkTime(string id = "0", string userid = "0", string start = "08:00 01.01.2024", string stop = "16:00 01.01.2024", string pause = "0"){
        ID = Convert.ToInt32(id);
        UserID = userid;
        Start = start;
        Stop = stop;
        Pause = pause;
    }

    public override string ToString()
    {
        return ID+";"+UserID+";"+Start+";"+Stop+";"+Pause;
    }
}
