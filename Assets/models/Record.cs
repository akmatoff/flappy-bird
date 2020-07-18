using System;

[Serializable]
public class Record {
    public string player;
    public int highscore;

}

[Serializable]
public class Records {
    public Record[] records;
}